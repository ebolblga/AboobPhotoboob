namespace ImgApp_2_WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using Patagames.Ocr;
    using Patagames.Ocr.Enums;

    public partial class Form1 : Form
    {

        #region variables
        private List<Image> _loadedImages { get; set; }

        public static Bitmap Image = null;
        private const int N = 20;   //максимальное кол-во изображений (чтоб краша из-за недостатка памяти небыло)
        private int[] _mode = new int[N];    //массив режимов наложения слоёв
        private int[] _opacityArray = Enumerable.Repeat(100, N).ToArray();   //массив прозрачности слоёв
        private bool _theme; //0 dark theme, 1 light theme
        private List<Point> _userPoints = new List<Point>();
        private List<Point> _points4Spline = new List<Point>();
        private List<MyPoint> _pointList = new List<MyPoint>();
        private List<MyPoint> _reversePointList = new List<MyPoint>();

        private struct SplineTuple
        {
            public double a;
            public double b;
            public double c;
            public double d;
            public double x;
        } // Структура, описывающая сплайн на каждом сегменте сетки

        private SplineTuple[] _splines; // Сплайны
        #endregion

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _loadedImages = new List<Image>();
            comboBox2.SelectedIndex = 0;
            channelBox.SelectedIndex = 0;
            filterMode.SelectedIndex = 1;
            //this.TopMost = true;
            //this.WindowState = FormWindowState.Maximized;

            Point point1 = new Point(200, 0);
            Point point2 = new Point(0, 200);
            _userPoints.Add(point1);
            _userPoints.Add(point2);
            GetSettings();
        }

        public void GetSettings()//загружает настройки пользователя
        {
            _theme = Properties.Settings.Default.Theme;
            if (_theme == false)
            {
                var dark = new Bitmap(@"..\..\..\Icons\lightThemeSmallest.png");
                themeBox1.Image = dark;

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
                curveEditBox.BackColor = Color.FromArgb(47, 47, 47);

                OCRText.BackColor = Color.FromArgb(69, 69, 69);
                OCRText.ForeColor = Color.FromArgb(224, 224, 224);
                BinarizationPanel.BackColor = Color.FromArgb(69, 69, 69);
                label4.ForeColor = Color.FromArgb(224, 224, 224);
                label5.ForeColor = Color.FromArgb(224, 224, 224);
                label6.ForeColor = Color.FromArgb(224, 224, 224);
                label7.ForeColor = Color.FromArgb(224, 224, 224);
                label8.ForeColor = Color.FromArgb(224, 224, 224);
            }
            else
            {
                var light = new Bitmap(@"..\..\..\Icons\darkThemeSmallest.png");
                themeBox1.Image = light;

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

                OCRText.BackColor = Color.FromArgb(219, 219, 219);
                OCRText.ForeColor = Color.FromArgb(47, 47, 47);
                BinarizationPanel.BackColor = Color.FromArgb(219, 219, 219);
                label4.ForeColor = Color.FromArgb(47, 47, 47);
                label5.ForeColor = Color.FromArgb(47, 47, 47);
                label6.ForeColor = Color.FromArgb(47, 47, 47);
                label7.ForeColor = Color.FromArgb(47, 47, 47);
                label8.ForeColor = Color.FromArgb(47, 47, 47);
            }

            histogrammToolStripMenuItem.Checked = Properties.Settings.Default.Histogram;
            autoHistogramToolStripMenuItem.Checked = Properties.Settings.Default.AutoHist;
            curveToolStripMenuItem.Checked = Properties.Settings.Default.Curve;
            additionalCurveMarkersToolStripMenuItem.Checked = Properties.Settings.Default.Markers;

            if (histogrammToolStripMenuItem.Checked == false)
            {
                histogram.Visible = false;
                comboBox2.Visible = false;
                button3.Visible = false;
                ImageOutput.Size = new Size(ImageOutput.Width, Convert.ToInt32(ImageOutput.Height * 1.25));
            }
            else
            {
                histogram.Visible = true;
                comboBox2.Visible = true;
                button3.Visible = true;
            }

            if (curveToolStripMenuItem.Checked == false)
            {
                curveEditBox.Visible = false;
                bCurve.Visible = false;
                bApplyCurve.Visible = false;
            }
            else
            {
                curveEditBox.Visible = true;
                bCurve.Visible = true;
                bApplyCurve.Visible = true;
            }
        }

        public void SaveSettings()//сохраняет настройки пользователя
        {
            Properties.Settings.Default.Theme = _theme;
            Properties.Settings.Default.Histogram = histogrammToolStripMenuItem.Checked;
            Properties.Settings.Default.AutoHist = autoHistogramToolStripMenuItem.Checked;
            Properties.Settings.Default.Curve = curveToolStripMenuItem.Checked;
            Properties.Settings.Default.Markers = additionalCurveMarkersToolStripMenuItem.Checked;

            Properties.Settings.Default.Save();
        }

        private void LoadImagesFromFolder(string[] paths) //загрузка изображений из выбранного файла
        {
            //LoadedImages = new List<Image>();
            foreach (var path in paths)
            {
                //string tempLocation = $@"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\ImgApp_2_WinForms-master\Images\{index}.jpg";
                var tempImage = System.Drawing.Image.FromFile(path);
                _loadedImages.Add(tempImage);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//запись режима наложения в отдельный массив
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                var selectedIndex = LayerList.SelectedIndices[0];
                _mode[selectedIndex] = comboBox1.SelectedIndex;
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
                var invertedIndex = _loadedImages.Count - 1 - selectedIndex;
                Image selectedImage = _loadedImages[invertedIndex];
                ImageOutput.Image = selectedImage;
                comboBox1.SelectedIndex = _mode[selectedIndex];
                channelBox.SelectedIndex = 0;

                opacityBar.Value = _opacityArray[selectedIndex];
                opacity.Text = "Opacity: " + opacityBar.Value.ToString() + "%";

                if (autoHistogramToolStripMenuItem.Checked == true && histogrammToolStripMenuItem.Checked == true)
                {
                    histogramRender2(sender, e);
                }
            }
        }

        private void bRender_Click(object sender, EventArgs e)//СОВСЕМ улучшенная отрисовка режимов наложения
        {
            if (_loadedImages.Count >= 2)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                int index = 1;
                Bitmap img1 = new Bitmap(_loadedImages[index - 1]);
                Bitmap img2 = new Bitmap(_loadedImages[index]);
                int modeIndex = _loadedImages.Count - 1 - index;
                int indexedOpacity = Convert.ToInt32(_opacityArray[modeIndex] * 2.55);
                Bitmap img_out = null;
                switch (_mode[modeIndex])
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

                for (; index < _loadedImages.Count - 1; ++index)
                {
                    img1 = new Bitmap(img_out);
                    img2 = new Bitmap(_loadedImages[index + 1]);
                    modeIndex = _loadedImages.Count - 2 - index;
                    indexedOpacity = Convert.ToInt32(_opacityArray[modeIndex] * 2.55);
                    switch (_mode[modeIndex])
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
            {
                MessageBox.Show("Needs at least 2 images in a project", "Error");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)//загрузка файлов в проект
        {
            if (_loadedImages.Count == 0)
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

                    foreach (var image in _loadedImages)
                    {
                        images.Images.Add(image);
                    }

                    //добавляю картинки в список слоёв
                    LayerList.LargeImageList = images;

                    for (int itemIndex = _loadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                    {
                        LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                    }
                }
            }
            else if (_loadedImages.Count <= N / 2)
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
                        for (int i = _mode.Length - 1; i > 0; --i)
                        {
                            _mode[i] = _mode[i - 1];
                            _opacityArray[i] = _opacityArray[i - 1];
                        }

                        _mode[0] = 0;
                        _opacityArray[0] = 100;
                    }

                    //очищаю список картинок
                    LayerList.Items.Clear();

                    //инициализация списка картинок
                    ImageList images = new ImageList();
                    images.ImageSize = new Size(77, 80);
                    foreach (var image in _loadedImages)
                    {
                        images.Images.Add(image);
                    }

                    //добавляю картинки в список слоёв
                    LayerList.LargeImageList = images;

                    //int itemIndex = 0; itemIndex < LoadedImages.Count; ++itemIndex
                    for (int itemIndex = _loadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                    {
                        LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                    }
                }
            }
            else if (_loadedImages.Count > N / 2)
            {
                MessageBox.Show("Too many images in a project, careful", "Error");
            }
        }

        private void SavetoLayerList(Bitmap img_out)//перезагрузка списка слоёв
        {
            this.Cursor = Cursors.WaitCursor;
            _loadedImages.Add(img_out);

            //очищаю список картинок
            LayerList.Items.Clear();

            //инициализация списка картинок
            ImageList images = new ImageList();
            images.ImageSize = new Size(77, 80);
            foreach (var image in _loadedImages)
            {
                images.Images.Add(image);
            }

            //добавляю картинки в список слоёв
            LayerList.LargeImageList = images;

            //int itemIndex = 0; itemIndex < LoadedImages.Count; ++itemIndex
            for (int itemIndex = _loadedImages.Count - 1; itemIndex >= 0; --itemIndex)
            {
                LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
            }

            for (int i = _mode.Length - 1; i > 0; --i)
            {
                _mode[i] = _mode[i - 1];
                _opacityArray[i] = _opacityArray[i - 1];
            }

            _mode[0] = 0;
            _opacityArray[0] = 100;

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

                var img_out = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
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
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        private void bClear_Click(object sender, EventArgs e)//удаление слоя/слоёв
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                ImageOutput.Image = null;

                var puk = LayerList.SelectedIndices[0];
                var puk2 = _loadedImages.Count - 1 - LayerList.SelectedIndices[0];
                for (int i = LayerList.SelectedIndices[0]; i < _mode.Length - 1; ++i)
                {
                    _mode[i] = _mode[i + 1];
                    _opacityArray[i] = _opacityArray[i + 1];
                }


                _loadedImages.RemoveAt(_loadedImages.Count - 1 - LayerList.SelectedIndices[0]);
                LayerList.Items.RemoveAt(LayerList.SelectedIndices[0]);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("No images are selected, you want to delete all?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < _mode.Length; ++i)
                    {
                        _mode[i] = 0;
                        _opacityArray[i] = 0;
                    }

                    ImageOutput.Image = null;
                    _loadedImages.Clear();
                    LayerList.Items.Clear();
                }
            }
        }

        private void brightnessContrastToolStripMenuItem_Click(object sender, EventArgs e)//вызов второй формы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Image = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
                Form2 brightnessForm = new Form2(this);
                brightnessForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        private void JPEGingToolStripMenuItem_Click(object sender, EventArgs e)//джепегирует изображение
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - selectedIndex]);

                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 5L);
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
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        private void transparencyBar_Scroll(object sender, EventArgs e)//скрол бар прозрачности
        {
            opacity.Text = "Opacity: " + opacityBar.Value.ToString() + "%";
            if (LayerList.SelectedIndices.Count > 0)
            {
                var selectedIndex = LayerList.SelectedIndices[0];
                _opacityArray[selectedIndex] = opacityBar.Value;
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)//открытие одного изображения
        {
            if (_loadedImages.Count == 0)
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.InitialDirectory = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images 2"; //C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images 2
                folderBrowser.Filter = "Image Files(**.JPG;*.PNG)|*.JPG;*.PNG|All files (*.*)|*.*";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string path = folderBrowser.FileName;
                        var tempImage = System.Drawing.Image.FromFile(path);
                        _loadedImages.Add(tempImage);

                        ImageList images = new ImageList();
                        images.ImageSize = new Size(77, 80);

                        foreach (var image in _loadedImages)
                        {
                            images.Images.Add(image);
                        }

                        LayerList.LargeImageList = images;

                        for (int itemIndex = _loadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                        {
                            LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                        }
                    }
                    catch
                    {
                        DialogResult rezult = MessageBox.Show("Impossible to open selected file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (_loadedImages.Count < N)
            {
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.InitialDirectory = @"C:\Users\kirill\Desktop\Учеба\Семестр 6\СЦОИ\Лаб 1\Images 2";
                folderBrowser.Filter = "Image Files(**.JPG;*.PNG)|*.JPG;*.PNG|All files (*.*)|*.*";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string path = folderBrowser.FileName;
                        var tempImage = System.Drawing.Image.FromFile(path);
                        _loadedImages.Add(tempImage);

                        for (int i = _mode.Length - 1; i > 0; --i)
                        {
                            _mode[i] = _mode[i - 1];
                            _opacityArray[i] = _opacityArray[i - 1];
                        }

                        _mode[0] = 0;
                        _opacityArray[0] = 100;

                        LayerList.Items.Clear();

                        ImageList images = new ImageList();

                        images.ImageSize = new Size(77, 80);
                        foreach (var image in _loadedImages)
                        {
                            images.Images.Add(image);
                        }

                        LayerList.LargeImageList = images;

                        for (int itemIndex = _loadedImages.Count - 1; itemIndex >= 0; --itemIndex)
                        {
                            LayerList.Items.Add(new ListViewItem($"Image {itemIndex}", itemIndex));
                        }
                    }
                    catch
                    {
                        DialogResult rezult = MessageBox.Show("Impossible to open selected file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (_loadedImages.Count >= N)
            {
                MessageBox.Show("Too many images in a project, careful", "Error");
            }
        }

        private void channelBox_SelectionChangeCommitted(object sender, EventArgs e)//RGB каналы
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - selectedIndex]);
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
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        private void aSCIIFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                var selectedIndex = LayerList.SelectedIndices[0];
                var img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - selectedIndex]);
                Bitmap img_out = new Bitmap(img.Width, img.Height);

                img_out = ASCII.Display(img);

                ImageOutput.Image = img_out;
                SavetoLayerList(img_out);

                this.Cursor = Cursors.Default;
                timer.Stop();
                debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
            }
            else
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        #region Histogram
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

                    int index = _loadedImages.Count - 1 - LayerList.SelectedIndices[0];
                    var img = new Bitmap(_loadedImages[index]);

                    byte[] imgBytes = GetRGBValues(img);

                    for (int i = 0; i < (img.Width * img.Height * 4) - 3; i += 4)
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
                    {
                        max = areaG.AxisY.Maximum;
                    }

                    if (areaB.AxisY.Maximum > max)
                    {
                        max = areaB.AxisY.Maximum;
                    }

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

                    int index = _loadedImages.Count - 1 - LayerList.SelectedIndices[0];
                    var img = new Bitmap(_loadedImages[index]);

                    byte[] imgBytes = GetRGBValues(img);

                    for (int i = 0; i < (img.Width * img.Height * 4) - 3; i += 4)
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
                    {
                        max = areaG.AxisY.Maximum;
                    }

                    if (areaB.AxisY.Maximum > max)
                    {
                        max = areaB.AxisY.Maximum;
                    }

                    if (areaBr.AxisY.Maximum > max)
                    {
                        max = areaBr.AxisY.Maximum;
                    }

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
                    if (_theme == true)
                    {
                        seriesBr.Color = Color.FromArgb(255, 47, 47, 47);
                    }

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
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        private void autoHistogramToolStripMenuItem_Click(object sender, EventArgs e)//автоматический расчёт гистограммы
        {
            if (autoHistogramToolStripMenuItem.Checked == true)
            {
                autoHistogramToolStripMenuItem.Checked = false;
            }
            else
            {
                autoHistogramToolStripMenuItem.Checked = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//автоматический расчёт гистограммы при смене канала
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                if (autoHistogramToolStripMenuItem.Checked == true && histogrammToolStripMenuItem.Checked == true)
                {
                    histogramRender2(sender, e);
                }
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

        #region Curve

        private int _highlightedPoint = 0;    //выделенная точка

        private void curveEditBox_MouseUp(object sender, MouseEventArgs e)//добавление точек
        {
            //проверка что рядом нет точек
            for (int i = 0; i < _userPoints.Count; ++i)
            {
                if ((_userPoints[i].X + 3 > e.Location.X) && (_userPoints[i].X - 3 < e.Location.X))
                {
                    return;
                }
            }

            _highlightedPoint = 0;
            _userPoints.Add(new Point((int)Clamp(e.Location.X, 0, 199), (int)Clamp(e.Location.Y, 0, 199)));
            _userPoints.Sort((p1, p2) => p1.X.CompareTo(p2.X));
            RenderCubicSpline();
            curveEditBox.Refresh();
        }

        private void curveEditBox_MouseMove(object sender, MouseEventArgs e)//движение точек
        {
            if (additionalCurveMarkersToolStripMenuItem.Checked == true)
            {
                label3.Text = $"{e.Location.X * 1.2814:N0} ; {(199 - e.Location.Y) * 1.2814:N0}";
            }

            if (e.Button == MouseButtons.Left)
            {
                if (_userPoints.Count <= 2)
                {
                    return;
                }

                bool found = false;
                for (int i = 1; i < _userPoints.Count - 1; ++i)
                {
                    double dX = e.Location.X - _userPoints[i].X;
                    double dY = e.Location.Y - _userPoints[i].Y;
                    if ((dX * dX) + (dY * dY) < 49)
                    {
                        _highlightedPoint = i;
                        _userPoints[i] = new Point((int)Clamp(e.Location.X, 0, 199), (int)Clamp(e.Location.Y, 0, 199));
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    _highlightedPoint = 0;
                    return;
                }

                _userPoints.Sort((p1, p2) => p1.X.CompareTo(p2.X));
                RenderCubicSpline();
                curveEditBox.Refresh();
                return;
            }

            //else      //код для хайлайта точек
            //{
            //    if (UserPoints.Count <= 2) return;
            //    bool found = false;
            //    for (int i = 1; i < UserPoints.Count - 1; ++i)
            //    {
            //        double dX = e.Location.X - UserPoints[i].X;
            //        double dY = e.Location.Y - UserPoints[i].Y;
            //        if (dX * dX + dY * dY < 49)
            //        {
            //            greendot = i;
            //            found = true;
            //            curveEditBox.Refresh();
            //            break;
            //        }
            //    }
            //    if (found == false)
            //        greendot = 0;
            //    return;
            //}
        }

        private void RenderCubicSpline()//поиск точек для кривой
        {
            double[] x = new double[_userPoints.Count];
            double[] y = new double[_userPoints.Count];

            x[0] = _userPoints[0].X;
            y[0] = _userPoints[0].Y;
            int newcount = 1;
            for (int j = 1; j < _userPoints.Count; ++j)
            {
                if (_userPoints[j].X == _userPoints[j - 1].X)
                {
                    if (_userPoints[j].Y < _userPoints[j - 1].Y)
                    {
                        x[newcount - 1] = _userPoints[j].X;
                        y[newcount - 1] = _userPoints[j].Y;
                    }

                    --newcount;
                }
                else
                {
                    x[newcount] = _userPoints[j].X;
                    y[newcount] = _userPoints[j].Y;
                }

                ++newcount;
            }

            BuildSpline(x.ToArray(), y.ToArray(), newcount);

            int n = 201;
            double[] xStep = new double[n];
            double[] yStep = new double[n];
            double stepSize = (_userPoints[_userPoints.Count - 1].X - _userPoints[0].X) / (n - 1);

            for (int i = 0; i < n; ++i)
            {
                xStep[i] = _userPoints[0].X + (i * stepSize);
            }

            _pointList.Clear();
            for (int i = 0; i < n; ++i)
            {
                _pointList.Add(new MyPoint(xStep[i], Interpolate(xStep[i])));
            }
        }

        private void curveEditBox_Paint(object sender, PaintEventArgs e)//рисование кривой
        {
            if (_userPoints.Count < 2)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (_pointList.Count < 2) //если меньше двух точек рисуем прямую
            {
                if (_theme == false)
                {
                    e.Graphics.DrawCurve(Pens.LightGray, _userPoints.ToArray());
                }
                else
                {
                    e.Graphics.DrawCurve(Pens.Black, _userPoints.ToArray());
                }

                return;
            }

            SolidBrush brush1 = new SolidBrush(Color.FromArgb(200, 255, 13, 0));
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(200, 255, 255, 0));

            for (int i = 1; i < _userPoints.Count - 1; ++i) //рисуем точки
            {
                e.Graphics.FillRectangle(brush1, _userPoints[i].X - 4, _userPoints[i].Y - 4, 8, 8);
                e.Graphics.FillRectangle(brush2, _userPoints[i].X - 3, _userPoints[i].Y - 3, 6, 6);
            }

            if (_highlightedPoint != 0) //зелёная "активная" точка
            {
                e.Graphics.FillRectangle(Brushes.Green, _userPoints[_highlightedPoint].X - 3, _userPoints[_highlightedPoint].Y - 3, 6, 6);
            }

            if (additionalCurveMarkersToolStripMenuItem.Checked == true)
            {
                //рисуем оранжевые прямые
                e.Graphics.DrawLines(Pens.DarkOrange, _userPoints.ToArray());

                PointF point1 = new PointF(89.5F, 99.5F);
                PointF point2 = new PointF(109.5F, 99.5F);
                e.Graphics.DrawLine(Pens.DarkGray, point1, point2);
                PointF point3 = new PointF(99.5F, 89.5F);
                PointF point4 = new PointF(99.5F, 109.5F);
                e.Graphics.DrawLine(Pens.DarkGray, point3, point4);
            }

            //рисуем кривую
            _points4Spline.Clear();

            for (int i = 0; i < _pointList.Count; ++i)
            {
                _points4Spline.Add(new Point((int)Clamp(_pointList[i].X, 0, 199), (int)Clamp(_pointList[i].Y, 0, 199)));
            }

            if (_theme == false)
            {
                e.Graphics.DrawCurve(Pens.LightGray, _points4Spline.ToArray());
            }
            else
            {
                e.Graphics.DrawCurve(Pens.Black, _points4Spline.ToArray());
            }
        }

        private void bApplyCurve2_Click(object sender, EventArgs e)//отрисовка изображения с кривыми
        {
            if (LayerList.SelectedIndices.Count > 0)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                this.Cursor = Cursors.WaitCursor;

                int index = _loadedImages.Count - 1 - LayerList.SelectedIndices[0];
                var img = new Bitmap(_loadedImages[index]);

                byte[] imgBytes = GetRGBValues(img);
                byte[] img_out_bytes = new byte[img.Width * img.Height * 4];

                _reversePointList.Clear();
                for (int i = 0; i < _pointList.Count; ++i)
                {
                    MyPoint newPoint = new MyPoint(_pointList[i].X, 200 - _pointList[i].Y);
                    _reversePointList.Add(newPoint);
                }

                _reversePointList.Sort((p1, p2) => p1.X.CompareTo(p2.X));

                for (int i = 0; i < (img.Width * img.Height * 4) - 3; i += 4)
                {
                    int newxR = Convert.ToInt32((double)imgBytes[i + 2] / 255 * 200);
                    int newxG = Convert.ToInt32((double)imgBytes[i + 1] / 255 * 200);
                    int newxB = Convert.ToInt32((double)imgBytes[i] / 255 * 200);

                    img_out_bytes[i + 2] = Convert.ToByte(Clamp((double)_reversePointList[newxR].Y * 1.275, 0, 255));
                    img_out_bytes[i + 1] = Convert.ToByte(Clamp((double)_reversePointList[newxG].Y * 1.275, 0, 255));
                    img_out_bytes[i] = Convert.ToByte(Clamp((double)_reversePointList[newxB].Y * 1.275, 0, 255));
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
            {
                MessageBox.Show("Image is not selected", "Error");
            }
        }

        private void bCurve_Click(object sender, EventArgs e)//очистка кривой
        {
            _userPoints.Clear();
            _points4Spline.Clear();
            _pointList.Clear();
            _highlightedPoint = 0;
            Point start = new Point(200, 0);
            Point end = new Point(0, 200);
            _userPoints.Add(start);
            _userPoints.Add(end);

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

        private void additionalCurveMarkersToolStripMenuItem_Click(object sender, EventArgs e)//прячет/показывает дополнительные линии
        {
            if (additionalCurveMarkersToolStripMenuItem.Checked == true)
            {
                additionalCurveMarkersToolStripMenuItem.Checked = false;
                label3.Text = string.Empty;
                curveEditBox.Refresh();
            }
            else
            {
                additionalCurveMarkersToolStripMenuItem.Checked = true;
                curveEditBox.Refresh();
            }
        }

        #region Cubic spline math

        public void BuildSpline(double[] x, double[] y, int n)
        {
            // x - узлы сетки, должны быть упорядочены по возрастанию, кратные узлы запрещены
            // y - значения функции в узлах сетки
            // n - количество узлов сетки

            // Инициализация массива сплайнов
            _splines = new SplineTuple[n];
            for (int i = 0; i < n; ++i)
            {
                _splines[i].x = x[i];
                _splines[i].a = y[i];
            }

            _splines[0].c = _splines[n - 1].c = 0.0;

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
                double F = 6.0 * (((y[i + 1] - y[i]) / hi1) - ((y[i] - y[i - 1]) / hi));
                double z = (A * alpha[i - 1]) + C;
                alpha[i] = -B / z;
                beta[i] = (F - (A * beta[i - 1])) / z;
            }

            // Нахождение решения - обратный ход метода прогонки
            for (int i = n - 2; i > 0; --i)
            {
                _splines[i].c = (alpha[i] * _splines[i + 1].c) + beta[i];
            }

            // По известным коэффициентам c[i] находим значения b[i] и d[i]
            for (int i = n - 1; i > 0; --i)
            {
                double hi = x[i] - x[i - 1];
                _splines[i].d = (_splines[i].c - _splines[i - 1].c) / hi;
                _splines[i].b = (hi * ((2.0 * _splines[i].c) + _splines[i - 1].c) / 6.0) + ((y[i] - y[i - 1]) / hi);
            }
        } // Построение сплайна

        public double Interpolate(double x)
        {
            if (_splines == null)
            {
                return double.NaN; // Если сплайны ещё не построены - возвращаем NaN
            }

            int n = _splines.Length;
            SplineTuple s;

            if (x <= _splines[0].x) // Если x меньше точки сетки x[0] - пользуемся первым эл-тов массива
            {
                s = _splines[0];
            }
            else if (x >= _splines[n - 1].x) // Если x больше точки сетки x[n - 1] - пользуемся последним эл-том массива
            {
                s = _splines[n - 1];
            }
            else // Иначе x лежит между граничными точками сетки - производим бинарный поиск нужного эл-та массива
            {
                int i = 0;
                int j = n - 1;
                while (i + 1 < j)
                {
                    int k = i + ((j - i) / 2);
                    if (x <= _splines[k].x)
                    {
                        j = k;
                    }
                    else
                    {
                        i = k;
                    }
                }

                s = _splines[j];
            }

            double dx = x - s.x;
            // Вычисляем значение сплайна в заданной точке по схеме Горнера (в принципе, "умный" компилятор применил бы схему Горнера сам, но ведь не все так умны, как кажутся)
            return s.a + ((s.b + (((s.c / 2.0) + (s.d * dx / 6.0)) * dx)) * dx);
        } //нахождение f(x) после построения сплайна

        #endregion

        #endregion

        #region Binarization
        private void Binarization1_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Gavrilov(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void SaveResult_Click(object sender, EventArgs e)
        {
            if (ImageOutput.Image == null)
            {
                return;
            }

            Bitmap img_out = new Bitmap(ImageOutput.Image);
            SavetoLayerList(img_out);
        }

        private void Binarization2_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Otsu(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void Binarization3_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Kochanovskiy(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void BinarizationSlider_Scroll(object sender, EventArgs e)
        {
            label7.Text = BinarizationSlider.Value.ToString();

            if (LayerList.SelectedIndices.Count < 1)
            {
                return;
            }

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Slider(img, BinarizationSlider.Value);
            ImageOutput.Image = img_out;
        }

        private void Binarization5_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Niblack(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void Binarization6_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Sauvola(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void Binarization7_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Wulff(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void Binarization8_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out = Binarization.Bradley(img);
            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }
        #endregion

        #region Text regonition
        private void GetText_Click(object sender, EventArgs e)//читает текст
        {
            if (ImageOutput.Image == null)
            {
                MessageBox.Show("No image is loaded", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            Bitmap img = new Bitmap(ImageOutput.Image);
            int side = 499;
            Rectangle crop = new Rectangle(Convert.ToInt32(((double)img.Width / 2) - ((double)side / 2)), Convert.ToInt32(((double)img.Height / 2) - ((double)side / 2)), side, side);
            Bitmap imgCropped = img.Clone(crop, img.PixelFormat);
            img.Dispose();
            ImageOutput.Image = imgCropped;

            using (var objOcr = OcrApi.Create())
            {
                objOcr.Init(Languages.English);

                //objOcr.Init(@"..\..\tessdata", "eng", OcrEngineMode.OEM_DEFAULT);
                //objOcr.SetVariable("tessedit_char_whitelist", "0123456789,/ -");
                string returnText = "Output:\n";
                try
                {
                    returnText += objOcr.GetTextFromImage(imgCropped);
                    Clipboard.SetText(returnText);
                    OCRText.Visible = true;
                    OCRText.Text = returnText;
                }
                catch
                {
                    img.Dispose();
                    this.Cursor = Cursors.Default;
                    timer.Stop();
                    debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
                    MessageBox.Show("Tesseract.Net.SDK error.\nProbably forgot to crop...", "Error");
                    return;
                }
            }

            img.Dispose();
            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";

        }

        private void OCRText_Click(object sender, EventArgs e)//прячет текстовое поля прочитанного текста
        {
            OCRText.Visible = false;
            OCRText.Text = string.Empty;
        }
        #endregion

        #region Helper functions
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
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
        }//сжимает значения в выбранный промежуток

        private byte[] GetRGBValues(Bitmap bmp)//конвертирует Bitmap в byte[]
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
            SaveSettings();
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//завершение программы
        {
            SaveSettings();
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

        #endregion

        #region Cosmetics
        private void themeBox1_Click(object sender, EventArgs e)//переключатель темы
        {
            if (_theme == false)
            {
                var light = new Bitmap(@"..\..\..\Icons\darkThemeSmallest.png");
                themeBox1.Image = light;
                _theme = true;

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

                OCRText.BackColor = Color.FromArgb(219, 219, 219);
                OCRText.ForeColor = Color.FromArgb(47, 47, 47);
                BinarizationPanel.BackColor = Color.FromArgb(219, 219, 219);
                label4.ForeColor = Color.FromArgb(47, 47, 47);
                label5.ForeColor = Color.FromArgb(47, 47, 47);
                label6.ForeColor = Color.FromArgb(47, 47, 47);
                label7.ForeColor = Color.FromArgb(47, 47, 47);
                label8.ForeColor = Color.FromArgb(47, 47, 47);
            }
            else
            {
                var dark = new Bitmap(@"..\..\..\Icons\lightThemeSmallest.png");
                themeBox1.Image = dark;
                _theme = false;

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
                curveEditBox.BackColor = Color.FromArgb(47, 47, 47);

                OCRText.BackColor = Color.FromArgb(69, 69, 69);
                OCRText.ForeColor = Color.FromArgb(224, 224, 224);
                BinarizationPanel.BackColor = Color.FromArgb(69, 69, 69);
                label4.ForeColor = Color.FromArgb(224, 224, 224);
                label5.ForeColor = Color.FromArgb(224, 224, 224);
                label6.ForeColor = Color.FromArgb(224, 224, 224);
                label7.ForeColor = Color.FromArgb(224, 224, 224);
                label8.ForeColor = Color.FromArgb(224, 224, 224);

                //photoshop theme
                //this.BackColor = Color.FromArgb(38, 38, 38);
                //LayerList.BackColor = Color.FromArgb(83, 83, 83);
                //LayerList.ForeColor = Color.FromArgb(221, 221, 221);
                //histogram.BackColor = Color.FromArgb(83, 83, 83);
                //histogram.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(83, 83, 83);
                //credits.ForeColor = Color.FromArgb(221, 221, 221);
                //debug.ForeColor = Color.FromArgb(221, 221, 221);
                //opacity.ForeColor = Color.FromArgb(221, 221, 221);

                // moder flat UI
                //this.BackColor = Color.FromArgb(21, 23, 39);
                //LayerList.BackColor = Color.FromArgb(23, 27, 44);
                //LayerList.ForeColor = Color.FromArgb(123, 128, 142);
                //histogram.BackColor = Color.FromArgb(23, 27, 44);
                //histogram.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(23, 27, 44);
                //credits.ForeColor = Color.FromArgb(123, 128, 142);
                //debug.ForeColor = Color.FromArgb(123, 128, 142);
                //opacity.ForeColor = Color.FromArgb(123, 128, 142);
                //label1.ForeColor = Color.FromArgb(123, 128, 142);
                //label2.ForeColor = Color.FromArgb(123, 128, 142);
                //curveEditBox.BackColor = Color.FromArgb(23, 27, 44);
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)//копирование изображения
        {
            if (ImageOutput.Image != null)
            {
                Clipboard.SetImage(ImageOutput.Image);
            }
        }
        #endregion

        #region Filter
        private void CalcMatrix()
        {
            int i = 1;
            int j = 1;

            try
            {
                i += Convert.ToInt32(MatrixX.Text) * 2;
                j += Convert.ToInt32(MatrixY.Text) * 2;
            }
            catch
            {
                MessageBox.Show("R1 or R2 is not an int", "Error");
                return;
            }

            dataGridView1.ColumnCount = i;
            dataGridView1.RowCount = j;

            switch (filterMode.SelectedIndex)
            {
                case 0:
                    {
                        int s = i * j;

                        for (int a = 0; a < i; a++)
                        {
                            for (int b = 0; b < j; b++)
                            {
                                dataGridView1.Rows[b].Cells[a].Value = Math.Round(1D / s, 3);
                            }
                        }

                        label9.Text = $"Sum: 1";
                        label9.Visible = true;
                        return;
                    }

                case 1:
                    {
                        double sig = 3; // сигма
                        double s = 0;
                        double g;

                        try
                        {
                            sig = Convert.ToDouble(MedianValue.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Sigma is not a value type double, will be calculated with value 3", "Error");
                        }

                        double sig_sqr = 2 * sig * sig;
                        double k = 1D / (sig_sqr * Math.PI);

                        int r1 = Convert.ToInt32((double)(i - 1) / 2);
                        int r2 = Convert.ToInt32((double)(j - 1) / 2);

                        for (int a = -r1; a <= r1; a++)
                        {
                            for (int b = -r2; b <= r2; b++)
                            {
                                double ij_sqr = (a * a) + (b * b);
                                g = k * Math.Exp(-(ij_sqr / sig_sqr));
                                s += g;
                                dataGridView1.Rows[b + r2].Cells[a + r1].Value = Math.Round(g, 4);
                            }
                        }

                        label9.Text = $"Sum: {Math.Round(s, 3)}";
                        label9.Visible = true;
                        return;
                    }

                case 2:
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.DataSource = null;

                        dataGridView1.ColumnCount = 3;
                        dataGridView1.RowCount = 3;

                        for (int i2 = 0; i2 < dataGridView1.ColumnCount; i2++)
                        {
                            for (int j2 = 0; j2 < dataGridView1.RowCount; j2++)
                            {
                                dataGridView1.Rows[j2].Cells[i2].Value = -1;
                            }
                        }

                        dataGridView1.Rows[1].Cells[1].Value = 9;

                        MatrixX.Text = "1";
                        MatrixY.Text = "1";

                        label9.Text = $"Sum: 1";
                        label9.Visible = true;
                        return;
                    }

                case 3:
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.DataSource = null;

                        dataGridView1.ColumnCount = 3;
                        dataGridView1.RowCount = 3;

                        for (int i2 = 0; i2 < dataGridView1.ColumnCount; i2++)
                        {
                            for (int j2 = 0; j2 < dataGridView1.RowCount; j2++)
                            {
                                dataGridView1.Rows[j2].Cells[i2].Value = 0;
                            }
                        }

                        dataGridView1.Rows[0].Cells[0].Value = -1;
                        dataGridView1.Rows[1].Cells[0].Value = -1;
                        dataGridView1.Rows[2].Cells[0].Value = -1;

                        dataGridView1.Rows[0].Cells[2].Value = 1;
                        dataGridView1.Rows[1].Cells[2].Value = 1;
                        dataGridView1.Rows[2].Cells[2].Value = 1;

                        MatrixX.Text = "1";
                        MatrixY.Text = "1";

                        label9.Text = $"Sum: 0";
                        label9.Visible = true;
                        return;
                    }

                case 4:
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.DataSource = null;

                        dataGridView1.ColumnCount = 3;
                        dataGridView1.RowCount = 3;

                        for (int i2 = 0; i2 < dataGridView1.ColumnCount; i2++)
                        {
                            for (int j2 = 0; j2 < dataGridView1.RowCount; j2++)
                            {
                                dataGridView1.Rows[j2].Cells[i2].Value = 0;
                            }
                        }

                        dataGridView1.Rows[0].Cells[0].Value = -1;
                        dataGridView1.Rows[1].Cells[0].Value = -2;
                        dataGridView1.Rows[2].Cells[0].Value = -1;

                        dataGridView1.Rows[0].Cells[2].Value = 1;
                        dataGridView1.Rows[1].Cells[2].Value = 2;
                        dataGridView1.Rows[2].Cells[2].Value = 1;

                        MatrixX.Text = "1";
                        MatrixY.Text = "1";

                        label9.Text = $"Sum: 0";
                        label9.Visible = true;
                        return;
                    }

                case 5:
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.DataSource = null;

                        dataGridView1.ColumnCount = 3;
                        dataGridView1.RowCount = 3;

                        for (int i2 = 0; i2 < dataGridView1.ColumnCount; i2++)
                        {
                            for (int j2 = 0; j2 < dataGridView1.RowCount; j2++)
                            {
                                dataGridView1.Rows[j2].Cells[i2].Value = 0;
                            }
                        }

                        dataGridView1.Rows[0].Cells[1].Value = 1;
                        dataGridView1.Rows[1].Cells[0].Value = 1;
                        dataGridView1.Rows[1].Cells[1].Value = -4;
                        dataGridView1.Rows[1].Cells[2].Value = 1;
                        dataGridView1.Rows[2].Cells[1].Value = 1;

                        MatrixX.Text = "1";
                        MatrixY.Text = "1";

                        label9.Text = $"Sum: 0";
                        label9.Visible = true;
                        return;
                    }

                case 6:
                    {
                        for (int i2 = 0; i2 < dataGridView1.ColumnCount; i2++)
                        {
                            for (int j2 = 0; j2 < dataGridView1.RowCount; j2++)
                            {
                                dataGridView1.Rows[j2].Cells[i2].Value = 0;
                            }
                        }

                        label9.Text = $"Sum: 0";
                        label9.Visible = true;
                        return;
                    }

                default:
                    return;
            }
        }

        private void bEmpty_Click(object sender, EventArgs e)
        {
            for (int i2 = 0; i2 < dataGridView1.ColumnCount; i2++)
            {
                for (int j2 = 0; j2 < dataGridView1.RowCount; j2++)
                {
                    dataGridView1.Rows[j2].Cells[i2].Value = 0;
                }
            }
            label9.Text = $"Sum: 0";
            label9.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LayerList.SelectedIndices.Count < 1)
            {
                MessageBox.Show("No image is selected", "Error");
                return;
            }

            if (dataGridView1.RowCount < 1)
            {
                MessageBox.Show("Matrix is not generated", "Error");
                return;
            }

            int r1;
            int r2;
            try
            {
                r1 = Convert.ToInt32(MatrixX.Text);
                r2 = Convert.ToInt32(MatrixY.Text);
            }
            catch
            {
                MessageBox.Show("R1 or R2 is not an int", "Error");
                return;
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.Cursor = Cursors.WaitCursor;

            double[,] matrix = new double[(r1 * 2) + 1, (r2 * 2) + 1];
            for (int a = 0; a < (r1 * 2) + 1; a++)
            {
                for (int b = 0; b < (r2 * 2) + 1; b++)
                {
                    matrix[a, b] = Convert.ToDouble(dataGridView1.Rows[b].Cells[a].Value);
                }
            }

            Bitmap img = new Bitmap(_loadedImages[_loadedImages.Count - 1 - LayerList.SelectedIndices[0]]);
            Bitmap img_out;
            if (filterMode.SelectedIndex == 6)
            {
                img_out = Filtration.Median(img, r1, r2, matrix);
            }
            else
            {
                img_out = Filtration.Linear(img, r1, r2, matrix);
            }

            img.Dispose();
            ImageOutput.Image = img_out;

            this.Cursor = Cursors.Default;
            timer.Stop();
            debug.Text = "Last calculation time: " + timer.ElapsedMilliseconds + " ms. or " + Math.Round(timer.Elapsed.TotalSeconds, 3) + " s.";
        }

        private void filterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcMatrix();
        }

        private void MatrixX_TextChanged(object sender, EventArgs e)
        {
            CalcMatrix();
        }

        private void MatrixY_TextChanged(object sender, EventArgs e)
        {
            CalcMatrix();
        }

        private void MedianValue_TextChanged(object sender, EventArgs e)
        {
            CalcMatrix();
        }
        #endregion
    }
}
