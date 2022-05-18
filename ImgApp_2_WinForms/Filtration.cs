namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    internal class Filtration
    {
        public static Bitmap Linear(Bitmap img, int r1, int r2, double[,] matrix)
        {
            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(w, h);
            Color pix;

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;

                    for (int a = i - r1; a <= i + r1; a++)
                    {
                        for (int b = j - r2; b <= j + r2; b++)
                        {
                            int k = a < 0 ? -a : a;
                            int l = b < 0 ? -b : b;

                            if (k >= h)
                            {
                                k -= k - h + 1;
                            }

                            if (l >= w)
                            {
                                l -= l - w + 1;
                            }

                            pix = img.GetPixel(l, k);
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

            img.Dispose();
            return img_out;
        }

        public static Bitmap Median(Bitmap img, int r1, int r2, double[,] matrix)
        {
            int w = img.Width;
            int h = img.Height;

            Bitmap img_out = new Bitmap(w, h);
            Color pix;
            int length = ((r1 * 2) + 1) * ((r2 * 2) + 1);
            int[] arrayR = new int[length];
            int[] arrayG = new int[length];
            int[] arrayB = new int[length];

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    for (int a = i - r1; a <= i + r1; a++)
                    {
                        for (int b = j - r2; b <= j + r2; b++)
                        {
                            int k = a < 0 ? -a : a;
                            int l = b < 0 ? -b : b;

                            if (k >= h)
                            {
                                k -= k - h + 1;
                            }

                            if (l >= w)
                            {
                                l -= l - w + 1;
                            }

                            pix = img.GetPixel(l, k);

                            int indexi = a - i + r1;
                            int indexj = b - j + r2;
                            int indexflat = (indexi * ((r1 * 2) + 1)) + indexj;

                            arrayR[indexflat] = pix.R;
                            arrayG[indexflat] = pix.G;
                            arrayB[indexflat] = pix.B;
                        }
                    }

                    //pix = Color.FromArgb(Quickselect(arrayR), Quickselect(arrayG), Quickselect(arrayB));
                    pix = Color.FromArgb(kthSmallest(arrayR, 0, length - 1, (int)((double)length / 2)), kthSmallest(arrayG, 0, length - 1, (int)((double)length / 2)), kthSmallest(arrayB, 0, length - 1, (int)((double)length / 2)));
                    img_out.SetPixel(j, i, pix);
                }
            }

            img.Dispose();
            return img_out;
        }

        public static Bitmap LinearFast(Bitmap img, int r1, int r2, double[,] matrix)
        {
            int w = img.Width;
            int h = img.Height;
            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            byte[] img_bytes = GetRGBValues(img);

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;

                    for (int a = i - r1; a <= i + r1; a++)
                    {
                        for (int b = j - r2; b <= j + r2; b++)
                        {
                            int k = a < 0 ? -a : a;
                            int l = b < 0 ? -b : b;

                            if (k >= h)
                            {
                                k -= k - h + 1;
                            }

                            if (l >= w)
                            {
                                l -= l - w + 1;
                            }

                            int indexi = a - i + r1;
                            int indexj = b - j + r2;
                            double matrixValue = matrix[indexi, indexj];

                            sumR += img_bytes[(((k * w) + l) * 4) + 2] * matrixValue;
                            sumG += img_bytes[(((k * w) + l) * 4) + 1] * matrixValue;
                            sumB += img_bytes[((k * w) + l) * 4] * matrixValue;
                        }
                    }

                    img_out_bytes[(((i * w) + j) * 4) + 2] = (byte)Clamp((int)sumR, 0, 255);
                    img_out_bytes[(((i * w) + j) * 4) + 1] = (byte)Clamp((int)sumG, 0, 255);
                    img_out_bytes[((i * w) + j) * 4] = (byte)Clamp((int)sumB, 0, 255);
                }
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);
            return img_out;
        }

        public static Bitmap MedianFast(Bitmap img, int r1, int r2, double[,] matrix)
        {
            int w = img.Width;
            int h = img.Height;
            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            byte[] img_bytes = GetRGBValues(img);

            int length = ((r1 * 2) + 1) * ((r2 * 2) + 1);
            int[] arrayR = new int[length];
            int[] arrayG = new int[length];
            int[] arrayB = new int[length];

            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    for (int a = i - r1; a <= i + r1; a++)
                    {
                        for (int b = j - r2; b <= j + r2; b++)
                        {
                            int k = a < 0 ? -a : a;
                            int l = b < 0 ? -b : b;

                            if (k >= h)
                            {
                                k -= k - h + 1;
                            }

                            if (l >= w)
                            {
                                l -= l - w + 1;
                            }

                            int indexi = a - i + r1;
                            int indexj = b - j + r2;
                            int indexflat = (indexi * ((r1 * 2) + 1)) + indexj;

                            arrayR[indexflat] = img_bytes[(((k * w) + l) * 4) + 2];
                            arrayG[indexflat] = img_bytes[(((k * w) + l) * 4) + 1];
                            arrayB[indexflat] = img_bytes[((k * w) + l) * 4];
                        }
                    }

                    img_out_bytes[(((i * w) + j) * 4) + 2] = (byte)Quickselect(arrayR);
                    img_out_bytes[(((i * w) + j) * 4) + 1] = (byte)Quickselect(arrayG);
                    img_out_bytes[((i * w) + j) * 4] = (byte)Quickselect(arrayB);
                }
            }

            Bitmap img_out = new Bitmap(w, h, PixelFormat.Format32bppRgb);
            writeImageBytes(img_out, img_out_bytes);
            return img_out;
        }

        #region Helper Functions
        private static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
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

        private static int Quickselect(int[] array)
        {
            Array.Sort(array);
            int n = (int)((double)array.Length / 2);
            return array[n];
        }

        private static int kthSmallest(int[] arr, int low, int high, int k)
        {
            // find the partition
            int partition = partitions(arr, low, high);

            // if partition value is equal to the kth position,
            // return value at k.
            if (partition == k)
                return arr[partition];

            // if partition value is less than kth position,
            // search right side of the array.
            else if (partition < k)
                return kthSmallest(arr, partition + 1, high, k);

            // if partition value is more than kth position,
            // search left side of the array.
            else
                return kthSmallest(arr, low, partition - 1, k);
        }

        private static int partitions(int[] arr, int low, int high)
        {
            int pivot = arr[high], pivotloc = low, temp;
            for (int i = low; i <= high; i++)
            {
                // inserting elements of less value
                // to the left of the pivot location
                if (arr[i] < pivot)
                {
                    temp = arr[i];
                    arr[i] = arr[pivotloc];
                    arr[pivotloc] = temp;
                    pivotloc++;
                }
            }

            // swapping pivot to the readonly pivot location
            temp = arr[high];
            arr[high] = arr[pivotloc];
            arr[pivotloc] = temp;

            return pivotloc;
        }

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
        #endregion
    }
}
