namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;

    internal class Filtration
    {
        public static Bitmap Linear(Bitmap img, int r1, int r2, double[,] matrix)
        {
            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(img);
            Color pix;

            for (int i = 0 + r1; i < h - r1; ++i)
            {
                for (int j = 0 + r2; j < w - r2; ++j)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;

                    for (int a = i - r1; a <= i + r1; a++)
                    {
                        for (int b = j - r2; b <= j + r2; b++)
                        {
                            pix = img.GetPixel(b, a);
                            int indexi = a - i + r1;
                            int indexj = b - j + r2;
                            double matrixValue = matrix[indexi, indexj];

                            sumR += pix.R * matrixValue;
                            sumG += pix.G * matrixValue;
                            sumB += pix.B * matrixValue;
                        }
                    }

                    pix = Color.FromArgb(Clamp(Convert.ToInt32(sumR), 0, 255), Clamp(Convert.ToInt32(sumG), 0, 255), Clamp(Convert.ToInt32(sumB), 0, 255));
                    img_out.SetPixel(j, i, pix);
                }
            }

            return img_out;
        }

        public static Bitmap Median(Bitmap img, int r1, int r2)
        {
            return img;
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }
            else if (val.CompareTo(max) > 0)
            {
                return max;
            }
            else
            {
                return val;
            }
        }
    }
}
