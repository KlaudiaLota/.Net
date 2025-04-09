namespace PhotoFiltersForm
{
    partial class Form1
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
            originalPictureBox = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            resultTable = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            SuspendLayout();
            // 
            // originalPictureBox
            // 
            originalPictureBox.BackColor = Color.SeaShell;
            originalPictureBox.Location = new Point(29, 50);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new Size(475, 475);
            originalPictureBox.TabIndex = 0;
            originalPictureBox.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Black", 11F);
            label1.Location = new Point(205, 20);
            label1.Name = "label1";
            label1.Size = new Size(100, 27);
            label1.TabIndex = 2;
            label1.Text = "Oryginał";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Black", 11F);
            label2.Location = new Point(655, 20);
            label2.Name = "label2";
            label2.Size = new Size(218, 27);
            label2.TabIndex = 3;
            label2.Text = "Przetworzony obraz";
            // 
            // button1
            // 
            button1.BackColor = Color.Tan;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Arial Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.Location = new Point(182, 554);
            button1.Name = "button1";
            button1.Size = new Size(150, 49);
            button1.TabIndex = 4;
            button1.Text = "Załaduj obraz";
            button1.UseVisualStyleBackColor = false;
            button1.Click += LoadImageButton_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Tan;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Arial Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button2.Location = new Point(678, 554);
            button2.Name = "button2";
            button2.Size = new Size(195, 49);
            button2.TabIndex = 5;
            button2.Text = "Przetwórz obraz";
            button2.UseVisualStyleBackColor = false;
            button2.Click += ApplyFiltersButton_Click;
            // 
            // resultTable
            // 
            resultTable.AutoScroll = true;
            resultTable.AutoSize = true;
            resultTable.BackColor = Color.SeaShell;
            resultTable.ColumnCount = 2;
            resultTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            resultTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            resultTable.Location = new Point(535, 50);
            resultTable.Name = "resultTable";
            resultTable.RowCount = 2;
            resultTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            resultTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            resultTable.Size = new Size(475, 475);
            resultTable.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Bisque;
            ClientSize = new Size(1042, 653);
            Controls.Add(resultTable);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(originalPictureBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox originalPictureBox;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private TableLayoutPanel resultTable;
    }
}
