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
        private const int N = 20;   //максимальное кол-во изображений (чтоб краша из-за недостатка памяти небыло)
        public static Bitmap image = null;
        private int[] mode = new int[N];    //массив режимов наложения слоёв
        private int[] opacityArray = Enumerable.Repeat(100, N).ToArray();   //массив прозрачности слоёв
        private bool theme = false; //0 dark theme, 1 light theme
        private List<Point> UserPoints = new List<Point>();
        private List<Point> Points4Spline = new List<Point>();
        private List<MyPoint> PointList = new List<MyPoint>();
        private List<MyPoint> ReversePointList = new List<MyPoint>();

        private double[] a = new double[0];
        private double[] b = new double[0];
        private double[] c = new double[0];
        private double[] d = new double[0];
        private double[] e = new double[0];
        private double[] f = new double[0];

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
            UserPoints.Add(point1);
            UserPoints.Add(point2);
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
                    histogramRender2(sender, e);
                }
            }
        }

        private void bRender_Click(object sender, EventArgs e)//СОВСЕМ улучшенная отрисовка режимов наложения
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
                        img_out = Render.averageByteRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 4:
                        img_out = Render.darkenByteRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 5:
                        img_out = Render.lightenByteRender(img1, img2, modeIndex, indexedOpacity);
                        break;

                    case 6:
                        img_out = Render.maskByteRender(img1, img2, modeIndex, indexedOpacity);
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
                            img_out = Render.averageByteRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 4:
                            img_out = Render.darkenByteRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 5:
                            img_out = Render.lightenByteRender(img1, img2, modeIndex, indexedOpacity);
                            break;

                        case 6:
                            img_out = Render.maskByteRender(img1, img2, modeIndex, indexedOpacity);
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

        private void transparencyBar_Scroll(object sender, EventArgs e)//скрол бар прозрачности
        {
            opacity.Text = "Opacity: " + opacityBar.Value.ToString() + "%";
            if (LayerList.SelectedIndices.Count > 0)
            {
                var selectedIndex = LayerList.SelectedIndices[0];
                opacityArray[selectedIndex] = opacityBar.Value;
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)//открытие одного изображения
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

                    histogram.Series.Clear();
                    histogram.ChartAreas.Clear();

                    int[] RpointsArray = new int[256];
                    int[] GpointsArray = new int[256];
                    int[] BpointsArray = new int[256];

                    int index = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                    var img = new Bitmap(LoadedImages[index]);

                    byte[] imgBytes = GetRGBValues(img);

                    for (int i = 0; i < img.Width * img.Height * 4 - 3; i += 4)
                    {
                        RpointsArray[imgBytes[i + 2]]++;
                        GpointsArray[imgBytes[i + 1]]++;
                        BpointsArray[imgBytes[i]]++;
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

                    areaR.AxisY.Maximum = max;
                    areaG.AxisY.Maximum = max;
                    areaB.AxisY.Maximum = max;

                    areaR.AxisX.Minimum = 0;
                    areaR.AxisX.Maximum = 255;
                    areaG.AxisX.Minimum = 0;
                    areaG.AxisX.Maximum = 255;
                    areaB.AxisX.Minimum = 0;
                    areaB.AxisX.Maximum = 255;
                    seriesR["PointWidth"] = "1";
                    seriesG["PointWidth"] = "1";
                    seriesB["PointWidth"] = "1";

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
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    this.Cursor = Cursors.WaitCursor;

                    histogram.Series.Clear();
                    histogram.ChartAreas.Clear();

                    int[] RpointsArray = new int[256];
                    int[] GpointsArray = new int[256];
                    int[] BpointsArray = new int[256];
                    int[] BrightnessArray = new int[256];

                    int index = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                    var img = new Bitmap(LoadedImages[index]);

                    byte[] imgBytes = GetRGBValues(img);

                    for (int i = 0; i < img.Width * img.Height * 4 - 3; i += 4)
                    {
                        RpointsArray[imgBytes[i + 2]]++;
                        GpointsArray[imgBytes[i + 1]]++;
                        BpointsArray[imgBytes[i]]++;
                        BrightnessArray[(int)Math.Round(Color.FromArgb(imgBytes[i + 2], imgBytes[i + 1], imgBytes[i]).GetBrightness() * 255)]++;
                    }

                    img.Dispose();

                    ChartArea areaR = new ChartArea();
                    histogram.ChartAreas.Add(areaR);

                    ChartArea areaG = new ChartArea();
                    histogram.ChartAreas.Add(areaG);

                    ChartArea areaB = new ChartArea();
                    histogram.ChartAreas.Add(areaB);

                    ChartArea areaBr = new ChartArea();
                    histogram.ChartAreas.Add(areaBr);

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

                    Series seriesBr = new Series();
                    seriesBr.ChartType = SeriesChartType.Column;
                    seriesBr.Name = "seriesBr";
                    seriesBr.ChartArea = areaBr.Name;
                    histogram.Series.Add(seriesBr);

                    for (int i = 0; i <= 255; ++i)
                        switch (comboBox2.SelectedIndex)
                        {
                            case 1:
                                histogram.Series["seriesR"].Points.AddXY(i, RpointsArray[i]);
                                break;

                            case 2:
                                histogram.Series["seriesG"].Points.AddXY(i, GpointsArray[i]);
                                break;

                            case 3:
                                histogram.Series["seriesB"].Points.AddXY(i, BpointsArray[i]);
                                break;

                            case 4:
                                histogram.Series["seriesBr"].Points.AddXY(i, BrightnessArray[i]);
                                break;

                            default:
                                break;
                        }

                    areaR.RecalculateAxesScale();
                    areaG.RecalculateAxesScale();
                    areaB.RecalculateAxesScale();
                    areaBr.RecalculateAxesScale();

                    var max = areaR.AxisY.Maximum;
                    if (areaG.AxisY.Maximum > max)
                        max = areaG.AxisY.Maximum;
                    if (areaB.AxisY.Maximum > max)
                        max = areaB.AxisY.Maximum;
                    if (areaBr.AxisY.Maximum > max)
                        max = areaBr.AxisY.Maximum;

                    areaR.AxisY.Maximum = max;
                    areaG.AxisY.Maximum = max;
                    areaB.AxisY.Maximum = max;
                    areaBr.AxisY.Maximum = max;

                    areaR.AxisX.Minimum = 0;
                    areaR.AxisX.Maximum = 255;
                    areaG.AxisX.Minimum = 0;
                    areaG.AxisX.Maximum = 255;
                    areaB.AxisX.Minimum = 0;
                    areaB.AxisX.Maximum = 255;
                    areaBr.AxisX.Minimum = 0;
                    areaBr.AxisX.Maximum = 255;
                    seriesR["PointWidth"] = "1";
                    seriesG["PointWidth"] = "1";
                    seriesB["PointWidth"] = "1";
                    seriesBr["PointWidth"] = "1";

                    areaR.Position = new ElementPosition(0, 0, 100, 100);
                    areaG.Position = new ElementPosition(0, 0, 100, 100);
                    areaB.Position = new ElementPosition(0, 0, 100, 100);
                    areaBr.Position = new ElementPosition(0, 0, 100, 100);

                    areaR.AxisX.IsMarginVisible = false;
                    areaG.AxisX.IsMarginVisible = false;
                    areaB.AxisX.IsMarginVisible = false;
                    areaBr.AxisX.IsMarginVisible = false;

                    seriesR.Color = Color.FromArgb(255, 255, 50, 30);
                    seriesG.Color = Color.FromArgb(255, 100, 255, 60);
                    seriesB.Color = Color.FromArgb(255, 40, 40, 255);
                    if (theme == true)
                        seriesBr.Color = Color.FromArgb(255, 47, 47, 47);

                    areaR.BackColor = Color.Transparent;
                    areaG.BackColor = Color.Transparent;
                    areaB.BackColor = Color.Transparent;
                    areaBr.BackColor = Color.Transparent;

                    this.Cursor = Cursors.Default;
                    timer.Stop();
                    debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";

                }
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//автоматический расчёт гистограммы при смене канала
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                if (autoHistogramToolStripMenuItem.Checked == true && histogrammToolStripMenuItem.Checked == true)
                    histogramRender2(sender, e);
            }
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
        private void curveEditBox_MouseUp(object sender, MouseEventArgs e)//добавление точек
        {
            //проверка что рядом нет точек
            for (int i = 0; i < UserPoints.Count; ++i)
                if ((UserPoints[i].X + 3 > e.Location.X) && (UserPoints[i].X - 3 < e.Location.X))
                    return;

            Point NewPoint = new Point(e.Location.X, e.Location.Y);
            UserPoints.Add(NewPoint);
            UserPoints.Sort((p1, p2) => (p1.X.CompareTo(p2.X)));
            RenderCubicSpline();
            curveEditBox.Refresh();
        }

        private void curveEditBox_MouseMove(object sender, MouseEventArgs e)//движение точек
        {
            if (e.Button == MouseButtons.Left)
            {
                if (UserPoints.Count <= 2) return;
                for (int i = 1; i < UserPoints.Count - 1; ++i)
                {
                    double dX = e.Location.X - UserPoints[i].X;
                    double dY = e.Location.Y - UserPoints[i].Y;
                    if (dX * dX + dY * dY < 49)
                    {
                        //UserPoints[minindex] = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
                        UserPoints[i] = new Point(e.Location.X, e.Location.Y);
                        break;
                    }
                }
                UserPoints.Sort((p1, p2) => (p1.X.CompareTo(p2.X)));
                RenderCubicSpline();
                curveEditBox.Refresh();
                return;
            }
        }

        private void RenderCubicSpline()//поиск точек для кривой
        {
            double[] x = new double[UserPoints.Count];
            double[] y = new double[UserPoints.Count];

            //for (int i = 1; i < UserPoints.Count; ++i)
            //    if (UserPoints[i].X == UserPoints[i - 1].X)
                    //UserPoints.RemoveAt(i);

            for (int i = 0; i < UserPoints.Count; ++i)
            {
                x[i] = UserPoints[i].X;
                y[i] = UserPoints[i].Y;
            }
            BuildSpline(x.ToArray(), y.ToArray(), UserPoints.Count);

            int n = 201;
            double[] xs = new double[n];
            double[] ys = new double[n];
            double stepSize = (UserPoints[UserPoints.Count - 1].X - UserPoints[0].X) / (n - 1);

            for (int i = 0; i < n; ++i)
                xs[i] = UserPoints[0].X + i * stepSize;
            PointList.Clear();
            for (int i = 0; i < n; ++i)
            {
                ys[i] = Interpolate(xs[i]);
                MyPoint NewPoint = new MyPoint(xs[i], ys[i]);
                PointList.Add(NewPoint);
            }
        }

        private void curveEditBox_Paint(object sender, PaintEventArgs e)//рисование кривой
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush brush1 = new SolidBrush(Color.FromArgb(200, 255, 13, 0));
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(200, 255, 255, 0));

            //рисуем точки
            //foreach (Point point in UserPoints)
            //{
            //    //e.Graphics.FillEllipse(Brushes.Red, point.X - 3, point.Y - 3, 5, 5);
            //    e.Graphics.FillRectangle(brush1, point.X - 4, point.Y - 4, 8, 8);
            //    e.Graphics.FillRectangle(brush2, point.X - 3, point.Y - 3, 6, 6);
            //}

            for (int i = 1; i < UserPoints.Count - 1; ++i)
            {
                e.Graphics.FillRectangle(brush1, UserPoints[i].X - 4, UserPoints[i].Y - 4, 8, 8);
                e.Graphics.FillRectangle(brush2, UserPoints[i].X - 3, UserPoints[i].Y - 3, 6, 6);
            }

            if (UserPoints.Count < 2) return;
            if (PointList.Count < 2)
            {
                e.Graphics.DrawCurve(Pens.LightGray, UserPoints.ToArray());
                return;
            }

            //рисуем кривую
            Points4Spline.Clear();
            for (int i = 0; i < PointList.Count; ++i)
            {
                Point NewPoint = new Point((int)Clamp(PointList[i].X, 0 , 199), (int)Clamp(PointList[i].Y, 0, 199));
                Points4Spline.Add(NewPoint);
            }

            if (theme == false)
                e.Graphics.DrawCurve(Pens.LightGray, Points4Spline.ToArray());
            else
                e.Graphics.DrawCurve(Pens.Black, Points4Spline.ToArray());
        }

        private void bApplyCurve2_Click(object sender, EventArgs e)//отрисовка изображения с кривыми
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                int index = LoadedImages.Count - 1 - LayerList.SelectedIndices[0];
                var img = new Bitmap(LoadedImages[index]);

                byte[] imgBytes = GetRGBValues(img);
                byte[] img_out_bytes = new byte[img.Width * img.Height * 4];

                ReversePointList.Clear();
                for (int i = 0; i < PointList.Count; ++i)
                {
                    MyPoint NewPoint = new MyPoint(PointList[i].X, 200 - PointList[i].Y);
                    ReversePointList.Add(NewPoint);
                }
                ReversePointList.Sort((p1, p2) => (p1.X.CompareTo(p2.X)));

                for (int i = 0; i < img.Width * img.Height * 4 - 3; i += 4)
                {
                    int newxR = Convert.ToInt32((double)imgBytes[i + 2] / 255 * 200);
                    int newxG = Convert.ToInt32((double)imgBytes[i + 1] / 255 * 200);
                    int newxB = Convert.ToInt32((double)imgBytes[i] / 255 * 200);

                    img_out_bytes[i + 2] = Convert.ToByte(Clamp((double)ReversePointList[newxR].Y * 1.275, 0, 255));
                    img_out_bytes[i + 1] = Convert.ToByte(Clamp((double)ReversePointList[newxG].Y * 1.275, 0, 255));
                    img_out_bytes[i] = Convert.ToByte(Clamp((double)ReversePointList[newxB].Y * 1.275, 0, 255));
                }

                Bitmap img_out = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppRgb);
                writeImageBytes(img_out, img_out_bytes);
                img.Dispose();

                ImageOutput.Image = img_out;
                SavetoLayerList(img_out);

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
                MessageBox.Show("Image is not selected", "Error");
        }

        private void bCurve_Click(object sender, EventArgs e)//очистка кривой
        {
            UserPoints.Clear();
            Points4Spline.Clear();
            PointList.Clear();
            Point start = new Point(200, 0);
            Point end = new Point(0, 200);
            UserPoints.Add(start);
            UserPoints.Add(end);
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

        #region Cubic spline math

        SplineTuple[] splines; // Сплайны

        private struct SplineTuple
        {
            public double a, b, c, d, x;
        }   // Структура, описывающая сплайн на каждом сегменте сетки

        public void BuildSpline(double[] x, double[] y, int n)
        {
            // x - узлы сетки, должны быть упорядочены по возрастанию, кратные узлы запрещены
            // y - значения функции в узлах сетки
            // n - количество узлов сетки

            // Инициализация массива сплайнов
            splines = new SplineTuple[n];
            for (int i = 0; i < n; ++i)
            {
                splines[i].x = x[i];
                splines[i].a = y[i];
            }
            splines[0].c = splines[n - 1].c = 0.0;

            // Решение СЛАУ относительно коэффициентов сплайнов c[i] методом прогонки для трехдиагональных матриц
            // Вычисление прогоночных коэффициентов - прямой ход метода прогонки
            double[] alpha = new double[n - 1];
            double[] beta = new double[n - 1];
            alpha[0] = beta[0] = 0.0;
            for (int i = 1; i < n - 1; ++i)
            {
                double hi = x[i] - x[i - 1];
                double hi1 = x[i + 1] - x[i];
                double A = hi;
                double C = 2.0 * (hi + hi1);
                double B = hi1;
                double F = 6.0 * ((y[i + 1] - y[i]) / hi1 - (y[i] - y[i - 1]) / hi);
                double z = (A * alpha[i - 1] + C);
                alpha[i] = -B / z;
                beta[i] = (F - A * beta[i - 1]) / z;
            }

            // Нахождение решения - обратный ход метода прогонки
            for (int i = n - 2; i > 0; --i)
            {
                splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
            }

            // По известным коэффициентам c[i] находим значения b[i] и d[i]
            for (int i = n - 1; i > 0; --i)
            {
                double hi = x[i] - x[i - 1];
                splines[i].d = (splines[i].c - splines[i - 1].c) / hi;
                splines[i].b = hi * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi;
            }
        }    // Построение сплайна

        public double Interpolate(double x)
        {
            if (splines == null)
            {
                return double.NaN; // Если сплайны ещё не построены - возвращаем NaN
            }

            int n = splines.Length;
            SplineTuple s;

            if (x <= splines[0].x) // Если x меньше точки сетки x[0] - пользуемся первым эл-тов массива
            {
                s = splines[0];
            }
            else if (x >= splines[n - 1].x) // Если x больше точки сетки x[n - 1] - пользуемся последним эл-том массива
            {
                s = splines[n - 1];
            }
            else // Иначе x лежит между граничными точками сетки - производим бинарный поиск нужного эл-та массива
            {
                int i = 0;
                int j = n - 1;
                while (i + 1 < j)
                {
                    int k = i + (j - i) / 2;
                    if (x <= splines[k].x)
                    {
                        j = k;
                    }
                    else
                    {
                        i = k;
                    }
                }
                s = splines[j];
            }

            double dx = x - s.x;
            // Вычисляем значение сплайна в заданной точке по схеме Горнера (в принципе, "умный" компилятор применил бы схему Горнера сам, но ведь не все так умны, как кажутся)
            return s.a + (s.b + (s.c / 2.0 + s.d * dx / 6.0) * dx) * dx;
        }   //нахождение f(x) после построения сплайна

        #endregion

        #endregion

        #region helper functions
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }//сжимает значения в выбранный промежуток

        private byte[] GetRGBValues(Bitmap bmp)//конвертирует Bitmap в byte[]
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
        }//настройки кодека Jpeg для фильтра

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)//завершение программы
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

        #endregion

        #region cosmetics
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

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)//копирование изображения
        {
            if (ImageOutput.Image != null)
                Clipboard.SetImage(ImageOutput.Image);
        }
        #endregion
    }
}
