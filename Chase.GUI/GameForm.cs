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
    }
}
