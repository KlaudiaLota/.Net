namespace PhotoFiltersForm
{
    public partial class Form1 : Form
    {
        private PictureBox originalPictureBox;
        private FlowLayoutPanel resultPanel;
        private Button loadButton;
        public Form1()
        {
            Text = "Image Filter Parallel Demo";
            Width = 1000;
            Height = 600;

            originalPictureBox = new PictureBox
            {
                Width = 300,
                Height = 300,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            resultPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 300,
                AutoScroll = true
            };

            loadButton = new Button
            {
                Text = "Load Image & Apply Filters",
                Dock = DockStyle.Top
            };

            loadButton.Click += LoadButton_Click;

            Controls.Add(loadButton);
            Controls.Add(originalPictureBox);
            Controls.Add(resultPanel);
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap original = new Bitmap(ofd.FileName);
                originalPictureBox.Image = original;

                resultPanel.Controls.Clear();

                var results = await Task.Run(() => ApplyFiltersInParallel(original));

                foreach (var (name, img) in results)
                {
                    resultPanel.Controls.Add(new Label { Text = name, AutoSize = true });
                    resultPanel.Controls.Add(new PictureBox
                    {
                        Width = 200,
                        Height = 200,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = img
                    });
                }
            }
        }

        private Dictionary<string, Bitmap> ApplyFiltersInParallel(Bitmap original)
        {
            var results = new Dictionary<string, Bitmap>();

            Parallel.Invoke(
                () => results["Grayscale"] = ImageFilters.ToGrayscale(original),
                () => results["Negative"] = ImageFilters.ToNegative(original),
                () => results["Mirror"] = ImageFilters.ToMirror(original),
                () => results["Threshold"] = ImageFilters.ToThreshold(original)
            );

            return results;
        }
    }
}
