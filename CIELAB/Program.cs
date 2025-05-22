using System.Drawing;
using ColorMine.ColorSpaces;

namespace CIELAB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "photos";
            string[] files = Directory.GetFiles(path);

            Color[] colors = new Color[files.Length];
            Parallel.For(0, colors.Length, i => {
                colors[i] = GetAverageColorFromBitmap(files[i]);
            });

            Console.WriteLine($"All colors: {colors.Length}");

            for (int i = 0; i < colors.Length; i++) {
                Console.WriteLine($"{colors[i].R} {colors[i].G} {colors[i].B}");
            }

            Console.WriteLine($"Average color:");
            Color averageColor = GetAverageColorFromArray(colors);
            Console.WriteLine($"{averageColor.R} {averageColor.G} {averageColor.B}");

            Console.ReadLine();
        }

        private static Color GetAverageColorFromBitmap(string path) {
            long r = 0, g = 0, b = 0;
            double d = 0;

            using (Bitmap bitmap = new Bitmap(path))
            {
                for (int y = 0; y < bitmap.Height; y++) {
                    for (int x = 0; x < bitmap.Width; x++) {
                        Color pixelColor = bitmap.GetPixel(x, y);

                        r += pixelColor.R;
                        g += pixelColor.G;
                        b += pixelColor.B;
                    }
                }

                d = bitmap.Width * bitmap.Height;
            }

            return Color.FromArgb((int)Math.Round(r / d), (int)Math.Round(g / d), (int)Math.Round(b / d));
        }

        private static Color GetAverageColorFromArray(Color[] array)
        {
            long r = 0, g = 0, b = 0;
            double d = array.Length;

            for (int i = 0; i < array.Length; i++)
            {
                Color color = array[i];

                r += color.R;
                g += color.G;
                b += color.B;
            }

            return Color.FromArgb((int)Math.Round(r / d), (int)Math.Round(g / d), (int)Math.Round(b / d));
        }

        private static double GetCieLabAFromRgb(int red, int green, int blue)
        {
            var rgb = new Rgb { R = red, G = green, B = blue };
            var lab = rgb.To<Lab>();
            return lab.A;
        }
    }
}