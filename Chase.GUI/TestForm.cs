using Chase.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chase.GUI
{
    public partial class TestForm : Form
    {
        Game game;

        public TestForm()
        {
            InitializeComponent();

            game = new Game();
            game.OnSearchProgress += Game_OnSearchProgress;
            game.OnFoundBestMove += Game_OnFoundBestMove;
        }

        private void Game_OnFoundBestMove(SearchResult result)
        {
            richTextBox1.Text += "Evals: " + result.Evaluations + "\r\n";
            richTextBox1.Text += "Score: " + result.Score + "\r\n";
            richTextBox1.Text += "Move: " + result.BestMove.ToString() + "\r\n";
            richTextBox1.Text += "PV: " + result.PrimaryVariation + "\r\n";
        }

        private void Game_OnSearchProgress(SearchStatus status)
        {
            label1.Text = "best: " + status.BestMoveSoFar.BestMove.ToString() +
                " score: " + status.BestMoveSoFar.Score +
                " nps: " + status.NodesPerSecond.ToString("0") +
                " pv: " + status.BestMoveSoFar.PrimaryVariation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Move move in game.GetAllMoves())
            {
                richTextBox1.Text += move.ToString() + "\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.BeginGetBestMove(2, -1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += game.GetStringVisualization();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchResult best = game.GetBestMove(2, -1);

            game.MakeMove(best.BestMove);

            richTextBox1.Text += "Evals: " + best.Evaluations + "\r\n";
            richTextBox1.Text += "Score: " + best.Score + "\r\n";
            richTextBox1.Text += "Move: " + best.BestMove.ToString() + "\r\n";
            richTextBox1.Text += "PV: " + best.PrimaryVariation + "\r\n";
            richTextBox1.Text += "Next Player: " + game.PlayerToMove.ToString() + "\r\n";

            richTextBox1.Text += game.GetStringVisualization();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            game.SaveGameToFile("game.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            game = new Game();
            SearchResult best;

            while (game.GetWinner() == Player.None)
            {
                best = game.GetBestMove(2, -1);
                game.MakeMove(best.BestMove);

                richTextBox1.Text += "Move: " + best.BestMove.ToString() + "\r\n";
                richTextBox1.Text += game.GetStringVisualization();
            }

            game.SaveGameToFile("game.txt");
        }

        public ulong NextInt64(RandomNumberGenerator rnd)
        {
            byte[] buffer = new byte[sizeof(ulong)];
            rnd.GetBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Programmatically generate the static multidimensional array of random numbers for zobrist-like hashes
            // Don't just want to do this on program load since we want it to be predictable across program loads
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            StringBuilder sb = new StringBuilder();

            sb.Append("\r\n{");
            // One per tile
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                sb.Append("\r\n    {");
                // One per possible piece on the tile
                // We're only going to use 12, but create 13 so we can just take the square value + 6 to change the range of [-6,6] to [0,12]
                for (int j = 0; j < 13; j++)
                {
                    sb.Append("\r\n        " + string.Format("0x{0:X}", NextInt64(rng)) + ",");
                }
                sb.Append("\r\n    },");
            }
            sb.Append("\r\n}");

            richTextBox1.Text = Regex.Replace(sb.ToString(), @"\,([^}{,]*)\}", m => m.ToString().Substring(1));
        }
    }
}
