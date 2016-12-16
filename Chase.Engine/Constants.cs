using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Constants
    {
        public const int EvalPieceWeight = 100;

        public const int EvalMobilityWeight = 1;

        public const int EvalDevelopmentWeight = 5;

        public const int MaximumPieceCount = 10;

        public const int PieceValueSum = 25;

        public const int MinimumPieceCount = 5;

        public const int MaximumPieceValue = 6;

        public const int VictoryScore = 10000;

        public const int ChamberIndex = 40;

        public const int BoardSize = 81;

        public const int InvalidMove = -1;

        public const int InvalidTile = -2;

        public static Random Rand = new Random();

        public static Direction[] Directions = new Direction[] 
        {
            Direction.UpRight,
            Direction.Right,
            Direction.DownRight,
            Direction.DownLeft,
            Direction.Left,
            Direction.UpLeft
        };
    }
}
