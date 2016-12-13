using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    class Move
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
    }
}
