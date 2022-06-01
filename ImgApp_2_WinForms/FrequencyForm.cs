namespace ImgApp_2_WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Numerics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class FrequencyForm : Form
    {
        private Bitmap _inputImg;       // Input image
        private Bitmap _fourierImg;     // Fourier original
        private Bitmap _brightFourierImg;//Fourier brightened up
        private Bitmap _maskImg;        // Mask
        private Bitmap _maskDrawing;    //Mask + Bright fourier

        Complex[,] redMatrix;
        Complex[,] greenMatrix;
        Complex[,] blueMatrix;

        private List<Shape> ShapeList = new List<Shape>();

        public FrequencyForm(Image img)
        {
            InitializeComponent();
            _inputImg = new Bitmap(img);
        }

        private void FrequencyForm_Load(object sender, EventArgs e)
        {
            pBInput.Image = _inputImg;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            _maskImg = new Bitmap(_inputImg.Width, _inputImg.Height);
            using (Graphics g = Graphics.FromImage(_maskImg))
            {
                g.FillRectangle(Brushes.Black, 0, 0, _inputImg.Width, _inputImg.Height);
            }

            pBMask.Image = _maskImg;

            redMatrix = new Complex[_inputImg.Width, _inputImg.Height];
            greenMatrix = new Complex[_inputImg.Width, _inputImg.Height];
            blueMatrix = new Complex[_inputImg.Width, _inputImg.Height];

            GetFourier();

            _brightFourierImg = new Bitmap(_fourierImg);
            _maskDrawing = new Bitmap(_fourierImg);
            pBFourierDrawing.Image = _maskDrawing;
        }

        #region Fourier rendering
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
            //Complex[,] redMatrix = new Complex[img.Width, img.Height];
            //Complex[,] greenMatrix = new Complex[img.Width, img.Height];
            //Complex[,] blueMatrix = new Complex[img.Width, img.Height];

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
                    oddArr[i] = array[(2 * i) + 1];
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
        #endregion

        #region Mask Drawing
        public class Shape
        {
            public bool shape { get; set; }

            public float X { get; set; }

            public float Y { get; set; }

            public float Width { get; set; }

            public float Height { get; set; }

            public Shape(bool shape, float x, float y, float width, float height)
            {
                this.shape = shape;
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
            }
        }

        private void pBMask_MouseMove(object sender, MouseEventArgs e)// mask UI drawing
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _maskDrawing.Dispose();
            _maskDrawing = new Bitmap(_brightFourierImg);

            float centerX = (float)pBFourierDrawing.ClientSize.Width / 2;
            float centerY = (float)pBFourierDrawing.ClientSize.Height / 2;

            float diameter = (float)(Math.Sqrt(Math.Pow(centerX - e.Location.X, 2) + Math.Pow(centerY - e.Location.Y, 2)) * 4);

            float width = (float)(Math.Abs(centerX - e.Location.X) * 4);
            float height = (float)(Math.Abs(centerY - e.Location.Y) * 4);

            int lineWidth;
            try
            {
                lineWidth = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                lineWidth = 8;
            }

            float x = (float)(_inputImg.Width / 2) - (diameter / 2) + ((float)lineWidth / 2);
            float y = (float)(_inputImg.Height / 2) - (diameter / 2) + ((float)lineWidth / 2);

            float x1 = (float)(_inputImg.Width / 2) - (width / 2) + ((float)lineWidth / 2);
            float y1 = (float)(_inputImg.Height / 2) - (height / 2) + ((float)lineWidth / 2);

            using (Graphics g = Graphics.FromImage(_maskDrawing))
            {
                Pen blackPen = new Pen(Color.Yellow, lineWidth);
                if (comboBox1.SelectedIndex == 0)
                {
                    g.DrawEllipse(blackPen, x, y, diameter - lineWidth, diameter - lineWidth);
                }
                else
                {
                    g.DrawRectangle(blackPen, x1, y1, width - lineWidth, height - lineWidth);
                }
            }

            pBFourierDrawing.Image = _maskDrawing;

            ShapeList.Clear();
            if (comboBox1.SelectedIndex == 0)
            {
                ShapeList.Add(new Shape(true, x, y, diameter, diameter));
            }
            else
            {
                ShapeList.Add(new Shape(false, x1, y1, width, height));
            }

            DrawMask();
        }

        private void button2_Click(object sender, EventArgs e)// applies Fourier transformations
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            DirectBitmap inputImg = new DirectBitmap(_fourierImg.Width, _fourierImg.Height);
            using (var g = Graphics.FromImage(inputImg.Bitmap))
            {
                g.DrawImage(_fourierImg, 0, 0, inputImg.Width, inputImg.Height);
            }

            DirectBitmap mask = new DirectBitmap(_inputImg.Width, _inputImg.Height);
            using (var g = Graphics.FromImage(mask.Bitmap))
            {
                g.DrawImage(_maskImg, 0, 0, mask.Width, mask.Height);
            }

            for (int j = 0; j < inputImg.Width; ++j)
            {
                for (int i = 0; i < inputImg.Height; ++i)
                {
                    var pix2 = mask.GetPixel(j, i);
                    var brightness = 1 - Color.FromArgb(pix2.R, pix2.G, pix2.B).GetBrightness();

                    redMatrix[j, i] = new Complex(redMatrix[j, i].Real * brightness, -redMatrix[j, i].Imaginary * brightness);
                    greenMatrix[j, i] = new Complex(greenMatrix[j, i].Real * brightness, -greenMatrix[j, i].Imaginary * brightness);
                    blueMatrix[j, i] = new Complex(blueMatrix[j, i].Real * brightness, -blueMatrix[j, i].Imaginary * brightness);
                }
            }

            Complex[] rRow = new Complex[inputImg.Width];
            Complex[] gRow = new Complex[inputImg.Width];
            Complex[] bRow = new Complex[inputImg.Width];

            Complex[] rCol = new Complex[inputImg.Height];
            Complex[] gCol = new Complex[inputImg.Height];
            Complex[] bCol = new Complex[inputImg.Height];

            for (int i = 0; i < inputImg.Height; ++i)
            {
                for (int j = 0; j < inputImg.Width; ++j)
                {
                    rRow[j] = redMatrix[j, i];
                    gRow[j] = greenMatrix[j, i];
                    bRow[j] = blueMatrix[j, i];
                }

                rRow = DFT(rRow);
                gRow = DFT(gRow);
                bRow = DFT(bRow);

                for (int j = 0; j < inputImg.Width; ++j)
                {
                    redMatrix[j, i] = rRow[j];
                    greenMatrix[j, i] = gRow[j];
                    blueMatrix[j, i] = bRow[j];
                }
            }

            for (int j = 0; j < inputImg.Width; ++j)
            {
                for (int i = 0; i < inputImg.Height; ++i)
                {
                    rCol[i] = redMatrix[j, i];
                    gCol[i] = greenMatrix[j, i];
                    bCol[i] = blueMatrix[j, i];
                }

                rCol = DFT(rCol);
                gCol = DFT(gCol);
                bCol = DFT(bCol);

                for (int i = 0; i < inputImg.Height; ++i)
                {
                    redMatrix[j, i] = rCol[i];
                    greenMatrix[j, i] = gCol[i];
                    blueMatrix[j, i] = bCol[i];
                }
            }

            for (int j = 0; j < inputImg.Width; ++j)
            {
                for (int i = 0; i < inputImg.Height; ++i)
                {
                    int r = Clamp(Convert.ToInt32(redMatrix[j, i].Real), 0, 255);
                    int g = Clamp(Convert.ToInt32(greenMatrix[j, i].Real), 0, 255);
                    int b = Clamp(Convert.ToInt32(blueMatrix[j, i].Real), 0, 255);

                    inputImg.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            }

            pBInput.Image = inputImg.Bitmap;

            this.Cursor = Cursors.Default;
            timer.Stop();
            label3.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ShapeList.Count < 1)
            {
                MessageBox.Show("No mask shape", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            DrawMask();

            this.Cursor = Cursors.Default;
            timer.Stop();
            label3.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void DrawMask()// draws mask
        {
            int lineWidth;
            try
            {
                lineWidth = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                lineWidth = 8;
            }

            using (Graphics g = Graphics.FromImage(_maskImg))
            {
                if (checkBox1.Checked)
                {
                    g.FillRectangle(Brushes.White, 0, 0, _maskImg.Width, _maskImg.Height);
                }
                else
                {
                    g.FillRectangle(Brushes.Black, 0, 0, _maskImg.Width, _maskImg.Height);
                }

                for (int i = 0; i < ShapeList.Count; i++)
                {
                    Pen pen = new Pen(Color.White, lineWidth);
                    if (ShapeList[i].shape)//circle
                    {
                        if (checkBox1.Checked)
                        {
                            pen = new Pen(Color.Black, lineWidth);
                        }

                        g.DrawEllipse(pen, ShapeList[i].X, ShapeList[i].Y, ShapeList[i].Width - lineWidth, ShapeList[i].Width - lineWidth);
                    }
                    else//square
                    {
                        if (checkBox1.Checked)
                        {
                            pen = new Pen(Color.Black, lineWidth);
                        }

                        g.DrawRectangle(pen, ShapeList[i].X, ShapeList[i].Y, ShapeList[i].Width - lineWidth, ShapeList[i].Height - lineWidth);
                    }

                    if (checkBox2.Checked)
                    {
                        SolidBrush brush = new SolidBrush(Color.White);
                        if (ShapeList[i].shape)//circle
                        {
                            if (checkBox1.Checked)
                            {
                                brush = new SolidBrush(Color.Black);
                            }

                            g.FillEllipse(brush, ShapeList[i].X, ShapeList[i].Y, ShapeList[i].Width - lineWidth, ShapeList[i].Width - lineWidth);
                        }
                        else//square
                        {
                            if (checkBox1.Checked)
                            {
                                brush = new SolidBrush(Color.Black);
                            }

                            g.FillRectangle(brush, ShapeList[i].X, ShapeList[i].Y, ShapeList[i].Width - lineWidth, ShapeList[i].Height - lineWidth);
                        }
                    }
                }
            }

            pBMask.Image = _maskImg;
        }
        #endregion

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
            pBFourierDrawing.Image = _brightFourierImg;
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
