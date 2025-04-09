using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoFiltersForm
{
    internal class ImageFilters
    {
        public static Bitmap ToGrayscale(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;
                    result.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            return result;
        }

        public static Bitmap ToNegative(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }

            return result;
        }

        public static Bitmap ToMirror(Bitmap original)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    result.SetPixel(original.Width - x - 1, y, pixel);
                }
            }

            return result;
        }

        public static Bitmap ToThreshold(Bitmap original, int threshold = 128)
        {
            Bitmap result = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int brightness = (pixel.R + pixel.G + pixel.B) / 3;
                    int val = brightness >= threshold ? 255 : 0;
                    result.SetPixel(x, y, Color.FromArgb(val, val, val));
                }
            }

            return result;
        }
    }
}
