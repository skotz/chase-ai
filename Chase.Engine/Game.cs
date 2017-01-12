using Chase.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Game
    {
        public Position Board { get; private set; }

        public Player PlayerToMove { get { return Board.PlayerToMove; } }

        public int RedInHand { get { return Constants.MaximumPieceCount - Board.CountPieces(Player.Red); } }

        public int BlueInHand { get { return Constants.MaximumPieceCount - Board.CountPieces(Player.Blue); } }

        private List<Position> BoardHistory;

        private List<Move> MoveHistory;

        private ISearchAlgorithm search;

        private string loadedCgn;
        
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

            BoardHistory = new List<Position>();
            BoardHistory.Add(Board.Clone());

            MoveHistory = new List<Move>();

            loadedCgn = "";
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
            bestMove = GetBestMove((SearchArgs)e.Argument);
        }

        public SearchResult GetBestMove(int searchDepth, int searchTimeSeconds)
        {
            return GetBestMove(new SearchArgs(searchDepth, searchTimeSeconds));
        }

        public SearchResult GetBestMove(SearchArgs settings)
        {
            return search.GetBestMove(Board, settings);
        }

        public void BeginGetBestMove(int searchDepth, int searchTimeSeconds)
        {
            if (!worker.IsBusy)
            {
                // Start the search in a background thread
                worker.RunWorkerAsync(new SearchArgs(searchDepth, searchTimeSeconds));
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
            Move parsed = Board.MakeMove(move);
            BoardHistory.Add(Board.Clone());
            MoveHistory.Add(parsed);
        }

        public void MakeMove(Move move)
        {
            Board.MakeMove(move);
            BoardHistory.Add(Board.Clone());
            MoveHistory.Add(move);

            // See if the game is over
            Player winner = GetWinner();
            if (winner != Player.None)
            {
                OnGameOver?.Invoke(this, winner);
            }
        }

        public Player GetWinner()
        {
            return Board.GetWinner();
        }

        public List<Move> GetAllMoves()
        {
            return Board.GetValidMoves();
        }

        public List<MoveHistory> GetMoveHistory()
        {
            List<MoveHistory> history = new List<MoveHistory>();
            MoveHistory mh = new MoveHistory();
            int num = 1;

            for (int i = 0; i < MoveHistory.Count; i++)
            {
                mh.Number = num;
                if (BoardHistory[i].PlayerToMove == Player.Red)
                {
                    mh.RedMove = MoveHistory[i].ToString();

                    if (i + 1 < MoveHistory.Count && BoardHistory[i + 1].PlayerToMove == Player.Red)
                    {
                        history.Add(mh);
                        mh = new MoveHistory();
                        num++;
                    }
                }
                else
                {
                    if (MoveHistory[i] != null)
                    {
                        mh.BlueMove = MoveHistory[i].ToString();

                        history.Add(mh);
                        mh = new MoveHistory();

                        num++;
                    }
                }
            }

            if (mh.Number > 0)
            {
                history.Add(mh);
            }

            return history;
        }

        public Move RecallState(int move)
        {
            // TODO: there's got to be a better way to load a state than to re-parse and re-play all past moves from a clean state...
            LoadFromGameNotationString(loadedCgn, move);

            if (MoveHistory.Count >= 1)
            {
                return MoveHistory[MoveHistory.Count - 1];
            }
            else
            {
                return null;
            }
        }

        public int LoadFromGameNotationString(string cgn)
        {
            return LoadFromGameNotationString(cgn, int.MaxValue);
        }

        public int LoadFromGameNotationString(string cgn, int stopOnMove)
        {
            StartNew();

            int moveNum = 0;
            foreach (string line in cgn.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!line.StartsWith("["))
                {
                    string[] parts = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < parts.Length; i++)
                    {
                        if (moveNum < stopOnMove)
                        {
                            MakeMove(parts[i]);
                            moveNum++;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(loadedCgn))
            {
                loadedCgn = cgn;
            }
            
            return moveNum;
        }

        public string GetGameNotationString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[Version \"1.0\"]");
            sb.AppendLine("[Date \"" + DateTime.Now.ToString("yyyy.MM.dd") + "\"]");
            if (BoardHistory.Count > 0)
            {
                Player winner = BoardHistory[BoardHistory.Count - 1].GetWinner();
                sb.AppendLine("[Result \"" + (winner != Player.None ? winner + " Won" : "?") + "\"]");
            }
            sb.AppendLine();

            foreach (MoveHistory move in GetMoveHistory())
            {
                sb.AppendLine((move.Number.ToString() + ".").PadRight(5, ' ') + (move.RedMove ?? "").PadRight(9, ' ') + move.BlueMove);
            }

            return sb.ToString();
        }

        public List<int> GetThreatenedPieces()
        {
            List<int> threats = new List<int>();
            Player opponent = PlayerToMove == Player.Blue ? Player.Red : Player.Blue;
            List<Move> moves = Board.GetValidMoves(opponent);

            foreach (Move move in moves)
            {
                Position copy = Board.Clone();
                copy.MakeMove(move);

                for (int i=0; i < Constants.BoardSize; i++)
                {
                    if (PlayerToMove == Player.Blue)
                    {
                        if (Board[i] > 0 && copy[i] < 0)
                        {
                            if (!threats.Contains(i))
                            {
                                threats.Add(i);
                            }
                        }
                    }
                    else
                    {
                        if (Board[i] < 0 && copy[i] > 0)
                        {
                            if (!threats.Contains(i))
                            {
                                threats.Add(i);
                            }
                        }
                    }
                }
            }

            return threats;   
        }

        public void AnalyzePosition(Position position, int depth, string file)
        {
            Semaphore s = new Semaphore(1, 1);
            
            using (StreamWriter w = new StreamWriter(file, false))
            {
                w.WriteLine("Last Move,Best Move,Move Score,Primary Variation,Depth,Evaluations,Seconds");
            }

            // Create a list of positions so we can process in parallel
            List<Position> positions = new List<Position>();
            positions.Add(position);
            foreach (Move move in position.GetValidMoves())
            {
                Position copy = position.Clone();
                copy.MakeMove(move);
                positions.Add(copy);
            }
            
            // Create a list of actions to run in parallel
            List<Action> actions = new List<Action>();
            foreach (Position p in positions)
            {
                actions.Add(() =>
                {
                    Stopwatch timer = Stopwatch.StartNew();
                    Search search = new Search();
                    SearchArgs args = new SearchArgs(depth, -1, false);
                    SearchResult best = search.GetBestMove(p, args);
                    timer.Stop();

                    s.WaitOne();
                    using (StreamWriter w = new StreamWriter(file, true))
                    {
                        string history = "";
                        if (!string.IsNullOrEmpty(p.MovesHistory))
                        {
                            string move = p.MovesHistory;
                            if (move.Contains(" "))
                            {
                                move = move.Split(' ')[0];
                            }

                            history = "After " + move;
                        }
                        else
                        {
                            history = "Starting Position";
                        }

                        w.WriteLine(history + "," + best.BestMove.ToString() + "," + best.Score + "," + best.PrimaryVariation + "," + depth + "," + best.Evaluations + "," + (timer.ElapsedMilliseconds / 1000.0).ToString("0.000"));
                    }
                    s.Release();
                });
            }

            // Analyze all positions in parallel
            actions.ForEach(a => Task.Run(a));
        }

        public void SaveGameToFile(string file)
        {
            using (StreamWriter w = new StreamWriter(file))
            {
                w.Write(GetGameNotationString());

                //w.WriteLine("Moves: " + Board.MovesHistory);
                //w.WriteLine("--------------------------------------");
                //foreach (Position position in BoardHistory)
                //{
                //    w.Write(GetStringVisualization(position));
                //    w.WriteLine("--------------------------------------");
                //}
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
