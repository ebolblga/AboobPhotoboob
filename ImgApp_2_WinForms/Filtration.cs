namespace ImgApp_2_WinForms
{
    using System.Drawing;

    internal class Filtration
    {
        public static Bitmap Linear(Bitmap img, int r1, int r2)
        {
            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(img);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix = img.GetPixel(j, i);
                    int sumR = 0;
                    int sumG = 0;
                    int sumB = 0;

                    for (int a = i - r1; a <= i + r1; a++)
                    {
                        for (int b = j - r2; b <= j + r2; b++)
                        {
                            sumR += pix.R;
                            sumG += pix.G;
                            sumB += pix.B;
                        }
                    }

                    int s = ((r1 * 2) + 1) * ((r2 * 2) + 1);
                    pix = Color.FromArgb(sumR / s, sumG / s, sumB / s);
                    img_out.SetPixel(j, i, pix);
                }
            }

            return img_out;
        }

        public static Bitmap Median(Bitmap img, int r1, int r2)
        {
            return img;
        }
    }
}
