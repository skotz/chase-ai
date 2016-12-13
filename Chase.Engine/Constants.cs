using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Constants
    {
        public const int MaximumPieceCount = 10;

        public const int MaximumPieceValue = 6;

        public const int ChamberIndex = 40;

        public const int ChamberValue = 99;

        public const int InvalidMove = -1;

        ///// <summary>
        ///// A lookup table of all moves from a given square.
        ///// Movements[SourceSquareIndex, Direction] = DestinationSquareIndex
        ///// </summary>
        //public const int[,] Movements = new int[81, 6]
        //{
        //    // 0
        //    {

        //    }
        //};
    }
}
