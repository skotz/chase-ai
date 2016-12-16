using Chase.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private ISearchAlgorithm search;
        
        public delegate void SearchProgress(SearchStatus status);

        /// <summary>
        /// Raised whenever there's an update to a pending search
        /// </summary>
        public event SearchProgress OnSearchProgress;

        public delegate void FoundBestMove(SearchResult result);

        /// <summary>
        /// Raised whenever a best move was found
        /// </summary>
        public event FoundBestMove OnFoundBestMove;

        public event EventHandler<Player> OnGameOver;

        private BackgroundWorker worker;

        private SearchResult bestMove;

        public Game()
        {
            StartNew();

            search = new Search();
            search.OnNewResult += Search_OnNewResult;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
        }

        public void StartNew()
        {
            StartNew(Position.NewPosition());
        }

        public void StartNew(Position position)
        {
            Board = position;

            History = new List<Position>();
            History.Add(Board.Clone());
        }

        private void Search_OnNewResult(object sender, SearchStatus e)
        {
            worker.ReportProgress(100, e);
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnSearchProgress?.Invoke((SearchStatus)e.UserState);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Notify our subscribers that we found a move
            OnFoundBestMove?.Invoke(bestMove);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int depth = (int)e.Argument;
            bestMove = GetBestMove(depth);
        }

        public SearchResult GetBestMove(int searchDepth)
        {
            return search.GetBestMove(Board, searchDepth);
        }

        public void BeginGetBestMove(int searchDepth)
        {
            if (!worker.IsBusy)
            {
                // Start the search in a background thread
                worker.RunWorkerAsync(searchDepth);
            }
        }

        public bool IsValidMove(Move move)
        {
            foreach (Move m in GetAllMoves())
            {
                if (m.FromIndex == move.FromIndex && m.ToIndex == move.ToIndex && m.Increment == move.Increment)
                {
                    return true;
                }
            }

            return false;
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
            Player winner = GetWinner();
            if (winner != Player.None)
            {
                OnGameOver?.Invoke(this, winner);
            }
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

        public List<Move> GetAllMoves()
        {
            return Board.GetValidMoves();
        }

        public void SaveGameToFile(string file)
        {
            using (StreamWriter w = new StreamWriter(file))
            {
                w.WriteLine("Moves: " + Board.MovesHistory);
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
