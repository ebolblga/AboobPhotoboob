namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    internal class Binarization
    {
        public static Bitmap Gavrilov(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            byte[] img_bytes = GetRGBValues(img);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            double threshold = 0;

            //Parallel.ForEach(Enumerable.Range(0, imglength / 4).Select(i => i * 4), i =>
            //{
            //    threshold += (double)(0.2125 * img_bytes[i + 2]) + (double)(0.7154 * img_bytes[i + 1]) + (double)(0.0721 * img_bytes[i]);
            //});

            for (int i = 0; i < imglength - 3; i += 4)
            {
                threshold += (double)(0.2125 * img_bytes[i + 2]) + (double)(0.7154 * img_bytes[i + 1]) + (double)(0.0721 * img_bytes[i]);
            }

            threshold /= w * h;

            for (int i = 0; i < imglength - 3; i += 4)
            {
                double brightness = (double)(0.2125 * img_bytes[i + 2]) + (double)(0.7154 * img_bytes[i + 1]) + (double)(0.0721 * img_bytes[i]);
                byte value = brightness > threshold ? (byte)255 : (byte)0;
                img_out_bytes[i + 2] = value;
                img_out_bytes[i + 1] = value;
                img_out_bytes[i] = value;
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap Otsu(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            byte[] img_bytes = GetRGBValues(img);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            double threshold = (double)OtsuOld(img);

            for (int i = 0; i < imglength - 3; i += 4)
            {
                double brightness = (double)(0.2125 * img_bytes[i + 2]) + (double)(0.7154 * img_bytes[i + 1]) + (double)(0.0721 * img_bytes[i]);
                byte value = brightness > threshold ? (byte)255 : (byte)0;
                img_out_bytes[i + 2] = value;
                img_out_bytes[i + 1] = value;
                img_out_bytes[i] = value;
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static int OtsuOld(Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];

            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);

            //Get histogram values
            double[] histogram = new double[256];
            for (int i = 0; i < bytes; i += 3)
            {
                histogram[buffer[i]]++;
            }

            //Normalize histogram
            histogram = histogram.Select(x => x / (w * h)).ToArray();

            //Global mean
            double mg = 0;
            for (int i = 0; i < 255; i++)
            {
                mg += i * histogram[i];
            }

            //Get max between-class variance
            double bcv = 0;
            int threshold = 0;
            for (int i = 0; i < 256; i++)
            {
                double cs = 0;
                double m = 0;
                for (int j = 0; j < i; j++)
                {
                    cs += histogram[j];
                    m += j * histogram[j];
                }

                if (cs == 0)
                {
                    continue;
                }

                double old_bcv = bcv;
                bcv = Math.Max(bcv, Math.Pow((mg * cs) - m, 2) / (cs * (1 - cs)));

                if (bcv > old_bcv)
                {
                    threshold = i;
                }
            }

            return threshold;
        }

        public static Bitmap Kochanovskiy(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            byte[] img_bytes = GetRGBValues(img);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            double threshold = 127.5;

            for (int i = 0; i < imglength - 2; i += 4)
            {
                double brightness = (double)(0.2125 * img_bytes[i + 2]) + (double)(0.7154 * img_bytes[i + 1]) + (double)(0.0721 * img_bytes[i]);
                var devideBy = ((float)i / 4) + 2;
                threshold = ((threshold * (devideBy - 1)) + brightness) / devideBy;
                byte value = brightness > threshold ? (byte)255 : (byte)0;
                img_out_bytes[i + 2] = value;
                img_out_bytes[i + 1] = value;
                img_out_bytes[i] = value;
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap Slider(Bitmap img, int thresholdint)
        {
            int w = img.Width;
            int h = img.Height;

            byte[] img_bytes = GetRGBValues(img);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            double threshold = (double)thresholdint;

            for (int i = 0; i < imglength - 3; i += 4)
            {
                double brightness = (double)(0.2125 * img_bytes[i + 2]) + (double)(0.7154 * img_bytes[i + 1]) + (double)(0.0721 * img_bytes[i]);
                byte value = brightness > threshold ? (byte)255 : (byte)0;
                img_out_bytes[i + 2] = value;
                img_out_bytes[i + 1] = value;
                img_out_bytes[i] = value;
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap FastSlider(Bitmap img, int threshold)
        {
            Bitmap img_out = ConvertBlackAndWhite(img);

            int w = img_out.Width;
            int h = img_out.Height;

            byte[] img_bytes = GetRGBValues(img_out);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 1, i =>
            {
                if (img_bytes[i] > threshold)
                {
                    img_out_bytes[i] = 255;
                }
                else
                {
                    img_out_bytes[i] = 0;
                }
            });

            Bitmap img_out2 = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out2, img_out_bytes);

            return img_out2;
        }

        public static Bitmap Niblack(Bitmap img)
        {
            int[,] integralIMG = IntegralImage(img);

            int window = 16; // Размер окна
            double k = -0.2; // коэффицент Ниблака

            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    int windowI = i < window ? i + 1 : window;
                    int windowJ = j < window ? j + 1 : window;

                    int windowSum = integralIMG[j + 1, i + 1] - integralIMG[j + 1, i + 1 - windowI] - integralIMG[j + 1 - windowJ, i + 1] + integralIMG[j + 1 - windowJ, i + 1 - windowI]; // сумма значений пикселей в окне
                    double expected_value = Convert.ToInt32((double)windowSum / (windowI * windowJ)); // математическое ожидание

                    Color pix = img.GetPixel(j, i);
                    double brightness = (0.2125 * pix.R) + (0.7154 * pix.G) + (0.0721 * pix.B); // яркость в данном пикселе
                    double dispersion = Math.Sqrt(Math.Abs((brightness * brightness) - (expected_value * expected_value))); // дисперсия

                    double threshold = expected_value + (k * dispersion); // порог

                    pix = brightness > threshold ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);

                    img_out.SetPixel(j, i, pix);
                }
            }

            return img_out;
        }

        public static Bitmap Sauvola(Bitmap img)
        {
            int[,] integralIMG = IntegralImage(img);

            int window = 16; // Размер окна
            double k = 0.35; // коэффицент Сауволы
            int r = 256; // С градациями цвета

            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    int windowI = i < window ? i + 1 : window;
                    int windowJ = j < window ? j + 1 : window;

                    int windowSum = integralIMG[j + 1, i + 1] - integralIMG[j + 1, i + 1 - windowI] - integralIMG[j + 1 - windowJ, i + 1] + integralIMG[j + 1 - windowJ, i + 1 - windowI]; // сумма значений пикселей в окне
                    double expected_value = Convert.ToInt32((double)windowSum / (windowI * windowJ)); // математическое ожидание

                    Color pix = img.GetPixel(j, i);
                    double brightness = (0.2125 * pix.R) + (0.7154 * pix.G) + (0.0721 * pix.B); // яркость в данном пикселе
                    double dispersion = Math.Sqrt(Math.Abs((brightness * brightness) - (expected_value * expected_value))); // дисперсия

                    double threshold = expected_value * (1 + (k * ((dispersion / r) - 1))); // порог

                    pix = brightness > threshold ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);

                    img_out.SetPixel(j, i, pix);
                }
            }

            return img_out;
        }

        public static Bitmap Wulff(Bitmap img)
        {
            int[,] integralIMG = IntegralImage(img);

            int window = 16; // Размер окна
            double r = 0; //максимальное стандартное отклонение
            double a = 0.5; // Усиление
            double min = 255; //самый тусклый пиксель изображения

            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    int windowI = i < window ? i + 1 : window;
                    int windowJ = j < window ? j + 1 : window;

                    int windowSum = integralIMG[j + 1, i + 1] - integralIMG[j + 1, i + 1 - windowI] - integralIMG[j + 1 - windowJ, i + 1] + integralIMG[j + 1 - windowJ, i + 1 - windowI]; // сумма значений пикселей в окне
                    double expected_value = Convert.ToInt32((double)windowSum / (windowI * windowJ)); // математическое ожидание

                    Color pix = img.GetPixel(j, i);
                    double brightness = (0.2125 * pix.R) + (0.7154 * pix.G) + (0.0721 * pix.B); // яркость в данном пикселе
                    min = brightness < min ? brightness : min;

                    double dispersion = Math.Sqrt(Math.Abs((brightness * brightness) - (expected_value * expected_value))); // дисперсия
                    r = dispersion > r ? dispersion : r;
                }
            }

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    int windowI = i < window ? i + 1 : window;
                    int windowJ = j < window ? j + 1 : window;

                    int windowSum = integralIMG[j + 1, i + 1] - integralIMG[j + 1, i + 1 - windowI] - integralIMG[j + 1 - windowJ, i + 1] + integralIMG[j + 1 - windowJ, i + 1 - windowI]; // сумма значений пикселей в окне
                    double expected_value = Convert.ToInt32((double)windowSum / (windowI * windowJ)); // математическое ожидание

                    Color pix = img.GetPixel(j, i);
                    double brightness = (0.2125 * pix.R) + (0.7154 * pix.G) + (0.0721 * pix.B); // яркость в данном пикселе
                    double dispersion = Math.Sqrt(Math.Abs((brightness * brightness) - (expected_value * expected_value))); // дисперсия

                    double threshold = ((1 - a) * expected_value) + (a * min) + (a * ((dispersion / r * expected_value) - min)); // порог

                    pix = brightness > threshold ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);

                    img_out.SetPixel(j, i, pix);
                }
            }

            return img_out;
        }

        public static Bitmap Bradley(Bitmap img)
        {
            int[,] integralIMG = IntegralImage(img);

            int window = 16; // Размер окна
            double k = 0.15; // коэффицент Бредли-Рота

            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    int windowI = i < window ? i + 1 : window;
                    int windowJ = j < window ? j + 1 : window;

                    int windowSum = integralIMG[j + 1, i + 1] - integralIMG[j + 1, i + 1 - windowI] - integralIMG[j + 1 - windowJ, i + 1] + integralIMG[j + 1 - windowJ, i + 1 - windowI]; // сумма значений пикселей в окне

                    Color pix = img.GetPixel(j, i);
                    double brightness = (0.2125 * pix.R) + (0.7154 * pix.G) + (0.0721 * pix.B); // яркость в данном пикселе

                    pix = (brightness * windowI * windowJ) < windowSum * (1 - k) ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);

                    img_out.SetPixel(j, i, pix);
                }
            }

            return img_out;
        }

        public static int[,] IntegralImage(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;

            int[,] integralIMG = new int[w + 1, h + 1];

            for (int i = 1; i < h; ++i)
            {
                for (int j = 1; j < w; ++j)
                {
                    Color pix = img.GetPixel(j, i);
                    int brightness = Convert.ToInt32((0.2125 * pix.R) + (0.7154 * pix.G) + (0.0721 * pix.B));
                    integralIMG[j, i] = (int)brightness + integralIMG[j - 1, i] + integralIMG[j, i - 1] - integralIMG[j - 1, i - 1];
                }
            }

            return integralIMG;
        }

        #region helper functions
        static void writeImageBytes(Bitmap img, byte[] bytes)//конвертирует byte[] в Bitmap
        {
            var data = img.LockBits(
                new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
                ImageLockMode.WriteOnly,
                img.PixelFormat);
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length); //копируем байты массива в изображение

            img.UnlockBits(data);  //разблокируем изображение
        }

        private static byte[] GetRGBValues(Bitmap bmp)//конвертирует Bitmap в byte[]
        {

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
             bmp.LockBits(rect, ImageLockMode.ReadOnly,
             bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);

            return rgbValues;
        }

        public static Bitmap ConvertBlackAndWhite(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1},
              });
            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        #endregion
    }
}
