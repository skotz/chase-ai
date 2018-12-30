using Chase.Engine.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class MonteCarloTreeSearch : ISearchAlgorithm
    {
        private long evaluations;
        private Stopwatch timer;
        private Stopwatch statusTimer;
        private SearchArgs args;
        private Random random;

        public event EventHandler<SearchStatus> OnNewResult;

        public SearchResult GetBestMove(Position position, SearchArgs settings)
        {
            evaluations = 0;
            timer = Stopwatch.StartNew();
            statusTimer = Stopwatch.StartNew();
            args = settings;
            random = new Random();
            
            return Search(position);
        }

        private SearchResult Search(Position rootState)
        {
            var rootNode = new MonteCarloTreeSearchNode(rootState);
            var milliseconds = args.MaxSeconds > 0 ? args.MaxSeconds * 1000 : args.MaxDepth * 1000;

            while (timer.ElapsedMilliseconds < milliseconds)
            {
                var node = rootNode;
                var state = rootState.Clone();
                evaluations++;

                // Select
                while (node.Untried.Count == 0 && node.Children.Count > 0)
                {
                    node = node.SelectChild();
                    state.MakeMove(node.Move);
                }

                // Expand
                if (node.Untried.Count > 0)
                {
                    var move = node.Untried[random.Next(node.Untried.Count)];
                    state.MakeMove(move);
                    node = node.AddChild(state, move);
                }

                // Simulate
                while (state.GetWinner() == Player.None)
                {
                    var moves = state.GetValidMoves();
                    state.MakeMove(moves[random.Next(moves.Count)]);
                }

                // Backpropagate
                var winner = state.GetWinner();
                while (node != null)
                {
                    node.Update(winner == node.LastToMove ? 1.0 : 0.0);
                    node = node.Parent;
                }
                
                // Update Status
                if (statusTimer.ElapsedMilliseconds >= 500 && OnNewResult != null)
                {
                    var best = rootNode.Children?.OrderBy(x => x.Visits)?.ThenBy(x => x.Wins)?.LastOrDefault();
                    if (best != null)
                    {
                        var pv = "";
                        var c = best;
                        var depth = 0;
                        while (c != null && c.Move != null)
                        {
                            depth++;
                            pv += c.Move.ToString() + " (" + c.Wins + "/" + c.Visits + ") ";
                            c = c.Children?.OrderBy(x => x.Visits)?.ThenBy(x => x.Wins)?.LastOrDefault();
                        }
                        var status = new SearchStatus
                        {
                            BestMoveSoFar = new SearchResult
                            {
                                BestMove = best.Move,
                                Score = (int)Math.Round((best.Visits > 0 ? 200.0 * best.Wins / best.Visits : 0) - 100.0, 2),
                                PrimaryVariation = pv,
                                Evaluations = evaluations,
                                HashLookups = 0
                            },
                            ElapsedMilliseconds = timer.ElapsedMilliseconds,
                            SearchedNodes = evaluations,
                            CurrentMove = 0,
                            Depth = depth,
                            HashLookups = 0,
                            TotalMoves = 0,
                            CurrentVariation = pv
                        };
                        OnNewResult(this, status);
                    }
                    statusTimer = Stopwatch.StartNew();
                }
            }
            
            var final = rootNode.Children.OrderBy(x => x.Visits).ThenBy(x => x.Wins).LastOrDefault();
            return new SearchResult
            {
                BestMove = final.Move,
                Score = (int)Math.Round((final.Visits > 0 ? 200.0 * final.Wins / final.Visits : 0) - 100.0, 2),
                Evaluations = evaluations,
                HashLookups = 0,
                PrimaryVariation = ""
            };
        }

        class MonteCarloTreeSearchNode
        {
            public double Wins;
            public double Visits;
            public MonteCarloTreeSearchNode Parent;
            public Player LastToMove;
            public Move Move;
            public List<MonteCarloTreeSearchNode> Children;
            public List<Move> Untried;

            public MonteCarloTreeSearchNode(Position state)
                : this(state, null, null)
            {
            }

            public MonteCarloTreeSearchNode(Position state, Move move, MonteCarloTreeSearchNode parent)
            {
                Move = move;
                Parent = parent;

                Children = new List<MonteCarloTreeSearchNode>();
                Wins = 0.0;
                Visits = 0.0;

                if (state != null)
                {
                    Untried = state.GetValidMoves();
                    LastToMove = state.LastPlayerToMove;
                }
                else
                {
                    Untried = new List<Move>();
                }
            }

            public MonteCarloTreeSearchNode SelectChild()
            {
                return Children.OrderBy(x => UpperConfidenceBound(x)).LastOrDefault();
            }

            public MonteCarloTreeSearchNode AddChild(Position state, Move move)
            {
                var newNode = new MonteCarloTreeSearchNode(state, move, this);
                Untried.Remove(move);
                Children.Add(newNode);
                return newNode;
            }

            public void Update(double result)
            {
                Visits++;
                Wins += result;
            }

            private double UpperConfidenceBound(MonteCarloTreeSearchNode node)
            {
                return node.Wins / node.Visits + Math.Sqrt(2.0 * Math.Log(Visits) / node.Visits);
            }
        }
    }
}
