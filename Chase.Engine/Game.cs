using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Game
    {
        public Position Board { get; private set; }

        public Player PlayerToMove { get; private set; }

        public Game()
        {
            Board = Position.NewPosition();
        }

        public List<Move> GetAllMoves()
        {
            return Board.GetValidMoves(PlayerToMove);
        }
    }
}
