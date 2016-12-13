using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Game
    {
        public Position Board { get; private set; }

        public Player PlayerToMove { get { return Board.PlayerToMove; } }

        private List<Position> History;

        public string Status { get; private set; }

        public Game()
        {
            Board = Position.NewPosition();
            History = new List<Position>();

            History.Add(Board.Clone());
        }

        public SearchResult GetBestMove(int searchDepth)
        {
            return Search.GetBestMove(Board, searchDepth);
        }

        public void MakeMove(string move)
        {
            Board.MakeMove(move);
            History.Add(Board.Clone());
        }

        public void MakeMove(Move move)
        {
            Board.MakeMove(move);
            History.Add(Board.Clone());
            
            // See if the game is over
            int eval = Search.EvaluatePosition(Board);
            if (eval == Constants.VictoryScore)
            {
                Status = "Blue Wins!";
            }
            else if (eval == -Constants.VictoryScore)
            {
                Status = "Red Wins!";
            }
        }

        public List<Move> GetAllMoves()
        {
            return Board.GetValidMoves();
        }

        public void SaveGameToFile(string file)
        {
            using (StreamWriter w = new StreamWriter(file))
            {
                w.WriteLine("Moves: " + Board.MovesHistory);
                w.WriteLine("Result: " + Status);
                w.WriteLine("--------------------------------------");
                foreach (Position position in History)
                {
                    w.Write(GetStringVisualization(position));
                    w.WriteLine("--------------------------------------");
                }
            }
        }

        public string GetStringVisualization()
        {
            return GetStringVisualization(Board);
        }

        public string GetStringVisualization(Position position)
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

            string board = "";

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (i.In(0, 18, 36, 54, 72))
                {
                    board += "  ";
                }

                if (i == Constants.ChamberIndex)
                {
                    board += " CH ";
                }
                else
                {
                    board += (position.Board[i] != 0 ? position.Board[i].ToString() : "<>").PadLeft(3, ' ') + " ";
                }

                if (i.In(8, 17, 26, 35, 44, 53, 62, 71, 80))
                {
                    board += "\r\n";
                }
            }

            return board;
        }
    }
}
