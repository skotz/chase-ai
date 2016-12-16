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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.gamePanel = new System.Windows.Forms.Panel();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selfPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightValidMovesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computerPlaysBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoLabel = new System.Windows.Forms.Label();
            this.csnPanel = new System.Windows.Forms.Panel();
            this.csnInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.loadPositionFromCSNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gamePanel.SuspendLayout();
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
            this.menuStrip1.SuspendLayout();
            this.csnPanel.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.infoLabel);
            this.splitContainer1.Size = new System.Drawing.Size(1095, 662);
            this.splitContainer1.SplitterDistance = 778;
            this.splitContainer1.TabIndex = 8;
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
            this.toolStripSeparator1,
            this.loadPositionFromCSNToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.newGameToolStripMenuItem.Text = "&New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // selfPlayToolStripMenuItem
            // 
            this.selfPlayToolStripMenuItem.Name = "selfPlayToolStripMenuItem";
            this.selfPlayToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.selfPlayToolStripMenuItem.Text = "Computer Self &Play";
            this.selfPlayToolStripMenuItem.Click += new System.EventHandler(this.selfPlayToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highlightValidMovesToolStripMenuItem,
            this.computerPlaysBlueToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // highlightValidMovesToolStripMenuItem
            // 
            this.highlightValidMovesToolStripMenuItem.Checked = true;
            this.highlightValidMovesToolStripMenuItem.CheckOnClick = true;
            this.highlightValidMovesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.highlightValidMovesToolStripMenuItem.Name = "highlightValidMovesToolStripMenuItem";
            this.highlightValidMovesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.highlightValidMovesToolStripMenuItem.Text = "&Highlight Valid Moves";
            // 
            // computerPlaysBlueToolStripMenuItem
            // 
            this.computerPlaysBlueToolStripMenuItem.Checked = true;
            this.computerPlaysBlueToolStripMenuItem.CheckOnClick = true;
            this.computerPlaysBlueToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.computerPlaysBlueToolStripMenuItem.Name = "computerPlaysBlueToolStripMenuItem";
            this.computerPlaysBlueToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.computerPlaysBlueToolStripMenuItem.Text = "Computer Plays Blue";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "&Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(3, 3);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(128, 24);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Ready to play!";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // csnInput
            // 
            this.csnInput.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csnInput.Location = new System.Drawing.Point(20, 49);
            this.csnInput.Name = "csnInput";
            this.csnInput.Size = new System.Drawing.Size(630, 30);
            this.csnInput.TabIndex = 0;
            this.csnInput.TextChanged += new System.EventHandler(this.csnInput_TextChanged);
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
            // loadPositionFromCSNToolStripMenuItem
            // 
            this.loadPositionFromCSNToolStripMenuItem.Name = "loadPositionFromCSNToolStripMenuItem";
            this.loadPositionFromCSNToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.loadPositionFromCSNToolStripMenuItem.Text = "&Load Position from CSN";
            this.loadPositionFromCSNToolStripMenuItem.Click += new System.EventHandler(this.loadPositionFromCSNToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
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
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.csnPanel.ResumeLayout(false);
            this.csnPanel.PerformLayout();
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
    }
}

