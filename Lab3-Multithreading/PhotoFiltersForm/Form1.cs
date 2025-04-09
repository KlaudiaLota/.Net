using System;

namespace PhotoFiltersForm
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage;
        public Form1()
        {
            InitializeComponent();
            originalPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(ofd.FileName);
                originalPictureBox.Image = originalImage;
                resultTable.Controls.Clear();
            }
        }

        private async void ApplyFiltersButton_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Najpierw za³aduj obraz.");
                return;
            }

            Bitmap copyForGrayscale = new Bitmap(originalImage);
            Bitmap copyForNegative = new Bitmap(originalImage);
            Bitmap copyForMirror = new Bitmap(originalImage);
            Bitmap copyForThreshold = new Bitmap(originalImage);

            resultTable.Controls.Clear();

            var results = await Task.Run(() =>
                ApplyFiltersInParallel(copyForGrayscale, copyForNegative, copyForMirror, copyForThreshold));

            int row = 0, col = 0;

            foreach (var (name, img) in results)
            {
                var container = new Panel
                {
                    Width = 220,
                    Height = 225,
                    Margin = new Padding(10)
                };

                var pictureBox = new PictureBox
                {
                    Image = img,
                    Width = 200,
                    Height = 200,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Top
                };

                var label = new Label
                {
                    Text = name,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Bottom,
                    Height = 30
                };

                container.Controls.Add(pictureBox);
                container.Controls.Add(label);

                resultTable.Controls.Add(container, col, row);

                col++;
                if (col >= 2)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private Dictionary<string, Bitmap> ApplyFiltersInParallel(Bitmap grayscaleCopy, Bitmap negativeCopy, Bitmap mirrorCopy, Bitmap thresholdCopy)
        {
            var results = new Dictionary<string, Bitmap>();

            Parallel.Invoke(
                () => results["Grayscale"] = ImageFilters.ToGrayscale(grayscaleCopy),
                () => results["Negative"] = ImageFilters.ToNegative(negativeCopy),
                () => results["Mirror"] = ImageFilters.ToMirror(mirrorCopy),
                () => results["Threshold"] = ImageFilters.ToThreshold(thresholdCopy)
            );

            return results;
        }

    }
}
