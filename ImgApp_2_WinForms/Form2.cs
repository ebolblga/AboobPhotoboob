namespace ImgApp_2_WinForms
{
    using System;
    using System.Windows.Forms;

    public partial class Form2 : Form
    {
        //Form1 OwnerForm;

        public Form2(Form1 ownerForm)
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "0";
            label3.Text = "0";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar2.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int w = Form1.Image.Width;
            int h = Form1.Image.Height;

            this.Close();
        }
    }
}
