namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.InteropServices;

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

            for (int i = 0; i < imglength - 3; i += 4)
            {
                threshold += Color.FromArgb(img_bytes[i + 2], img_bytes[i + 1], img_bytes[i]).GetBrightness();
            }

            threshold /= w * h;

            for (int i = 0; i < imglength - 3; i += 4)
            {
                var brightness = Color.FromArgb(img_bytes[i + 2], img_bytes[i + 1], img_bytes[i]).GetBrightness();
                if (brightness > threshold)
                {
                    img_out_bytes[i + 2] = 255;
                    img_out_bytes[i + 1] = 255;
                    img_out_bytes[i] = 255;
                }
                else
                {
                    img_out_bytes[i + 2] = 0;
                    img_out_bytes[i + 1] = 0;
                    img_out_bytes[i] = 0;
                }
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

            double threshold = (double)OtsuOld(img) / 255;

            for (int i = 0; i < imglength - 3; i += 4)
            {
                var brightness = Color.FromArgb(img_bytes[i + 2], img_bytes[i + 1], img_bytes[i]).GetBrightness();
                if (brightness > threshold)
                {
                    img_out_bytes[i + 2] = 255;
                    img_out_bytes[i + 1] = 255;
                    img_out_bytes[i] = 255;
                }
                else
                {
                    img_out_bytes[i + 2] = 0;
                    img_out_bytes[i + 1] = 0;
                    img_out_bytes[i] = 0;
                }
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

            double threshold = 0.5;

            for (int i = 0; i < imglength - 2; i += 4)
            {
                var brightness = Color.FromArgb(img_bytes[i + 2], img_bytes[i + 1], img_bytes[i]).GetBrightness();
                var devideBy = ((float)i / 4) + 2;
                threshold = ((threshold * (devideBy - 1)) + brightness) / devideBy;
                if (brightness > threshold)
                {
                    img_out_bytes[i + 2] = 255;
                    img_out_bytes[i + 1] = 255;
                    img_out_bytes[i] = 255;
                }
                else
                {
                    img_out_bytes[i + 2] = 0;
                    img_out_bytes[i + 1] = 0;
                    img_out_bytes[i] = 0;
                }
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

            double threshold = (double)thresholdint / 255;

            for (int i = 0; i < imglength - 3; i += 4)
            {
                var brightness = Color.FromArgb(img_bytes[i + 2], img_bytes[i + 1], img_bytes[i]).GetBrightness();
                if (brightness > threshold)
                {
                    img_out_bytes[i + 2] = 255;
                    img_out_bytes[i + 1] = 255;
                    img_out_bytes[i] = 255;
                }
                else
                {
                    img_out_bytes[i + 2] = 0;
                    img_out_bytes[i + 1] = 0;
                    img_out_bytes[i] = 0;
                }
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap Niblack(Bitmap img)
        {

            return img;
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
        }//сжимает значения в выбранный промежуток
        #endregion
    }
}
