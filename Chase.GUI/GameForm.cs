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
    public partial class GameForm : Form
    {
        Game game;

        public GameForm()
        {
            InitializeComponent();

            game = new Game();
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
            SearchResult best = game.GetBestMove(2);

            richTextBox1.Text += "Evals: " + best.Evaluations + "\r\n";
            richTextBox1.Text += "Score: " + best.Score + "\r\n";
            richTextBox1.Text += "Move: " + best.BestMove.ToString() + "\r\n";
            richTextBox1.Text += "PV: " + best.PrimaryVariation + "\r\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += game.GetStringVisualization();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchResult best = game.GetBestMove(3);

            game.MakeMove(best.BestMove);

            richTextBox1.Text += "Evals: " + best.Evaluations + "\r\n";
            richTextBox1.Text += "Score: " + best.Score + "\r\n";
            richTextBox1.Text += "Move: " + best.BestMove.ToString() + "\r\n";
            richTextBox1.Text += "PV: " + best.PrimaryVariation + "\r\n";
            richTextBox1.Text += "Next Player: " + game.PlayerToMove.ToString() + "\r\n";

            richTextBox1.Text += game.GetStringVisualization();
        }
    }
}
