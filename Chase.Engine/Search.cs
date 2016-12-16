using Chase.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Search : ISearchAlgorithm
    {
        private static int evaluations;

        private static Stopwatch timer;

        public event EventHandler<SearchStatus> OnNewResult;

        public SearchResult GetBestMove(Position position, int searchDepth)
        {
            timer = Stopwatch.StartNew();
            evaluations = 0;

            SearchResult result = AlphaBetaSearch(position, int.MinValue, int.MaxValue, searchDepth * 2, 1);
            result.Evaluations = evaluations;

            return result;
        }

        private int EvaluatePosition(Position position)
        {
            evaluations++;

            // Start with a small amount of randomness to prevent always choosing one of two equal moves
            int eval = Constants.Rand.Next(3) - 1;            

            // Figure in how many pieces we have over our opponent
            eval += EvaluateMaterial(position);

            // Figure in a penalty for every piece on the A or I rows where a piece has only have 4 direction it can move
            eval += EvaluateDevelopment(position);

            // Figure in how many valid moves we have over our opponent
            // eval += EvaluateMobility(position);

            return eval;
        }

        private int EvaluateMaterial(Position position)
        {
            int bluePieces = 0;
            int redPieces = 0;

            // Material (number of pieces) difference
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (position.Board[i] > 0)
                {
                    bluePieces++;
                }
                else if (position.Board[i] < 0)
                {
                    redPieces++;
                }
            }

            // Game over scores
            if (bluePieces < Constants.MinimumPieceCount)
            {
                return -Constants.VictoryScore;
            }
            else if (redPieces < Constants.MinimumPieceCount)
            {
                return Constants.VictoryScore;
            }
            else
            {
                return (bluePieces - redPieces) * Constants.EvalPieceWeight;
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

            return (blueUndeveloped - redUndeveloped) * (-1) * Constants.EvalDevelopmentWeight;
        }

        private SearchResult AlphaBetaSearch(Position position, int alpha, int beta, int depth, int reportdepth)
        {
            // Evaluate the position
            int eval = EvaluatePosition(position);

            // See if someone won
            if (Math.Abs(eval) == Constants.VictoryScore)
            {
                return new SearchResult()
                {
                    Score = eval,
                    PrimaryVariation = eval == Constants.VictoryScore ? "BLUE-WINS" : "RED-WINS"
                };
            }

            // We've reached the depth of our search, so return the heuristic evaluation of the position
            if (depth <= 0)
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

            List<Move> moves = position.GetValidMoves();
            int movenum = 1;
            foreach (Move move in moves)
            {
                // Copy the board and make a move
                Position copy = position.Clone();
                copy.MakeMove(move);

                // Find opponents best counter move
                SearchResult child = AlphaBetaSearch(copy, alpha, beta, depth - 1, reportdepth - 1);

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
                    // Report on the progress of our search
                    OnNewResult?.Invoke(null, new SearchStatus()
                    {
                        BestMoveSoFar = best,
                        SearchedNodes = evaluations,
                        ElapsedMilliseconds = timer.ElapsedMilliseconds,
                        CurrentMove = movenum++,
                        TotalMoves = moves.Count
                    });
                }
            }

            return best;
        }
    }
}
