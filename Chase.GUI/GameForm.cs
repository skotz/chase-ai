using Chase.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chase.GUI
{
    public partial class GameForm : Form
    {
        Game game;

        int depth = 2;

        public GameForm()
        {
            InitializeComponent();

            game = new Game();
            game.OnSearchProgress += Game_OnSearchProgress;
            game.OnFoundBestMove += Game_OnFoundBestMove;

            InitializeGameGUI();
            RefreshBoard();
        }

        private void selfPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.StartNew();
            game.BeginGetBestMove(depth);
        }

        private void Game_OnFoundBestMove(SearchResult result)
        {
            if (result.BestMove != null)
            {
                game.MakeMove(result.BestMove);
            }

            RefreshBoard(result.BestMove);

            Player winner = game.GetWinner();
            if (winner == Player.None)
            {
                game.BeginGetBestMove(depth);
            }
            else
            {
                game.SaveGameToFile("game." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
                MessageBox.Show(winner.ToString() + " wins!");
            }
        }

        private void Game_OnSearchProgress(SearchStatus status)
        {
            searchStatusLabel.Text = "best: " + status.BestMoveSoFar.BestMove.ToString() +
                " score: " + status.BestMoveSoFar.Score +
                " nps: " + status.NodesPerSecond.ToString("0") + 
                " pv: " + status.BestMoveSoFar.PrimaryVariation;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestForm test = new TestForm();
            test.ShowDialog();
        }

        private void RefreshBoard()
        {
            RefreshBoard(null);
        }

        private void RefreshBoard(Move lastmove)
        {
            for (int i = 0; i < 81; i++)
            {
                int piece = game.Board[i];
                if (i == 40)
                {
                    SetButton(i, "CH", Color.Gray);
                }
                else if (piece == 0)
                {
                    SetButton(i, "", Color.Black);
                }
                else
                {
                    SetButton(i, Math.Abs(piece), piece > 0 ? Color.Blue : Color.Red);
                }
            }

            if (lastmove != null)
            {
                if (lastmove.FromIndex > 0)
                {
                    gamePanel.Controls["tile" + lastmove.FromIndex].BackColor = Color.LightYellow;
                }
                if (lastmove.ToIndex > 0)
                {
                    gamePanel.Controls["tile" + lastmove.ToIndex].BackColor = Color.LightYellow;
                }
            }
        }

        private void InitializeGameGUI()
        {
            double hexFactor = 0.2471264367816092;
            Point firstTile = new Point(40, 0);
            Point nextTile = firstTile;
            Point bottomright;
            Size size;
            
            for (int i = 0; i < 81; i++)
            {
                CreateButton(i, nextTile, out bottomright, out size);
                
                if (i == 8 || i == 26 || i == 44 || i == 62)
                {
                    nextTile = new Point(firstTile.X - size.Width / 2, bottomright.Y - (int)(size.Height * hexFactor));
                }
                else if (i == 17 || i == 35 || i == 53 || i == 71)
                {
                    nextTile = new Point(firstTile.X, bottomright.Y - (int)(size.Height * hexFactor));
                }
                else
                {
                    nextTile = new Point(bottomright.X, nextTile.Y);
                }
            }
        }

        private void CreateButton(int moveIndex, Point location, out Point lowerright, out Size size)
        {
            int space = 5;
            float scale = 0.5f;
            PointF[] pts = {
                new PointF(0 * scale + space, 43 * scale + space),
                new PointF(0 * scale + space, 131 * scale + space),
                new PointF(76 * scale + space, 174 * scale + space),
                new PointF(152 * scale + space, 131 * scale + space),
                new PointF(152 * scale + space, 43 * scale + space),
                new PointF(76 * scale + space, 0 * scale + space)
            };
            GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
            polygon_path.AddPolygon(pts);
            Region polygon_region = new Region(polygon_path);

            Button button = new Button();
            button.Location = location;
            button.Name = "tile" + moveIndex;
            button.Size = new Size((int)pts[3].X, (int)pts[2].Y);
            button.TabIndex = 7;
            button.Text = moveIndex.ToString();
            button.UseVisualStyleBackColor = true;
            button.Font = new Font("Tahoma", 14.0f, FontStyle.Bold);
            button.ForeColor = Color.Red;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Region = polygon_region;
            button.SetBounds(button.Location.X, button.Location.Y, (int)pts[3].X + space * 2, (int)pts[2].Y + space * 2);
            gamePanel.Controls.Add(button);

            lowerright = new Point(button.Location.X + button.Width - space * 2, button.Location.Y + button.Height - space * 2);
            size = new Size(button.Size.Width - space * 2, button.Size.Height - space * 2);
        }

        private void SetButton(int index, int value, Color team)
        {
            SetButton(index, value.ToString(), team);
        }

        private void SetButton(int index, string value, Color team)
        {
            gamePanel.Controls["tile" + index].Text = value;
            gamePanel.Controls["tile" + index].ForeColor = team;
            (gamePanel.Controls["tile" + index] as Button).UseVisualStyleBackColor = true;
        }
    }
}
