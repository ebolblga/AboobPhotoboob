using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImgApp_2_WinForms
{
    class Render
    {
        public static Bitmap normalByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            if (indexedOpacity == 255)
                return img2;

            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = GetRGBValues(img1);
            byte[] img2_bytes = GetRGBValues(img2);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((img2_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
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
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
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
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
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
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
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
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
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
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
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

                img_out_bytes[i + 2] = Convert.ToByte((img_out_bytes[i + 2] * indexedOpacity + img1_bytes[i + 2] * (255 - indexedOpacity)) / 255);
                img_out_bytes[i + 1] = Convert.ToByte((img_out_bytes[i + 1] * indexedOpacity + img1_bytes[i + 1] * (255 - indexedOpacity)) / 255);
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        #region helper functions
        static void writeImageBytes(Bitmap img, byte[] bytes)//конвертирует byte[] в Bitmap
        {
            var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
                ImageLockMode.WriteOnly,
                img.PixelFormat);
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length); //копируем байты массива в изображение

            img.UnlockBits(data);  //разблокируем изображение
        }

        private static byte[] GetRGBValues(Bitmap bmp)//конвертирует Bitmap в byte[]
        {

            // Lock the bitmap's bits. 
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
             bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
             bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes); bmp.UnlockBits(bmpData);

            return rgbValues;
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }//сжимает значения в выбранный промежуток
        #endregion

        #region old rendering method through Bitmap
        //public static Bitmap normalRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);

        //    if (indexedOpacity == 255)
        //        return img2;

        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            int r = (pix2.R * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //            int g = (pix2.G * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //            int b = (pix2.B * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}

        //public static Bitmap additionRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);
        //    int r;
        //    int g;
        //    int b;
        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            r = (int)Clamp(pix1.R + pix2.R, 0, 255);
        //            g = (int)Clamp(pix1.G + pix2.G, 0, 255);
        //            b = (int)Clamp(pix1.B + pix2.B, 0, 255);

        //            if (indexedOpacity != 255)
        //            {
        //                r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //                g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //                b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
        //            }

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}

        //public static Bitmap multiplyRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);

        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            int r = Convert.ToInt32((float)pix1.R / 255 * pix2.R);
        //            int g = Convert.ToInt32((float)pix1.G / 255 * pix2.G);
        //            int b = Convert.ToInt32((float)pix1.B / 255 * pix2.B);

        //            if (indexedOpacity != 255)
        //            {
        //                r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //                g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //                b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
        //            }

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}

        //public static Bitmap averageRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);

        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            int r = (int)Clamp((pix1.R + pix2.R) / 2, 0, 255);
        //            int g = (int)Clamp((pix1.G + pix2.G) / 2, 0, 255);
        //            int b = (int)Clamp((pix1.B + pix2.B) / 2, 0, 255);

        //            if (indexedOpacity != 255)
        //            {
        //                r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //                g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //                b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
        //            }

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}

        //public static Bitmap darkenRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);

        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            int r = Math.Min(pix1.R, pix2.R);
        //            int g = Math.Min(pix1.G, pix2.G);
        //            int b = Math.Min(pix1.B, pix2.B);


        //            if (indexedOpacity != 255)
        //            {
        //                r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //                g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //                b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
        //            }

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}

        //public static Bitmap lightenRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);

        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            int r = Math.Max(pix1.R, pix2.R);
        //            int g = Math.Max(pix1.G, pix2.G);
        //            int b = Math.Max(pix1.B, pix2.B);

        //            if (indexedOpacity != 255)
        //            {
        //                r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //                g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //                b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
        //            }

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}

        //public static Bitmap maskRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        //{
        //    int w = Math.Min(img1.Width, img2.Width);
        //    int h = Math.Min(img1.Height, img2.Height);
        //    var img_out = new Bitmap(w, h);

        //    int r;
        //    int g;
        //    int b;

        //    for (int i = 0; i < h; ++i)
        //    {
        //        for (int j = 0; j < w; ++j)
        //        {
        //            Color pix1 = img1.GetPixel(j, i);
        //            Color pix2 = img2.GetPixel(j, i);

        //            var brightness = pix2.GetBrightness();

        //            r = Convert.ToInt32(pix1.R * brightness);
        //            g = Convert.ToInt32(pix1.G * brightness);
        //            b = Convert.ToInt32(pix1.B * brightness);

        //            if (indexedOpacity != 255)
        //            {
        //                r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
        //                g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
        //                b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
        //            }

        //            pix1 = Color.FromArgb(r, g, b);
        //            img_out.SetPixel(j, i, pix1);
        //        }
        //    }

        //    return img_out;
        //}
#endregion
    }
}
