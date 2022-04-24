namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    class Render
    {
        public static Bitmap normalByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            if (indexedOpacity == 255)
            {
                return img2;
            }

            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte(((img2_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap additionByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((int)Clamp(img1_bytes[i] + img2_bytes[i], 0, 255));
                img_out_bytes[i] = Convert.ToByte(((img_out_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap multiplyByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((float)img1_bytes[i] / 255 * img2_bytes[i]);
                img_out_bytes[i] = Convert.ToByte(((img_out_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap averageByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((int)Clamp((img1_bytes[i] + img2_bytes[i]) / 2, 0, 255));
                img_out_bytes[i] = Convert.ToByte(((img_out_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap darkenByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte(Math.Min(img1_bytes[i], img2_bytes[i]));
                img_out_bytes[i] = Convert.ToByte(((img_out_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap lightenByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte(Math.Max(img1_bytes[i], img2_bytes[i]));
                img_out_bytes[i] = Convert.ToByte(((img_out_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap maskByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            for (int i = 0; i < imglength - 3; i += 4)
            {
                var brightness = Color.FromArgb(img2_bytes[i + 2], img2_bytes[i + 1], img2_bytes[i]).GetBrightness();

                img_out_bytes[i + 2] = Convert.ToByte(img1_bytes[i + 2] * brightness);
                img_out_bytes[i + 1] = Convert.ToByte(img1_bytes[i + 1] * brightness);
                img_out_bytes[i] = Convert.ToByte(img1_bytes[i] * brightness);

                img_out_bytes[i + 2] = Convert.ToByte(((img_out_bytes[i + 2] * indexedOpacity) + (img1_bytes[i + 2] * (255 - indexedOpacity))) / 255);
                img_out_bytes[i + 1] = Convert.ToByte(((img_out_bytes[i + 1] * indexedOpacity) + (img1_bytes[i + 1] * (255 - indexedOpacity))) / 255);
                img_out_bytes[i] = Convert.ToByte(((img_out_bytes[i] * indexedOpacity) + (img1_bytes[i] * (255 - indexedOpacity))) / 255);
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
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
