using Chase.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            game.BeginGetBestMove(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += game.GetStringVisualization();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchResult best = game.GetBestMove(2);

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
                best = game.GetBestMove(2);
                game.MakeMove(best.BestMove);

                richTextBox1.Text += "Move: " + best.BestMove.ToString() + "\r\n";
                richTextBox1.Text += game.GetStringVisualization();
            }

            game.SaveGameToFile("game.txt");
        }
    }
}
