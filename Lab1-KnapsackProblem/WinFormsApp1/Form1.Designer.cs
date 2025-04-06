namespace WinFormsApp1
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
            solveButton = new Button();
            textBoxSeed = new TextBox();
            textBoxItemCount = new TextBox();
            textBoxCapacity = new TextBox();
            labelSeed = new Label();
            labelItemCount = new Label();
            labelCapacity = new Label();
            richTextBoxResult = new RichTextBox();
            richTextBoxItems = new RichTextBox();
            labelSeedError = new Label();
            labelItemCountError = new Label();
            labelCapacityError = new Label();
            SuspendLayout();
            // 
            // solveButton
            // 
            solveButton.Location = new Point(77, 297);
            solveButton.Name = "solveButton";
            solveButton.Size = new Size(125, 29);
            solveButton.TabIndex = 0;
            solveButton.Text = "Solve problem";
            solveButton.UseVisualStyleBackColor = true;
            solveButton.Click += button_Click;
            // 
            // textBoxSeed
            // 
            textBoxSeed.Location = new Point(77, 64);
            textBoxSeed.Name = "textBoxSeed";
            textBoxSeed.Size = new Size(125, 27);
            textBoxSeed.TabIndex = 1;
            textBoxSeed.TextChanged += textBoxSeed_TextChanged;
            // 
            // textBoxItemCount
            // 
            textBoxItemCount.Location = new Point(77, 142);
            textBoxItemCount.Name = "textBoxItemCount";
            textBoxItemCount.Size = new Size(125, 27);
            textBoxItemCount.TabIndex = 2;
            textBoxItemCount.TextChanged += textBoxItemCount_TextChanged;
            // 
            // textBoxCapacity
            // 
            textBoxCapacity.Location = new Point(77, 223);
            textBoxCapacity.Name = "textBoxCapacity";
            textBoxCapacity.Size = new Size(125, 27);
            textBoxCapacity.TabIndex = 3;
            textBoxCapacity.TextChanged += textBoxCapacity_TextChanged;
            // 
            // labelSeed
            // 
            labelSeed.AutoSize = true;
            labelSeed.Location = new Point(77, 41);
            labelSeed.Name = "labelSeed";
            labelSeed.Size = new Size(106, 20);
            labelSeed.TabIndex = 4;
            labelSeed.Text = "Enter the seed:";
            // 
            // labelItemCount
            // 
            labelItemCount.AutoSize = true;
            labelItemCount.Location = new Point(77, 119);
            labelItemCount.Name = "labelItemCount";
            labelItemCount.Size = new Size(184, 20);
            labelItemCount.TabIndex = 5;
            labelItemCount.Text = "Enter the number of items:";
            // 
            // labelCapacity
            // 
            labelCapacity.AutoSize = true;
            labelCapacity.Location = new Point(77, 200);
            labelCapacity.Name = "labelCapacity";
            labelCapacity.Size = new Size(130, 20);
            labelCapacity.TabIndex = 6;
            labelCapacity.Text = "Enter the capacity:";
            // 
            // richTextBoxResult
            // 
            richTextBoxResult.Location = new Point(77, 362);
            richTextBoxResult.Name = "richTextBoxResult";
            richTextBoxResult.ReadOnly = true;
            richTextBoxResult.Size = new Size(570, 145);
            richTextBoxResult.TabIndex = 8;
            richTextBoxResult.Text = "";
            // 
            // richTextBoxItems
            // 
            richTextBoxItems.Location = new Point(304, 41);
            richTextBoxItems.Name = "richTextBoxItems";
            richTextBoxItems.ReadOnly = true;
            richTextBoxItems.Size = new Size(343, 246);
            richTextBoxItems.TabIndex = 9;
            richTextBoxItems.Text = "";
            // 
            // labelSeedError
            // 
            labelSeedError.AutoSize = true;
            labelSeedError.ForeColor = Color.Red;
            labelSeedError.Location = new Point(77, 94);
            labelSeedError.Name = "labelSeedError";
            labelSeedError.Size = new Size(50, 20);
            labelSeedError.TabIndex = 10;
            labelSeedError.Text = "label4";
            labelSeedError.Visible = false;
            // 
            // labelItemCountError
            // 
            labelItemCountError.AutoSize = true;
            labelItemCountError.ForeColor = Color.Red;
            labelItemCountError.Location = new Point(77, 172);
            labelItemCountError.Name = "labelItemCountError";
            labelItemCountError.Size = new Size(50, 20);
            labelItemCountError.TabIndex = 11;
            labelItemCountError.Text = "label5";
            labelItemCountError.Visible = false;
            // 
            // labelCapacityError
            // 
            labelCapacityError.AutoSize = true;
            labelCapacityError.ForeColor = Color.Red;
            labelCapacityError.Location = new Point(77, 253);
            labelCapacityError.Name = "labelCapacityError";
            labelCapacityError.Size = new Size(50, 20);
            labelCapacityError.TabIndex = 12;
            labelCapacityError.Text = "label6";
            labelCapacityError.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 592);
            Controls.Add(labelCapacityError);
            Controls.Add(labelItemCountError);
            Controls.Add(labelSeedError);
            Controls.Add(richTextBoxItems);
            Controls.Add(richTextBoxResult);
            Controls.Add(labelCapacity);
            Controls.Add(labelItemCount);
            Controls.Add(labelSeed);
            Controls.Add(textBoxCapacity);
            Controls.Add(textBoxItemCount);
            Controls.Add(textBoxSeed);
            Controls.Add(solveButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button solveButton;
        private TextBox textBoxSeed;
        private TextBox textBoxItemCount;
        private TextBox textBoxCapacity;
        private Label labelSeed;
        private Label labelItemCount;
        private Label labelCapacity;
        private RichTextBox richTextBoxResult;
        private RichTextBox richTextBoxItems;
        private Label labelSeedError;
        private Label labelItemCountError;
        private Label labelCapacityError;
    }
}
