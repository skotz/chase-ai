using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Position
    {
        public int[] Board { get; private set; }

        public Player PlayerToMove { get; private set; }

        public int PointsToDistribute { get; private set; }

        public string MovesHistory { get; set; }

        public Position()
        {
        }

        public void MakeMove(string move)
        {
            // TODO: parse string moves and validate before making the move

            // TODO: 
        }

        public void MakeMove(Move move)
        {
            // Store the piece we're moving and clear the source tile
            int sourcePiece = Board[move.FromIndex];
            Player opponent = PlayerToMove == Player.Blue ? Player.Red : Player.Blue;
            Board[move.FromIndex] = 0;

            // Are we bumping another one of our pieces?
            if ((sourcePiece > 0 && Board[move.ToIndex] > 0) || (sourcePiece < 0 && Board[move.ToIndex] < 0))
            {
                // Figure out what move needs to be made to move the bumbed piece
                Direction direction = move.FinalDirection;
                int targetIndex = GetDestinationIndexIfValidMove(move.ToIndex, ref direction, 1, true);
                Move bumpMove = new Move()
                {
                    FromIndex = move.ToIndex,
                    ToIndex = targetIndex,
                    Increment = 0,
                    FinalDirection = direction
                };

                // Recursively make the bump move(s)
                MakeMove(bumpMove);
            }

            // Are we capturing an enemy piece?
            if ((sourcePiece > 0 && Board[move.ToIndex] < 0) || (sourcePiece < 0 && Board[move.ToIndex] > 0))
            {
                // Keep track of how many points the enemy will need to distribute to other dice
                PointsToDistribute = Math.Abs(Board[move.ToIndex]);
            }

            // Are we landing on the chamber?
            if (move.ToIndex == Constants.ChamberIndex)
            {
                // Figure out the point split
                int leftValue = (int)Math.Ceiling(sourcePiece / 2.0);
                int rightValue = leftValue * 2 > sourcePiece ? leftValue - 1 : leftValue;
                
                // Figure out the destination tiles based on the direction we were going when we landed on the chamber, and move new pieces there
                int leftIndex = -1;
                int rightIndex = -1;
                Direction leftDirection = (Direction)(-1);
                Direction rightDirection = (Direction)(-1);
                switch (move.FinalDirection)
                {
                    case Direction.DownLeft:
                        leftIndex = 41;
                        rightIndex = 31;
                        leftDirection = Direction.Right;
                        rightDirection = Direction.UpLeft;
                        break;
                    case Direction.UpLeft:
                        leftIndex = 49;
                        rightIndex = 41;
                        leftDirection = Direction.DownLeft;
                        rightDirection = Direction.Right;
                        break;
                    case Direction.DownRight:
                        leftIndex = 32;
                        rightIndex = 39;
                        leftDirection = Direction.UpRight;
                        rightDirection = Direction.Left;
                        break;
                    case Direction.UpRight:
                        leftIndex = 39;
                        rightIndex = 50;
                        leftDirection = Direction.Left;
                        rightDirection = Direction.DownRight;
                        break;
                    case Direction.Left:
                        leftIndex = 50;
                        rightIndex = 32;
                        leftDirection = Direction.UpLeft;
                        rightDirection = Direction.DownLeft;
                        break;
                    case Direction.Right:
                        leftIndex = 31;
                        rightIndex = 49;
                        leftDirection = Direction.DownRight;
                        rightDirection = Direction.UpRight;
                        break;
                }

                // Create the new tiles and them recursively move them (in case they need to bump or capture)
                Board[Constants.ChamberIndex] = leftValue;
                Move leftMove = new Move()
                {
                    FromIndex = Constants.ChamberIndex,
                    ToIndex = leftIndex,
                    Increment = 0,
                    FinalDirection = leftDirection
                };
                MakeMove(leftMove);
                Board[Constants.ChamberIndex] = rightValue;
                Move rightMove = new Move()
                {
                    FromIndex = Constants.ChamberIndex,
                    ToIndex = rightIndex,
                    Increment = 0,
                    FinalDirection = rightDirection
                };
                MakeMove(rightMove);
            }
            
            if (move.ToIndex != Constants.ChamberIndex)
            {
                // Assume the move is valid and go ahead and make it
                Board[move.ToIndex] = sourcePiece;
            }

            // It's now the other player's turn to move
            PlayerToMove = opponent;
            MovesHistory = string.IsNullOrEmpty(MovesHistory) ? move.ToString() : MovesHistory + " " + move.ToString();
        }

        public List<Move> GetValidMoves()
        {
            List<Move> moves = new List<Move>();
            int destination;

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                // A piece can never reside on the chamber
                if (i == Constants.ChamberIndex)
                {
                    continue;
                }
                
                // There's a piece on this tile
                if (Board[i] != 0)
                {
                    // Only look for moves for the player whose turn it is to move
                    if ((PlayerToMove == Player.Blue && Board[i] > 0) || (PlayerToMove == Player.Red && Board[i] < 0))
                    {
                        foreach (Direction direction in Constants.Directions)
                        {
                            // Move in a direction for as many tiles as the value of the die on that tile
                            Direction movement = direction;
                            destination = GetDestinationIndexIfValidMove(i, ref movement, Math.Abs(Board[i]));
                            
                            if (destination != Constants.InvalidMove)
                            {
                                moves.Add(new Move()
                                {
                                    FromIndex = i,
                                    ToIndex = destination,
                                    Increment = 0,
                                    FinalDirection = movement
                                });
                            }
                        }                                
                    }
                }
            }

            return moves;
        }

        /// <summary>
        /// Check if it's possible to move a piece from a given tile in a given direction a given number of tiles.
        /// If the move is valid then the destination index will be returned.
        /// </summary>
        /// <param name="sourceIndex">The index of the tile from which we're moving</param>
        /// <param name="direction">The initial direction of the movement</param>
        /// <param name="distance">The number of tiles to move</param>
        /// <returns></returns>
        public int GetDestinationIndexIfValidMove(int sourceIndex, Direction direction, int distance, bool isBounce = false)
        {
            Direction d = direction;
            return GetDestinationIndexIfValidMove(sourceIndex, ref direction, distance, isBounce);
        }

        /// <summary>
        /// Check if it's possible to move a piece from a given tile in a given direction a given number of tiles.
        /// If the move is valid then the destination index will be returned.
        /// </summary>
        /// <param name="sourceIndex">The index of the tile from which we're moving</param>
        /// <param name="direction">The initial direction of the movement</param>
        /// <param name="distance">The number of tiles to move</param>
        /// <returns></returns>
        public int GetDestinationIndexIfValidMove(int sourceIndex, ref Direction direction, int distance, bool isBounce = false)
        {
            int index = sourceIndex;

            for (int i = 1; i <= distance; i++)
            {
                if (i > 1 || isBounce)
                {
                    // Check for richochets
                    if (direction == Direction.UpRight && index.In(0, 1, 2, 3, 4, 5, 6, 7, 8))
                    {
                        direction = Direction.DownRight;
                    }
                    else if (direction == Direction.UpLeft && index.In(0, 1, 2, 3, 4, 5, 6, 7, 8))
                    {
                        direction = Direction.DownLeft;
                    }
                    else if (direction == Direction.DownLeft && index.In(72, 73, 74, 75, 76, 77, 78, 79, 80))
                    {
                        direction = Direction.UpLeft;
                    }
                    else if (direction == Direction.DownRight && index.In(72, 73, 74, 75, 76, 77, 78, 79, 80))
                    {
                        direction = Direction.UpRight;
                    }
                }

                // Move one tile
                index = GetIndexInDirection(index, direction);

                if (index == Constants.InvalidMove)
                {
                    return Constants.InvalidMove;
                }

                // Our move ends on either a blank tile or another piece (which we can capture or bump)
                if (i == distance)
                {
                    return index;
                }

                // Did we hit another piece before (blue or red) before the end of our distance?
                if (Board[index] != 0)
                {
                    return Constants.InvalidMove;
                }

                // Check to see if we landed on the chamber
                if (index == Constants.ChamberIndex)
                {
                    return Constants.InvalidMove;
                }
            }

            return Constants.InvalidMove;
        }

        /// <summary>
        /// Get the index of one tile by moving one unit from a given tile.
        /// </summary>
        /// <param name="sourceIndex">The index of the source tile.</param>
        /// <param name="direction">The direction to move one unit.</param>
        /// <returns></returns>
        public int GetIndexInDirection(int sourceIndex, Direction direction)
        {
            // Indexes of each piece on the board...
            // ----------------------------------------
            //     1   2   3   4   5   6   7   8   9  
            // ----------------------------------------
            // I     0,  1,  2,  3,  4,  5,  6,  7,  8,  
            // H   9, 10, 11, 12, 13, 14, 15, 16, 17,    
            // G    18, 19, 20, 21, 22, 23, 24, 25, 26,  
            // F  27, 28, 29, 30, 31, 32, 33, 34, 35,    
            // E    36, 37, 38, 39, 40, 41, 42, 43, 44,  
            // D  45, 46, 47, 48, 49, 50, 51, 52, 53,    
            // C    54, 55, 56, 57, 58, 59, 60, 61, 62,  
            // B  63, 64, 65, 66, 67, 68, 69, 70, 71,    
            // A    72, 73, 74, 75, 76, 77, 78, 79, 80   
            // ----------------------------------------

            switch (direction)
            {
                case Direction.Left:
                    int left = sourceIndex - 1;
                    if (left.In(-1, 8, 17, 26, 35, 44, 53, 62, 71))
                    {
                        // We looped around the left side
                        left += 9;
                    }
                    return left;

                case Direction.Right:
                    int right = sourceIndex + 1;
                    if (right.In(9, 18, 27, 36, 45, 54, 63, 72, 81))
                    {
                        // We looped around the right side
                        right -= 9;
                    }
                    return right;

                case Direction.UpLeft:
                    int upleft = sourceIndex;
                    if (sourceIndex.In(10, 11, 12, 13, 14, 15, 16, 17, 28, 29, 30, 31, 32, 33, 34, 35, 46, 47, 48, 49, 50, 51, 52, 53, 64, 65, 66, 67, 68, 69, 70, 71))
                    {
                        // On rows B, D, F, and H we can subtract 10 to get the upper left item (with the exception of the 4 leftmost items handled in the next case)
                        upleft -= 10;
                    }
                    else if (sourceIndex.In(9, 27, 45, 63))
                    {
                        // On the leftmost tile in rows B, D, F, and H we just need to subtract 1
                        upleft -= 1;
                    }
                    else if (sourceIndex <= 8)
                    {
                        // Can't move up when you're already at the top
                        upleft = Constants.InvalidMove;
                    }
                    else
                    {
                        // Everything else just needs a 9 subtracted
                        upleft -= 9;
                    }
                    return upleft;

                case Direction.UpRight:
                    int upright = sourceIndex;
                    if (sourceIndex.In(18, 19, 20, 21, 22, 23, 24, 25, 36, 37, 38, 39, 40, 41, 42, 43, 54, 55, 56, 57, 58, 59, 60, 61, 72, 73, 74, 75, 76, 77, 78, 79))
                    {
                        // On rows A, C, E, and G we can subtract 8 to get the upper right item (with the exception of the 4 rightmost items handled in the next case)
                        upright -= 8;
                    }
                    else if (sourceIndex.In(26, 44, 62, 80))
                    {
                        // On the rightmost tile in rows A, C, E, and G we need to subtract 17
                        upright -= 17;
                    }
                    else if (sourceIndex <= 8)
                    {
                        // Can't move up when you're already at the top
                        upright = Constants.InvalidMove;
                    }
                    else
                    {
                        // Everything else just needs a 9 subtracted
                        upright -= 9;
                    }
                    return upright;

                case Direction.DownLeft:
                    int downleft = sourceIndex;
                    if (sourceIndex.In(10, 11, 12, 13, 14, 15, 16, 17, 28, 29, 30, 31, 32, 33, 34, 35, 46, 47, 48, 49, 50, 51, 52, 53, 64, 65, 66, 67, 68, 69, 70, 71))
                    {
                        downleft += 8;
                    }
                    else if (sourceIndex.In(9, 27, 45, 63))
                    {
                        downleft += 17;
                    }
                    else if (sourceIndex >= 72)
                    {
                        // Can't move down when you're already at the bottom
                        downleft = Constants.InvalidMove;
                    }
                    else
                    {
                        // Everything else just needs a 9 added
                        downleft += 9;
                    }
                    return downleft;

                case Direction.DownRight:
                    int downright = sourceIndex;
                    if (sourceIndex.In(0, 1, 2, 3, 4, 5, 6, 7, 18, 19, 20, 21, 22, 23, 24, 25, 36, 37, 38, 39, 40, 41, 42, 43, 54, 55, 56, 57, 58, 59, 60, 61))
                    {
                        downright += 10;
                    }
                    else if (sourceIndex.In(8, 26, 44, 62))
                    {
                        downright += 1;
                    }
                    else if (sourceIndex >= 72)
                    {
                        // Can't move down when you're already at the bottom
                        downright = Constants.InvalidMove;
                    }
                    else
                    {
                        // Everything else just needs a 9 added
                        downright += 9;
                    }
                    return downright;

                default:
                    return -1;
            }
        }

        public void SetPiece(int index, int pieceValue)
        {
            Board[index] = pieceValue;
        }

        public static Position NewPosition()
        {
            Position p = new Position();

            p.Board = new int[]
            {
                  1,  2,  3,  4,  5,  4,  3,  2,  1, // i
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // h
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // g
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // f
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // e
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // d
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // c
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // b
                 -1, -2, -3, -4, -5, -4, -3, -2, -1  // a
            };

            p.PointsToDistribute = 0;

            return p;
        }

        public static Position EmptyPosition()
        {
            Position p = new Position();

            p.Board = new int[]
            {
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // i
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // h
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // g
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // f
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // e
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // d
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // c
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // b
                  0,  0,  0,  0,  0,  0,  0,  0,  0  // a
            };
            
            p.PointsToDistribute = 0;

            return p;
        }

        public Position Clone()
        {
            Position position = new Position();

            position.Board = new int[Constants.BoardSize];
            Array.Copy(Board, position.Board, Board.Length);

            position.PlayerToMove = PlayerToMove;
            position.PointsToDistribute = PointsToDistribute;
            position.MovesHistory = MovesHistory;

            return position;
        }
    }
}
