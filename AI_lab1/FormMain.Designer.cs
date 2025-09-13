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
            buttonStart.Location = new Point(618, 355);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Старт";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(618, 397);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 5;
            buttonReset.Text = "Сброс";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(705, 440);
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
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonKnight;
        private Button buttonKing;
        private Button buttonBurn;
        private Button buttonStart;
        private Button buttonReset;
    }
}
