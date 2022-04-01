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

            byte[] img1_bytes = new byte[0];
            byte[] img2_bytes = new byte[0];

            using (Bitmap _tmp = new Bitmap(w, h, PixelFormat.Format24bppRgb))
            {
                _tmp.SetResolution(img1.HorizontalResolution, img1.VerticalResolution); //устанавливаем DPI такой же как у исходного

                using (var g = Graphics.FromImage(_tmp)) //рисуем исходное изображение на временном, "типо-копируем"
                {
                    g.DrawImageUnscaled(img1, 0, 0);
                }
                img1_bytes = getImgBytes(_tmp); //получаем байты изображения, см. описание ф-ции 
            }

            using (Bitmap _tmp = new Bitmap(w, h, PixelFormat.Format24bppRgb))
            {
                _tmp.SetResolution(img2.HorizontalResolution, img2.VerticalResolution);

                using (var g = Graphics.FromImage(_tmp))
                {
                    g.DrawImageUnscaled(img2, 0, 0);
                }
                img2_bytes = getImgBytes(_tmp); 
            }

            int imglength = w * h * 3;

            byte[] img_out_bytes = new byte[imglength];

            //for (int i = 0; i < imglength - 1; ++i)
            //    img_out_bytes[i] = Convert.ToByte((img2_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((img2_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            img_out.SetResolution(img1.HorizontalResolution, img1.VerticalResolution);

            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap additionByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = new byte[0];
            byte[] img2_bytes = new byte[0];

            using (Bitmap _tmp = new Bitmap(w, h, PixelFormat.Format24bppRgb))
            {
                _tmp.SetResolution(img1.HorizontalResolution, img1.VerticalResolution); //устанавливаем DPI такой же как у исходного

                using (var g = Graphics.FromImage(_tmp)) //рисуем исходное изображение на временном, "типо-копируем"
                {
                    g.DrawImageUnscaled(img1, 0, 0);
                }
                img1_bytes = getImgBytes(_tmp); //получаем байты изображения, см. описание ф-ции 
            }

            using (Bitmap _tmp = new Bitmap(w, h, PixelFormat.Format24bppRgb))
            {
                _tmp.SetResolution(img2.HorizontalResolution, img2.VerticalResolution);

                using (var g = Graphics.FromImage(_tmp))
                {
                    g.DrawImageUnscaled(img2, 0, 0);
                }
                img2_bytes = getImgBytes(_tmp);
            }

            int imglength = w * h * 3;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((int)Clamp(img1_bytes[i] + img2_bytes[i], 0, 255));
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            img_out.SetResolution(img1.HorizontalResolution, img1.VerticalResolution);

            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        public static Bitmap multiplyByteRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);

            byte[] img1_bytes = new byte[0];
            byte[] img2_bytes = new byte[0];

            using (Bitmap _tmp = new Bitmap(w, h, PixelFormat.Format24bppRgb))
            {
                _tmp.SetResolution(img1.HorizontalResolution, img1.VerticalResolution); //устанавливаем DPI такой же как у исходного

                using (var g = Graphics.FromImage(_tmp)) //рисуем исходное изображение на временном, "типо-копируем"
                {
                    g.DrawImageUnscaled(img1, 0, 0);
                }
                img1_bytes = getImgBytes(_tmp); //получаем байты изображения, см. описание ф-ции 
            }

            using (Bitmap _tmp = new Bitmap(w, h, PixelFormat.Format24bppRgb))
            {
                _tmp.SetResolution(img2.HorizontalResolution, img2.VerticalResolution);

                using (var g = Graphics.FromImage(_tmp))
                {
                    g.DrawImageUnscaled(img2, 0, 0);
                }
                img2_bytes = getImgBytes(_tmp);
            }

            int imglength = w * h * 3;

            byte[] img_out_bytes = new byte[imglength];

            Parallel.For(0, imglength - 2, i =>
            {
                img_out_bytes[i] = Convert.ToByte((float)img1_bytes[i] / 255 * img2_bytes[i]);
                img_out_bytes[i] = Convert.ToByte((img_out_bytes[i] * indexedOpacity + img1_bytes[i] * (255 - indexedOpacity)) / 255);
            });

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            img_out.SetResolution(img1.HorizontalResolution, img1.VerticalResolution);

            writeImageBytes(img_out, img_out_bytes);

            return img_out;
        }

        static byte[] getImgBytes(Bitmap img)
        {
            byte[] bytes = new byte[img.Width * img.Height * 3];  //выделяем память под массив байтов
            var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
                ImageLockMode.ReadOnly,
                img.PixelFormat);
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);  //копируем байты изображения в массив
            img.UnlockBits(data);   //разблокируем изображение
            return bytes; //возвращаем байты
        }

        static void writeImageBytes(Bitmap img, byte[] bytes)
        {
            var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
                ImageLockMode.WriteOnly,
                img.PixelFormat);
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length); //копируем байты массива в изображение

            img.UnlockBits(data);  //разблокируем изображение
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        #region old rendering method through Bitmap
        public static Bitmap normalRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);

            if (indexedOpacity == 255)
                return img2;

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    int r = (pix2.R * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                    int g = (pix2.G * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                    int b = (pix2.B * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }

        public static Bitmap additionRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);
            int r;
            int g;
            int b;
            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    r = (int)Clamp(pix1.R + pix2.R, 0, 255);
                    g = (int)Clamp(pix1.G + pix2.G, 0, 255);
                    b = (int)Clamp(pix1.B + pix2.B, 0, 255);

                    if (indexedOpacity != 255)
                    {
                        r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                        g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                        b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
                    }

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }

        public static Bitmap multiplyRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    int r = Convert.ToInt32((float)pix1.R / 255 * pix2.R);
                    int g = Convert.ToInt32((float)pix1.G / 255 * pix2.G);
                    int b = Convert.ToInt32((float)pix1.B / 255 * pix2.B);

                    if (indexedOpacity != 255)
                    {
                        r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                        g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                        b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
                    }

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }

        public static Bitmap averageRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    int r = (int)Clamp((pix1.R + pix2.R) / 2, 0, 255);
                    int g = (int)Clamp((pix1.G + pix2.G) / 2, 0, 255);
                    int b = (int)Clamp((pix1.B + pix2.B) / 2, 0, 255);

                    if (indexedOpacity != 255)
                    {
                        r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                        g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                        b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
                    }

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }

        public static Bitmap darkenRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    int r = Math.Min(pix1.R, pix2.R);
                    int g = Math.Min(pix1.G, pix2.G);
                    int b = Math.Min(pix1.B, pix2.B);


                    if (indexedOpacity != 255)
                    {
                        r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                        g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                        b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
                    }

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }

        public static Bitmap lightenRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    int r = Math.Max(pix1.R, pix2.R);
                    int g = Math.Max(pix1.G, pix2.G);
                    int b = Math.Max(pix1.B, pix2.B);

                    if (indexedOpacity != 255)
                    {
                        r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                        g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                        b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
                    }

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }

        public static Bitmap maskRender(Bitmap img1, Bitmap img2, int index, int indexedOpacity)
        {
            int w = Math.Min(img1.Width, img2.Width);
            int h = Math.Min(img1.Height, img2.Height);
            var img_out = new Bitmap(w, h);

            int r;
            int g;
            int b;

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    Color pix1 = img1.GetPixel(j, i);
                    Color pix2 = img2.GetPixel(j, i);

                    var brightness = pix2.GetBrightness();

                    r = Convert.ToInt32(pix1.R * brightness);
                    g = Convert.ToInt32(pix1.G * brightness);
                    b = Convert.ToInt32(pix1.B * brightness);

                    if (indexedOpacity != 255)
                    {
                        r = (r * indexedOpacity + pix1.R * (255 - indexedOpacity)) / 255;
                        g = (g * indexedOpacity + pix1.G * (255 - indexedOpacity)) / 255;
                        b = (b * indexedOpacity + pix1.B * (255 - indexedOpacity)) / 255;
                    }

                    pix1 = Color.FromArgb(r, g, b);
                    img_out.SetPixel(j, i, pix1);
                }
            }

            return img_out;
        }
#endregion
    }
}
