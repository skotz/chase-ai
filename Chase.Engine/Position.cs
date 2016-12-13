using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Position
    {
        private int[] board;

        public Position()
        {
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

        public static Position NewPosition()
        {
            Position p = new Position();
            int cv = Constants.ChamberValue;

            p.board = new int[]
            {
                 -1, -2, -3, -4, -5, -4, -3, -2, -1, // i
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // h
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // g
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // f
                  0,  0,  0,  0, cv,  0,  0,  0,  0, // e
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // d
                  0,  0,  0,  0,  0,  0,  0,  0,  0, // c
                0,  0,  0,  0,  0,  0,  0,  0,  0,   // b
                  1,  2,  3,  4,  5,  6,  7,  8,  9  // a
            };

            return p;
        }
    }
}
