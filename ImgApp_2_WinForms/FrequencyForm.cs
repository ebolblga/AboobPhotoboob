namespace ImgApp_2_WinForms
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Numerics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class FrequencyForm : Form
    {
        private Bitmap _inputImg;
        private Bitmap _fourierImg;
        private Bitmap _brightFourierImg;
        private Bitmap _maskImg;

        public FrequencyForm(Image img)
        {
            InitializeComponent();
            _inputImg = new Bitmap(img);
        }

        private void FrequencyForm_Load(object sender, EventArgs e)
        {
            pBInput.Image = _inputImg;
            GetFourier();
        }

        public class DirectBitmap : IDisposable
        {
            public Bitmap Bitmap { get; private set; }

            public Int32[] Bits { get; private set; }

            public bool Disposed { get; private set; }

            public int Height { get; private set; }

            public int Width { get; private set; }

            protected GCHandle BitsHandle { get; private set; }

            public DirectBitmap(int width, int height)
            {
                Width = width;
                Height = height;
                Bits = new Int32[width * height];
                BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
                Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
            }

            public void SetPixel(int x, int y, Color colour)
            {
                int index = x + (y * Width);
                int col = colour.ToArgb();

                Bits[index] = col;
            }

            public Color GetPixel(int x, int y)
            {
                int index = x + (y * Width);
                int col = Bits[index];
                Color result = Color.FromArgb(col);

                return result;
            }

            public void Dispose()
            {
                if (Disposed)
                {
                    return;
                }

                Disposed = true;
                Bitmap.Dispose();
                BitsHandle.Free();
            }
        }

        private void GetFourier()// renders fourier
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            //Locks future image bits
            DirectBitmap img = new DirectBitmap(_inputImg.Width, _inputImg.Height);

            //Draws img to those bits
            using (var g = Graphics.FromImage(img.Bitmap))
            {
                g.DrawImage(_inputImg, 0, 0, img.Width, img.Height);
            }

            //Temporary arrays for rows
            Complex[] rRow = new Complex[img.Width];
            Complex[] gRow = new Complex[img.Width];
            Complex[] bRow = new Complex[img.Width];

            //Temporary arrays for columns
            Complex[] rCol = new Complex[img.Height];
            Complex[] gCol = new Complex[img.Height];
            Complex[] bCol = new Complex[img.Height];

            //Fourier transforms of a two-dimensional signals for each color channel
            Complex[,] redMatrix = new Complex[img.Width, img.Height];
            Complex[,] greenMatrix = new Complex[img.Width, img.Height];
            Complex[,] blueMatrix = new Complex[img.Width, img.Height];

            double rMax, gMax, bMax;
            rMax = gMax = bMax = -50;

            //For each row I do FFT, write to G(___Matrix)
            for (int i = 0; i < img.Height; ++i)
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    int r = pix.R;
                    int g = pix.G;
                    int b = pix.B;

                    rRow[j] = new Complex(r * Math.Pow(-1, j + i), 0);
                    gRow[j] = new Complex(g * Math.Pow(-1, j + i), 0);
                    bRow[j] = new Complex(b * Math.Pow(-1, j + i), 0);
                }

                rRow = FFT(rRow);
                gRow = FFT(gRow);
                bRow = FFT(bRow);

                for (int j = 0; j < img.Width; ++j)
                {
                    redMatrix[j, i] = rRow[j] / img.Width;
                    greenMatrix[j, i] = gRow[j] / img.Width;
                    blueMatrix[j, i] = bRow[j] / img.Width;
                }
            }

            //For each column I do FFT, write to G(___Matrix)
            for (int j = 0; j < img.Width; ++j)
            {
                for (int i = 0; i < img.Height; ++i)
                {
                    rCol[i] = redMatrix[j, i];
                    gCol[i] = greenMatrix[j, i];
                    bCol[i] = blueMatrix[j, i];
                }

                rCol = FFT(rCol);
                gCol = FFT(gCol);
                bCol = FFT(bCol);

                for (int i = 0; i < img.Height; ++i)
                {
                    redMatrix[j, i] = rCol[i] / img.Height;
                    greenMatrix[j, i] = gCol[i] / img.Height;
                    blueMatrix[j, i] = bCol[i] / img.Height;

                    if (Math.Log(Math.Abs(rCol[i].Magnitude) + 1) > rMax)
                    {
                        rMax = Math.Log(Math.Abs(rCol[i].Magnitude) + 1);
                    }

                    if (Math.Log(Math.Abs(gCol[i].Magnitude) + 1) > gMax)
                    {
                        gMax = Math.Log(Math.Abs(gCol[i].Magnitude) + 1);
                    }

                    if (Math.Log(Math.Abs(bCol[i].Magnitude) + 1) > bMax)
                    {
                        bMax = Math.Log(Math.Abs(bCol[i].Magnitude) + 1);
                    }
                }
            }

            //Rendering the Fourier Transform
            for (int j = 0; j < img.Width; ++j)
            {
                for (int i = 0; i < img.Height; ++i)
                {
                    int r = Clamp(Convert.ToInt32(Math.Log(Math.Abs(redMatrix[j, i].Magnitude) + 1) * 255 / rMax * 8), 0, 255);
                    int g = Clamp(Convert.ToInt32(Math.Log(Math.Abs(greenMatrix[j, i].Magnitude) + 1) * 255 / gMax * 8), 0, 255);
                    int b = Clamp(Convert.ToInt32(Math.Log(Math.Abs(blueMatrix[j, i].Magnitude) + 1) * 255 / bMax * 8), 0, 255);

                    img.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            }

            _fourierImg = new Bitmap(img.Bitmap);
            img.Dispose();

            pBFourier.Image = _fourierImg;
            _maskImg = new Bitmap(_fourierImg);
            pBMask.Image = _maskImg;

            this.Cursor = Cursors.Default;
            timer.Stop();
            label3.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private Complex[] DFT(Complex[] x, double n = 1)// Одномерное дискретное преобразование Фурье
        {
            int N = x.Length;
            Complex[] G = new Complex[N];

            for (int u = 0; u < N; ++u)
            {
                double _fi = -2.0 * Math.PI * u / N;
                for (int k = 0; k < N; ++k)
                {
                    double fi = _fi * k;
                    G[u] += new Complex(Math.Cos(fi), Math.Sin(fi)) * x[k];
                }

                G[u] = n * G[u];    //для умножения на 1/N для прямого преобразования I
            }

            return G;
        }

        private static Complex[] FFT(Complex[] array)// Одномерное дискретное преобразование Фурье алгоритм Кули-Тьюки
        {
            Complex[] modArray;
            int len = array.Length;
            if (len == 2)
            {
                modArray = new Complex[2];
                modArray[0] = array[0] + array[1];
                modArray[1] = array[0] - array[1];
            }
            else
            {
                Complex[] evenArr = new Complex[len / 2];
                Complex[] oddArr = new Complex[len / 2];
                for (int i = 0; i < len / 2; i++)
                {
                    evenArr[i] = array[2 * i];
                    oddArr[i] = array[2 * i + 1];
                }

                Complex[] evenModArr = FFT(evenArr);
                Complex[] oddModArr = FFT(oddArr);
                modArray = new Complex[len];
                for (int i = 0; i < len / 2; i++)
                {
                    modArray[i] = evenModArr[i] + (m(i, len) * oddModArr[i]);
                    modArray[i + (len / 2)] = evenModArr[i] - (m(i, len) * oddModArr[i]);
                }
            }
            return modArray;
        }

        public static Complex m(int k, int N)
        {
            double arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }

        private void pBMask_MouseMove(object sender, MouseEventArgs e)// mask UI drawing
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _maskImg.Dispose();

            if (_brightFourierImg != null)
            {
                _maskImg = new Bitmap(_brightFourierImg);
            }
            else
            {
                _maskImg = new Bitmap(_fourierImg);
            }

            float centerX = (float)pBMask.ClientSize.Width / 2;
            float centerY = (float)pBMask.ClientSize.Height / 2;

            float diameter = (float)(Math.Sqrt(Math.Pow(centerX - e.Location.X, 2) + Math.Pow(centerY - e.Location.Y, 2)) * 4);
            if (diameter > pBMask.ClientSize.Width)
            {
                diameter = pBMask.ClientSize.Width;
            }

            if (diameter > pBMask.ClientSize.Height)
            {
                diameter = pBMask.ClientSize.Height;
            }

            int lineWidth = 8;

            float x = (float)(_inputImg.Width / 2) - (diameter / 2) + ((float)lineWidth / 2);
            float y = (float)(_inputImg.Height / 2) - (diameter / 2) + ((float)lineWidth / 2);

            using (Graphics g = Graphics.FromImage(_maskImg))
            {
                Pen blackPen = new Pen(Color.Yellow, lineWidth);
                g.DrawEllipse(blackPen, x, y, diameter - lineWidth, diameter - lineWidth);
            }

            pBMask.Image = _maskImg;
        }     

        #region trash
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (_brightFourierImg != null)
            {
                _brightFourierImg.Dispose();
            }

            label1.Text = trackBar2.Value.ToString();

            int value = Convert.ToInt32(label1.Text);

            //Locks future image bits
            DirectBitmap img = new DirectBitmap(_inputImg.Width, _inputImg.Height);

            //Draws img to those bits
            using (var g = Graphics.FromImage(img.Bitmap))
            {
                g.DrawImage(_fourierImg, 0, 0, img.Width, img.Height);
            }

            for (int j = 0; j < img.Width; ++j)
            {
                for (int i = 0; i < img.Height; ++i)
                {
                    var pix = img.GetPixel(j, i);

                    int r = Clamp(pix.R * value, 0, 255);
                    int g = Clamp(pix.G * value, 0, 255);
                    int b = Clamp(pix.B * value, 0, 255);

                    img.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            }

            _brightFourierImg = new Bitmap(img.Bitmap);
            img.Dispose();
            pBMask.Image = _brightFourierImg;
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }//clamps all values
        #endregion
    }
}
