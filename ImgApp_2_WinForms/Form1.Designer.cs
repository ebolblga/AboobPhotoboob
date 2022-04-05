
namespace ImgApp_2_WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LayerList = new System.Windows.Forms.ListView();
            this.ImageListMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binarizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otsusMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.niblacksMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvolasMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wulffsMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bradleysMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sliderMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.джепегированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessContrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorBalanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoHistogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.curveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bClear = new System.Windows.Forms.Button();
            this.histogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.credits = new System.Windows.Forms.Label();
            this.themeBox1 = new System.Windows.Forms.PictureBox();
            this.ImageOutput = new System.Windows.Forms.PictureBox();
            this.PictureBoxMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debug = new System.Windows.Forms.Label();
            this.opacityBar = new System.Windows.Forms.TrackBar();
            this.opacity = new System.Windows.Forms.Label();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.channelBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.curveEditBox = new System.Windows.Forms.Panel();
            this.bCurve = new System.Windows.Forms.Button();
            this.bApplyCurve = new System.Windows.Forms.Button();
            this.bRender3 = new System.Windows.Forms.Button();
            this.ImageListMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.histogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.themeBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOutput)).BeginInit();
            this.PictureBoxMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).BeginInit();
            this.SuspendLayout();
            // 
            // LayerList
            // 
            this.LayerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.LayerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LayerList.ContextMenuStrip = this.ImageListMenuStrip1;
            this.LayerList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LayerList.HideSelection = false;
            this.LayerList.Location = new System.Drawing.Point(1037, 152);
            this.LayerList.MultiSelect = false;
            this.LayerList.Name = "LayerList";
            this.LayerList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LayerList.Size = new System.Drawing.Size(141, 402);
            this.LayerList.TabIndex = 3;
            this.LayerList.UseCompatibleStateImageBehavior = false;
            this.LayerList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LayerList_ItemSelectionChanged);
            // 
            // ImageListMenuStrip1
            // 
            this.ImageListMenuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.ImageListMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.ImageListMenuStrip1.Name = "ImageListMenuStrip1";
            this.ImageListMenuStrip1.Size = new System.Drawing.Size(108, 70);
            // 
            // addImageToolStripMenuItem
            // 
            this.addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            this.addImageToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addImageToolStripMenuItem.Text = "Add";
            this.addImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.bClear_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Normal",
            "Addition",
            "Multiply",
            "Average",
            "Darken (min)",
            "Lighten (max)",
            "Mask"});
            this.comboBox1.Location = new System.Drawing.Point(1098, 94);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 23);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1190, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openImageToolStripMenuItem.Text = "Open...";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "Open Folder...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "Save As...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binarizationToolStripMenuItem,
            this.джепегированиеToolStripMenuItem,
            this.brightnessContrastToolStripMenuItem,
            this.colorBalanceToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.blurToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.editToolStripMenuItem.Text = "Adjustments";
            // 
            // binarizationToolStripMenuItem
            // 
            this.binarizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fToolStripMenuItem,
            this.otsusMethodToolStripMenuItem,
            this.niblacksMethodToolStripMenuItem,
            this.sauvolasMethodToolStripMenuItem,
            this.wulffsMethodToolStripMenuItem,
            this.bradleysMethodToolStripMenuItem,
            this.sliderMethodToolStripMenuItem});
            this.binarizationToolStripMenuItem.Name = "binarizationToolStripMenuItem";
            this.binarizationToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.binarizationToolStripMenuItem.Text = "Binarization";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.fToolStripMenuItem.Text = "Gavrilov\'s method";
            this.fToolStripMenuItem.Click += new System.EventHandler(this.fToolStripMenuItem_Click);
            // 
            // otsusMethodToolStripMenuItem
            // 
            this.otsusMethodToolStripMenuItem.Name = "otsusMethodToolStripMenuItem";
            this.otsusMethodToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.otsusMethodToolStripMenuItem.Text = "Otsu\'s method";
            // 
            // niblacksMethodToolStripMenuItem
            // 
            this.niblacksMethodToolStripMenuItem.Name = "niblacksMethodToolStripMenuItem";
            this.niblacksMethodToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.niblacksMethodToolStripMenuItem.Text = "Niblack\'s method";
            // 
            // sauvolasMethodToolStripMenuItem
            // 
            this.sauvolasMethodToolStripMenuItem.Name = "sauvolasMethodToolStripMenuItem";
            this.sauvolasMethodToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sauvolasMethodToolStripMenuItem.Text = "Sauvola\'s method";
            // 
            // wulffsMethodToolStripMenuItem
            // 
            this.wulffsMethodToolStripMenuItem.Name = "wulffsMethodToolStripMenuItem";
            this.wulffsMethodToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.wulffsMethodToolStripMenuItem.Text = "Wulff\'s method";
            // 
            // bradleysMethodToolStripMenuItem
            // 
            this.bradleysMethodToolStripMenuItem.Name = "bradleysMethodToolStripMenuItem";
            this.bradleysMethodToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bradleysMethodToolStripMenuItem.Text = "Bradley\'s method";
            // 
            // sliderMethodToolStripMenuItem
            // 
            this.sliderMethodToolStripMenuItem.Name = "sliderMethodToolStripMenuItem";
            this.sliderMethodToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sliderMethodToolStripMenuItem.Text = "Slider method";
            // 
            // джепегированиеToolStripMenuItem
            // 
            this.джепегированиеToolStripMenuItem.Name = "джепегированиеToolStripMenuItem";
            this.джепегированиеToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.джепегированиеToolStripMenuItem.Text = "JPEG Compression";
            this.джепегированиеToolStripMenuItem.Click += new System.EventHandler(this.JPEGingToolStripMenuItem_Click);
            // 
            // brightnessContrastToolStripMenuItem
            // 
            this.brightnessContrastToolStripMenuItem.Name = "brightnessContrastToolStripMenuItem";
            this.brightnessContrastToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.brightnessContrastToolStripMenuItem.Text = "WIP Brightness / Contrast...";
            this.brightnessContrastToolStripMenuItem.Click += new System.EventHandler(this.brightnessContrastToolStripMenuItem_Click);
            // 
            // colorBalanceToolStripMenuItem
            // 
            this.colorBalanceToolStripMenuItem.Name = "colorBalanceToolStripMenuItem";
            this.colorBalanceToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.colorBalanceToolStripMenuItem.Text = "WIP Color Balance...";
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.sharpenToolStripMenuItem.Text = "WIP Sharpen";
            // 
            // blurToolStripMenuItem
            // 
            this.blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            this.blurToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.blurToolStripMenuItem.Text = "WIP Blur";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogrammToolStripMenuItem,
            this.autoHistogramToolStripMenuItem,
            this.curveToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // histogrammToolStripMenuItem
            // 
            this.histogrammToolStripMenuItem.Checked = true;
            this.histogrammToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.histogrammToolStripMenuItem.Name = "histogrammToolStripMenuItem";
            this.histogrammToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.histogrammToolStripMenuItem.Text = "Histogram";
            this.histogrammToolStripMenuItem.Click += new System.EventHandler(this.histogrammToolStripMenuItem_Click);
            // 
            // autoHistogramToolStripMenuItem
            // 
            this.autoHistogramToolStripMenuItem.Name = "autoHistogramToolStripMenuItem";
            this.autoHistogramToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.autoHistogramToolStripMenuItem.Text = "Auto Histogram";
            this.autoHistogramToolStripMenuItem.Click += new System.EventHandler(this.autoHistogramToolStripMenuItem_Click);
            // 
            // curveToolStripMenuItem
            // 
            this.curveToolStripMenuItem.Checked = true;
            this.curveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.curveToolStripMenuItem.Name = "curveToolStripMenuItem";
            this.curveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.curveToolStripMenuItem.Text = "Curve";
            this.curveToolStripMenuItem.Click += new System.EventHandler(this.curveToolStripMenuItem_Click);
            // 
            // bClear
            // 
            this.bClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bClear.BackColor = System.Drawing.Color.Transparent;
            this.bClear.Location = new System.Drawing.Point(1110, 36);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(68, 23);
            this.bClear.TabIndex = 7;
            this.bClear.Text = "Delete";
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // histogram
            // 
            this.histogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.histogram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.histogram.BorderSkin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.histogram.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            legend1.Enabled = false;
            legend1.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.histogram.Legends.Add(legend1);
            this.histogram.Location = new System.Drawing.Point(95, 464);
            this.histogram.Name = "histogram";
            this.histogram.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            series1.ChartArea = "ChartArea1";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.histogram.Series.Add(series1);
            this.histogram.Size = new System.Drawing.Size(936, 90);
            this.histogram.TabIndex = 8;
            this.histogram.Text = "chart1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "RGB",
            "R",
            "G",
            "B",
            "Brightness"});
            this.comboBox2.Location = new System.Drawing.Point(14, 493);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(75, 23);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // credits
            // 
            this.credits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.credits.AutoSize = true;
            this.credits.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.credits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.credits.Location = new System.Drawing.Point(1100, 557);
            this.credits.Name = "credits";
            this.credits.Size = new System.Drawing.Size(90, 11);
            this.credits.TabIndex = 12;
            this.credits.Text = "by Kirill Kochanovskiy";
            // 
            // themeBox1
            // 
            this.themeBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.themeBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.themeBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.themeBox1.Location = new System.Drawing.Point(1169, 3);
            this.themeBox1.Name = "themeBox1";
            this.themeBox1.Size = new System.Drawing.Size(18, 18);
            this.themeBox1.TabIndex = 11;
            this.themeBox1.TabStop = false;
            this.themeBox1.Click += new System.EventHandler(this.themeBox1_Click);
            // 
            // ImageOutput
            // 
            this.ImageOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageOutput.ContextMenuStrip = this.PictureBoxMenuStrip1;
            this.ImageOutput.Location = new System.Drawing.Point(12, 36);
            this.ImageOutput.Name = "ImageOutput";
            this.ImageOutput.Size = new System.Drawing.Size(1019, 422);
            this.ImageOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageOutput.TabIndex = 0;
            this.ImageOutput.TabStop = false;
            // 
            // PictureBoxMenuStrip1
            // 
            this.PictureBoxMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
            this.PictureBoxMenuStrip1.Name = "PictureBoxMenuStrip1";
            this.PictureBoxMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // debug
            // 
            this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.debug.AutoSize = true;
            this.debug.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.debug.Location = new System.Drawing.Point(-1, 557);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(0, 11);
            this.debug.TabIndex = 13;
            // 
            // opacityBar
            // 
            this.opacityBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.opacityBar.AutoSize = false;
            this.opacityBar.Location = new System.Drawing.Point(1030, 123);
            this.opacityBar.Maximum = 100;
            this.opacityBar.Name = "opacityBar";
            this.opacityBar.Size = new System.Drawing.Size(96, 23);
            this.opacityBar.TabIndex = 14;
            this.opacityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.opacityBar.Value = 100;
            this.opacityBar.Scroll += new System.EventHandler(this.transparencyBar_Scroll);
            // 
            // opacity
            // 
            this.opacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.opacity.AutoSize = true;
            this.opacity.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opacity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.opacity.Location = new System.Drawing.Point(1118, 129);
            this.opacity.Name = "opacity";
            this.opacity.Size = new System.Drawing.Size(60, 11);
            this.opacity.TabIndex = 15;
            this.opacity.Text = "Opacity: 100%";
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // channelBox
            // 
            this.channelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.channelBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.channelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channelBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.channelBox.FormattingEnabled = true;
            this.channelBox.Items.AddRange(new object[] {
            "RGB",
            "R",
            "G",
            "B",
            "Brightness"});
            this.channelBox.Location = new System.Drawing.Point(1098, 65);
            this.channelBox.Name = "channelBox";
            this.channelBox.Size = new System.Drawing.Size(80, 23);
            this.channelBox.TabIndex = 17;
            this.channelBox.SelectionChangeCommitted += new System.EventHandler(this.channelBox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(1037, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 11);
            this.label1.TabIndex = 18;
            this.label1.Text = "Channel:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(1037, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 11);
            this.label2.TabIndex = 19;
            this.label2.Text = "Blend mode:";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(14, 464);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Run Histogram";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.histogramRender2);
            // 
            // curveEditBox
            // 
            this.curveEditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.curveEditBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.curveEditBox.Location = new System.Drawing.Point(831, 36);
            this.curveEditBox.Name = "curveEditBox";
            this.curveEditBox.Size = new System.Drawing.Size(200, 200);
            this.curveEditBox.TabIndex = 22;
            this.curveEditBox.Paint += new System.Windows.Forms.PaintEventHandler(this.curveEditBox_Paint);
            this.curveEditBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.curveEditBox_MouseClick);
            this.curveEditBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.curveEditBox_MouseUp);
            // 
            // bCurve
            // 
            this.bCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCurve.Location = new System.Drawing.Point(963, 242);
            this.bCurve.Name = "bCurve";
            this.bCurve.Size = new System.Drawing.Size(68, 23);
            this.bCurve.TabIndex = 23;
            this.bCurve.Text = "Refresh";
            this.bCurve.UseVisualStyleBackColor = true;
            this.bCurve.Click += new System.EventHandler(this.bCurve_Click);
            // 
            // bApplyCurve
            // 
            this.bApplyCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bApplyCurve.Location = new System.Drawing.Point(889, 242);
            this.bApplyCurve.Name = "bApplyCurve";
            this.bApplyCurve.Size = new System.Drawing.Size(68, 23);
            this.bApplyCurve.TabIndex = 24;
            this.bApplyCurve.Text = "Apply";
            this.bApplyCurve.UseVisualStyleBackColor = true;
            this.bApplyCurve.Click += new System.EventHandler(this.bApplyCurve_Click);
            // 
            // bRender3
            // 
            this.bRender3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRender3.Location = new System.Drawing.Point(1036, 36);
            this.bRender3.Name = "bRender3";
            this.bRender3.Size = new System.Drawing.Size(68, 23);
            this.bRender3.TabIndex = 25;
            this.bRender3.Text = "Render 2.0";
            this.bRender3.UseVisualStyleBackColor = true;
            this.bRender3.Click += new System.EventHandler(this.bRender_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(1190, 568);
            this.Controls.Add(this.bRender3);
            this.Controls.Add(this.bApplyCurve);
            this.Controls.Add(this.bCurve);
            this.Controls.Add(this.curveEditBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.channelBox);
            this.Controls.Add(this.opacity);
            this.Controls.Add(this.opacityBar);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.credits);
            this.Controls.Add(this.themeBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.histogram);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.LayerList);
            this.Controls.Add(this.ImageOutput);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aboobe Photoboob";
            this.ImageListMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.histogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.themeBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOutput)).EndInit();
            this.PictureBoxMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageOutput;
        private System.Windows.Forms.ListView LayerList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binarizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessContrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorBalanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blurToolStripMenuItem;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.DataVisualization.Charting.Chart histogram;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ToolStripMenuItem джепегированиеToolStripMenuItem;
        private System.Windows.Forms.PictureBox themeBox1;
        private System.Windows.Forms.Label credits;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogrammToolStripMenuItem;
        private System.Windows.Forms.Label debug;
        private System.Windows.Forms.TrackBar opacityBar;
        private System.Windows.Forms.Label opacity;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ComboBox channelBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip PictureBoxMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ImageListMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoHistogramToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otsusMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem niblacksMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvolasMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wulffsMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bradleysMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sliderMethodToolStripMenuItem;
        private System.Windows.Forms.Panel curveEditBox;
        private System.Windows.Forms.Button bCurve;
        private System.Windows.Forms.ToolStripMenuItem curveToolStripMenuItem;
        private System.Windows.Forms.Button bApplyCurve;
        private System.Windows.Forms.Button bRender3;
    }
}

