namespace AI_lab1
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonKnight = new Button();
            buttonKing = new Button();
            buttonBurn = new Button();
            buttonStart = new Button();
            buttonReset = new Button();
            checkBoxDFS = new CheckBox();
            checkBoxBFS = new CheckBox();
            buttonSound = new Button();
            textBoxIterations = new TextBox();
            textBoxStates = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxMemory = new TextBox();
            checkBoxIterDFS = new CheckBox();
            checkBoxBiSearch = new CheckBox();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Location = new Point(20, 20);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            tableLayoutPanel1.Size = new Size(400, 400);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonKnight
            // 
            buttonKnight.Location = new Point(456, 20);
            buttonKnight.Name = "buttonKnight";
            buttonKnight.Size = new Size(75, 23);
            buttonKnight.TabIndex = 1;
            buttonKnight.Text = "Конь";
            buttonKnight.UseVisualStyleBackColor = true;
            buttonKnight.Click += buttonKnight_Click;
            // 
            // buttonKing
            // 
            buttonKing.Location = new Point(537, 20);
            buttonKing.Name = "buttonKing";
            buttonKing.Size = new Size(75, 23);
            buttonKing.TabIndex = 2;
            buttonKing.Text = "Король";
            buttonKing.UseVisualStyleBackColor = true;
            buttonKing.Click += buttonKing_Click;
            // 
            // buttonBurn
            // 
            buttonBurn.Location = new Point(618, 20);
            buttonBurn.Name = "buttonBurn";
            buttonBurn.Size = new Size(75, 23);
            buttonBurn.TabIndex = 3;
            buttonBurn.Text = "Блок";
            buttonBurn.UseVisualStyleBackColor = true;
            buttonBurn.Click += buttonBurn_Click;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(618, 405);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Старт";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(537, 405);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 5;
            buttonReset.Text = "Сброс";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // checkBoxDFS
            // 
            checkBoxDFS.AutoSize = true;
            checkBoxDFS.Location = new Point(647, 364);
            checkBoxDFS.Name = "checkBoxDFS";
            checkBoxDFS.Size = new Size(46, 19);
            checkBoxDFS.TabIndex = 6;
            checkBoxDFS.Text = "DFS";
            checkBoxDFS.UseVisualStyleBackColor = true;
            // 
            // checkBoxBFS
            // 
            checkBoxBFS.AutoSize = true;
            checkBoxBFS.Location = new Point(548, 364);
            checkBoxBFS.Name = "checkBoxBFS";
            checkBoxBFS.Size = new Size(45, 19);
            checkBoxBFS.TabIndex = 7;
            checkBoxBFS.Text = "BFS";
            checkBoxBFS.UseVisualStyleBackColor = true;
            // 
            // buttonSound
            // 
            buttonSound.Location = new Point(456, 405);
            buttonSound.Name = "buttonSound";
            buttonSound.Size = new Size(75, 23);
            buttonSound.TabIndex = 8;
            buttonSound.Text = "Вкл. звук";
            buttonSound.UseVisualStyleBackColor = true;
            buttonSound.Click += buttonSound_Click;
            // 
            // textBoxIterations
            // 
            textBoxIterations.Location = new Point(648, 49);
            textBoxIterations.Name = "textBoxIterations";
            textBoxIterations.Size = new Size(45, 23);
            textBoxIterations.TabIndex = 9;
            // 
            // textBoxStates
            // 
            textBoxStates.Location = new Point(648, 78);
            textBoxStates.Name = "textBoxStates";
            textBoxStates.Size = new Size(45, 23);
            textBoxStates.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(456, 57);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 11;
            label1.Text = "Кол-во итераций в цикле";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(456, 86);
            label2.Name = "label2";
            label2.Size = new Size(188, 15);
            label2.TabIndex = 12;
            label2.Text = "Кол-во порожденных состояний";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(456, 115);
            label3.Name = "label3";
            label3.Size = new Size(166, 15);
            label3.TabIndex = 13;
            label3.Text = "Макс. кол-во узлов в памяти";
            // 
            // textBoxMemory
            // 
            textBoxMemory.Location = new Point(648, 107);
            textBoxMemory.Name = "textBoxMemory";
            textBoxMemory.Size = new Size(45, 23);
            textBoxMemory.TabIndex = 14;
            // 
            // checkBoxIterDFS
            // 
            checkBoxIterDFS.AutoSize = true;
            checkBoxIterDFS.Location = new Point(598, 364);
            checkBoxIterDFS.Name = "checkBoxIterDFS";
            checkBoxIterDFS.Size = new Size(43, 19);
            checkBoxIterDFS.TabIndex = 15;
            checkBoxIterDFS.Text = "IDS";
            checkBoxIterDFS.UseVisualStyleBackColor = true;
            // 
            // checkBoxBiSearch
            // 
            checkBoxBiSearch.AutoSize = true;
            checkBoxBiSearch.Enabled = false;
            checkBoxBiSearch.Location = new Point(456, 364);
            checkBoxBiSearch.Name = "checkBoxBiSearch";
            checkBoxBiSearch.Size = new Size(86, 19);
            checkBoxBiSearch.TabIndex = 16;
            checkBoxBiSearch.Text = "BiDirectBFS";
            checkBoxBiSearch.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            BackgroundImage = Properties.Resources.attila_backimage;
            ClientSize = new Size(705, 440);
            Controls.Add(checkBoxBiSearch);
            Controls.Add(checkBoxIterDFS);
            Controls.Add(textBoxMemory);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxStates);
            Controls.Add(textBoxIterations);
            Controls.Add(buttonSound);
            Controls.Add(checkBoxBFS);
            Controls.Add(checkBoxDFS);
            Controls.Add(buttonReset);
            Controls.Add(buttonStart);
            Controls.Add(buttonBurn);
            Controls.Add(buttonKing);
            Controls.Add(buttonKnight);
            Controls.Add(tableLayoutPanel1);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Attila's Horse";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonKnight;
        private Button buttonKing;
        private Button buttonBurn;
        private Button buttonStart;
        private Button buttonReset;
        private CheckBox checkBoxDFS;
        private CheckBox checkBoxBFS;
        private Button buttonSound;
        private TextBox textBoxIterations;
        private TextBox textBoxStates;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxMemory;
        private CheckBox checkBoxIterDFS;
        private CheckBox checkBoxBiSearch;
    }
}
