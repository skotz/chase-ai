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
    public class Search : ISearchAlgorithm
    {
        private long evaluations;

        private long hashLookups;

        private Stopwatch timer;

        private int timeLimitMilliseconds;

        private bool cutoff;

        private int currentDepth;

        private string levelOneNode;

        private Player initiatingPlayer;

        private Dictionary<ulong, SearchResult> hashtable;

        public event EventHandler<SearchStatus> OnNewResult;

        public SearchResult GetBestMove(Position position, SearchArgs settings)
        {
            SearchResult result = new SearchResult();

            initiatingPlayer = position.PlayerToMove;

            timer = Stopwatch.StartNew();
            cutoff = false;
            evaluations = 0;
            hashLookups = 0;
            timeLimitMilliseconds = settings.MaxSeconds < 0 ? 10000000 : settings.MaxSeconds * 1000;

            List<Move> moves = null;
            List<int> depths = new List<int>();

            // Instead of creating some complicated for loop, just store up the depths we intend to search ahead of time
            if (settings.MaxSeconds < 0)
            {
                if (settings.MaxDepth != 1)
                {
                    // If we're searching deeper than 3 (6 half moves) then do a fast two level deep search to help with move ordering
                    depths.Add(4);
                }
                else if (settings.MaxDepth > 1)
                {
                    // If we're searching deeper than 1 (2 half moves) then do a super fast one level deep search to help with move ordering
                    depths.Add(2);
                }
                depths.Add(settings.MaxDepth * 2);
            }
            else
            {
                // All the depths we intend to search, in order
                for (int depth = 2; depth <= settings.MaxDepth * 2; depth += 2)
                {
                    depths.Add(depth);
                }
            }

            // Iterative deepening
            // Increment depth by 2 since we always want to consider pairs of move (our move and opponent's move)
            foreach (int depth in depths)
            {
                // Need to re-initialize hash table at each depth
                hashtable = new Dictionary<ulong, SearchResult>();

                currentDepth = depth;

                SearchResult nextDepth = AlphaBetaSearch(position, int.MinValue, int.MaxValue, depth, 1, 1, ref moves);

                // Move ordering for next iteration
                if (initiatingPlayer == Player.Red)
                {
                    moves.Sort((c, n) => c.Evaluation.CompareTo(n.Evaluation));
                }
                else
                {
                    moves.Sort((c, n) => n.Evaluation.CompareTo(c.Evaluation));
                }

                if (!cutoff)
                {
                    result = nextDepth;
                }
            }

            result.Evaluations = evaluations;
            result.HashLookups = hashLookups;

            return result;
        }

        private int EvaluatePosition(Position position, int currentDepth)
        {
            evaluations++;

            // Start with a small amount of randomness to prevent always choosing one of two equal moves
            int eval = Constants.Rand.Next(3) - 1;

            // Figure in how many pieces we have over our opponent
            eval += EvaluateMaterial(position, currentDepth);

            // Figure in a penalty for every piece on the A or I rows where a piece has only have 4 direction it can move
            eval += EvaluateDevelopment(position);

            // Figure in how many valid moves we have over our opponent
            // eval += EvaluateMobility(position);

            return eval;
        }

        private int EvaluateMaterial(Position position, int currentDepth)
        {
            int bluePieces = 0;
            int redPieces = 0;

            int blueGoodPieces = 0;
            int redGoodPieces = 0;

            // Material (number of pieces) difference
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (position.Board[i] > 0)
                {
                    bluePieces++;

                    if (position.Board[i] == 4 || position.Board[i] == 5)
                    {
                        // 4s and 5s are good
                        blueGoodPieces++;
                    }
                    else if (position.Board[i] == 6)
                    {
                        // 6s are bad
                        blueGoodPieces--;
                    }
                }
                else if (position.Board[i] < 0)
                {
                    redPieces++;

                    if (position.Board[i] == -4 || position.Board[i] == -5)
                    {
                        redGoodPieces++;
                    }
                    else if (position.Board[i] == -6)
                    {
                        redGoodPieces--;
                    }
                }
            }

            // Game over scores
            if (bluePieces < Constants.MinimumPieceCount)
            {
                // Give slightly better evaluations to the faster forced win
                return -Constants.VictoryScore + currentDepth;
            }
            else if (redPieces < Constants.MinimumPieceCount)
            {
                return Constants.VictoryScore - currentDepth;
            }
            else
            {
                int eval = 0;

                // Just the difference between the piece count between blue and red
                // eval += (bluePieces - redPieces) * Constants.EvalPieceWeight;

                // Figure in the ideal value of pieces
                // Range: [-4 - 5, 5 - (-4)] --> [-9, 9]
                eval += (blueGoodPieces - redGoodPieces) * Constants.EvalPieceValueWeight;

                // Just subtracting red from blue results in the same score for (blue = 6, red = 5) and (blue = 10, red = 9) even thought the lead of 1 means more in the first case
                // This calculation figures in that an extra piece means more the fewer you have, so... 
                // (blue = 6, red = 5) --> (6 * 100) / 5 - 100 = 20
                // (blue = 10, red = 9) --> (10 * 100) / 9 - 100 = 11
                // Range: [-(10 * 100) / 5 - 100, (10 * 100) / 5 - 100] --> [-100, 100]
                if (bluePieces > redPieces)
                {
                    eval += (bluePieces * 100) / redPieces - 100;
                }
                else if (redPieces > bluePieces)
                {
                    eval -= (redPieces * 100) / bluePieces - 100;
                }

                return eval * Constants.EvalPieceWeight;
            }
        }
        
        private int EvaluateMobility(Position position)
        {
            Player savePlayer = position.PlayerToMove;

            position.PlayerToMove = Player.Blue;
            int blueMoves = position.GetValidMoves().Count;

            position.PlayerToMove = Player.Red;
            int redMoves = position.GetValidMoves().Count;

            // Mobility (number of valid moves) difference
            int eval = (blueMoves - redMoves) * Constants.EvalMobilityWeight;

            position.PlayerToMove = savePlayer;

            return eval;
        }

        private int EvaluateDevelopment(Position position)
        {
            int blueUndeveloped = 0;
            int redUndeveloped = 0;

            // Get a penalty for each piece on the edge where it has at most 4 directions it can move
            for (int i = 0; i < 9; i++)
            {
                if (position.Board[i] > 0)
                {
                    blueUndeveloped++;
                }
                if (position.Board[i] < 0)
                {
                    redUndeveloped++;
                }
            }
            for (int i = 72; i < 81; i++)
            {
                if (position.Board[i] > 0)
                {
                    blueUndeveloped++;
                }
                if (position.Board[i] < 0)
                {
                    redUndeveloped++;
                }
            }

            // Range: [(10 - 0) * (-1), (0 - 10) * (-1)] * weight --> [-10, 10] * weight
            return (blueUndeveloped - redUndeveloped) * (-1) * Constants.EvalDevelopmentWeight;
        }

        private SearchResult AlphaBetaSearch(Position position, int alpha, int beta, int depth, int reportdepth, int depthUp, ref List<Move> orderedMoves)
        {
            // If we've already processed this position, use the saved evaluation
            ulong hash = position.GetHash();
            if (hashtable.ContainsKey(hash))
            {
                hashLookups++;
                return hashtable[hash];
            }

            // Evaluate the position
            int eval = EvaluatePosition(position, depthUp);

            // See if someone won
            if (Math.Abs(eval) > Constants.VictoryScore - 100)
            {
                return new SearchResult()
                {
                    Score = eval,
                    PrimaryVariation = eval > 0 ? "BLUE-WINS" : "RED-WINS"
                };
            }

            // See if we need to immediately stop searching
            if (timer.ElapsedMilliseconds >= timeLimitMilliseconds)
            {
                cutoff = true;
                return new SearchResult();
            }

            // We've reached the depth of our search, so return the heuristic evaluation of the position
            // Make sure we're evaluating after our opponent's last move (meaning it's our turn to move again) so that we calculate full move pairs
            if (depth <= 0 && position.PlayerToMove == initiatingPlayer)
            {
                return new SearchResult()
                {
                    Score = eval,
                    PrimaryVariation = ""
                };
            }

            bool maximizingPlayer = position.PlayerToMove == Player.Blue;
            SearchResult best = new SearchResult()
            {
                Score = maximizingPlayer ? int.MinValue : int.MaxValue
            };

            List<Move> nullMoves = null;
            List<Move> moves;
            if (orderedMoves != null)
            {
                // Use an ordered list of moves based on the evaluations from a previous search depth
                moves = orderedMoves;
            }
            else
            {
                moves = position.GetValidMoves();
            }

            // If we have no moves, return the evaluation of the position
            if (moves.Count == 0)
            {
                return new SearchResult()
                {
                    Score = eval,
                    PrimaryVariation = ""
                };
            }

            int movenum = 1;
            foreach (Move move in moves)
            {
                // Copy the board and make a move
                Position copy = position.Clone();
                copy.MakeMove(move);

                // Don't repeat positions
                if (copy.LastMoveWasRepetition())
                {
                    continue;
                }

                // Store the current node for search reporting
                if (reportdepth > 0 && depthUp == 1)
                {
                    levelOneNode = move.ToString();
                }

                // Find opponents best counter move
                SearchResult child = AlphaBetaSearch(copy, alpha, beta, depth - 1, reportdepth - 1, depthUp + 1, ref nullMoves);

                // Store the evaluation for iterative deepening move ordering
                move.Evaluation = child.Score;

                if (maximizingPlayer)
                {
                    if (child.Score > best.Score)
                    {
                        best.Score = child.Score;
                        best.BestMove = move;
                        best.PrimaryVariation = move.ToString() + " " + child.PrimaryVariation;
                    }

                    alpha = Math.Max(alpha, best.Score);

                    if (beta <= alpha)
                    {
                        // Beta cutoff
                        break;
                    }
                }
                else
                {
                    if (child.Score < best.Score)
                    {
                        best.Score = child.Score;
                        best.BestMove = move;
                        best.PrimaryVariation = move.ToString() + " " + child.PrimaryVariation;
                    }
                    
                    beta = Math.Min(beta, best.Score);
                    
                    if (beta <= alpha)
                    {
                        // Alpha cutoff
                        break;
                    }
                }

                if (reportdepth > 0)
                {
                    if (best == null)
                    {
                        best = new SearchResult();
                    }
                    if (best.BestMove == null)
                    {
                        best.BestMove = new Move();
                    }

                    // Report on the progress of our search
                    OnNewResult?.Invoke(null, new SearchStatus()
                    {
                        BestMoveSoFar = best,
                        SearchedNodes = evaluations,
                        ElapsedMilliseconds = timer.ElapsedMilliseconds,
                        CurrentMove = movenum++,
                        TotalMoves = moves.Count,
                        Depth = currentDepth,
                        HashLookups = hashLookups,
                        CurrentVariation = (depthUp != 1 ? levelOneNode + " " : "") + move.ToString() + " " + child.PrimaryVariation
                    });
                }
            }

            if (!hashtable.ContainsKey(hash))
            {
                hashtable.Add(hash, best);
            }
            
            // Store the moves for the next depth (if using iterative deepening
            if (depthUp == 1)
            {
                orderedMoves = moves;
            }

            return best;
        }
    }
}
