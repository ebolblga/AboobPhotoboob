using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImgApp_2_WinForms
{
    public partial class Form1 : Form
    {
        private List<Image> LoadedImages { get; set; }
        const int N = 20; //max amount of images
        public static Bitmap image = null;
        private int[] mode = new int[N];
        int[] opacityArray = Enumerable.Repeat(100, N).ToArray();
        bool theme = false; //0 dark theme, 1 light theme
        private List<Point> Points = new List<Point>();
        private List<Point> Points2 = new List<Point>();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            LoadedImages = new List<Image>();
            comboBox2.SelectedIndex = 0;
            channelBox.SelectedIndex = 0;
            var dark = new Bitmap(@"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\ImgApp_2_WinForms-master\lightThemeSmallest.png");
            themeBox1.Image = dark;
            //this.TopMost = true;
            //this.WindowState = FormWindowState.Maximized;

            Point point1 = new Point(200, 0);
            Point point2 = new Point(0, 200);
            Points.Add(point1);
            Points.Add(point2);
        }

        private void LoadImagesFromFolder(string[] paths) //загрузка изображений из выбранного файла
        {
            //LoadedImages = new List<Image>();
            foreach (var path in paths)
            {
                //string tempLocation = $@"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\ImgApp_2_WinForms-master\Images\{index}.jpg";
                var tempImage = Image.FromFile(path);
                LoadedImages.Add(tempImage);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//запись режима наложения в отдельный массив
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                var selectedIndex = LayerList.SelectedIndices[0];
                mode[selectedIndex] = comboBox1.SelectedIndex;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
                MessageBox.Show("Image is not selected", "Error");                
            }
        }

        private void LayerList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)//переключатель для режима наложения
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                var selectedIndex = LayerList.SelectedIndices[0];
                var invertedIndex = LoadedImages.Count - 1 - selectedIndex;
                Image selectedImage = LoadedImages[invertedIndex];
                ImageOutput.Image = selectedImage;
                comboBox1.SelectedIndex = mode[selectedIndex];
                channelBox.SelectedIndex = 0;

                opacityBar.Value = opacityArray[selectedIndex];
                opacity.Text = "Opacity: " + opacityBar.Value.ToString() + "%";

                if (autoHistogramToolStripMenuItem.Checked == true && histogrammToolStripMenuItem.Checked == true)
                {
                    histogramRender(sender, e);
                }
            }
        }

        private void bRender3_Click(object sender, EventArgs e)//СОВСЕМ улучшенная отрисовка режимов наложения
        {
            if (LoadedImages.Count >= 2)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                int Index = 1;
                Bitmap img1 = new Bitmap(LoadedImages[Index - 1]);
                Bitmap img2 = new Bitmap(LoadedImages[Index]);
                int modeIndex = LoadedImages.Count - 1 - Index;
                int indexedOpacity = Convert.ToInt32(opacityArray[modeIndex] * 2.55);
                Bitmap img_out = null;
                switch (mode[modeIndex])
                {
                    case 0:
                        img_out = Render.normalByteRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 1:
                        img_out = Render.additionByteRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 2:
                        img_out = Render.multiplyByteRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 3:
                        img_out = Render.averageRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 4:
                        img_out = Render.darkenRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 5:
                        img_out = Render.lightenRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 6:
                        img_out = Render.maskRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    default:
                        break;
                }

                for (; Index < LoadedImages.Count - 1; ++Index)
                {
                    img1 = new Bitmap(img_out);
                    img2 = new Bitmap(LoadedImages[Index + 1]);
                    modeIndex = LoadedImages.Count - 2 - Index;
                    indexedOpacity = Convert.ToInt32(opacityArray[modeIndex] * 2.55);
                    switch (mode[modeIndex])
                    {
                        case 0:
                            img_out = Render.normalByteRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 1:
                            img_out = Render.additionByteRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 2:
                            img_out = Render.multiplyByteRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 3:
                            img_out = Render.averageRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 4:
                            img_out = Render.darkenRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 5:
                            img_out = Render.lightenRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 6:
                            img_out = Render.maskRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        default:
                            break;
                    }
                }

                ImageOutput.Image = img_out;
                SavetoLayerList(img_out);

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Needs at least 2 images in a project", "Error");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)//загрузка файлов в проект
        {
            if (LoadedImages.Count == 0)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                //дефолтная папка с картинками
                folderBrowser.SelectedPath = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    //выделенная директория
                    var selectedDirectory = folderBrowser.SelectedPath;
                    //пути картинок из выделенной директории
                    var imagePaths = Directory.GetFiles(selectedDirectory);
                    //загрузка картинок из их путей
                    LoadImagesFromFolder(imagePaths);

                    //инициализация списка картинок
                    ImageList images = new ImageList();
                    images.ImageSize = new Size(77, 80);

                    foreach (var image in LoadedImages)
                    {
                        images.Images.Add(image);
                    }
                    //добавляю картинки в список слоёв
                    LayerList.LargeImageList = images;

                    for (int itemIndex = LoadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                        LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                }
            }
            else if (LoadedImages.Count <= N / 2)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                //дефолтная папка с картинками
                folderBrowser.SelectedPath = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    //выделенная директория
                    var selectedDirectory = folderBrowser.SelectedPath;
                    //пути картинок из выделенной директории
                    var imagePaths = Directory.GetFiles(selectedDirectory);
                    //загрузка картинок из их путей
                    LoadImagesFromFolder(imagePaths);

                    foreach (var path in imagePaths)
                    {
                        for (int i = mode.Length - 1; i > 0; --i)
                        {
                            mode[i] = mode[i - 1];
                            opacityArray[i] = opacityArray[i - 1];
                        }
                        mode[0] = 0;
                        opacityArray[0] = 100;
                    }

                    //очищаю список картинок
                    LayerList.Items.Clear();

                    //инициализация списка картинок
                    ImageList images = new ImageList();
                    images.ImageSize = new Size(77, 80);
                    foreach (var image in LoadedImages)
                        images.Images.Add(image);

                    //добавляю картинки в список слоёв
                    LayerList.LargeImageList = images;

                    //int itemIndex = 0; itemIndex < LoadedImages.Count; ++itemIndex
                    for (int itemIndex = LoadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                        LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                }
            }
            else if (LoadedImages.Count > N / 2)
                MessageBox.Show("Too many images in a project, careful", "Error");
        }

        private void SavetoLayerList(Bitmap img_out)//перезагрузка списка слоёв
        {
            this.Cursor = Cursors.WaitCursor;
            LoadedImages.Add(img_out);

            //очищаю список картинок
            LayerList.Items.Clear();

            //инициализация списка картинок
            ImageList images = new ImageList();
            images.ImageSize = new Size(77, 80);
            foreach (var image in LoadedImages)
                images.Images.Add(image);

            //добавляю картинки в список слоёв
            LayerList.LargeImageList = images;

            //int itemIndex = 0; itemIndex < LoadedImages.Count; ++itemIndex
            for (int itemIndex = LoadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));

            for (int i = mode.Length - 1; i > 0; --i)
            {
                mode[i] = mode[i - 1];
                opacityArray[i] = opacityArray[i - 1];
            }
            mode[0] = 0;
            opacityArray[0] = 100;

            this.Cursor = Cursors.Default;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)//сохранение изображения
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                using SaveFileDialog saveFileFialog = new SaveFileDialog();
                saveFileFialog.InitialDirectory = @"..\..\..\..\img_out";
                //saveFileFialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
                saveFileFialog.Filter = "JPG images (*.jpg)|*.jpg";
                saveFileFialog.RestoreDirectory = true;

                var img_out = new Bitmap(LoadedImages[LoadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                Graphics graphics = Graphics.FromImage(img_out);

                Rectangle rect = new Rectangle(50, 50, ClientSize.Width - 10, ClientSize.Height - 10);

                //рисуем водный знак
                Font drawFont = new Font("NewZelek", 72);
                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(64, 255, 255, 255));
                graphics.DrawString("СТАНКИН", drawFont, semiTransBrush, 30, img_out.Height / 2);

                //сохраняем
                if (saveFileFialog.ShowDialog() == DialogResult.OK)
                {
                    if (img_out != null)
                    {
                        img_out.Save(saveFileFialog.FileName);
                    }
                }
                drawFont.Dispose();
                semiTransBrush.Dispose();
                img_out.Dispose();
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)//завершение программы
        {
            this.Close();
        }

        private void bClear_Click(object sender, EventArgs e)//удаление слоя/слоёв
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                ImageOutput.Image = null;

                var puk = LayerList.SelectedIndices[0];
                var puk2 = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                for (int i = LayerList.SelectedIndices[0]; i < mode.Length - 1; ++i)
                {
                    mode[i] = mode[i + 1];
                    opacityArray[i] = opacityArray[i + 1];
                }
                    

                LoadedImages.RemoveAt(LoadedImages.Count - 1 - LayerList.SelectedIndices[0]);
                LayerList.Items.RemoveAt(LayerList.SelectedIndices[0]);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("No images are selected, you want to delete all?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < mode.Length; ++i)
                    {
                        mode[i] = 0;
                        opacityArray[i] = 0;
                    }
                    ImageOutput.Image = null;
                    LoadedImages.Clear();
                    LayerList.Items.Clear();
                }
            }
        }

        private void brightnessContrastToolStripMenuItem_Click(object sender, EventArgs e)//вызов второй формы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                image = new Bitmap(LoadedImages[LoadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
                Form2 BrightnessForm = new Form2(this);
                BrightnessForm.ShowDialog();
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void JPEGingToolStripMenuItem_Click(object sender, EventArgs e)//джепегирует изображение
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img = new Bitmap(LoadedImages[LoadedImages.Count - 1 - selectedIndex]);

                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 5L);
                img.Save(@"..\..\..\..\out.jpg", GetEncoder(ImageFormat.Jpeg), encoderParameters);

                var img2 = new Bitmap(@"..\..\..\..\out.jpg");
                img = new Bitmap(img2);
                img2.Dispose();
                ImageOutput.Image = img;
                SavetoLayerList(img);

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void themeBox1_Click(object sender, EventArgs e)//переключатель темы
        {
            if (theme == false)
            {
                var light = new Bitmap("..\\..\\..\\darkThemeSmallest.png");
                themeBox1.Image = light;
                theme = true;

                this.BackColor = Color.FromArgb(240, 240, 240);
                LayerList.BackColor = Color.FromArgb(219, 219, 219);
                LayerList.ForeColor = Color.FromArgb(47, 47, 47);
                histogram.BackColor = Color.FromArgb(219, 219, 219);
                histogram.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(219, 219, 219);
                credits.ForeColor = Color.FromArgb(47, 47, 47);
                debug.ForeColor = Color.FromArgb(47, 47, 47);
                opacity.ForeColor = Color.FromArgb(47, 47, 47);
                label1.ForeColor = Color.FromArgb(47, 47, 47);
                label2.ForeColor = Color.FromArgb(47, 47, 47);
                curveEditBox.BackColor = Color.FromArgb(219, 219, 219);
            }
            else
            {
                var dark = new Bitmap("..\\..\\..\\lightThemeSmallest.png");
                themeBox1.Image = dark;
                theme = false;

                this.BackColor = Color.FromArgb(47, 47, 47);
                LayerList.BackColor = Color.FromArgb(69, 69, 69);
                LayerList.ForeColor = Color.FromArgb(224, 224, 224);
                histogram.BackColor = Color.FromArgb(69, 69, 69);
                histogram.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(69, 69, 69);
                credits.ForeColor = Color.FromArgb(224, 224, 224);
                debug.ForeColor = Color.FromArgb(224, 224, 224);
                opacity.ForeColor = Color.FromArgb(224, 224, 224);
                label1.ForeColor = Color.FromArgb(224, 224, 224);
                label2.ForeColor = Color.FromArgb(224, 224, 224);
                curveEditBox.BackColor = Color.FromArgb(69, 69, 69);

                //photoshop theme
                //this.BackColor = Color.FromArgb(38, 38, 38);
                //LayerList.BackColor = Color.FromArgb(83, 83, 83);
                //LayerList.ForeColor = Color.FromArgb(221, 221, 221);
                //histogram.BackColor = Color.FromArgb(83, 83, 83);
                //histogram.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(83, 83, 83);
                //credits.ForeColor = Color.FromArgb(221, 221, 221);
                //debug.ForeColor = Color.FromArgb(221, 221, 221);
                //opacity.ForeColor = Color.FromArgb(221, 221, 221);
            }
        }

        private void transparencyBar_Scroll(object sender, EventArgs e)//скрол бар прозрачности
        {
            opacity.Text = "Opacity: " + opacityBar.Value.ToString() + "%";
            if (LayerList.SelectedIndices.Count > 0)
            {
                var selectedIndex = LayerList.SelectedIndices[0];
                opacityArray[selectedIndex] = opacityBar.Value;
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)//открытия одного изображения
        {
            if (LoadedImages.Count == 0)
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.InitialDirectory = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images 2"; //C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images 2
                folderBrowser.Filter = "Image Files(**.JPG;*.PNG)|*.JPG;*.PNG|All files (*.*)|*.*";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string path = folderBrowser.FileName;
                        var tempImage = Image.FromFile(path);
                        LoadedImages.Add(tempImage);

                        ImageList images = new ImageList();
                        images.ImageSize = new Size(77, 80);

                        foreach (var image in LoadedImages)
                            images.Images.Add(image);

                        LayerList.LargeImageList = images;

                        for (int itemIndex = LoadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                            LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                    }
                    catch
                    {
                        DialogResult rezult = MessageBox.Show("Impossible to open selected file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (LoadedImages.Count < N)
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.InitialDirectory = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images 2";
                folderBrowser.Filter = "Image Files(**.JPG;*.PNG)|*.JPG;*.PNG|All files (*.*)|*.*";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string path = folderBrowser.FileName;
                        var tempImage = Image.FromFile(path);
                        LoadedImages.Add(tempImage);

                        for (int i = mode.Length - 1; i > 0; --i)
                        {
                            mode[i] = mode[i - 1];
                            opacityArray[i] = opacityArray[i - 1];
                        }
                        mode[0] = 0;
                        opacityArray[0] = 100;

                        LayerList.Items.Clear();

                        ImageList images = new ImageList();

                        images.ImageSize = new Size(77, 80);
                        foreach (var image in LoadedImages)
                            images.Images.Add(image);

                        LayerList.LargeImageList = images;

                        for (int itemIndex = LoadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                            LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                    }
                    catch
                    {
                        DialogResult rezult = MessageBox.Show("Impossible to open selected file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                    ////дефолтная папка с картинками
                    //folderBrowser.SelectedPath = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images";
                    //if (folderBrowser.ShowDialog() == DialogResult.OK)
                    //{
                    //    //выделенная директория
                    //    var selectedDirectory = folderBrowser.SelectedPath;
                    //    //пути картинок из выделенной директории
                    //    var imagePaths = Directory.GetFiles(selectedDirectory);
                    //    //загрузка картинок из их путей
                    //    LoadImagesFromFolder(imagePaths);

                    //    foreach (var path in imagePaths)
                    //    {
                    //        for (int i = mode.Length - 1; i > 0; --i)
                    //        {
                    //            mode[i] = mode[i - 1];
                    //            opacityArray[i] = opacityArray[i - 1];
                    //        }
                    //        mode[0] = 0;
                    //        opacityArray[0] = 100;
                    //    }

                    //    //очищаю список картинок
                    //    LayerList.Items.Clear();

                    //    //инициализация списка картинок
                    //    ImageList images = new ImageList();
                    //    images.ImageSize = new Size(77, 80);
                    //    foreach (var image in LoadedImages)
                    //        images.Images.Add(image);

                    //    //добавляю картинки в список слоёв
                    //    LayerList.LargeImageList = images;

                    //    //int itemIndex = 0; itemIndex < LoadedImages.Count; ++itemIndex
                    //    for (int itemIndex = LoadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                    //        LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                    //}
                }
            }
            else if (LoadedImages.Count >= N)
                MessageBox.Show("Too many images in a project, careful", "Error");
        }

        private float imageResize(Image image)//изменение размера изображения не портя соотношение сторон
        {
            int originalWidth = image.Width;
            int originalHeight = image.Height;
            float maxWidth = 77;
            float maxHeight = 80;

            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;

            float ratio = Math.Min(ratioX, ratioY);

            return ratio;
        }

        private void channelBox_SelectionChangeCommitted(object sender, EventArgs e)//RGB каналы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img = new Bitmap(LoadedImages[LoadedImages.Count - 1 - selectedIndex]);
                int w = img.Width;
                int h = img.Height;
                for (int i = 0; i < h; ++i)
                {
                    for (int j = 0; j < w; ++j)
                    {
                        var pix = img.GetPixel(j, i);
                        switch (channelBox.SelectedIndex)
                        {
                            case 0:
                                pix = Color.FromArgb(pix.R, pix.G, pix.B);
                                break;

                            case 1:
                                pix = Color.FromArgb(pix.R, 0, 0);
                                break;

                            case 2:
                                pix = Color.FromArgb(0, pix.G, 0);
                                break;

                            case 3:
                                pix = Color.FromArgb(0, 0, pix.B);
                                break;

                            case 4:
                                var brightness = Color.FromArgb(pix.R, pix.G, pix.B).GetBrightness();
                                int pixcolor = Convert.ToInt32(brightness * 255);
                                pix = Color.FromArgb(pixcolor, pixcolor, pixcolor);
                                break;

                            default:
                                break;
                        }
                        img.SetPixel(j, i, pix);
                    }
                }
                ImageOutput.Image = img;

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)//копирование изображения
        {
            if (ImageOutput.Image != null)
                Clipboard.SetImage(ImageOutput.Image);
        }

        #region histogram
        private void histogramRender2(object sender, EventArgs e)//СОВСЕМ улучшенная отрисовка гистограммы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    this.Cursor = Cursors.WaitCursor;

                    histogram.Series[0].Points.Clear();
                    histogram.Series.Clear();

                    int[] RpointsArray = new int[256];
                    int[] GpointsArray = new int[256];
                    int[] BpointsArray = new int[256];

                    int index = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                    var img1 = new Bitmap(LoadedImages[index]);

                    byte[] img1_bytes = new byte[0];

                    using (Bitmap _tmp = new Bitmap(img1.Width, img1.Height, PixelFormat.Format24bppRgb))
                    {
                        _tmp.SetResolution(img1.HorizontalResolution, img1.VerticalResolution); //устанавливаем DPI такой же как у исходного

                        using (var g = Graphics.FromImage(_tmp)) //рисуем исходное изображение на временном, "типо-копируем"
                        {
                            g.DrawImageUnscaled(img1, 0, 0);
                        }
                        img1_bytes = getImgBytes(_tmp); //получаем байты изображения, см. описание ф-ции 
                    }

                    //Parallel.For(0, (img1.Width * img1.Height * 3) - 2, i =>
                    // {
                    //     if (i % 3 != 0)
                    //         return;
                    //     RpointsArray[img1_bytes[i + 2]]++;
                    //     GpointsArray[img1_bytes[i + 1]]++;
                    //     BpointsArray[img1_bytes[i]]++;
                    // });

                    //for (int i = 0; i < img1.Width * img1.Height * 3 - 2; i += 3)
                    //{
                    //    int r = img1_bytes[i + 2];
                    //    int g = img1_bytes[i + 1];
                    //    int b = img1_bytes[i];

                    //    RpointsArray[r]++;
                    //    GpointsArray[g]++;
                    //    BpointsArray[b]++;
                    //    if (i >= (img1.Width * 3)-3)
                    //        continue;
                    //}

                    for (int i = 0; i < img1.Width * img1.Height * 3; i++)
                    {
                        if (i % 3 == 0)
                            BpointsArray[img1_bytes[i]]++;

                        if (i % 3 == 1)
                            GpointsArray[img1_bytes[i]]++;

                        if (i % 3 == 2)
                            RpointsArray[img1_bytes[i]]++;
                    }

                    img1.Dispose();

                    ChartArea areaR = new ChartArea();
                    histogram.ChartAreas.Add(areaR);

                    ChartArea areaG = new ChartArea();
                    histogram.ChartAreas.Add(areaG);

                    ChartArea areaB = new ChartArea();
                    histogram.ChartAreas.Add(areaB);

                    Series seriesR = new Series();
                    seriesR.ChartType = SeriesChartType.Column;
                    seriesR.Name = "seriesR";
                    seriesR.ChartArea = areaR.Name;
                    histogram.Series.Add(seriesR);

                    Series seriesG = new Series();
                    seriesG.ChartType = SeriesChartType.Column;
                    seriesG.Name = "seriesG";
                    seriesG.ChartArea = areaG.Name;
                    histogram.Series.Add(seriesG);

                    Series seriesB = new Series();
                    seriesB.ChartType = SeriesChartType.Column;
                    seriesB.Name = "seriesB";
                    seriesB.ChartArea = areaB.Name;
                    histogram.Series.Add(seriesB);

                    for (int i = 0; i <= 255; ++i)
                    {
                        histogram.Series["seriesR"].Points.AddXY(i, RpointsArray[i]);
                        histogram.Series["seriesG"].Points.AddXY(i, GpointsArray[i]);
                        histogram.Series["seriesB"].Points.AddXY(i, BpointsArray[i]);
                    }

                    areaR.RecalculateAxesScale();
                    areaG.RecalculateAxesScale();
                    areaB.RecalculateAxesScale();

                    var max = areaR.AxisY.Maximum;
                    if (areaG.AxisY.Maximum > max)
                        max = areaG.AxisY.Maximum;
                    if (areaB.AxisY.Maximum > max)
                        max = areaB.AxisY.Maximum;

                    areaG.AxisY.Maximum = max;
                    areaG.AxisY.Maximum = max;
                    areaB.AxisY.Maximum = max;

                    histogram.ChartAreas[0].AxisX.Minimum = 0;
                    histogram.ChartAreas[0].AxisX.Maximum = 255;
                    histogram.ChartAreas[1].AxisX.Minimum = 0;
                    histogram.ChartAreas[1].AxisX.Maximum = 255;
                    histogram.ChartAreas[2].AxisX.Minimum = 0;
                    histogram.ChartAreas[2].AxisX.Maximum = 255;
                    histogram.Series[0]["PointWidth"] = "1";
                    histogram.Series[1]["PointWidth"] = "1";
                    histogram.Series[2]["PointWidth"] = "1";

                    areaR.Position = new ElementPosition(0, 0, 100, 100);
                    areaG.Position = new ElementPosition(0, 0, 100, 100);
                    areaB.Position = new ElementPosition(0, 0, 100, 100);

                    areaR.AxisX.IsMarginVisible = false;
                    areaG.AxisX.IsMarginVisible = false;
                    areaB.AxisX.IsMarginVisible = false;

                    seriesR.Color = Color.FromArgb(128, 255, 50, 30);
                    seriesG.Color = Color.FromArgb(128, 100, 255, 60);
                    seriesB.Color = Color.FromArgb(128, 40, 40, 255);

                    areaR.BackColor = Color.Transparent;
                    areaG.BackColor = Color.Transparent;
                    areaB.BackColor = Color.Transparent;

                    this.Cursor = Cursors.Default;
                    timer.Stop();
                    debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
                }
                else
                    button1_Click(sender, e);
            }
            else
                MessageBox.Show("Image is not selected", "Error");

        }

        private void histogramRender(object sender, EventArgs e)//улучшенная отрисовка гистограммы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    this.Cursor = Cursors.WaitCursor;

                    histogram.Series[0].Points.Clear();
                    histogram.Series.Clear();

                    int[] RpointsArray = new int[256];
                    int[] GpointsArray = new int[256];
                    int[] BpointsArray = new int[256];

                    int index = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                    var img = new Bitmap(LoadedImages[index]);
                    for (int i = 0; i < img.Height; ++i)
                        for (int j = 0; j < img.Width; ++j)
                        {
                            var pix = img.GetPixel(j, i);
                            RpointsArray[pix.R]++;
                            GpointsArray[pix.G]++;
                            BpointsArray[pix.B]++;
                        }
                    img.Dispose();

                    ChartArea areaR = new ChartArea();
                    histogram.ChartAreas.Add(areaR);

                    ChartArea areaG = new ChartArea();
                    histogram.ChartAreas.Add(areaG);

                    ChartArea areaB = new ChartArea();
                    histogram.ChartAreas.Add(areaB);

                    Series seriesR = new Series();
                    seriesR.ChartType = SeriesChartType.Column;
                    seriesR.Name = "seriesR";
                    seriesR.ChartArea = areaR.Name;
                    histogram.Series.Add(seriesR);

                    Series seriesG = new Series();
                    seriesG.ChartType = SeriesChartType.Column;
                    seriesG.Name = "seriesG";
                    seriesG.ChartArea = areaG.Name;
                    histogram.Series.Add(seriesG);

                    Series seriesB = new Series();
                    seriesB.ChartType = SeriesChartType.Column;
                    seriesB.Name = "seriesB";
                    seriesB.ChartArea = areaB.Name;
                    histogram.Series.Add(seriesB);

                    for (int i = 0; i <= 255; ++i)
                    {
                        histogram.Series["seriesR"].Points.AddXY(i, RpointsArray[i]);
                        histogram.Series["seriesG"].Points.AddXY(i, GpointsArray[i]);
                        histogram.Series["seriesB"].Points.AddXY(i, BpointsArray[i]);
                    }

                    areaR.RecalculateAxesScale();
                    areaG.RecalculateAxesScale();
                    areaB.RecalculateAxesScale();

                    var max = areaR.AxisY.Maximum;
                    if (areaG.AxisY.Maximum > max)
                        max = areaG.AxisY.Maximum;
                    if (areaB.AxisY.Maximum > max)
                        max = areaB.AxisY.Maximum;

                    areaG.AxisY.Maximum = max;
                    areaG.AxisY.Maximum = max;
                    areaB.AxisY.Maximum = max;

                    histogram.ChartAreas[0].AxisX.Minimum = 0;
                    histogram.ChartAreas[0].AxisX.Maximum = 255;
                    histogram.ChartAreas[1].AxisX.Minimum = 0;
                    histogram.ChartAreas[1].AxisX.Maximum = 255;
                    histogram.ChartAreas[2].AxisX.Minimum = 0;
                    histogram.ChartAreas[2].AxisX.Maximum = 255;
                    histogram.Series[0]["PointWidth"] = "1";
                    histogram.Series[1]["PointWidth"] = "1";
                    histogram.Series[2]["PointWidth"] = "1";

                    areaR.Position = new ElementPosition(0, 0, 100, 100);
                    areaG.Position = new ElementPosition(0, 0, 100, 100);
                    areaB.Position = new ElementPosition(0, 0, 100, 100);

                    areaR.AxisX.IsMarginVisible = false;
                    areaG.AxisX.IsMarginVisible = false;
                    areaB.AxisX.IsMarginVisible = false;

                    seriesR.Color = Color.FromArgb(128, 255, 50, 30);
                    seriesG.Color = Color.FromArgb(128, 100, 255, 60);
                    seriesB.Color = Color.FromArgb(128, 40, 40, 255);

                    areaR.BackColor = Color.Transparent;
                    areaG.BackColor = Color.Transparent;
                    areaB.BackColor = Color.Transparent;

                    this.Cursor = Cursors.Default;
                    timer.Stop();
                    debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
                }
                else
                    button1_Click(sender, e);
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void button1_Click(object sender, EventArgs e)//отрисовка гистограммы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                histogram.Series[0].Points.Clear();
                histogram.Series.Clear();

                int[] pointsArray = new int[256];

                var index = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                var img = new Bitmap(LoadedImages[index]);
                for (int i = 0; i < img.Height; ++i)
                {
                    for (int j = 0; j < img.Width; ++j)
                    {
                        var pix = img.GetPixel(j, i);
                        switch (comboBox2.SelectedIndex)
                        {
                            case 1:
                                pointsArray[pix.R]++;
                                break;

                            case 2:
                                pointsArray[pix.G]++;
                                break;

                            case 3:
                                pointsArray[pix.B]++;
                                break;

                            case 4:
                                int brightness = (int)Math.Round(Color.FromArgb(pix.R, pix.G, pix.B).GetBrightness() * 255);
                                pointsArray[brightness]++;
                                break;

                            default:
                                break;
                        }
                    }
                }
                img.Dispose();

                Series series = new Series();
                series.ChartType = SeriesChartType.Column;
                series.Name = "series1";
                histogram.Series.Add(series);

                for (int i = 0; i <= 255; ++i)
                    histogram.Series["series1"].Points.AddXY(i, pointsArray[i]);

                histogram.ChartAreas[0].AxisX.Minimum = 0;
                histogram.ChartAreas[0].AxisX.Maximum = 255;
                histogram.Series[0]["PointWidth"] = "1";

                switch (comboBox2.SelectedIndex)
                {
                    case 1:
                        series.Color = Color.FromArgb(255, 50, 30);
                        break;

                    case 2:
                        series.Color = Color.FromArgb(100, 255, 60);
                        break;

                    case 3:
                        series.Color = Color.FromArgb(40, 40, 255);
                        break;

                    case 4:
                        if (theme == true)
                            series.Color = Color.FromArgb(47, 47, 47);
                        break;
                    default:
                        break;
                }

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void autoHistogramToolStripMenuItem_Click(object sender, EventArgs e)//автоматический расчёт гистограммы
        {
            if (autoHistogramToolStripMenuItem.Checked == true)
                autoHistogramToolStripMenuItem.Checked = false;
            else
                autoHistogramToolStripMenuItem.Checked = true;
        }

        private void histogrammToolStripMenuItem_Click(object sender, EventArgs e)//прячет/показывает окно гистограммы
        {
            if (histogrammToolStripMenuItem.Checked == true)
            {
                histogrammToolStripMenuItem.Checked = false;
                histogram.Visible = false;
                comboBox2.Visible = false;
                button3.Visible = false;
                ImageOutput.Size = new Size(ImageOutput.Width, Convert.ToInt32(ImageOutput.Height * 1.25));
            }
            else
            {
                histogrammToolStripMenuItem.Checked = true;
                histogram.Visible = true;
                comboBox2.Visible = true;
                button3.Visible = true;
                ImageOutput.Size = new Size(ImageOutput.Width, Convert.ToInt32(ImageOutput.Height * 0.8));
            }
        }
        #endregion

        #region binarization
        private void fToolStripMenuItem_Click(object sender, EventArgs e)//банальная бинаризация
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img1 = new Bitmap(LoadedImages[LoadedImages.Count - 1 - selectedIndex]);
                int w = img1.Width;
                int h = img1.Height;

                var img_out = new Bitmap(w, h);
                //переменная порога
                float average = 0;
                //первый проход для нахождения среднего значения порога
                for (int i = 0; i < h; ++i)
                {
                    for (int j = 0; j < w; ++j)
                    {
                        var pix = img1.GetPixel(j, i);
                        average += Color.FromArgb(pix.R, pix.G, pix.B).GetBrightness();
                    }
                }
                average /= w * h;

                for (int i = 0; i < h; ++i)
                {
                    for (int j = 0; j < w; ++j)
                    {
                        var pix = img1.GetPixel(j, i);
                        if (Color.FromArgb(pix.R, pix.G, pix.B).GetBrightness() > average)
                            pix = Color.FromArgb(255, 255, 255);
                        else
                            pix = Color.FromArgb(0, 0, 0);
                        img_out.SetPixel(j, i, pix);
                    }
                }
                ImageOutput.Image = img_out;
                SavetoLayerList(img_out);

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }
        #endregion

        #region curve
        private void bCurve_Click(object sender, EventArgs e)//очистка кривой
        {
            Points.Clear();
            Point start = new Point(200, 0);
            Point end = new Point(0, 200);
            Points.Add(start);
            Points.Add(end);
            curveEditBox.Refresh();
        }

        private void curveEditBox_Paint(object sender, PaintEventArgs e)//рисование кривой
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //рисуем точки
            foreach (Point point in Points)
                e.Graphics.FillEllipse(Brushes.Red, point.X - 3, point.Y - 3, 5, 5);
            if (Points.Count < 2) return;

            //рисуем кривую
            if (theme == false)
                e.Graphics.DrawCurve(Pens.LightGray, Points.ToArray());
            else
                e.Graphics.DrawCurve(Pens.Black, Points.ToArray());

        }

        private void curveEditBox_MouseClick(object sender, MouseEventArgs e)//движение точек
        {
            //var min = e.Location.X - 8;
            //var max = e.Location.X + 8;

            //for (int i = 0; i < Points.Count; ++i)
            //    if (Points[i].X > min && Points[i].X < max)
            //    {
            //        Points[i] = e.Location;
            //        break;
            //    }
            //Points.Sort((p1, p2) => (p1.X.CompareTo(p2.X)));
            //curveEditBox.Refresh();
        }

        private void curveEditBox_MouseUp(object sender, MouseEventArgs e)//добавление точек
        {
            //for (int i = 0; i < Points.Count; ++i)
            //    if (Points[i].X == e.Location.X)
            //        return;

            for (int i = 0; i < Points.Count; ++i)
                if ((Points[i].X + 10 > e.Location.X) && (Points[i].X - 10 < e.Location.X))
                    return;

            Points.Add(e.Location);
            Points.Sort((p1, p2) => (p1.X.CompareTo(p2.X)));
            Refresh();
            curveEditBox.Refresh();
        }

        private void curveToolStripMenuItem_Click(object sender, EventArgs e)//дисплэй кривой
        {
            if (curveToolStripMenuItem.Checked == true)
            {
                curveToolStripMenuItem.Checked = false;
                curveEditBox.Visible = false;
                bCurve.Visible = false;
                bApplyCurve.Visible = false;
            }
            else
            {
                curveToolStripMenuItem.Checked = true;
                curveEditBox.Visible = true;
                bCurve.Visible = true;
                bApplyCurve.Visible = true;
            }
        }

        private void bApplyCurve_Click(object sender, EventArgs e)//отрисовка изображения с кривыми
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img = new Bitmap(LoadedImages[LoadedImages.Count - 1 - selectedIndex]);

                var img_out = new Bitmap(img.Width, img.Height);

                //for (int i = 0; i < img.Height; ++i)
                //{
                //    for (int j = 0; j < img.Width; ++j)
                //    {
                //        var pix = img.GetPixel(j, i);
                //        var bruh = (double)pix.R / 255;
                //        int r = Convert.ToInt32(255 * Math.Pow((double)pix.R / 255, 2));
                //        int g = Convert.ToInt32(255 * Math.Pow((double)pix.G / 255, 2));
                //        int b = Convert.ToInt32(255 * Math.Pow((double)pix.B / 255, 2));
                //        pix = Color.FromArgb(r, g, b);
                //        img_out.SetPixel(j, i, pix);
                //    }
                //}
                Points2.Add(Points[0]);
                int j = 1;
                for (int i = 0; i < 255;)
                {
                    while (i < Points[j].X && i <= 255)
                    {
                        Points2.Add(new Point(Convert.ToInt32(Points[j - 1].X + Points[j].X / 2), Convert.ToInt32(Points[j - 1].Y + Points[j].Y / 2)));
                        i++;
                    }
                    j++;
                }

                for (int i = 0; i < img.Height; ++i)
                {
                    for (int j2 = 0; j2 < img.Width; ++j2)
                    {
                        var pix = img.GetPixel(j2, i);

                        int r = Clamp(Points2[pix.R].Y, 0, 255);
                        int g = Clamp(Points2[pix.G].Y, 0, 255);
                        int b = Clamp(Points2[pix.B].Y, 0, 255);


                        pix = Color.FromArgb(r, g, b);
                        img_out.SetPixel(j2, i, pix);
                    }
                }


                ImageOutput.Image = img_out;
                SavetoLayerList(img_out);

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }
        #endregion

        #region helper functions
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
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

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        #endregion
    }
}
