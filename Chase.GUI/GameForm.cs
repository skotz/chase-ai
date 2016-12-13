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
            SearchResult best = game.GetBestMove(2);

            //SearchResult best = new SearchResult() { BestMove = new Engine.Move() { FromIndex = 0, ToIndex = 8, FinalDirection = Direction.Left, Increment = 0 } };
            //game.MakeMove(best.BestMove);
            //richTextBox1.Text += game.GetStringVisualization();
            //best = new SearchResult() { BestMove = new Engine.Move() { FromIndex = 72, ToIndex = 73, FinalDirection = Direction.Right, Increment = 0 } };
            //game.MakeMove(best.BestMove);
            //richTextBox1.Text += game.GetStringVisualization();
            //best = new SearchResult() { BestMove = new Engine.Move() { FromIndex = 2, ToIndex = 40, FinalDirection = Direction.DownRight, Increment = 0 } };

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

            while (string.IsNullOrEmpty(game.Status))
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
