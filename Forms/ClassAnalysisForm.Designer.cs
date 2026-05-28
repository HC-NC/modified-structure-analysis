namespace modified_structure_analysis.Forms
{
    partial class ClassAnalysisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassAnalysisForm));
            statusStrip1 = new StatusStrip();
            _summaryToolStripStatusLabel = new ToolStripStatusLabel();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            _classesListBox = new ListBox();
            toolStrip1 = new ToolStrip();
            _exportToolStripDropDownButton = new ToolStripDropDownButton();
            _allStatsToolStripMenuItem = new ToolStripMenuItem();
            _pixelsToolStripMenuItem = new ToolStripMenuItem();
            _plotToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            _statsTabPage = new TabPage();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            _bandsListBox = new ListBox();
            groupBox3 = new GroupBox();
            _propertyGrid = new PropertyGrid();
            _kdeTabPage = new TabPage();
            _kdeSplitContainer = new SplitContainer();
            _kdeListBox = new ListBox();
            _kdeSingleButton = new Button();
            _kdeProductButton = new Button();
            _kdeMultivariateButton = new Button();
            _kdeClearButton = new Button();
            _kdeProgressBar = new ProgressBar();
            _kdePlotView = new OxyPlot.WindowsForms.PlotView();
            _scatterTabPage = new TabPage();
            _scatterSplitContainer1 = new SplitContainer();
            _scatterSplitContainer2 = new SplitContainer();
            _scatterXAxisGroupBox = new GroupBox();
            _scatterXListBox = new ListBox();
            _scatterYAxisGroupBox = new GroupBox();
            _scatterYListBox = new ListBox();
            _buildScatterButton = new Button();
            _scatterProgressBar = new ProgressBar();
            _scatterPlotView = new OxyPlot.WindowsForms.PlotView();
            _pixelsTabPage = new TabPage();
            dataGridView1 = new DataGridView();
            _indexColumn = new DataGridViewTextBoxColumn();
            _colColumn = new DataGridViewTextBoxColumn();
            _rowColumn = new DataGridViewTextBoxColumn();
            _mapXColumn = new DataGridViewTextBoxColumn();
            _mapYColumn = new DataGridViewTextBoxColumn();
            _valueColumn = new DataGridViewTextBoxColumn();
            _zScoreColumn = new DataGridViewTextBoxColumn();
            _saveFileDialog = new SaveFileDialog();
            _backgroundWorker = new System.ComponentModel.BackgroundWorker();
            _scatterWorker = new System.ComponentModel.BackgroundWorker();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            _statsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            _kdeTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_kdeSplitContainer).BeginInit();
            _kdeSplitContainer.Panel1.SuspendLayout();
            _kdeSplitContainer.Panel2.SuspendLayout();
            _kdeSplitContainer.SuspendLayout();
            _scatterTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_scatterSplitContainer1).BeginInit();
            _scatterSplitContainer1.Panel1.SuspendLayout();
            _scatterSplitContainer1.Panel2.SuspendLayout();
            _scatterSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_scatterSplitContainer2).BeginInit();
            _scatterSplitContainer2.Panel1.SuspendLayout();
            _scatterSplitContainer2.Panel2.SuspendLayout();
            _scatterSplitContainer2.SuspendLayout();
            _scatterXAxisGroupBox.SuspendLayout();
            _scatterYAxisGroupBox.SuspendLayout();
            _pixelsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { _summaryToolStripStatusLabel });
            statusStrip1.Location = new Point(0, 712);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1178, 32);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // _summaryToolStripStatusLabel
            // 
            _summaryToolStripStatusLabel.Name = "_summaryToolStripStatusLabel";
            _summaryToolStripStatusLabel.Size = new Size(88, 25);
            _summaryToolStripStatusLabel.Text = "Summary";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Controls.Add(toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(1178, 712);
            splitContainer1.SplitterDistance = 250;
            splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(_classesListBox);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 34);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(250, 678);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Classes";
            // 
            // _classesListBox
            // 
            _classesListBox.Dock = DockStyle.Fill;
            _classesListBox.FormattingEnabled = true;
            _classesListBox.Location = new Point(3, 27);
            _classesListBox.Name = "_classesListBox";
            _classesListBox.Size = new Size(244, 648);
            _classesListBox.TabIndex = 0;
            _classesListBox.SelectedIndexChanged += ClassesListBox_SelectedIndexChanged;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { _exportToolStripDropDownButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(250, 34);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // _exportToolStripDropDownButton
            // 
            _exportToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _exportToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { _allStatsToolStripMenuItem, _pixelsToolStripMenuItem, _plotToolStripMenuItem });
            _exportToolStripDropDownButton.Image = (Image)resources.GetObject("_exportToolStripDropDownButton.Image");
            _exportToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            _exportToolStripDropDownButton.Name = "_exportToolStripDropDownButton";
            _exportToolStripDropDownButton.Size = new Size(81, 29);
            _exportToolStripDropDownButton.Text = "Export";
            // 
            // _allStatsToolStripMenuItem
            // 
            _allStatsToolStripMenuItem.Name = "_allStatsToolStripMenuItem";
            _allStatsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            _allStatsToolStripMenuItem.Size = new Size(270, 34);
            _allStatsToolStripMenuItem.Text = "All Stats";
            _allStatsToolStripMenuItem.Click += ExportToolStripButton_Click;
            // 
            // _pixelsToolStripMenuItem
            // 
            _pixelsToolStripMenuItem.Name = "_pixelsToolStripMenuItem";
            _pixelsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            _pixelsToolStripMenuItem.Size = new Size(270, 34);
            _pixelsToolStripMenuItem.Text = "Class Pixels";
            _pixelsToolStripMenuItem.Click += ExportPixelsTable_Click;
            // 
            // _plotToolStripMenuItem
            // 
            _plotToolStripMenuItem.Name = "_plotToolStripMenuItem";
            _plotToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            _plotToolStripMenuItem.Size = new Size(270, 34);
            _plotToolStripMenuItem.Text = "Active Plot";
            _plotToolStripMenuItem.Click += ExportActivePlot_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(_statsTabPage);
            tabControl1.Controls.Add(_kdeTabPage);
            tabControl1.Controls.Add(_scatterTabPage);
            tabControl1.Controls.Add(_pixelsTabPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size(125, 30);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(924, 712);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 0;
            // 
            // _statsTabPage
            // 
            _statsTabPage.Controls.Add(splitContainer2);
            _statsTabPage.Location = new Point(4, 34);
            _statsTabPage.Name = "_statsTabPage";
            _statsTabPage.Padding = new Padding(3);
            _statsTabPage.Size = new Size(916, 674);
            _statsTabPage.TabIndex = 0;
            _statsTabPage.Text = "Stats";
            _statsTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Location = new Point(3, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(groupBox3);
            splitContainer2.Size = new Size(910, 668);
            splitContainer2.SplitterDistance = 300;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(_bandsListBox);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(300, 668);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Bands";
            // 
            // _bandsListBox
            // 
            _bandsListBox.Dock = DockStyle.Fill;
            _bandsListBox.FormattingEnabled = true;
            _bandsListBox.Location = new Point(3, 27);
            _bandsListBox.Name = "_bandsListBox";
            _bandsListBox.Size = new Size(294, 638);
            _bandsListBox.TabIndex = 0;
            _bandsListBox.SelectedIndexChanged += BandsListBox_SelectedIndexChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(_propertyGrid);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(606, 668);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Property";
            // 
            // _propertyGrid
            // 
            _propertyGrid.BackColor = SystemColors.Control;
            _propertyGrid.Dock = DockStyle.Fill;
            _propertyGrid.Location = new Point(3, 27);
            _propertyGrid.Name = "_propertyGrid";
            _propertyGrid.Size = new Size(600, 638);
            _propertyGrid.TabIndex = 0;
            // 
            // _kdeTabPage
            // 
            _kdeTabPage.Controls.Add(_kdeSplitContainer);
            _kdeTabPage.Location = new Point(4, 34);
            _kdeTabPage.Name = "_kdeTabPage";
            _kdeTabPage.Padding = new Padding(3);
            _kdeTabPage.Size = new Size(916, 674);
            _kdeTabPage.TabIndex = 1;
            _kdeTabPage.Text = "KDE";
            _kdeTabPage.UseVisualStyleBackColor = true;
            // 
            // _kdeSplitContainer
            // 
            _kdeSplitContainer.Dock = DockStyle.Fill;
            _kdeSplitContainer.FixedPanel = FixedPanel.Panel1;
            _kdeSplitContainer.Location = new Point(3, 3);
            _kdeSplitContainer.Name = "_kdeSplitContainer";
            // 
            // _kdeSplitContainer.Panel1
            // 
            _kdeSplitContainer.Panel1.Controls.Add(_kdeListBox);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeSingleButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeProductButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeMultivariateButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeClearButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeProgressBar);
            // 
            // _kdeSplitContainer.Panel2
            // 
            _kdeSplitContainer.Panel2.Controls.Add(_kdePlotView);
            _kdeSplitContainer.Size = new Size(910, 668);
            _kdeSplitContainer.SplitterDistance = 300;
            _kdeSplitContainer.TabIndex = 1;
            // 
            // _kdeListBox
            // 
            _kdeListBox.Dock = DockStyle.Fill;
            _kdeListBox.FormattingEnabled = true;
            _kdeListBox.Location = new Point(0, 0);
            _kdeListBox.Name = "_kdeListBox";
            _kdeListBox.SelectionMode = SelectionMode.MultiExtended;
            _kdeListBox.Size = new Size(300, 474);
            _kdeListBox.TabIndex = 0;
            // 
            // _kdeSingleButton
            // 
            _kdeSingleButton.AutoSize = true;
            _kdeSingleButton.Dock = DockStyle.Bottom;
            _kdeSingleButton.Location = new Point(0, 474);
            _kdeSingleButton.Name = "_kdeSingleButton";
            _kdeSingleButton.Size = new Size(300, 40);
            _kdeSingleButton.TabIndex = 1;
            _kdeSingleButton.Text = "Single";
            _kdeSingleButton.UseVisualStyleBackColor = true;
            _kdeSingleButton.Click += KdeSingleButton_Click;
            // 
            // _kdeProductButton
            // 
            _kdeProductButton.Dock = DockStyle.Bottom;
            _kdeProductButton.Location = new Point(0, 514);
            _kdeProductButton.Name = "_kdeProductButton";
            _kdeProductButton.Size = new Size(300, 40);
            _kdeProductButton.TabIndex = 2;
            _kdeProductButton.Text = "Product";
            _kdeProductButton.UseVisualStyleBackColor = true;
            _kdeProductButton.Click += KdeProductButton_Click;
            // 
            // _kdeMultivariateButton
            // 
            _kdeMultivariateButton.Dock = DockStyle.Bottom;
            _kdeMultivariateButton.Location = new Point(0, 554);
            _kdeMultivariateButton.Name = "_kdeMultivariateButton";
            _kdeMultivariateButton.Size = new Size(300, 40);
            _kdeMultivariateButton.TabIndex = 8;
            _kdeMultivariateButton.Text = "Multivar";
            _kdeMultivariateButton.UseVisualStyleBackColor = true;
            _kdeMultivariateButton.Click += KdeMultivariateButton_Click;
            // 
            // _kdeClearButton
            // 
            _kdeClearButton.Dock = DockStyle.Bottom;
            _kdeClearButton.Location = new Point(0, 594);
            _kdeClearButton.Name = "_kdeClearButton";
            _kdeClearButton.Size = new Size(300, 40);
            _kdeClearButton.TabIndex = 9;
            _kdeClearButton.Text = "Clear";
            _kdeClearButton.UseVisualStyleBackColor = true;
            _kdeClearButton.Click += KdeClearButton_Click;
            // 
            // _kdeProgressBar
            // 
            _kdeProgressBar.Dock = DockStyle.Bottom;
            _kdeProgressBar.Location = new Point(0, 634);
            _kdeProgressBar.Name = "_kdeProgressBar";
            _kdeProgressBar.Size = new Size(300, 34);
            _kdeProgressBar.TabIndex = 10;
            _kdeProgressBar.Visible = false;
            // 
            // _kdePlotView
            // 
            _kdePlotView.Dock = DockStyle.Fill;
            _kdePlotView.Location = new Point(0, 0);
            _kdePlotView.Name = "_kdePlotView";
            _kdePlotView.PanCursor = Cursors.Hand;
            _kdePlotView.Size = new Size(606, 668);
            _kdePlotView.TabIndex = 1;
            _kdePlotView.Text = "plotView1";
            _kdePlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _kdePlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _kdePlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _kdePlotView.DoubleClick += ResetPlotAxes;
            // 
            // _scatterTabPage
            // 
            _scatterTabPage.Controls.Add(_scatterSplitContainer1);
            _scatterTabPage.Location = new Point(4, 34);
            _scatterTabPage.Name = "_scatterTabPage";
            _scatterTabPage.Padding = new Padding(3);
            _scatterTabPage.Size = new Size(916, 674);
            _scatterTabPage.TabIndex = 3;
            _scatterTabPage.Text = "Scatter Plot";
            _scatterTabPage.UseVisualStyleBackColor = true;
            // 
            // _scatterSplitContainer1
            // 
            _scatterSplitContainer1.Dock = DockStyle.Fill;
            _scatterSplitContainer1.FixedPanel = FixedPanel.Panel1;
            _scatterSplitContainer1.Location = new Point(3, 3);
            _scatterSplitContainer1.Name = "_scatterSplitContainer1";
            // 
            // _scatterSplitContainer1.Panel1
            // 
            _scatterSplitContainer1.Panel1.Controls.Add(_scatterSplitContainer2);
            _scatterSplitContainer1.Panel1.Controls.Add(_buildScatterButton);
            _scatterSplitContainer1.Panel1.Controls.Add(_scatterProgressBar);
            // 
            // _scatterSplitContainer1.Panel2
            // 
            _scatterSplitContainer1.Panel2.Controls.Add(_scatterPlotView);
            _scatterSplitContainer1.Size = new Size(910, 668);
            _scatterSplitContainer1.SplitterDistance = 300;
            _scatterSplitContainer1.TabIndex = 1;
            // 
            // _scatterSplitContainer2
            // 
            _scatterSplitContainer2.Dock = DockStyle.Fill;
            _scatterSplitContainer2.Location = new Point(0, 0);
            _scatterSplitContainer2.Name = "_scatterSplitContainer2";
            _scatterSplitContainer2.Orientation = Orientation.Horizontal;
            // 
            // _scatterSplitContainer2.Panel1
            // 
            _scatterSplitContainer2.Panel1.Controls.Add(_scatterXAxisGroupBox);
            // 
            // _scatterSplitContainer2.Panel2
            // 
            _scatterSplitContainer2.Panel2.Controls.Add(_scatterYAxisGroupBox);
            _scatterSplitContainer2.Size = new Size(300, 594);
            _scatterSplitContainer2.SplitterDistance = 287;
            _scatterSplitContainer2.TabIndex = 3;
            // 
            // _scatterXAxisGroupBox
            // 
            _scatterXAxisGroupBox.Controls.Add(_scatterXListBox);
            _scatterXAxisGroupBox.Dock = DockStyle.Fill;
            _scatterXAxisGroupBox.Location = new Point(0, 0);
            _scatterXAxisGroupBox.Name = "_scatterXAxisGroupBox";
            _scatterXAxisGroupBox.Size = new Size(300, 287);
            _scatterXAxisGroupBox.TabIndex = 0;
            _scatterXAxisGroupBox.TabStop = false;
            _scatterXAxisGroupBox.Text = "X axis";
            // 
            // _scatterXListBox
            // 
            _scatterXListBox.Dock = DockStyle.Fill;
            _scatterXListBox.FormattingEnabled = true;
            _scatterXListBox.Location = new Point(3, 27);
            _scatterXListBox.Name = "_scatterXListBox";
            _scatterXListBox.Size = new Size(294, 257);
            _scatterXListBox.TabIndex = 0;
            // 
            // _scatterYAxisGroupBox
            // 
            _scatterYAxisGroupBox.Controls.Add(_scatterYListBox);
            _scatterYAxisGroupBox.Dock = DockStyle.Fill;
            _scatterYAxisGroupBox.Location = new Point(0, 0);
            _scatterYAxisGroupBox.Name = "_scatterYAxisGroupBox";
            _scatterYAxisGroupBox.Size = new Size(300, 303);
            _scatterYAxisGroupBox.TabIndex = 1;
            _scatterYAxisGroupBox.TabStop = false;
            _scatterYAxisGroupBox.Text = "Y axis";
            // 
            // _scatterYListBox
            // 
            _scatterYListBox.Dock = DockStyle.Fill;
            _scatterYListBox.FormattingEnabled = true;
            _scatterYListBox.Location = new Point(3, 27);
            _scatterYListBox.Name = "_scatterYListBox";
            _scatterYListBox.Size = new Size(294, 273);
            _scatterYListBox.TabIndex = 0;
            // 
            // _buildScatterButton
            // 
            _buildScatterButton.Dock = DockStyle.Bottom;
            _buildScatterButton.Location = new Point(0, 594);
            _buildScatterButton.Name = "_buildScatterButton";
            _buildScatterButton.Size = new Size(300, 40);
            _buildScatterButton.TabIndex = 2;
            _buildScatterButton.Text = "Build";
            _buildScatterButton.UseVisualStyleBackColor = true;
            _buildScatterButton.Click += BuildScatterButton_Click;
            // 
            // _scatterProgressBar
            // 
            _scatterProgressBar.Dock = DockStyle.Bottom;
            _scatterProgressBar.Location = new Point(0, 634);
            _scatterProgressBar.Name = "_scatterProgressBar";
            _scatterProgressBar.Size = new Size(300, 34);
            _scatterProgressBar.TabIndex = 4;
            _scatterProgressBar.Visible = false;
            // 
            // _scatterPlotView
            // 
            _scatterPlotView.Dock = DockStyle.Fill;
            _scatterPlotView.Location = new Point(0, 0);
            _scatterPlotView.Name = "_scatterPlotView";
            _scatterPlotView.PanCursor = Cursors.Hand;
            _scatterPlotView.Size = new Size(606, 668);
            _scatterPlotView.TabIndex = 2;
            _scatterPlotView.Text = "plotView1";
            _scatterPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _scatterPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _scatterPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _scatterPlotView.Click += ResetPlotAxes;
            // 
            // _pixelsTabPage
            // 
            _pixelsTabPage.Controls.Add(dataGridView1);
            _pixelsTabPage.Location = new Point(4, 34);
            _pixelsTabPage.Name = "_pixelsTabPage";
            _pixelsTabPage.Padding = new Padding(3);
            _pixelsTabPage.Size = new Size(916, 674);
            _pixelsTabPage.TabIndex = 2;
            _pixelsTabPage.Text = "Pixels";
            _pixelsTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { _indexColumn, _colColumn, _rowColumn, _mapXColumn, _mapYColumn, _valueColumn, _zScoreColumn });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(910, 668);
            dataGridView1.TabIndex = 0;
            // 
            // _indexColumn
            // 
            _indexColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _indexColumn.HeaderText = "Index";
            _indexColumn.MinimumWidth = 8;
            _indexColumn.Name = "_indexColumn";
            _indexColumn.ReadOnly = true;
            _indexColumn.Width = 91;
            // 
            // _colColumn
            // 
            _colColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _colColumn.HeaderText = "Column";
            _colColumn.MinimumWidth = 8;
            _colColumn.Name = "_colColumn";
            _colColumn.ReadOnly = true;
            // 
            // _rowColumn
            // 
            _rowColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _rowColumn.HeaderText = "Row";
            _rowColumn.MinimumWidth = 8;
            _rowColumn.Name = "_rowColumn";
            _rowColumn.ReadOnly = true;
            // 
            // _mapXColumn
            // 
            _mapXColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _mapXColumn.HeaderText = "Map X";
            _mapXColumn.MinimumWidth = 8;
            _mapXColumn.Name = "_mapXColumn";
            _mapXColumn.ReadOnly = true;
            // 
            // _mapYColumn
            // 
            _mapYColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _mapYColumn.HeaderText = "Map Y";
            _mapYColumn.MinimumWidth = 8;
            _mapYColumn.Name = "_mapYColumn";
            _mapYColumn.ReadOnly = true;
            // 
            // _valueColumn
            // 
            _valueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _valueColumn.HeaderText = "Value";
            _valueColumn.MinimumWidth = 8;
            _valueColumn.Name = "_valueColumn";
            _valueColumn.ReadOnly = true;
            // 
            // _zScoreColumn
            // 
            _zScoreColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _zScoreColumn.HeaderText = "ZScore";
            _zScoreColumn.MinimumWidth = 8;
            _zScoreColumn.Name = "_zScoreColumn";
            _zScoreColumn.ReadOnly = true;
            // 
            // _backgroundWorker
            // 
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += KdeWorker_DoWork;
            _backgroundWorker.ProgressChanged += KdeWorker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += KdeWorker_RunWorkerCompleted;
            // 
            // _scatterWorker
            // 
            _scatterWorker.WorkerReportsProgress = true;
            _scatterWorker.WorkerSupportsCancellation = true;
            _scatterWorker.DoWork += ScatterWorker_DoWork;
            _scatterWorker.ProgressChanged += ScatterWorker_ProgressChanged;
            _scatterWorker.RunWorkerCompleted += ScatterWorker_RunWorkerCompleted;
            // 
            // ClassAnalysisForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 744);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Name = "ClassAnalysisForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Class Statistics Analysis";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            _statsTabPage.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            _kdeTabPage.ResumeLayout(false);
            _kdeSplitContainer.Panel1.ResumeLayout(false);
            _kdeSplitContainer.Panel1.PerformLayout();
            _kdeSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_kdeSplitContainer).EndInit();
            _kdeSplitContainer.ResumeLayout(false);
            _scatterTabPage.ResumeLayout(false);
            _scatterSplitContainer1.Panel1.ResumeLayout(false);
            _scatterSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_scatterSplitContainer1).EndInit();
            _scatterSplitContainer1.ResumeLayout(false);
            _scatterSplitContainer2.Panel1.ResumeLayout(false);
            _scatterSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_scatterSplitContainer2).EndInit();
            _scatterSplitContainer2.ResumeLayout(false);
            _scatterXAxisGroupBox.ResumeLayout(false);
            _scatterYAxisGroupBox.ResumeLayout(false);
            _pixelsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel _summaryToolStripStatusLabel;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private ListBox _classesListBox;
        private TabControl tabControl1;
        private TabPage _statsTabPage;
        private TabPage _kdeTabPage;
        private TabPage _pixelsTabPage;
        private SplitContainer splitContainer2;
        private GroupBox groupBox2;
        private ListBox _bandsListBox;
        private GroupBox groupBox3;
        private PropertyGrid _propertyGrid;
        private SplitContainer _kdeSplitContainer;
        private ListBox _kdeListBox;
        private Button _kdeSingleButton;
        private Button _kdeProductButton;
        private Button _kdeMultivariateButton;
        private Button _kdeClearButton;
        private OxyPlot.WindowsForms.PlotView _kdePlotView;
        private SaveFileDialog _saveFileDialog;
        private ToolStrip toolStrip1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn _indexColumn;
        private DataGridViewTextBoxColumn _colColumn;
        private DataGridViewTextBoxColumn _rowColumn;
        private DataGridViewTextBoxColumn _mapXColumn;
        private DataGridViewTextBoxColumn _mapYColumn;
        private DataGridViewTextBoxColumn _valueColumn;
        private DataGridViewTextBoxColumn _zScoreColumn;
        private System.ComponentModel.BackgroundWorker _backgroundWorker;
        private ProgressBar _kdeProgressBar;
        private TabPage _scatterTabPage;
        private SplitContainer _scatterSplitContainer1;
        private SplitContainer _scatterSplitContainer2;
        private GroupBox _scatterXAxisGroupBox;
        private ListBox _scatterXListBox;
        private GroupBox _scatterYAxisGroupBox;
        private ListBox _scatterYListBox;
        private Button _buildScatterButton;
        private OxyPlot.WindowsForms.PlotView _scatterPlotView;
        private System.ComponentModel.BackgroundWorker _scatterWorker;
        private ProgressBar _scatterProgressBar;
        private ToolStripDropDownButton _exportToolStripDropDownButton;
        private ToolStripMenuItem _allStatsToolStripMenuItem;
        private ToolStripMenuItem _pixelsToolStripMenuItem;
        private ToolStripMenuItem _plotToolStripMenuItem;
    }
}