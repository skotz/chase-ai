using Chase.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chase.GUI
{
    public partial class GameForm : Form
    {
        Game game;
        GameType type;

        List<HexTile> tiles;
        Move lastMove;

        bool addPieceMode = false;
        int maxAddPiece = 5;

        int selectedFromTile = -1;
        int selectedToTile = -1;

        int analysisMaxMoves = 0;
        int analysisMoveNum = 0;

        public GameForm()
        {
            InitializeComponent();

            game = new Game();
            game.OnSearchProgress += Game_OnSearchProgress;
            game.OnFoundBestMove += Game_OnFoundBestMove;

            type = GameType.NotStarted;

            InitializeGameGUI();
            RefreshBoard();

            gameTimer.Start();
        }

        private void selfPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = GameType.ComputerSelfPlay;
            infoLabel.Text = "Thinking...";
            searchStatusLabel.Text = "Thinking...";

            game.StartNew();
            game.BeginGetBestMove(GetDepth(), GetSeconds());
        }

        private void Game_OnFoundBestMove(SearchResult result)
        {
            if (result.BestMove != null)
            {
                game.MakeMove(result.BestMove);
            }
            
            if (!showComputerAnalysisToolStripMenuItem.Checked)
            {
                searchStatusLabel.Text = "Your turn!";
            }

            RefreshBoard(result.BestMove);

            Player winner = game.GetWinner();
            if (winner == Player.None)
            {
                // If it's a computer self-play game, always make a move. If the computer is playing a human, then it might have to make multiple moves in a row after a capture
                if ((type == GameType.ComputerSelfPlay) || (game.PlayerToMove == Player.Blue && computerPlaysBlueToolStripMenuItem.Checked) || (game.PlayerToMove == Player.Red && !computerPlaysBlueToolStripMenuItem.Checked))
                {
                    game.BeginGetBestMove(GetDepth(), GetSeconds());
                }
            }
            else
            {
                try
                {
                    game.SaveGameToFile("game." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
                }
                catch (UnauthorizedAccessException)
                {
                    // This doesn't work when running from the installed Programs folder, so eat the exception.
                }

                MessageBox.Show(winner.ToString() + " wins!", "Chase", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Game_OnSearchProgress(SearchStatus status)
        {
            if (showComputerAnalysisToolStripMenuItem.Checked)
            {
                searchStatusLabel.Text = "best: " + status.BestMoveSoFar.BestMove.ToString() +
                    " score: " + status.BestMoveSoFar.Score +
                    " depth: " + status.Depth +
                    " nps: " + status.NodesPerSecond.ToString("0") +
                    " hl: " + status.HashLookups +
                    " pv: " + status.CurrentVariation;
            }
            else
            {
                searchStatusLabel.Text = "Thinking...";
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestForm test = new TestForm();
            test.ShowDialog();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = GameType.ComputerVsHuman;

            game.StartNew();

            RefreshBoard();

            if (!computerPlaysBlueToolStripMenuItem.Checked)
            {
                searchStatusLabel.Text = "Thinking...";
                game.BeginGetBestMove(GetDepth(), GetSeconds());
            }
        }

        private void RefreshBoard()
        {
            RefreshBoard(null);
        }

        private void RefreshBoard(Move lastmove)
        {
            lastMove = lastmove;

            if (gamePanel.Controls["tile0"] == null)
            {
                // Probably in the process of closing the program
                return;
            }
            
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

            Player winner = game.GetWinner();
            if (winner == Player.None)
            {
                // Show threatened pieces
                if (showThreatenedPiecesToolStripMenuItem.Checked)
                {
                    List<int> threats = game.GetThreatenedPieces();
                    foreach (int threatIndex in threats)
                    {
                        gamePanel.Controls["tile" + threatIndex].BackColor = Color.LightPink;
                    }
                }

                // Show valid moves
                if (highlightValidMovesToolStripMenuItem.Checked && game.PlayerToMove == (computerPlaysBlueToolStripMenuItem.Checked ? Player.Red : Player.Blue))
                {
                    List<Move> moves = game.GetAllMoves();
                    if (moves.Count > 0 && moves[0].Increment > 0 && moves[0].FromIndex == -1)
                    {
                        // Add points after a capture
                        foreach (Move move in moves)
                        {
                            gamePanel.Controls["tile" + move.ToIndex].Text += "+" + move.Increment;
                            gamePanel.Controls["tile" + move.ToIndex].BackColor = Color.LightGreen;
                        }
                    }
                    else if (selectedFromTile >= 0)
                    {
                        // Find movement moves
                        foreach (Move move in moves)
                        {
                            if (move.FromIndex == selectedFromTile)
                            {
                                gamePanel.Controls["tile" + move.ToIndex].BackColor = Color.LightGreen;
                                gamePanel.Controls["tile" + move.FromIndex].BackColor = Color.White;
                            }
                        }
                    }
                }

                // Status information
                if (type == GameType.NotStarted)
                {
                    infoLabel.Text = "Ready";
                    handRed.Text = "?";
                    handBlue.Text = "?";
                }
                else
                {
                    infoLabel.Text = game.PlayerToMove.ToString() + "'s Turn";
                    handRed.Text = game.RedInHand.ToString();
                    handBlue.Text = game.BlueInHand.ToString();
                }
            }
            else
            {
                infoLabel.Text = winner.ToString() + " Won!";
                handRed.Text = game.RedInHand.ToString();
                handBlue.Text = game.BlueInHand.ToString();
            }

            // Refresh move list
            List<MoveHistory> history = game.GetMoveHistory();
            moveHistory.DataSource = history;

            // Analysis information
            if (type == GameType.Loaded)
            {
                nextMove.Enabled = true;
                previousMove.Enabled = true;
                analysisLabel.Text = analysisMoveNum + "/" + analysisMaxMoves;
            }
            else
            {
                nextMove.Enabled = false;
                previousMove.Enabled = false;
                analysisLabel.Text = "CGN";
            }
        }

        private void ClickTile(int index)
        {
            if (game.GetWinner() == Player.None)
            {
                if (type == GameType.ComputerVsHuman && ((game.PlayerToMove == Player.Blue && !computerPlaysBlueToolStripMenuItem.Checked) || (game.PlayerToMove == Player.Red && computerPlaysBlueToolStripMenuItem.Checked)))
                {
                    if (selectedFromTile >= 0)
                    {
                        if (selectedFromTile == index)
                        {
                            // Unselect an already selected tile
                            selectedFromTile = -1;
                        }
                        else
                        {
                            // Select destination tile
                            selectedToTile = index;

                            List<Move> moves = game.GetAllMoves();
                            List<Move> options = moves.Where(x => x.FromIndex == selectedFromTile && x.ToIndex == selectedToTile && x.Increment > 0).ToList();
                            if (options.Count > 0)
                            {
                                // If we're transferring points to an adjacent piece
                                List<int> increments = options.Select(x => x.Increment).Distinct().ToList();

                                add1.Enabled = increments.Contains(1);
                                add2.Enabled = increments.Contains(2);
                                add3.Enabled = increments.Contains(3);
                                add4.Enabled = increments.Contains(4);
                                add5.Enabled = increments.Contains(5);

                                addPanel.Visible = true;
                                addPieceMode = true;
                                maxAddPiece = increments.Max();
                            }
                            else
                            {
                                options = moves.Where(x => x.FromIndex == selectedFromTile && x.ToIndex == selectedToTile && x.Increment == 0).ToList();

                                if (options.Count > 0)
                                {
                                    Move move = options.First();

                                    MakeMove(move);
                                }
                                else
                                {
                                    // Invalid from square?
                                    selectedFromTile = -1;
                                }
                            }
                        }
                    }
                    else
                    {
                        List<Move> moves = game.GetAllMoves();
                        if (moves.Count > 0 && moves.Any(x => x.FromIndex == index))
                        {
                            // Select a source tile
                            selectedFromTile = index;
                        }

                        // If the move we need to make is filling in points after a capture
                        if (moves[0].Increment > 0)
                        {
                            Move move = moves.FirstOrDefault(x => x.ToIndex == index);

                            if (move != null)
                            {
                                MakeMove(move);
                            }
                        }
                    }
                }
            }

            RefreshBoard();
        }

        private void MakeMove(Move move)
        {
            game.MakeMove(move);

            RefreshBoard();

            selectedFromTile = -1;
            selectedToTile = -1;

            // If we're playing the computer, let the computer know it's his turn
            if (type == GameType.ComputerVsHuman)
            {
                if ((game.PlayerToMove == Player.Blue && computerPlaysBlueToolStripMenuItem.Checked) || (game.PlayerToMove == Player.Red && !computerPlaysBlueToolStripMenuItem.Checked))
                {
                    searchStatusLabel.Text = "Thinking...";
                    game.BeginGetBestMove(GetDepth(), GetSeconds());
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((Button)sender).Name.Replace("tile", ""));
            ClickTile(index);
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

            tiles = new List<HexTile>();
            for (int i = 0; i < 81; i++)
            {
                tiles.Add(CreateHexTile(i));
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

            HexButton button = new HexButton();
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
            button.Points = pts;
            button.SetBounds(button.Location.X, button.Location.Y, (int)pts[3].X + space * 2, (int)pts[2].Y + space * 2);
            button.Click += Button_Click;
            button.DisplayTileLabel = () => showTileLabelsToolStripMenuItem.Checked;
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

        private void add1_Click(object sender, EventArgs e)
        {
            int amount = int.Parse(((Button)sender).Name.Replace("add", ""));

            List<Move> options = game.GetAllMoves().Where(x => x.FromIndex == selectedFromTile && x.ToIndex == selectedToTile && x.Increment  == amount).ToList();
            if (options.Count > 0)
            {
                Move move = options.First();
                MakeMove(move);
            }

            addPanel.Visible = false;
            addPieceMode = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
            addPieceMode = false;
        }

        private void loadPositionFromCSNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            csnInput.Text = "";
            csnPanel.Visible = true;
            csnInput.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            csnPanel.Visible = false;
        }

        private void csnInput_TextChanged(object sender, EventArgs e)
        {
            Position position = Position.FromStringNotation(csnInput.Text);
            if (position == null)
            {
                csnInput.BackColor = Color.LightPink;
            }
            else
            {
                csnInput.BackColor = Color.White;
                csnPanel.Visible = false;

                game.StartNew(position);

                type = GameType.Analysis;

                RefreshBoard();
            }
        }

        private void copyCSNFromPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(game.Board.ToStringNotation());
            MessageBox.Show("Position copied to clipboard!", "Chase", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showTileLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private int GetDepth()
        {
            if (oneMoveDeep.Checked)
            {
                return 1;
            }
            else if (twoMovesDeep.Checked)
            {
                return 2;
            }
            else if (threeMovesDeep.Checked)
            {
                return 3;
            }
            else if (fourMovesDeep.Checked)
            {
                return 4;
            }
            else
            {
                return 100;
            }
        }

        private int GetSeconds()
        {
            if (fiveSecondsMove.Checked)
            {
                return 5;
            }
            else if (twentySecondsMove.Checked)
            {
                return 20;
            }
            else if (sixtySecondsMove.Checked)
            {
                return 60;
            }
            else
            {
                return -1;
            }
        }

        private void depthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ToolStripMenuItem> options = new List<ToolStripMenuItem>()
            {
                oneMoveDeep,
                twoMovesDeep,
                threeMovesDeep,
                fourMovesDeep,
                fiveSecondsMove,
                twentySecondsMove,
                sixtySecondsMove
            };

            options.Where(x => x.Name != ((ToolStripMenuItem)sender).Name).ToList().ForEach(x => x.Checked = false);

            ((ToolStripMenuItem)sender).Checked = true;
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveCgn.ShowDialog();
            if (dr == DialogResult.OK)
            {
                using (StreamWriter w = new StreamWriter(saveCgn.FileName))
                {
                    w.Write(game.GetGameNotationString());
                }
            }
        }

        private void loadGameFromCGNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = openCgn.ShowDialog();
            if (dr == DialogResult.OK)
            {
                using (StreamReader r = new StreamReader(openCgn.FileName))
                {
                    analysisMaxMoves = game.LoadFromGameNotationString(r.ReadToEnd());
                }

                type = GameType.Loaded;
                analysisMoveNum = analysisMaxMoves;

                Move lastMove = game.RecallState(analysisMoveNum);
                RefreshBoard(lastMove);
            }
        }

        private void previousMove_Click(object sender, EventArgs e)
        {
            if (type == GameType.Loaded)
            {
                analysisMoveNum--;
                if (analysisMoveNum < 0)
                {
                    analysisMoveNum = 0;
                }

                Move lastMove = game.RecallState(analysisMoveNum);
                RefreshBoard(lastMove);
            }
        }

        private void nextMove_Click(object sender, EventArgs e)
        {
            if (type == GameType.Loaded)
            {
                analysisMoveNum++;
                if (analysisMoveNum > analysisMaxMoves)
                {
                    analysisMoveNum = analysisMaxMoves;
                }
                
                Move lastMove = game.RecallState(analysisMoveNum);
                RefreshBoard(lastMove);
            }
        }

        private Color backgroundColor = Color.FromArgb(171, 171, 171);
        private Color dialogColor = Color.FromArgb(200, 171, 171, 171);

        private Color tileColor = Color.FromArgb(225, 225, 225);
        private Color tileRedColor = Color.FromArgb(255, 0, 0);
        private Color tileBlueColor = Color.FromArgb(0, 0, 255);

        private Color tileLabelColor = Color.FromArgb(128, 0, 0, 0);
        private Color tileRedLabelColor = Color.FromArgb(100, 255, 255, 255);
        private Color tileBlueLabelColor = Color.FromArgb(100, 255, 255, 255);

        private Color tileTextColor = Color.FromArgb(0, 0, 0);
        private Color tileRedTextColor = Color.FromArgb(200, 255, 255, 255);
        private Color tileBlueTextColor = Color.FromArgb(200, 255, 255, 255);

        private Color tileLastMove = Color.FromArgb(255, 255, 0);
        private Color tileAvailableMove = Color.FromArgb(0, 255, 0);
        private Color tileThreatenedPiece = Color.FromArgb(255, 0, 255);
        private Color tileSelectedPiece = Color.FromArgb(0, 255, 255);

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            DrawBoard();
        }

        private void DrawBoard()
        {
            PointF mouseLocation = chasePanel.PointToClient(Cursor.Position);

            using (Bitmap b = new Bitmap(chasePanel.Width, chasePanel.Height))
            using (Graphics g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.Clear(backgroundColor);

                for (int i = 0; i < 81; i++)
                {
                    DrawHexTile(g, i, tiles[i].IsPointInHex(mouseLocation));
                }

                if (addPieceMode)
                {
                    SolidBrush dialog = new SolidBrush(dialogColor);
                    g.FillRectangle(dialog, 0, 0, b.Width, b.Height);

                    // Need to distribute captured points after opponent's last move
                    DrawAddPieceDialog(g, mouseLocation);
                }

                using (Graphics game = chasePanel.CreateGraphics())
                {
                    game.DrawImage(b, 0, 0, chasePanel.Width, chasePanel.Height);
                }
            }
        }

        private HexTile CreateHexTile(int hexIndex)
        {
            int space = 5;
            int tileWidth = 76;
            int rowOffset = 70;

            int rowIndex = (int)Math.Floor(hexIndex / 9.0);
            int columnIndex = hexIndex % 9;
            float insetOffset = (space + tileWidth) / 2f;

            float x = space + (space + tileWidth) * columnIndex;
            float y = rowOffset * rowIndex + space;

            if (rowIndex % 2 == 0)
            {
                // Inset rows
                x += insetOffset;
            }

            float pointFactTop = 0.2828947f;
            float pointFactMid = 0.8618421f;
            float pointFactBottom = 1.1447368f;
            float widthToHeight = 1.1547005f;

            PointF[] hexPoints = {
                new PointF(x, tileWidth * pointFactTop + y),
                new PointF(x, tileWidth * pointFactMid + y),
                new PointF(tileWidth / 2f + x, tileWidth * pointFactBottom + y),
                new PointF(tileWidth + x, tileWidth * pointFactMid + y),
                new PointF(tileWidth + x, tileWidth * pointFactTop + y),
                new PointF(tileWidth / 2f + x, y)
            };

            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            path.AddPolygon(hexPoints);

            return new HexTile(hexPoints, path, x, y, tileWidth, tileWidth * widthToHeight);
        }

        private void DrawAddPieceDialog(Graphics g, PointF mouse)
        {
            int chIndex = 40;
            int[] options = new int[]
            {
                chIndex - 8,
                chIndex + 1,
                chIndex + 10,
                chIndex + 9,
                chIndex - 1
            };
            int cancel = chIndex - 9;

            SolidBrush tileCancelBrush = new SolidBrush(tileThreatenedPiece);
            SolidBrush tileTextBrush = new SolidBrush(tileTextColor);

            for (int i = 0; i < maxAddPiece; i++)
            {
                // Change the color if the mouse is hovering over the tile
                SolidBrush tileBrush = new SolidBrush(tileColor);
                if (tiles[options[i]].IsPointInHex(mouse))
                {
                    Color newColor = Color.FromArgb(128, tileBrush.Color.R, tileBrush.Color.G, tileBrush.Color.B);
                    tileBrush = new SolidBrush(newColor);
                }

                // Draw the hex option
                g.FillPath(tileBrush, tiles[options[i]].Path);

                // Draw the option text
                DrawTileText(g, options[i], tileTextBrush, "+" + (i + 1));
            }

            // Draw the cancel button
            if (tiles[cancel].IsPointInHex(mouse))
            {
                Color newColor = Color.FromArgb(128, tileCancelBrush.Color.R, tileCancelBrush.Color.G, tileCancelBrush.Color.B);
                tileCancelBrush = new SolidBrush(newColor);
            }
            g.FillPath(tileCancelBrush, tiles[cancel].Path);
            DrawTileText(g, cancel, tileTextBrush, "X");
        }

        private void DrawHexTile(Graphics g, int hexIndex, bool hover)
        {
            // Determine ownership of tile
            SolidBrush tileBrush;
            SolidBrush tileTextBrush;
            SolidBrush labelBrush;
            int piece = game.Board[hexIndex];
            string value = "";
            if (hexIndex == 40)
            {
                // The chamber
                tileBrush = new SolidBrush(tileColor);
                tileTextBrush = new SolidBrush(tileTextColor);
                labelBrush = new SolidBrush(tileLabelColor);
                value = "CH";
            }
            else if (piece > 0)
            {
                // Blue team
                tileBrush = new SolidBrush(tileBlueColor);
                tileTextBrush = new SolidBrush(tileBlueTextColor);
                labelBrush = new SolidBrush(tileBlueLabelColor);
                value = piece.ToString();
            }
            else if (piece < 0)
            {
                // Red team
                tileBrush = new SolidBrush(tileRedColor);
                tileTextBrush = new SolidBrush(tileRedTextColor);
                labelBrush = new SolidBrush(tileRedLabelColor);
                value = Math.Abs(piece).ToString();
            }
            else
            {
                // Empty
                tileBrush = new SolidBrush(tileColor);
                tileTextBrush = new SolidBrush(tileTextColor);
                labelBrush = new SolidBrush(tileLabelColor);
                value = " ";
            }

            // Highlight the last move
            if (lastMove != null)
            {
                if (lastMove.FromIndex == hexIndex || lastMove.ToIndex == hexIndex)
                {
                    tileBrush = new SolidBrush(tileLastMove);
                    tileTextBrush = new SolidBrush(tileTextColor);
                    labelBrush = new SolidBrush(tileLabelColor);
                }
            }

            // Show threatened pieces
            if (showThreatenedPiecesToolStripMenuItem.Checked)
            {
                if (game.GetThreatenedPieces().Contains(hexIndex))
                {
                    tileBrush = new SolidBrush(tileThreatenedPiece);
                }
            }

            // Highlight valid moves
            if (highlightValidMovesToolStripMenuItem.Checked && game.PlayerToMove == (computerPlaysBlueToolStripMenuItem.Checked ? Player.Red : Player.Blue))
            {
                List<Move> moves = game.GetAllMoves();
                if (moves.Count > 0 && moves[0].Increment > 0 && moves[0].FromIndex == -1)
                {
                    // Add points after a capture
                    foreach (Move move in moves)
                    {
                        if (move.ToIndex == hexIndex)
                        {
                            value += "+" + move.Increment;
                            tileBrush = new SolidBrush(tileAvailableMove);
                        }
                    }
                }
                else if (selectedFromTile >= 0)
                {
                    // Find movement moves
                    foreach (Move move in moves)
                    {
                        if (move.FromIndex == selectedFromTile && hexIndex == move.ToIndex)
                        {
                            tileBrush = new SolidBrush(tileAvailableMove);
                        }
                        if (move.FromIndex == selectedFromTile && hexIndex == move.FromIndex)
                        {
                            tileBrush = new SolidBrush(tileSelectedPiece);
                        }
                    }
                }
            }

            // Change the color if the mouse is hovering over the tile
            if (hover && !addPieceMode)
            {
                Color newColor = Color.FromArgb(128, tileBrush.Color.R, tileBrush.Color.G, tileBrush.Color.B);
                tileBrush = new SolidBrush(newColor);
            }

            // Draw the hex tile
            g.FillPath(tileBrush, tiles[hexIndex].Path);

            // Draw the tile label
            if (showTileLabelsToolStripMenuItem.Checked)
            {
                string tileLabel = Engine.Move.GetTileFromIndex(hexIndex);
                if (tileLabel != "CH")
                {
                    Font font = new Font("Tahoma", 8f, FontStyle.Regular);
                    SizeF size = g.MeasureString(tileLabel, font);
                    RectangleF location = new RectangleF(new PointF(tiles[hexIndex].BoundingBox.X + tiles[hexIndex].BoundingBox.Width / 2 - size.Width / 2, tiles[hexIndex].BoundingBox.Y + 15), size);
                    g.DrawString(tileLabel, font, labelBrush, location);
                }
            }

            // Draw the piece value
            DrawTileText(g, hexIndex, tileTextBrush, value);
        }

        private void DrawTileText(Graphics g, int tileIndex, SolidBrush textBrush, string text)
        {
            Font cancelFont = new Font("Tahoma", 16.0f, FontStyle.Bold);
            SizeF cancelSize = g.MeasureString(text, cancelFont);
            RectangleF cancelLocation = new RectangleF(new PointF(tiles[tileIndex].BoundingBox.X + tiles[tileIndex].BoundingBox.Width / 2 - cancelSize.Width / 2, tiles[tileIndex].BoundingBox.Y + tiles[tileIndex].BoundingBox.Height / 2 - cancelSize.Height / 2), cancelSize);
            g.DrawString(text, cancelFont, textBrush, cancelLocation);
        }

        private void chasePanel_Click(object sender, EventArgs e)
        {
            // Get the index of the tile that was clicked
            PointF mouseLocation = chasePanel.PointToClient(Cursor.Position);
            int hexIndex = -1;
            for (int i = 0; i < 81; i++)
            {
                if (tiles[i].IsPointInHex(mouseLocation))
                {
                    hexIndex = i; 
                    break;
                }
            }

            if (hexIndex >= 0)
            {
                if (addPieceMode)
                {
                    ClickDialog(hexIndex);
                }
                else
                {
                    ClickTile(hexIndex);
                }
            }
        }

        private void ClickDialog(int hexIndex)
        {
            int chIndex = 40;
            int[] options = new int[]
            {
                chIndex - 8,
                chIndex + 1,
                chIndex + 10,
                chIndex + 9,
                chIndex - 1
            };
            int cancel = chIndex - 9;
            
            int amount = options.ToList().IndexOf(hexIndex) + 1;
            if (amount > 0)
            {
                List<Move> moves = game.GetAllMoves().Where(x => x.FromIndex == selectedFromTile && x.ToIndex == selectedToTile && x.Increment == amount).ToList();
                if (moves.Count > 0)
                {
                    Move move = moves.First();
                    MakeMove(move);
                }

                addPanel.Visible = false;
                addPieceMode = false;
            }

            // User clicked cancel, so close dialog
            if (hexIndex == cancel)
            {
                addPanel.Visible = false;
                addPieceMode = false;
            }
        }
    }
}
