namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;

    class ASCII
    {
        public static Bitmap Display(Bitmap img)
        {
            int w = Convert.ToInt32((float)img.Width / 16);
            int h = Convert.ToInt32((float)img.Height / 20);
            Bitmap img_sized = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(img_sized))
            {
                g.DrawImage(img, 0, 0, w, h);
            }

            Bitmap img_out = new Bitmap(img.Width, img.Height);

            char[,] ascii = new char[h, w];

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix = img_sized.GetPixel(j, i);
                    float brightness = Color.FromArgb(pix.R, pix.G, pix.B).GetBrightness();

                    if (brightness >= 0.666)
                    {
                        ascii[i, j] = '▓';
                    }
                    else if (brightness >= 0.333)
                    {
                        ascii[i, j] = '▒';
                    }
                    else
                    {
                        ascii[i, j] = '░';
                    }
                }
            }

            //if (brightness >= 0.875)
            //    ascii[i, j] = '@';
            //else if (brightness >= 0.75)
            //    ascii[i, j] = '%';
            //else if (brightness >= 0.625)
            //    ascii[i, j] = '#';
            //else if (brightness >= 0.5)
            //    ascii[i, j] = '*';
            //else if (brightness >= 0.375)
            //    ascii[i, j] = '+';
            //else if (brightness >= 0.25)
            //    ascii[i, j] = '=';
            //else
            //    ascii[i, j] = '-';

            string shading = "ASCII ART";
            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    shading += ascii[i, j];
                }

                shading += '\t';
            }

            RectangleF rectf = new RectangleF(0, 0, img.Width, img.Height);

            Graphics g2 = Graphics.FromImage(img_out);
            g2.FillRectangle(Brushes.Black, rectf);
            g2.DrawString(shading, new Font("Consolas", 16), Brushes.White, rectf);

            g2.Flush();
            return img_out;
        }
    }
}
