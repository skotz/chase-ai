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

        public static string GetTileFromIndex(int index)
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
                return GetTileFromIndex(ToIndex) + "+=" + Increment;
            }
            else
            {
                return GetTileFromIndex(FromIndex) + "-" + GetTileFromIndex(ToIndex);
            }
        }
    }
}
