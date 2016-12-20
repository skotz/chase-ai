namespace Chase.GUI
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.gamePanel = new System.Windows.Forms.Panel();
            this.csnPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.csnInput = new System.Windows.Forms.TextBox();
            this.addPanel = new System.Windows.Forms.Panel();
            this.addNone = new System.Windows.Forms.Button();
            this.add5 = new System.Windows.Forms.Button();
            this.add4 = new System.Windows.Forms.Button();
            this.add3 = new System.Windows.Forms.Button();
            this.add2 = new System.Windows.Forms.Button();
            this.add1 = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.searchStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.handBlue = new System.Windows.Forms.Label();
            this.handRed = new System.Windows.Forms.Label();
            this.analysisLabel = new System.Windows.Forms.Label();
            this.previousMove = new System.Windows.Forms.Button();
            this.nextMove = new System.Windows.Forms.Button();
            this.moveHistory = new System.Windows.Forms.DataGridView();
            this.MoveNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.player1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.player2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selfPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoMovesDeep = new System.Windows.Forms.ToolStripMenuItem();
            this.threeMovesDeep = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveSecondsMove = new System.Windows.Forms.ToolStripMenuItem();
            this.twentySecondsMove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadPositionFromCSNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCSNFromPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameFromCGNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computerPlaysBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.highlightValidMovesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showThreatenedPiecesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showComputerAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTileLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCgn = new System.Windows.Forms.SaveFileDialog();
            this.openCgn = new System.Windows.Forms.OpenFileDialog();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.oneMoveDeep = new System.Windows.Forms.ToolStripMenuItem();
            this.fourMovesDeep = new System.Windows.Forms.ToolStripMenuItem();
            this.sixtySecondsMove = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePanel.SuspendLayout();
            this.csnPanel.SuspendLayout();
            this.addPanel.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveHistory)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.gamePanel.Controls.Add(this.csnPanel);
            this.gamePanel.Controls.Add(this.addPanel);
            this.gamePanel.Location = new System.Drawing.Point(3, 3);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(774, 656);
            this.gamePanel.TabIndex = 7;
            // 
            // csnPanel
            // 
            this.csnPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.csnPanel.Controls.Add(this.button1);
            this.csnPanel.Controls.Add(this.label1);
            this.csnPanel.Controls.Add(this.csnInput);
            this.csnPanel.Location = new System.Drawing.Point(49, 145);
            this.csnPanel.Name = "csnPanel";
            this.csnPanel.Size = new System.Drawing.Size(672, 100);
            this.csnPanel.TabIndex = 1;
            this.csnPanel.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(575, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paste a Chase String Notation position to load:";
            // 
            // csnInput
            // 
            this.csnInput.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csnInput.Location = new System.Drawing.Point(20, 49);
            this.csnInput.Name = "csnInput";
            this.csnInput.Size = new System.Drawing.Size(630, 30);
            this.csnInput.TabIndex = 0;
            this.csnInput.TextChanged += new System.EventHandler(this.csnInput_TextChanged);
            // 
            // addPanel
            // 
            this.addPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addPanel.Controls.Add(this.addNone);
            this.addPanel.Controls.Add(this.add5);
            this.addPanel.Controls.Add(this.add4);
            this.addPanel.Controls.Add(this.add3);
            this.addPanel.Controls.Add(this.add2);
            this.addPanel.Controls.Add(this.add1);
            this.addPanel.Location = new System.Drawing.Point(49, 251);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(672, 156);
            this.addPanel.TabIndex = 0;
            this.addPanel.Visible = false;
            // 
            // addNone
            // 
            this.addNone.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNone.Location = new System.Drawing.Point(550, 29);
            this.addNone.Name = "addNone";
            this.addNone.Size = new System.Drawing.Size(100, 100);
            this.addNone.TabIndex = 1;
            this.addNone.Text = "Cancel";
            this.addNone.UseVisualStyleBackColor = true;
            this.addNone.Click += new System.EventHandler(this.button5_Click);
            // 
            // add5
            // 
            this.add5.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add5.Location = new System.Drawing.Point(444, 29);
            this.add5.Name = "add5";
            this.add5.Size = new System.Drawing.Size(100, 100);
            this.add5.TabIndex = 0;
            this.add5.Text = "+5";
            this.add5.UseVisualStyleBackColor = true;
            this.add5.Click += new System.EventHandler(this.add1_Click);
            // 
            // add4
            // 
            this.add4.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add4.Location = new System.Drawing.Point(338, 29);
            this.add4.Name = "add4";
            this.add4.Size = new System.Drawing.Size(100, 100);
            this.add4.TabIndex = 0;
            this.add4.Text = "+4";
            this.add4.UseVisualStyleBackColor = true;
            this.add4.Click += new System.EventHandler(this.add1_Click);
            // 
            // add3
            // 
            this.add3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add3.Location = new System.Drawing.Point(232, 29);
            this.add3.Name = "add3";
            this.add3.Size = new System.Drawing.Size(100, 100);
            this.add3.TabIndex = 0;
            this.add3.Text = "+3";
            this.add3.UseVisualStyleBackColor = true;
            this.add3.Click += new System.EventHandler(this.add1_Click);
            // 
            // add2
            // 
            this.add2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add2.Location = new System.Drawing.Point(126, 29);
            this.add2.Name = "add2";
            this.add2.Size = new System.Drawing.Size(100, 100);
            this.add2.TabIndex = 0;
            this.add2.Text = "+2";
            this.add2.UseVisualStyleBackColor = true;
            this.add2.Click += new System.EventHandler(this.add1_Click);
            // 
            // add1
            // 
            this.add1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add1.Location = new System.Drawing.Point(20, 29);
            this.add1.Name = "add1";
            this.add1.Size = new System.Drawing.Size(100, 100);
            this.add1.TabIndex = 0;
            this.add1.Text = "+1";
            this.add1.UseVisualStyleBackColor = true;
            this.add1.Click += new System.EventHandler(this.add1_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1095, 662);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1095, 708);
            this.toolStripContainer1.TabIndex = 8;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1095, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            // 
            // searchStatusLabel
            // 
            this.searchStatusLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchStatusLabel.Name = "searchStatusLabel";
            this.searchStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.searchStatusLabel.Text = "Ready";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gamePanel);
            this.splitContainer1.Panel1MinSize = 778;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.analysisLabel);
            this.splitContainer1.Panel2.Controls.Add(this.previousMove);
            this.splitContainer1.Panel2.Controls.Add(this.nextMove);
            this.splitContainer1.Panel2.Controls.Add(this.moveHistory);
            this.splitContainer1.Size = new System.Drawing.Size(1095, 662);
            this.splitContainer1.SplitterDistance = 778;
            this.splitContainer1.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.infoLabel);
            this.groupBox2.Location = new System.Drawing.Point(7, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 61);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(6, 24);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(164, 25);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Ready to play!";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.handBlue);
            this.groupBox1.Controls.Add(this.handRed);
            this.groupBox1.Location = new System.Drawing.Point(203, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 61);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "In Hand";
            // 
            // handBlue
            // 
            this.handBlue.BackColor = System.Drawing.Color.LightSkyBlue;
            this.handBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.handBlue.Location = new System.Drawing.Point(52, 16);
            this.handBlue.Name = "handBlue";
            this.handBlue.Size = new System.Drawing.Size(40, 40);
            this.handBlue.TabIndex = 6;
            this.handBlue.Text = "4";
            this.handBlue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // handRed
            // 
            this.handRed.BackColor = System.Drawing.Color.LightCoral;
            this.handRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.handRed.Location = new System.Drawing.Point(6, 16);
            this.handRed.Name = "handRed";
            this.handRed.Size = new System.Drawing.Size(40, 40);
            this.handRed.TabIndex = 6;
            this.handRed.Text = "4";
            this.handRed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // analysisLabel
            // 
            this.analysisLabel.Location = new System.Drawing.Point(88, 636);
            this.analysisLabel.Name = "analysisLabel";
            this.analysisLabel.Size = new System.Drawing.Size(132, 23);
            this.analysisLabel.TabIndex = 4;
            this.analysisLabel.Text = "CGN";
            this.analysisLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // previousMove
            // 
            this.previousMove.Location = new System.Drawing.Point(7, 636);
            this.previousMove.Name = "previousMove";
            this.previousMove.Size = new System.Drawing.Size(75, 23);
            this.previousMove.TabIndex = 3;
            this.previousMove.Text = "<";
            this.previousMove.UseVisualStyleBackColor = true;
            this.previousMove.Click += new System.EventHandler(this.previousMove_Click);
            // 
            // nextMove
            // 
            this.nextMove.Location = new System.Drawing.Point(226, 636);
            this.nextMove.Name = "nextMove";
            this.nextMove.Size = new System.Drawing.Size(75, 23);
            this.nextMove.TabIndex = 2;
            this.nextMove.Text = ">";
            this.nextMove.UseVisualStyleBackColor = true;
            this.nextMove.Click += new System.EventHandler(this.nextMove_Click);
            // 
            // moveHistory
            // 
            this.moveHistory.AllowUserToAddRows = false;
            this.moveHistory.AllowUserToDeleteRows = false;
            this.moveHistory.AllowUserToResizeColumns = false;
            this.moveHistory.AllowUserToResizeRows = false;
            this.moveHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moveHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MoveNumber,
            this.player1,
            this.player2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.moveHistory.DefaultCellStyle = dataGridViewCellStyle1;
            this.moveHistory.Location = new System.Drawing.Point(7, 70);
            this.moveHistory.Name = "moveHistory";
            this.moveHistory.ReadOnly = true;
            this.moveHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.moveHistory.RowHeadersVisible = false;
            this.moveHistory.Size = new System.Drawing.Size(294, 560);
            this.moveHistory.TabIndex = 1;
            // 
            // MoveNumber
            // 
            this.MoveNumber.DataPropertyName = "Number";
            this.MoveNumber.HeaderText = "#";
            this.MoveNumber.Name = "MoveNumber";
            this.MoveNumber.ReadOnly = true;
            this.MoveNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MoveNumber.Width = 50;
            // 
            // player1
            // 
            this.player1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.player1.DataPropertyName = "RedMove";
            this.player1.HeaderText = "Red";
            this.player1.Name = "player1";
            this.player1.ReadOnly = true;
            this.player1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // player2
            // 
            this.player2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.player2.DataPropertyName = "BlueMove";
            this.player2.HeaderText = "Blue";
            this.player2.Name = "player2";
            this.player2.ReadOnly = true;
            this.player2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1095, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.selfPlayToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadPositionFromCSNToolStripMenuItem,
            this.copyCSNFromPositionToolStripMenuItem,
            this.toolStripSeparator4,
            this.saveGameToolStripMenuItem,
            this.loadGameFromCGNToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.newGameToolStripMenuItem.Text = "&New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // selfPlayToolStripMenuItem
            // 
            this.selfPlayToolStripMenuItem.Name = "selfPlayToolStripMenuItem";
            this.selfPlayToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.selfPlayToolStripMenuItem.Text = "Computer Self &Play";
            this.selfPlayToolStripMenuItem.Click += new System.EventHandler(this.selfPlayToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneMoveDeep,
            this.twoMovesDeep,
            this.threeMovesDeep,
            this.fourMovesDeep,
            this.toolStripSeparator5,
            this.fiveSecondsMove,
            this.twentySecondsMove,
            this.sixtySecondsMove});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // twoMovesDeep
            // 
            this.twoMovesDeep.Checked = true;
            this.twoMovesDeep.CheckOnClick = true;
            this.twoMovesDeep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.twoMovesDeep.Name = "twoMovesDeep";
            this.twoMovesDeep.Size = new System.Drawing.Size(168, 22);
            this.twoMovesDeep.Text = "2 Moves Deep";
            this.twoMovesDeep.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // threeMovesDeep
            // 
            this.threeMovesDeep.CheckOnClick = true;
            this.threeMovesDeep.Name = "threeMovesDeep";
            this.threeMovesDeep.Size = new System.Drawing.Size(168, 22);
            this.threeMovesDeep.Text = "3 Moves Deep";
            this.threeMovesDeep.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // fiveSecondsMove
            // 
            this.fiveSecondsMove.CheckOnClick = true;
            this.fiveSecondsMove.Name = "fiveSecondsMove";
            this.fiveSecondsMove.Size = new System.Drawing.Size(168, 22);
            this.fiveSecondsMove.Text = "5 Seconds/Move";
            this.fiveSecondsMove.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // twentySecondsMove
            // 
            this.twentySecondsMove.CheckOnClick = true;
            this.twentySecondsMove.Name = "twentySecondsMove";
            this.twentySecondsMove.Size = new System.Drawing.Size(168, 22);
            this.twentySecondsMove.Text = "20 Seconds/Move";
            this.twentySecondsMove.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // loadPositionFromCSNToolStripMenuItem
            // 
            this.loadPositionFromCSNToolStripMenuItem.Name = "loadPositionFromCSNToolStripMenuItem";
            this.loadPositionFromCSNToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.loadPositionFromCSNToolStripMenuItem.Text = "&Load Position from CSN";
            this.loadPositionFromCSNToolStripMenuItem.Click += new System.EventHandler(this.loadPositionFromCSNToolStripMenuItem_Click);
            // 
            // copyCSNFromPositionToolStripMenuItem
            // 
            this.copyCSNFromPositionToolStripMenuItem.Name = "copyCSNFromPositionToolStripMenuItem";
            this.copyCSNFromPositionToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyCSNFromPositionToolStripMenuItem.Text = "&Copy CSN from Position";
            this.copyCSNFromPositionToolStripMenuItem.Click += new System.EventHandler(this.copyCSNFromPositionToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.saveGameToolStripMenuItem.Text = "&Save Game as CGN";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.saveGameToolStripMenuItem_Click);
            // 
            // loadGameFromCGNToolStripMenuItem
            // 
            this.loadGameFromCGNToolStripMenuItem.Name = "loadGameFromCGNToolStripMenuItem";
            this.loadGameFromCGNToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.loadGameFromCGNToolStripMenuItem.Text = "Load &Game from CGN";
            this.loadGameFromCGNToolStripMenuItem.Click += new System.EventHandler(this.loadGameFromCGNToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.computerPlaysBlueToolStripMenuItem,
            this.toolStripSeparator2,
            this.highlightValidMovesToolStripMenuItem,
            this.showThreatenedPiecesToolStripMenuItem,
            this.toolStripSeparator3,
            this.showComputerAnalysisToolStripMenuItem,
            this.showTileLabelsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // computerPlaysBlueToolStripMenuItem
            // 
            this.computerPlaysBlueToolStripMenuItem.Checked = true;
            this.computerPlaysBlueToolStripMenuItem.CheckOnClick = true;
            this.computerPlaysBlueToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.computerPlaysBlueToolStripMenuItem.Name = "computerPlaysBlueToolStripMenuItem";
            this.computerPlaysBlueToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.computerPlaysBlueToolStripMenuItem.Text = "Computer Plays Blue";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(220, 6);
            // 
            // highlightValidMovesToolStripMenuItem
            // 
            this.highlightValidMovesToolStripMenuItem.Checked = true;
            this.highlightValidMovesToolStripMenuItem.CheckOnClick = true;
            this.highlightValidMovesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.highlightValidMovesToolStripMenuItem.Name = "highlightValidMovesToolStripMenuItem";
            this.highlightValidMovesToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.highlightValidMovesToolStripMenuItem.Text = "Highlight Valid &Moves";
            // 
            // showThreatenedPiecesToolStripMenuItem
            // 
            this.showThreatenedPiecesToolStripMenuItem.Checked = true;
            this.showThreatenedPiecesToolStripMenuItem.CheckOnClick = true;
            this.showThreatenedPiecesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showThreatenedPiecesToolStripMenuItem.Name = "showThreatenedPiecesToolStripMenuItem";
            this.showThreatenedPiecesToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.showThreatenedPiecesToolStripMenuItem.Text = "Highlight &Threatened Pieces";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(220, 6);
            // 
            // showComputerAnalysisToolStripMenuItem
            // 
            this.showComputerAnalysisToolStripMenuItem.Checked = true;
            this.showComputerAnalysisToolStripMenuItem.CheckOnClick = true;
            this.showComputerAnalysisToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showComputerAnalysisToolStripMenuItem.Name = "showComputerAnalysisToolStripMenuItem";
            this.showComputerAnalysisToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.showComputerAnalysisToolStripMenuItem.Text = "Show Computer &Analysis";
            // 
            // showTileLabelsToolStripMenuItem
            // 
            this.showTileLabelsToolStripMenuItem.Checked = true;
            this.showTileLabelsToolStripMenuItem.CheckOnClick = true;
            this.showTileLabelsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showTileLabelsToolStripMenuItem.Name = "showTileLabelsToolStripMenuItem";
            this.showTileLabelsToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.showTileLabelsToolStripMenuItem.Text = "Show Tile &Labels";
            this.showTileLabelsToolStripMenuItem.Click += new System.EventHandler(this.showTileLabelsToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "&Test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // saveCgn
            // 
            this.saveCgn.DefaultExt = "cgn";
            this.saveCgn.FileName = "game.cgn";
            this.saveCgn.Filter = "Chase Game Notation|*.cgn";
            this.saveCgn.Title = "Save Game";
            // 
            // openCgn
            // 
            this.openCgn.DefaultExt = "cgn";
            this.openCgn.FileName = "game.cgn";
            this.openCgn.Filter = "Chase Game Notation|*.cgn";
            this.openCgn.Title = "Load a saved game";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(165, 6);
            // 
            // oneMoveDeep
            // 
            this.oneMoveDeep.CheckOnClick = true;
            this.oneMoveDeep.Name = "oneMoveDeep";
            this.oneMoveDeep.Size = new System.Drawing.Size(168, 22);
            this.oneMoveDeep.Text = "1 Move Deep";
            this.oneMoveDeep.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // fourMovesDeep
            // 
            this.fourMovesDeep.CheckOnClick = true;
            this.fourMovesDeep.Name = "fourMovesDeep";
            this.fourMovesDeep.Size = new System.Drawing.Size(168, 22);
            this.fourMovesDeep.Text = "4 Moves Deep";
            this.fourMovesDeep.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // sixtySecondsMove
            // 
            this.sixtySecondsMove.CheckOnClick = true;
            this.sixtySecondsMove.Name = "sixtySecondsMove";
            this.sixtySecondsMove.Size = new System.Drawing.Size(168, 22);
            this.sixtySecondsMove.Text = "60 Seconds/Move";
            this.sixtySecondsMove.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 708);
            this.Controls.Add(this.toolStripContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Chase by Scott Clayton";
            this.gamePanel.ResumeLayout(false);
            this.csnPanel.ResumeLayout(false);
            this.csnPanel.PerformLayout();
            this.addPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moveHistory)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel searchStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selfPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightValidMovesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computerPlaysBlueToolStripMenuItem;
        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.Button add5;
        private System.Windows.Forms.Button add4;
        private System.Windows.Forms.Button add3;
        private System.Windows.Forms.Button add2;
        private System.Windows.Forms.Button add1;
        private System.Windows.Forms.Button addNone;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Panel csnPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox csnInput;
        private System.Windows.Forms.ToolStripMenuItem loadPositionFromCSNToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showThreatenedPiecesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCSNFromPositionToolStripMenuItem;
        private System.Windows.Forms.DataGridView moveHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoveNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn player1;
        private System.Windows.Forms.DataGridViewTextBoxColumn player2;
        private System.Windows.Forms.ToolStripMenuItem showComputerAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem showTileLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoMovesDeep;
        private System.Windows.Forms.ToolStripMenuItem threeMovesDeep;
        private System.Windows.Forms.ToolStripMenuItem fiveSecondsMove;
        private System.Windows.Forms.ToolStripMenuItem twentySecondsMove;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveCgn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem loadGameFromCGNToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openCgn;
        private System.Windows.Forms.Button previousMove;
        private System.Windows.Forms.Button nextMove;
        private System.Windows.Forms.Label analysisLabel;
        private System.Windows.Forms.Label handRed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label handBlue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem oneMoveDeep;
        private System.Windows.Forms.ToolStripMenuItem fourMovesDeep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem sixtySecondsMove;
    }
}

