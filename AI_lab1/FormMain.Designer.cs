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
            buttonSound = new Button();
            textBoxIterations = new TextBox();
            textBoxStates = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxMemory = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            radioButtonBFS = new RadioButton();
            radioButtonIDS = new RadioButton();
            radioButtonAStar = new RadioButton();
            checkBoxSaveState = new CheckBox();
            comboBoxHeuristic = new ComboBox();
            numericUpDownSMA = new NumericUpDown();
            label4 = new Label();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSMA).BeginInit();
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
            buttonStart.BackColor = Color.Lime;
            buttonStart.Location = new Point(618, 405);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Старт";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonReset
            // 
            buttonReset.BackColor = Color.Gold;
            buttonReset.Location = new Point(537, 405);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 5;
            buttonReset.Text = "Сброс";
            buttonReset.UseVisualStyleBackColor = false;
            buttonReset.Click += buttonReset_Click;
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(radioButtonBFS);
            flowLayoutPanel1.Controls.Add(radioButtonIDS);
            flowLayoutPanel1.Controls.Add(radioButtonAStar);
            flowLayoutPanel1.Location = new Point(548, 372);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(145, 27);
            flowLayoutPanel1.TabIndex = 17;
            // 
            // radioButtonBFS
            // 
            radioButtonBFS.AutoSize = true;
            radioButtonBFS.Location = new Point(3, 3);
            radioButtonBFS.Name = "radioButtonBFS";
            radioButtonBFS.Size = new Size(44, 19);
            radioButtonBFS.TabIndex = 0;
            radioButtonBFS.TabStop = true;
            radioButtonBFS.Text = "BFS";
            radioButtonBFS.UseVisualStyleBackColor = true;
            // 
            // radioButtonIDS
            // 
            radioButtonIDS.AutoSize = true;
            radioButtonIDS.Location = new Point(53, 3);
            radioButtonIDS.Name = "radioButtonIDS";
            radioButtonIDS.Size = new Size(42, 19);
            radioButtonIDS.TabIndex = 2;
            radioButtonIDS.TabStop = true;
            radioButtonIDS.Text = "IDS";
            radioButtonIDS.UseVisualStyleBackColor = true;
            // 
            // radioButtonAStar
            // 
            radioButtonAStar.AutoSize = true;
            radioButtonAStar.Location = new Point(101, 3);
            radioButtonAStar.Name = "radioButtonAStar";
            radioButtonAStar.Size = new Size(38, 19);
            radioButtonAStar.TabIndex = 4;
            radioButtonAStar.TabStop = true;
            radioButtonAStar.Text = "A*";
            radioButtonAStar.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveState
            // 
            checkBoxSaveState.AutoSize = true;
            checkBoxSaveState.Location = new Point(548, 347);
            checkBoxSaveState.Name = "checkBoxSaveState";
            checkBoxSaveState.Size = new Size(145, 19);
            checkBoxSaveState.TabIndex = 18;
            checkBoxSaveState.Text = "Сохранить состояние";
            checkBoxSaveState.UseVisualStyleBackColor = true;
            // 
            // comboBoxHeuristic
            // 
            comboBoxHeuristic.FormattingEnabled = true;
            comboBoxHeuristic.Location = new Point(456, 318);
            comboBoxHeuristic.Name = "comboBoxHeuristic";
            comboBoxHeuristic.Size = new Size(237, 23);
            comboBoxHeuristic.TabIndex = 19;
            // 
            // numericUpDownSMA
            // 
            numericUpDownSMA.Location = new Point(648, 136);
            numericUpDownSMA.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSMA.Name = "numericUpDownSMA";
            numericUpDownSMA.Size = new Size(45, 23);
            numericUpDownSMA.TabIndex = 20;
            numericUpDownSMA.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(456, 144);
            label4.Name = "label4";
            label4.Size = new Size(136, 15);
            label4.TabIndex = 21;
            label4.Text = "Ограничение для SMA*";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            BackgroundImage = Properties.Resources.attila_backimage;
            ClientSize = new Size(705, 440);
            Controls.Add(label4);
            Controls.Add(numericUpDownSMA);
            Controls.Add(comboBoxHeuristic);
            Controls.Add(checkBoxSaveState);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(textBoxMemory);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxStates);
            Controls.Add(textBoxIterations);
            Controls.Add(buttonSound);
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
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSMA).EndInit();
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
        private Button buttonSound;
        private TextBox textBoxIterations;
        private TextBox textBoxStates;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxMemory;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton radioButtonBFS;
        private RadioButton radioButtonIDS;
        private CheckBox checkBoxSaveState;
        private RadioButton radioButtonAStar;
        private ComboBox comboBoxHeuristic;
        private NumericUpDown numericUpDownSMA;
        private Label label4;
    }
}
