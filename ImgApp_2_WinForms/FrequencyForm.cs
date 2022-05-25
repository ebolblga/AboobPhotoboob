namespace ImgApp_2_WinForms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class FrequencyForm : Form
    {
        private Bitmap _inputImg;
        private Bitmap img_fourier;
        private Bitmap mask;

        public FrequencyForm(Image img)
        {
            InitializeComponent();
            _inputImg = new Bitmap(img);
        }

        private void FrequencyForm_Load(object sender, EventArgs e)
        {
            pBInput.Image = _inputImg;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar2.Value.ToString();
        }

        private void bGenerate_Click(object sender, EventArgs e)
        {
            img_fourier = new Bitmap(_inputImg.Width, _inputImg.Height);
            using (Graphics g = Graphics.FromImage(img_fourier))
            {
                g.FillRectangle(Brushes.Black, 0, 0, _inputImg.Width, _inputImg.Height);
            }

            pBFourier.Image = img_fourier;
            mask = new Bitmap(img_fourier);
            pBMask.Image = mask;
        }

        private void pBMask_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            mask.Dispose();
            mask = new Bitmap(img_fourier);

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

            using (Graphics g = Graphics.FromImage(mask))
            {
                Pen blackPen = new Pen(Color.Yellow, lineWidth);
                g.DrawEllipse(blackPen, x, y, diameter - lineWidth, diameter - lineWidth);
            }

            pBMask.Image = mask;
        }
    }
}
