
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.aSCIIFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.джепегированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessContrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorBalanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoHistogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.curveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalCurveMarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.label3 = new System.Windows.Forms.Label();
            this.BinarizationPanel = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SaveResult = new System.Windows.Forms.Button();
            this.GetText = new System.Windows.Forms.Button();
            this.BinarizationSlider = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Binarization8 = new System.Windows.Forms.Button();
            this.Binarization7 = new System.Windows.Forms.Button();
            this.Binarization6 = new System.Windows.Forms.Button();
            this.Binarization5 = new System.Windows.Forms.Button();
            this.Binarization3 = new System.Windows.Forms.Button();
            this.Binarization2 = new System.Windows.Forms.Button();
            this.Binarization1 = new System.Windows.Forms.Button();
            this.OCRText = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.filterMode = new System.Windows.Forms.ComboBox();
            this.bEmpty = new System.Windows.Forms.Button();
            this.labelMedian = new System.Windows.Forms.Label();
            this.MedianValue = new System.Windows.Forms.TextBox();
            this.MatrixY = new System.Windows.Forms.TextBox();
            this.MatrixX = new System.Windows.Forms.TextBox();
            this.labelDimentions = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.frequencyFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageListMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.histogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.themeBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOutput)).BeginInit();
            this.PictureBoxMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).BeginInit();
            this.BinarizationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationSlider)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.frequencyFilterToolStripMenuItem,
            this.aSCIIFilterToolStripMenuItem,
            this.джепегированиеToolStripMenuItem,
            this.brightnessContrastToolStripMenuItem,
            this.colorBalanceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.editToolStripMenuItem.Text = "Adjustments";
            // 
            // aSCIIFilterToolStripMenuItem
            // 
            this.aSCIIFilterToolStripMenuItem.Name = "aSCIIFilterToolStripMenuItem";
            this.aSCIIFilterToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.aSCIIFilterToolStripMenuItem.Text = "ASCII Filter";
            this.aSCIIFilterToolStripMenuItem.Click += new System.EventHandler(this.aSCIIFilterToolStripMenuItem_Click);
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
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogrammToolStripMenuItem,
            this.autoHistogramToolStripMenuItem,
            this.curveToolStripMenuItem,
            this.additionalCurveMarkersToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // histogrammToolStripMenuItem
            // 
            this.histogrammToolStripMenuItem.Checked = true;
            this.histogrammToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.histogrammToolStripMenuItem.Name = "histogrammToolStripMenuItem";
            this.histogrammToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.histogrammToolStripMenuItem.Text = "Histogram";
            this.histogrammToolStripMenuItem.Click += new System.EventHandler(this.histogrammToolStripMenuItem_Click);
            // 
            // autoHistogramToolStripMenuItem
            // 
            this.autoHistogramToolStripMenuItem.Checked = true;
            this.autoHistogramToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoHistogramToolStripMenuItem.Name = "autoHistogramToolStripMenuItem";
            this.autoHistogramToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.autoHistogramToolStripMenuItem.Text = "Auto Histogram";
            this.autoHistogramToolStripMenuItem.Click += new System.EventHandler(this.autoHistogramToolStripMenuItem_Click);
            // 
            // curveToolStripMenuItem
            // 
            this.curveToolStripMenuItem.Checked = true;
            this.curveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.curveToolStripMenuItem.Name = "curveToolStripMenuItem";
            this.curveToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.curveToolStripMenuItem.Text = "Curve";
            this.curveToolStripMenuItem.Click += new System.EventHandler(this.curveToolStripMenuItem_Click);
            // 
            // additionalCurveMarkersToolStripMenuItem
            // 
            this.additionalCurveMarkersToolStripMenuItem.Checked = true;
            this.additionalCurveMarkersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.additionalCurveMarkersToolStripMenuItem.Name = "additionalCurveMarkersToolStripMenuItem";
            this.additionalCurveMarkersToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.additionalCurveMarkersToolStripMenuItem.Text = "Additional Curve Markers";
            this.additionalCurveMarkersToolStripMenuItem.Click += new System.EventHandler(this.additionalCurveMarkersToolStripMenuItem_Click);
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
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            chartArea2.Name = "ChartArea1";
            chartArea2.ShadowColor = System.Drawing.Color.White;
            this.histogram.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(67)))), ((int)(((byte)(67)))));
            legend2.Enabled = false;
            legend2.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.histogram.Legends.Add(legend2);
            this.histogram.Location = new System.Drawing.Point(95, 464);
            this.histogram.Name = "histogram";
            this.histogram.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            series2.ChartArea = "ChartArea1";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.histogram.Series.Add(series2);
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
            this.ImageOutput.BackColor = System.Drawing.Color.Transparent;
            this.ImageOutput.ContextMenuStrip = this.PictureBoxMenuStrip1;
            this.ImageOutput.Location = new System.Drawing.Point(138, 36);
            this.ImageOutput.Name = "ImageOutput";
            this.ImageOutput.Size = new System.Drawing.Size(609, 422);
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
            this.opacityBar.Location = new System.Drawing.Point(1036, 123);
            this.opacityBar.Maximum = 100;
            this.opacityBar.Name = "opacityBar";
            this.opacityBar.Size = new System.Drawing.Size(90, 23);
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
            this.curveEditBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.curveEditBox.Location = new System.Drawing.Point(85, 4);
            this.curveEditBox.Name = "curveEditBox";
            this.curveEditBox.Size = new System.Drawing.Size(200, 200);
            this.curveEditBox.TabIndex = 22;
            this.curveEditBox.Paint += new System.Windows.Forms.PaintEventHandler(this.curveEditBox_Paint);
            this.curveEditBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.curveEditBox_MouseUp);
            this.curveEditBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.curveEditBox_MouseMove);
            // 
            // bCurve
            // 
            this.bCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCurve.Location = new System.Drawing.Point(11, 33);
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
            this.bApplyCurve.Location = new System.Drawing.Point(10, 4);
            this.bApplyCurve.Name = "bApplyCurve";
            this.bApplyCurve.Size = new System.Drawing.Size(68, 23);
            this.bApplyCurve.TabIndex = 24;
            this.bApplyCurve.Text = "Apply";
            this.bApplyCurve.UseVisualStyleBackColor = true;
            this.bApplyCurve.Click += new System.EventHandler(this.bApplyCurve2_Click);
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
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(990, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 10);
            this.label3.TabIndex = 26;
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // BinarizationPanel
            // 
            this.BinarizationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BinarizationPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.BinarizationPanel.Controls.Add(this.label8);
            this.BinarizationPanel.Controls.Add(this.label7);
            this.BinarizationPanel.Controls.Add(this.SaveResult);
            this.BinarizationPanel.Controls.Add(this.GetText);
            this.BinarizationPanel.Controls.Add(this.BinarizationSlider);
            this.BinarizationPanel.Controls.Add(this.label6);
            this.BinarizationPanel.Controls.Add(this.label5);
            this.BinarizationPanel.Controls.Add(this.label4);
            this.BinarizationPanel.Controls.Add(this.Binarization8);
            this.BinarizationPanel.Controls.Add(this.Binarization7);
            this.BinarizationPanel.Controls.Add(this.Binarization6);
            this.BinarizationPanel.Controls.Add(this.Binarization5);
            this.BinarizationPanel.Controls.Add(this.Binarization3);
            this.BinarizationPanel.Controls.Add(this.Binarization2);
            this.BinarizationPanel.Controls.Add(this.Binarization1);
            this.BinarizationPanel.Location = new System.Drawing.Point(12, 36);
            this.BinarizationPanel.Name = "BinarizationPanel";
            this.BinarizationPanel.Size = new System.Drawing.Size(120, 422);
            this.BinarizationPanel.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label8.Location = new System.Drawing.Point(0, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Slider";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Location = new System.Drawing.Point(0, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "0";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveResult
            // 
            this.SaveResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveResult.Location = new System.Drawing.Point(16, 346);
            this.SaveResult.Name = "SaveResult";
            this.SaveResult.Size = new System.Drawing.Size(89, 23);
            this.SaveResult.TabIndex = 39;
            this.SaveResult.Text = "Save result";
            this.SaveResult.UseVisualStyleBackColor = true;
            this.SaveResult.Click += new System.EventHandler(this.SaveResult_Click);
            // 
            // GetText
            // 
            this.GetText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GetText.Location = new System.Drawing.Point(16, 375);
            this.GetText.Name = "GetText";
            this.GetText.Size = new System.Drawing.Size(89, 23);
            this.GetText.TabIndex = 38;
            this.GetText.Text = "Read Text";
            this.GetText.UseVisualStyleBackColor = true;
            this.GetText.Click += new System.EventHandler(this.GetText_Click);
            // 
            // BinarizationSlider
            // 
            this.BinarizationSlider.LargeChange = 1;
            this.BinarizationSlider.Location = new System.Drawing.Point(0, 158);
            this.BinarizationSlider.Maximum = 255;
            this.BinarizationSlider.Name = "BinarizationSlider";
            this.BinarizationSlider.Size = new System.Drawing.Size(120, 45);
            this.BinarizationSlider.TabIndex = 37;
            this.BinarizationSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BinarizationSlider.Scroll += new System.EventHandler(this.BinarizationSlider_Scroll);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Montserrat SemiBold", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(0, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 18);
            this.label6.TabIndex = 36;
            this.label6.Text = "Binarization";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(0, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 35;
            this.label5.Text = "Local methods";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(0, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Global methods";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Binarization8
            // 
            this.Binarization8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization8.Location = new System.Drawing.Point(16, 309);
            this.Binarization8.Name = "Binarization8";
            this.Binarization8.Size = new System.Drawing.Size(89, 23);
            this.Binarization8.TabIndex = 32;
            this.Binarization8.Text = "Bradley";
            this.Binarization8.UseVisualStyleBackColor = true;
            this.Binarization8.Click += new System.EventHandler(this.Binarization8_Click);
            // 
            // Binarization7
            // 
            this.Binarization7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization7.Location = new System.Drawing.Point(15, 280);
            this.Binarization7.Name = "Binarization7";
            this.Binarization7.Size = new System.Drawing.Size(89, 23);
            this.Binarization7.TabIndex = 31;
            this.Binarization7.Text = "Wulff";
            this.Binarization7.UseVisualStyleBackColor = true;
            this.Binarization7.Click += new System.EventHandler(this.Binarization7_Click);
            // 
            // Binarization6
            // 
            this.Binarization6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization6.Location = new System.Drawing.Point(15, 251);
            this.Binarization6.Name = "Binarization6";
            this.Binarization6.Size = new System.Drawing.Size(89, 23);
            this.Binarization6.TabIndex = 30;
            this.Binarization6.Text = "Sauvola";
            this.Binarization6.UseVisualStyleBackColor = true;
            this.Binarization6.Click += new System.EventHandler(this.Binarization6_Click);
            // 
            // Binarization5
            // 
            this.Binarization5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization5.Location = new System.Drawing.Point(15, 222);
            this.Binarization5.Name = "Binarization5";
            this.Binarization5.Size = new System.Drawing.Size(89, 23);
            this.Binarization5.TabIndex = 29;
            this.Binarization5.Text = "Niblack";
            this.Binarization5.UseVisualStyleBackColor = true;
            this.Binarization5.Click += new System.EventHandler(this.Binarization5_Click);
            // 
            // Binarization3
            // 
            this.Binarization3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization3.Font = new System.Drawing.Font("Montserrat", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Binarization3.Location = new System.Drawing.Point(15, 103);
            this.Binarization3.Name = "Binarization3";
            this.Binarization3.Size = new System.Drawing.Size(89, 23);
            this.Binarization3.TabIndex = 28;
            this.Binarization3.Text = "Kochanovskiy";
            this.Binarization3.UseVisualStyleBackColor = true;
            this.Binarization3.Click += new System.EventHandler(this.Binarization3_Click);
            // 
            // Binarization2
            // 
            this.Binarization2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization2.Location = new System.Drawing.Point(15, 74);
            this.Binarization2.Name = "Binarization2";
            this.Binarization2.Size = new System.Drawing.Size(89, 23);
            this.Binarization2.TabIndex = 27;
            this.Binarization2.Text = "Otsu";
            this.Binarization2.UseVisualStyleBackColor = true;
            this.Binarization2.Click += new System.EventHandler(this.Binarization2_Click);
            // 
            // Binarization1
            // 
            this.Binarization1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Binarization1.Location = new System.Drawing.Point(15, 45);
            this.Binarization1.Name = "Binarization1";
            this.Binarization1.Size = new System.Drawing.Size(89, 23);
            this.Binarization1.TabIndex = 26;
            this.Binarization1.Text = "Gavrilov";
            this.Binarization1.UseVisualStyleBackColor = true;
            this.Binarization1.Click += new System.EventHandler(this.Binarization1_Click);
            // 
            // OCRText
            // 
            this.OCRText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OCRText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.OCRText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OCRText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OCRText.Location = new System.Drawing.Point(138, 36);
            this.OCRText.Name = "OCRText";
            this.OCRText.Size = new System.Drawing.Size(120, 422);
            this.OCRText.TabIndex = 43;
            this.OCRText.Text = "";
            this.OCRText.Visible = false;
            this.OCRText.Click += new System.EventHandler(this.OCRText_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.filterMode);
            this.panel1.Controls.Add(this.bEmpty);
            this.panel1.Controls.Add(this.labelMedian);
            this.panel1.Controls.Add(this.MedianValue);
            this.panel1.Controls.Add(this.MatrixY);
            this.panel1.Controls.Add(this.MatrixX);
            this.panel1.Controls.Add(this.labelDimentions);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.curveEditBox);
            this.panel1.Controls.Add(this.bCurve);
            this.panel1.Controls.Add(this.bApplyCurve);
            this.panel1.Location = new System.Drawing.Point(743, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 422);
            this.panel1.TabIndex = 42;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(10, 321);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 47;
            this.button2.Text = "Apply but fast";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(10, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label9.Location = new System.Drawing.Point(8, 410);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 11);
            this.label9.TabIndex = 45;
            this.label9.Text = "Sum: ";
            this.label9.Visible = false;
            // 
            // filterMode
            // 
            this.filterMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filterMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.filterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.filterMode.FormattingEnabled = true;
            this.filterMode.Items.AddRange(new object[] {
            "Linear",
            "Gauss",
            "Sharpen",
            "Prewitt operator",
            "Sobel operator",
            "Laplace operator",
            "Median"});
            this.filterMode.Location = new System.Drawing.Point(11, 263);
            this.filterMode.Name = "filterMode";
            this.filterMode.Size = new System.Drawing.Size(68, 23);
            this.filterMode.TabIndex = 44;
            this.filterMode.SelectedIndexChanged += new System.EventHandler(this.filterMode_SelectedIndexChanged);
            // 
            // bEmpty
            // 
            this.bEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bEmpty.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEmpty.Location = new System.Drawing.Point(44, 292);
            this.bEmpty.Name = "bEmpty";
            this.bEmpty.Size = new System.Drawing.Size(35, 23);
            this.bEmpty.TabIndex = 33;
            this.bEmpty.Text = "Clear";
            this.bEmpty.UseVisualStyleBackColor = true;
            this.bEmpty.Click += new System.EventHandler(this.bEmpty_Click);
            // 
            // labelMedian
            // 
            this.labelMedian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMedian.AutoSize = true;
            this.labelMedian.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMedian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelMedian.Location = new System.Drawing.Point(8, 372);
            this.labelMedian.Name = "labelMedian";
            this.labelMedian.Size = new System.Drawing.Size(32, 11);
            this.labelMedian.TabIndex = 32;
            this.labelMedian.Text = "Sigma:";
            // 
            // MedianValue
            // 
            this.MedianValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MedianValue.Location = new System.Drawing.Point(10, 386);
            this.MedianValue.Name = "MedianValue";
            this.MedianValue.Size = new System.Drawing.Size(68, 21);
            this.MedianValue.TabIndex = 31;
            this.MedianValue.Text = "1";
            this.MedianValue.TextChanged += new System.EventHandler(this.MedianValue_TextChanged);
            // 
            // MatrixY
            // 
            this.MatrixY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MatrixY.Location = new System.Drawing.Point(51, 236);
            this.MatrixY.Name = "MatrixY";
            this.MatrixY.Size = new System.Drawing.Size(28, 21);
            this.MatrixY.TabIndex = 28;
            this.MatrixY.Text = "1";
            this.MatrixY.TextChanged += new System.EventHandler(this.MatrixY_TextChanged);
            // 
            // MatrixX
            // 
            this.MatrixX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MatrixX.Location = new System.Drawing.Point(11, 236);
            this.MatrixX.Name = "MatrixX";
            this.MatrixX.Size = new System.Drawing.Size(28, 21);
            this.MatrixX.TabIndex = 27;
            this.MatrixX.Text = "1";
            this.MatrixX.TextChanged += new System.EventHandler(this.MatrixX_TextChanged);
            // 
            // labelDimentions
            // 
            this.labelDimentions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDimentions.AutoSize = true;
            this.labelDimentions.Font = new System.Drawing.Font("Montserrat", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimentions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelDimentions.Location = new System.Drawing.Point(8, 221);
            this.labelDimentions.Name = "labelDimentions";
            this.labelDimentions.Size = new System.Drawing.Size(73, 11);
            this.labelDimentions.TabIndex = 26;
            this.labelDimentions.Text = "Dimentions r1 : r2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Location = new System.Drawing.Point(88, 222);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.Size = new System.Drawing.Size(197, 197);
            this.dataGridView1.TabIndex = 25;
            // 
            // frequencyFilterToolStripMenuItem
            // 
            this.frequencyFilterToolStripMenuItem.Name = "frequencyFilterToolStripMenuItem";
            this.frequencyFilterToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.frequencyFilterToolStripMenuItem.Text = "Frequency Filter";
            this.frequencyFilterToolStripMenuItem.Click += new System.EventHandler(this.frequencyFilterToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(1190, 568);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.OCRText);
            this.Controls.Add(this.BinarizationPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bRender3);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ImageListMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.histogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.themeBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageOutput)).EndInit();
            this.PictureBoxMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).EndInit();
            this.BinarizationPanel.ResumeLayout(false);
            this.BinarizationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationSlider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView LayerList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessContrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorBalanceToolStripMenuItem;
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
        private System.Windows.Forms.Panel curveEditBox;
        private System.Windows.Forms.Button bCurve;
        private System.Windows.Forms.ToolStripMenuItem curveToolStripMenuItem;
        private System.Windows.Forms.Button bApplyCurve;
        private System.Windows.Forms.Button bRender3;
        private System.Windows.Forms.ToolStripMenuItem additionalCurveMarkersToolStripMenuItem;
        public System.Windows.Forms.PictureBox ImageOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem aSCIIFilterToolStripMenuItem;
        private System.Windows.Forms.Panel BinarizationPanel;
        private System.Windows.Forms.Button GetText;
        private System.Windows.Forms.TrackBar BinarizationSlider;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Binarization8;
        private System.Windows.Forms.Button Binarization7;
        private System.Windows.Forms.Button Binarization6;
        private System.Windows.Forms.Button Binarization5;
        private System.Windows.Forms.Button Binarization3;
        private System.Windows.Forms.Button Binarization2;
        private System.Windows.Forms.Button Binarization1;
        private System.Windows.Forms.Button SaveResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox OCRText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelMedian;
        private System.Windows.Forms.TextBox MedianValue;
        private System.Windows.Forms.TextBox MatrixY;
        private System.Windows.Forms.TextBox MatrixX;
        private System.Windows.Forms.Label labelDimentions;
        private System.Windows.Forms.Button bEmpty;
        private System.Windows.Forms.ComboBox filterMode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem frequencyFilterToolStripMenuItem;
    }
}

