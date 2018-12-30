﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Position
    {
        public int[] Board;

        public int this[int index] { get { return Board[index]; } }

        public Player PlayerToMove { get; set; }

        public Player LastPlayerToMove { get; set; }

        public int PointsToDistribute { get; private set; }

        public string MovesHistory { get; set; }

        private ulong[] pastPositions;

        private int lastHashIndex;

        private static int[,] tileDirectionTable;

        public Position()
        {
            if (tileDirectionTable == null)
            {
                // Create a lookup table for each direction from each source tile index that gives you the destination index
                int[,] table = new int[Constants.BoardSize, 6];

                for (int i = 0; i < Constants.BoardSize; i++)
                {
                    for (int d = 0; d < 6; d++)
                    {
                        table[i, d] = GetIndexInDirection(i, (Direction)d);
                    }
                }

                tileDirectionTable = table;
            }
        }

        public Move MakeMove(string move)
        {
            Move parse = Move.ParseMove(move);
            if (parse.IsValid)
            {
                foreach (Move m in GetValidMoves())
                {
                    if (m.FromIndex == parse.FromIndex && m.ToIndex == parse.ToIndex && m.Increment == parse.Increment)
                    {
                        // We can't use the parsed move since it doesn't contain the final direction information
                        MakeMove(m);
                        return m;
                    }
                }
            }

            return null;
        }

        public void MakeMove(Move move)
        {
            MakeMove(move, true, -1);
        }

        private void MakeMove(Move move, bool firstLevel, int basePieceCount)
        {
            Player opponent = PlayerToMove == Player.Blue ? Player.Red : Player.Blue;

            if (move.Increment > 0)
            {
                if (move.FromIndex >= 0)
                {
                    // We're moving points from one die to an adjacent die
                    Board[move.ToIndex] += Board[move.ToIndex] > 0 ? move.Increment : -move.Increment;
                    Board[move.FromIndex] -= Board[move.FromIndex] > 0 ? move.Increment : -move.Increment;
                }
                else
                {
                    // We're adding points to a die after another one of our dice was captured
                    Board[move.ToIndex] += Board[move.ToIndex] > 0 ? move.Increment : -move.Increment;

                    // We've distributed at least some of the points
                    PointsToDistribute -= move.Increment;

                    // It's still our turn after adding points to a piece
                    opponent = PlayerToMove;
                }
            }
            else
            {
                // Count my pieces (before clearing it in the next step)
                if (basePieceCount < 0)
                {
                    basePieceCount = CountPieces(PlayerToMove);
                }

                // Store the piece we're moving and clear the source tile
                int sourcePiece = Board[move.FromIndex];
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
                    MakeMove(bumpMove, false, basePieceCount);
                }

                // Are we capturing an enemy piece?
                if ((sourcePiece > 0 && Board[move.ToIndex] < 0) || (sourcePiece < 0 && Board[move.ToIndex] > 0))
                {
                    // Keep track of how many points the enemy will need to distribute to other die
                    PointsToDistribute += Math.Abs(Board[move.ToIndex]);
                }

                // Are we landing on the chamber?
                if (move.ToIndex == Constants.ChamberIndex)
                {
                    // Figure out the point split
                    int sourcePieceValue = Math.Abs(sourcePiece);
                    int leftValue = (int)Math.Ceiling(sourcePieceValue / 2.0);
                    int rightValue = leftValue * 2 > sourcePieceValue ? leftValue - 1 : leftValue;

                    // If we're at the piece limit, just slide to the left
                    if (basePieceCount >= Constants.MaximumPieceCount)
                    {
                        leftValue = sourcePieceValue;
                        rightValue = 0;
                    }

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
                    Board[Constants.ChamberIndex] = sourcePiece > 0 ? leftValue : -leftValue;
                    Move leftMove = new Move()
                    {
                        FromIndex = Constants.ChamberIndex,
                        ToIndex = leftIndex,
                        Increment = 0,
                        FinalDirection = leftDirection
                    };
                    MakeMove(leftMove, false, basePieceCount);
                    if (rightValue > 0)
                    {
                        Board[Constants.ChamberIndex] = sourcePiece > 0 ? rightValue : -rightValue;
                        Move rightMove = new Move()
                        {
                            FromIndex = Constants.ChamberIndex,
                            ToIndex = rightIndex,
                            Increment = 0,
                            FinalDirection = rightDirection
                        };
                        MakeMove(rightMove, false, basePieceCount);
                    }
                }

                if (move.ToIndex != Constants.ChamberIndex)
                {
                    // Assume the move is valid and go ahead and make it
                    Board[move.ToIndex] = sourcePiece;
                }
            }

            // It's now the other player's turn to move
            LastPlayerToMove = PlayerToMove;
            PlayerToMove = opponent;
            MovesHistory = string.IsNullOrEmpty(MovesHistory) ? move.ToString() : MovesHistory + " " + move.ToString();

            if (pastPositions == null)
            {
                pastPositions = new ulong[Constants.MaxPreviousHashes];
                lastHashIndex = 0;
            }

            if (firstLevel)
            {
                pastPositions[lastHashIndex] = GetHash();
                lastHashIndex = (lastHashIndex + 1) % Constants.MaxPreviousHashes;
            }
        }

        public int CountPieces(Player player)
        {
            int count = 0;
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if ((Board[i] > 0 && player == Player.Blue) || (Board[i] < 0 && player == Player.Red))
                {
                    count++;
                }
            }
            return count;
        }

        public List<Move> GetValidMoves()
        {
            return GetValidMoves(PlayerToMove);
        }

        public List<Move> GetValidMoves(Player player)
        {
            List<Move> moves = new List<Move>();
            int destination;

            if (GetWinner() != Player.None)
            {
                return moves;
            }

            // We must first distribute points if a piece was just captured
            if (PointsToDistribute > 0)
            {
                // Find our lowest valued piece
                int smallest = Board.Where(x => player == Player.Blue ? x > 0 : x < 0).Select(x => Math.Abs(x)).Min();

                // Find out how what the new value of this piece will be and if we have any points left over
                int maxTo = Constants.MaximumPieceValue - smallest;
                int max = Math.Min(maxTo, PointsToDistribute);

                if (player == Player.Red)
                {
                    smallest *= -1;
                }

                // If the smallest value piece we have is a 6 then we don't want to generate invalid moves
                if (max > 0)
                {
                    // Find all the pieces with this minimum value and create a move
                    for (int i = 0; i < Constants.BoardSize; i++)
                    {
                        if (Board[i] == smallest)
                        {
                            moves.Add(new Move()
                            {
                                FromIndex = -1,
                                ToIndex = i,
                                Increment = max,
                                FinalDirection = (Direction)(-1)
                            });
                        }
                    }
                }
            }
            else
            {
                // Physical moves
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
                        if ((player == Player.Blue && Board[i] > 0) || (player == Player.Red && Board[i] < 0))
                        {
                            foreach (Direction direction in Constants.Directions)
                            {
                                // Find physical moves
                                // Move in a direction for as many tiles as the value of the die on that tile
                                Direction movement = direction;
                                int[] path;
                                destination = GetDestinationIndexIfValidMove(i, ref movement, Math.Abs(Board[i]), out path);

                                if (destination != Constants.InvalidMove)
                                {
                                    moves.Add(new Move()
                                    {
                                        FromIndex = i,
                                        ToIndex = destination,
                                        Increment = 0,
                                        FinalDirection = movement,
                                        Path = path
                                    });
                                }

                                // Find point distribution moves
                                if (Board[i] > 1 || Board[i] < -1)
                                {
                                    destination = GetDestinationIndexIfValidMove(i, ref movement, 1, out path);

                                    if (destination != Constants.InvalidMove)
                                    {
                                        if (Math.Abs(Board[destination]) < Constants.MaximumPieceValue)
                                        {
                                            if ((Board[destination] > 0 && Board[i] > 0) || (Board[destination] < 0 && Board[i] < 0))
                                            {
                                                // Figure out what we're allowed to transfer
                                                int maxFrom = Math.Abs(Board[i]) - 1;
                                                int maxTo = Constants.MaximumPieceValue - Math.Abs(Board[destination]);
                                                int max = Math.Min(maxFrom, maxTo);

                                                // Create a move for each possible point transfer
                                                for (int points = 1; points <= max; points++)
                                                {
                                                    moves.Add(new Move()
                                                    {
                                                        FromIndex = i,
                                                        ToIndex = destination,
                                                        Increment = points,
                                                        FinalDirection = movement,
                                                        Path = path
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return moves;
        }

        public Player GetWinner()
        {
            int bluePieces = 0;
            int redPieces = 0;
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (Board[i] > 0)
                {
                    bluePieces++;
                }
                else if (Board[i] < 0)
                {
                    redPieces++;
                }
            }

            if (bluePieces < Constants.MinimumPieceCount)
            {
                return Player.Red;
            }
            else if (redPieces < Constants.MinimumPieceCount)
            {
                return Player.Blue;
            }
            return Player.None;
        }

        /// <summary>
        /// Check if it's possible to move a piece from a given tile in a given direction a given number of tiles.
        /// If the move is valid then the destination index will be returned.
        /// </summary>
        /// <param name="sourceIndex">The index of the tile from which we're moving</param>
        /// <param name="direction">The initial direction of the movement</param>
        /// <param name="distance">The number of tiles to move</param>
        /// <returns></returns>
        public int GetDestinationIndexIfValidMove(int sourceIndex, Direction direction, int distance)
        {
            return GetDestinationIndexIfValidMove(sourceIndex, ref direction, distance, false);
        }

        /// <summary>
        /// Check if it's possible to move a piece from a given tile in a given direction a given number of tiles.
        /// If the move is valid then the destination index will be returned.
        /// </summary>
        /// <param name="sourceIndex">The index of the tile from which we're moving</param>
        /// <param name="direction">The initial direction of the movement</param>
        /// <param name="distance">The number of tiles to move</param>
        /// <returns></returns>
        public int GetDestinationIndexIfValidMove(int sourceIndex, ref Direction direction, int distance, bool isBounce)
        {
            Direction d = direction;
            int[] p;
            return GetDestinationIndexIfValidMove(sourceIndex, ref direction, distance, out p, isBounce);
        }

        /// <summary>
        /// Check if it's possible to move a piece from a given tile in a given direction a given number of tiles.
        /// If the move is valid then the destination index will be returned.
        /// </summary>
        /// <param name="sourceIndex">The index of the tile from which we're moving</param>
        /// <param name="direction">The initial direction of the movement</param>
        /// <param name="distance">The number of tiles to move</param>
        /// <returns></returns>
        public int GetDestinationIndexIfValidMove(int sourceIndex, ref Direction direction, int distance, out int[] path)
        {
            return GetDestinationIndexIfValidMove(sourceIndex, ref direction, distance, out path, false);
        }

        /// <summary>
        /// Check if it's possible to move a piece from a given tile in a given direction a given number of tiles.
        /// If the move is valid then the destination index will be returned.
        /// </summary>
        /// <param name="sourceIndex">The index of the tile from which we're moving</param>
        /// <param name="direction">The initial direction of the movement</param>
        /// <param name="distance">The number of tiles to move</param>
        /// <returns></returns>
        public int GetDestinationIndexIfValidMove(int sourceIndex, ref Direction direction, int distance, out int[] path, bool isBounce = false)
        {
            int index = sourceIndex;
            int pathIndex = 0;
            path = new int[] { -1, -1, -1, -1, -1, -1 };

            for (int i = 1; i <= distance; i++)
            {
                if (i > 1 || isBounce)
                {
                    // Check for richochets
                    if (direction == Direction.UpRight && index >= 0 && index <= 8)
                    {
                        direction = Direction.DownRight;
                    }
                    else if (direction == Direction.UpLeft && index >= 0 && index <= 8)
                    {
                        direction = Direction.DownLeft;
                    }
                    else if (direction == Direction.DownLeft && index >= 72 && index <= 80)
                    {
                        direction = Direction.UpLeft;
                    }
                    else if (direction == Direction.DownRight && index >= 72 && index <= 80)
                    {
                        direction = Direction.UpRight;
                    }
                }

                // Move one tile
                index = GetIndexInDirection(index, direction);

                // Keep a history of the tiles we've moved through
                if (pathIndex < 6)
                {
                    path[pathIndex++] = index;
                }

                if (index == Constants.InvalidMove)
                {
                    return Constants.InvalidMove;
                }

                // Our move ends on either a blank tile or another piece (which we can capture or bump)
                if (i == distance)
                {
                    return index;
                }

                // Did we hit another piece (blue or red) before the end of our distance?
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

            // Use the pre-calculated lookup table if initialized
            if (tileDirectionTable != null)
            {
                return tileDirectionTable[sourceIndex, (int)direction];
            }

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
                    return Constants.InvalidMove;
            }
        }

        public bool LastMoveWasRepetition()
        {
            if (pastPositions != null)
            {
                ulong last = pastPositions[(lastHashIndex + Constants.MaxPreviousHashes - 1) % Constants.MaxPreviousHashes];
                for (int i = 1; i < Constants.MaxPreviousHashes; i++)
                {
                    int index = (lastHashIndex - 1 + Constants.MaxPreviousHashes - i) % Constants.MaxPreviousHashes;
                    if (last == pastPositions[index])
                    {
                        return true;
                    }
                }
            }

            return false;
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
            p.PlayerToMove = Player.Red;
            p.LastPlayerToMove = Player.Blue;

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
            p.PlayerToMove = Player.Red;
            p.LastPlayerToMove = Player.Blue;

            return p;
        }

        public static Position FromStringNotation(string csn)
        {
            try
            {
                Position position = Position.EmptyPosition();

                string[] sections = csn.Split(' ');
                string[] rows = sections[0].Split('/');
                string tomove = sections[1];

                int blueTotal = 0;
                int redTotal = 0;

                // Initialize the board
                int index = 0;
                for (int i = 0; i < 9; i++)
                {
                    foreach (char piece in rows[i])
                    {
                        if (piece >= 'a' && piece <= 'f')
                        {
                            int value = (piece - 'a') + 1;
                            position.Board[index++] = value;
                            blueTotal += value;
                        }
                        else if (piece >= 'A' && piece <= 'F')
                        {
                            int value = (piece - 'A') + 1;
                            position.Board[index++] = -value;
                            redTotal += value;
                        }
                        else
                        {
                            int empty = int.Parse(piece.ToString());
                            for (int x = 0; x < empty; x++)
                            {
                                position.Board[index++] = 0;
                            }
                        }
                    }
                }

                if (index != 81)
                {
                    throw new Exception("Invalid chase board notation string!");
                }

                // Set the player to move
                position.PlayerToMove = tomove == "b" ? Player.Blue : Player.Red;

                // Set the points to distribute
                int points = 0;
                if (blueTotal < Constants.PieceValueSum)
                {
                    points = Constants.PieceValueSum - blueTotal;
                }
                else if (redTotal < Constants.PieceValueSum)
                {
                    points = Constants.PieceValueSum - redTotal;
                }
                position.PointsToDistribute = points;

                return position;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ToStringNotation()
        {
            int blanks = 0;
            string csn = "";

            Func<string> addBlanks = () =>
            {
                string count = blanks > 0 ? blanks.ToString() : "";
                blanks = 0;
                return count;
            };

            for (int index = 0; index < Constants.BoardSize; index++)
            {
                if (Board[index] > 0)
                {
                    csn += addBlanks() + ((char)('a' + (Board[index] - 1))).ToString();
                }
                else if (Board[index] < 0)
                {
                    csn += addBlanks() + ((char)('A' + (-Board[index] - 1))).ToString();
                }
                else
                {
                    blanks++;
                }
                
                if (index.In(8, 17, 26, 35, 44, 53, 62, 71))
                {
                    csn += addBlanks() + "/";
                }
            }
            csn += addBlanks();

            // Side to move
            csn += " " + (PlayerToMove == Player.Blue ? "b" : "r");

            return csn;
        }

        public ulong GetHash()
        {
            ulong hash = 0UL;

            if (PlayerToMove == Player.Blue)
            {
                hash ^= Constants.HashRandomSideToMoveBlue;
            }

            for (int index = 0; index < Constants.BoardSize; index++)
            {
                if (Board[index] != 0)
                {
                    hash ^= Constants.HashRandomTilePiece[index, Board[index] + 6];
                }
            }

            return hash;
        }

        public Position Clone()
        {
            Position position = new Position();

            position.Board = new int[Constants.BoardSize];
            Array.Copy(Board, position.Board, Board.Length);

            if (pastPositions != null)
            {
                position.lastHashIndex = lastHashIndex;
                position.pastPositions = new ulong[pastPositions.Length];
                Array.Copy(pastPositions, position.pastPositions, pastPositions.Length);
            }

            position.PlayerToMove = PlayerToMove;
            position.LastPlayerToMove = LastPlayerToMove;
            position.PointsToDistribute = PointsToDistribute;
            position.MovesHistory = MovesHistory;

            return position;
        }
    }
}
