using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Move
    {
        /// <summary>
        /// The index of the tile from which the die is moving
        /// </summary>
        public int FromIndex { get; set; }

        /// <summary>
        /// The index of the tile to which the die is moving
        /// </summary>
        public int ToIndex { get; set; }

        /// <summary>
        /// The amount of points to increment the destination tile
        /// </summary>
        public int Increment { get; set; }

        public int Evaluation { get; set; }

        /// <summary>
        /// The path this move takes to get from the source tile to the destination tile
        /// </summary>
        public int[] Path { get; set; }

        public bool IsValid { get { return FromIndex != -1 || ToIndex != -1 || Increment != -1; } }

        /// <summary>
        /// The direction the piece was moving when it stopped
        /// </summary>
        public Direction FinalDirection { get; set; }

        private static Dictionary<string, int> IndexLookup;

        private static string[] TileFromIndex;

        public static int GetIndexFromTile(string tile)
        {
            if (IndexLookup == null)
            {
                IndexLookup = new Dictionary<string, int>();
                for (int i=0; i < Constants.BoardSize; i++)
                {
                    IndexLookup.Add(GetTileFromIndex(i), i);
                }
            }

            return IndexLookup.ContainsKey(tile) ? IndexLookup[tile] : Constants.InvalidTile; 
        }

        public static Move ParseMove(string move)
        {
            // There are three types of moves...
            // Move from one tile to another:   A1-A2
            // Add points after a capture:      A1+=1
            // Add points to an adjacent piece: A1-A2+=1

            try
            {
                string[] parts = move.ToUpper().Split(new string[] { "+=" }, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    int increment = int.Parse(parts[1]);
                    string[] moves = parts[0].Split('-');
                    int fromTile = GetIndexFromTile(moves[0]);
                    if (moves.Length == 2)
                    {
                        int toTile = GetIndexFromTile(moves[1]);

                        return new Move()
                        {
                            FromIndex = fromTile,
                            ToIndex = toTile,
                            Increment = increment
                        };
                    }
                    else
                    {
                        return new Move()
                        {
                            FromIndex = -1,
                            ToIndex = fromTile,
                            Increment = increment
                        };
                    }
                }
                else
                {
                    string[] moves = parts[0].Split('-');
                    int fromTile = GetIndexFromTile(moves[0]);
                    int toTile = GetIndexFromTile(moves[1]);

                    return new Move()
                    {
                        FromIndex = fromTile,
                        ToIndex = toTile,
                        Increment = 0
                    };
                }
            }
            catch (Exception)
            {
                return new Move()
                {
                    FromIndex = -1,
                    ToIndex = -1,
                    Increment = -1
                };
            }
        }

        public static string GetTileFromIndex(int index)
        {
            if (TileFromIndex == null)
            {
                TileFromIndex = new string[Constants.BoardSize];
                for (int i = 0; i < Constants.BoardSize; i++)
                {
                    TileFromIndex[i] = FindTileFromIndex(i);
                }
            }

            return TileFromIndex[index];
        }

        private static string FindTileFromIndex(int index)
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

            string tile = "";

            if (index == Constants.ChamberIndex)
            {
                tile = "CH";
            }
            else if (index >= 0 && index <= 8)
            {
                tile = "I" + (index + 1);
            }
            else if (index >= 9 && index <= 17)
            {
                tile = "H" + (index - 8);
            }
            else if (index >= 18 && index <= 26)
            {
                tile = "G" + (index - 17);
            }
            else if (index >= 27 && index <= 35)
            {
                tile = "F" + (index - 26);
            }
            else if (index >= 36 && index <= 44)
            {
                tile = "E" + (index - 35);
            }
            else if (index >= 45 && index <= 53)
            {
                tile = "D" + (index - 44);
            }
            else if (index >= 54 && index <= 62)
            {
                tile = "C" + (index - 53);
            }
            else if (index >= 63 && index <= 71)
            {
                tile = "B" + (index - 62);
            }
            else if (index >= 72 && index <= 80)
            {
                tile = "A" + (index - 71);
            }

            return tile;
        }

        public override string ToString()
        {
            if (Increment > 0)
            {
                if (FromIndex >= 0)
                {
                    // Distributing points to an adjacent piece
                    return GetTileFromIndex(FromIndex) + "-" + GetTileFromIndex(ToIndex) + "+=" + Increment;
                }
                else
                {
                    // Distributing points after a capture
                    return GetTileFromIndex(ToIndex) + "+=" + Increment;
                }
            }
            else
            {
                // Regular move
                return GetTileFromIndex(FromIndex) + "-" + GetTileFromIndex(ToIndex);
            }
        }
    }
}
