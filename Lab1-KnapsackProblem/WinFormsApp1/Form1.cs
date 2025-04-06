using System.Windows.Forms;
using KnapsackProblem;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSeed.Text) || string.IsNullOrWhiteSpace(textBoxItemCount.Text) || string.IsNullOrWhiteSpace(textBoxCapacity.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            else if (textBoxSeed.BackColor == Color.LightCoral || textBoxItemCount.BackColor == Color.LightCoral || textBoxCapacity.BackColor == Color.LightCoral)
            {
                MessageBox.Show("Please enter valid input.");
                return;
            }
            int seed = int.Parse(textBoxSeed.Text);
            int itemCount = int.Parse(textBoxItemCount.Text);
            int capacity = int.Parse(textBoxCapacity.Text);

            Problem problem = new Problem(itemCount, seed);

            richTextBoxItems.Clear();
            foreach (var item in problem.GetItems())
            {
                richTextBoxItems.AppendText(item.ToString() + Environment.NewLine);
            }

            Result result = problem.Solve(capacity);

            richTextBoxResult.Text = result.ToString();
        }

        private void ValidateTextBox(TextBox textBox, Label errorLabel)
        {
            if (!int.TryParse(textBox.Text, out int value) || value < 0)
            {
                textBox.BackColor = Color.LightCoral;
                errorLabel.Text = "Invalid input! Enter a positive number.";
                errorLabel.Visible = true;
            }
            else
            {
                textBox.BackColor = Color.White;
                errorLabel.Visible = false;
            }
        }
        private void textBoxSeed_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(textBoxSeed, labelSeedError);
        }

        private void textBoxItemCount_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(textBoxItemCount, labelItemCountError);
        }

        private void textBoxCapacity_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(textBoxCapacity, labelCapacityError);
        }
    }
}
