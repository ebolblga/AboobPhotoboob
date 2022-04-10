using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgApp_2_WinForms
{
    public partial class FormSliderBinarization : Form
    {
        public Bitmap img { get; set; }
        public FormSliderBinarization(Form1 ownerForm)
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int w = img.Width;
            int h = img.Height;

            float threshold = (float)trackBar1.Value / 255;

            byte[] img_bytes = GetRGBValues(img);

            int imglength = w * h * 4;

            byte[] img_out_bytes = new byte[imglength];

            for (int i = 0; i < imglength - 2; i += 4)
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

            //Form1 f = new Form1();
            //f.ImageOutput.Image = img_out;
            Form1.image = img_out;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
    }
}
