using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Engine
{
    public class Search
    {
        private static int evaluations;

        public static SearchResult GetBestMove(Position position, int searchDepth)
        {
            evaluations = 0;

            SearchResult result = AlphaBetaSearch(position, int.MinValue, int.MaxValue, searchDepth * 2);
            result.Evaluations = evaluations;

            return result;
        }

        private static int EvaluatePosition(Position position)
        {
            int bluePieces = 0;
            int redPieces = 0;

            evaluations++;

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

            if (bluePieces < Constants.MinimumPieceCount)
            {
                return -10000;
            }
            if (redPieces < Constants.MinimumPieceCount)
            {
                return 10000;
            }

            // Just a basic material calculation
            return (bluePieces - redPieces) * 100 + Constants.Rand.Next(51) - 25;
        }

        private static SearchResult AlphaBetaSearch(Position position, int alpha, int beta, int depth)
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
            foreach (Move move in moves)
            {
                // Copy the board and make a move
                Position copy = position.Clone();
                copy.MakeMove(move);

                // Find opponents best counter move
                SearchResult child = AlphaBetaSearch(copy, alpha, beta, depth - 1);

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
            }

            return best;
        }
    }
}
