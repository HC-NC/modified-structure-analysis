using System.ComponentModel;

namespace modified_structure_analysis.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _saveFileDialog?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            _menuStrip = new MenuStrip();
            _fileToolStripMenuItem = new ToolStripMenuItem();
            _openToolStripMenuItem = new ToolStripMenuItem();
            _exportGraphToolStripMenuItem = new ToolStripMenuItem();
            _exportClassificationToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            _exitToolStripMenuItem = new ToolStripMenuItem();
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();
            _statusStrip = new StatusStrip();
            _mainStatusLabel = new ToolStripStatusLabel();
            _mainProgressBar = new ToolStripProgressBar();
            _bandListBox = new ListBox();
            _bandPropertyGrid = new PropertyGrid();
            _bandsDataGroupBox = new GroupBox();
            _bandsDataPropertiesGroupBox = new GroupBox();
            _mainTabControl = new TabControl();
            _dataTabPage = new TabPage();
            _dataSplitContainer = new SplitContainer();
            _bandsDataSplitContainer = new SplitContainer();
            _dataTabControl = new TabControl();
            _dataViewportTabPage = new TabPage();
            _dataViewport = new Viewport();
            _dataViewportToolStrip = new ToolStrip();
            _redToolStripDropDownButton = new ToolStripDropDownButton();
            _greenToolStripDropDownButton = new ToolStripDropDownButton();
            _blueToolStripDropDownButton = new ToolStripDropDownButton();
            _dataHistogramTabPage = new TabPage();
            _histogramPlotView = new OxyPlot.WindowsForms.PlotView();
            _explorationTabPage = new TabPage();
            _explorationTabControl = new TabControl();
            _correlationTabPage = new TabPage();
            _correlationDataGridView = new DataGridView();
            _kdeTabPage = new TabPage();
            _kdeSplitContainer = new SplitContainer();
            _kdeBandsListBox = new ListBox();
            _kdeSingleButton = new Button();
            _kdeProductButton = new Button();
            _kdeMultivariateButton = new Button();
            _kdeClearButton = new Button();
            _kdePlotView = new OxyPlot.WindowsForms.PlotView();
            _scatterTabPage = new TabPage();
            _scatterSplitContainer1 = new SplitContainer();
            _scatterSplitContainer2 = new SplitContainer();
            _scatterXAxisGroupBox = new GroupBox();
            _scatterXListBox = new ListBox();
            _scatterYAxisGroupBox = new GroupBox();
            _scatterYListBox = new ListBox();
            _buildScatterButton = new Button();
            _scatterPlotView = new OxyPlot.WindowsForms.PlotView();
            _classificationTabPage = new TabPage();
            _classificationTabControl = new TabControl();
            _primaryClassificationTabPage = new TabPage();
            _primaryClassificationSplitContainer = new SplitContainer();
            _primaryClassificationRuleSplitContainer = new SplitContainer();
            _primaryRuleDataGridView = new DataGridView();
            _primaryClassificationRichTextBox = new RichTextBox();
            _primaryClassificationRuleToolStrip = new ToolStrip();
            _primaryClassificationAddRuleToolStripButton = new ToolStripButton();
            _primaryClassificationDeleteRuleToolStripButton = new ToolStripButton();
            toolStripSeparator7 = new ToolStripSeparator();
            _primaryClassificationMoveRuleUpToolStripButton = new ToolStripButton();
            _primaryClassificationMoveRuleDownToolStripButton = new ToolStripButton();
            _primaryClassificationAutoGenerateToolStripButton = new ToolStripButton();
            _primaryClassificationTabControl = new TabControl();
            _primaryClassificationViewportTabPage = new TabPage();
            _primaryClassificationViewport = new Viewport();
            _primaryClassificationTableTabPage = new TabPage();
            _primaryClassificationDataGridView = new DataGridView();
            _primaryClassificationToolStrip = new ToolStrip();
            _primaryClassificationCompareToolStripButton = new ToolStripButton();
            _primaryClassificationClassifyToolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            _primaryClassificationModeToolStripComboBox = new ToolStripComboBox();
            toolStripSeparator8 = new ToolStripSeparator();
            _primaryClassificationExportToolStripButton = new ToolStripButton();
            _secondaryClassificationTabPage = new TabPage();
            _secondaryClassificationSplitContainer = new SplitContainer();
            _secondaryClassificationRuleSplitContainer = new SplitContainer();
            _secondaryRuleDataGridView = new DataGridView();
            _secondaryClassificationRichTextBox = new RichTextBox();
            _secondaryClassificationRuleToolStrip = new ToolStrip();
            _secondaryClassificationAddRuleToolStripButton = new ToolStripButton();
            _secondaryClassificationDeleteRuleToolStripButton = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            _secondaryClassificationMoveRuleUpToolStripButton = new ToolStripButton();
            _secondaryClassificationMoveRuleDownToolStripButton = new ToolStripButton();
            _secondaryClassificationTabControl = new TabControl();
            _secondaryClassificationViewportTabPage = new TabPage();
            _secondaryClassificationViewport = new Viewport();
            _secondaryClassificationTableTabPage = new TabPage();
            _secondaryClassificationDataGridView = new DataGridView();
            _secondaryClassificationToolStrip = new ToolStrip();
            _secondaryClassificationCompareToolStripButton = new ToolStripButton();
            _secondaryClassificationClassifyToolStripButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            _secondaryClassificationExportToolStripButton = new ToolStripButton();
            _classificationAbortButton = new Button();
            _ruleContextMenuStrip = new ContextMenuStrip(components);
            _moveUpToolStripMenuItem = new ToolStripMenuItem();
            _moveDownToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            _editToolStripMenuItem = new ToolStripMenuItem();
            _cloneToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            _removeToolStripMenuItem = new ToolStripMenuItem();
            _classifyWorker = new BackgroundWorker();
            _statsWorker = new BackgroundWorker();
            _kdeWorker = new BackgroundWorker();
            _scatterWorker = new BackgroundWorker();
            _loadWorker = new BackgroundWorker();
            _secondaryClassificationColorCol = new DataGridViewImageColumn();
            _secondaryClassificationNameCol = new DataGridViewTextBoxColumn();
            _secondaryClassificationCountCol = new DataGridViewTextBoxColumn();
            _secondaryClassificationEditCol = new DataGridViewButtonColumn();
            NameColumn = new DataGridViewTextBoxColumn();
            EditColumn = new DataGridViewButtonColumn();
            _primaryClassificationColorCol = new DataGridViewImageColumn();
            _primaryClassificationNameCol = new DataGridViewTextBoxColumn();
            _primaryClassificationCountCol = new DataGridViewTextBoxColumn();
            _primaryClassificationEditCol = new DataGridViewButtonColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewButtonColumn1 = new DataGridViewButtonColumn();
            _menuStrip.SuspendLayout();
            _statusStrip.SuspendLayout();
            _bandsDataGroupBox.SuspendLayout();
            _bandsDataPropertiesGroupBox.SuspendLayout();
            _mainTabControl.SuspendLayout();
            _dataTabPage.SuspendLayout();
            ((ISupportInitialize)_dataSplitContainer).BeginInit();
            _dataSplitContainer.Panel1.SuspendLayout();
            _dataSplitContainer.Panel2.SuspendLayout();
            _dataSplitContainer.SuspendLayout();
            ((ISupportInitialize)_bandsDataSplitContainer).BeginInit();
            _bandsDataSplitContainer.Panel1.SuspendLayout();
            _bandsDataSplitContainer.Panel2.SuspendLayout();
            _bandsDataSplitContainer.SuspendLayout();
            _dataTabControl.SuspendLayout();
            _dataViewportTabPage.SuspendLayout();
            _dataViewportToolStrip.SuspendLayout();
            _dataHistogramTabPage.SuspendLayout();
            _explorationTabPage.SuspendLayout();
            _explorationTabControl.SuspendLayout();
            _correlationTabPage.SuspendLayout();
            ((ISupportInitialize)_correlationDataGridView).BeginInit();
            _kdeTabPage.SuspendLayout();
            ((ISupportInitialize)_kdeSplitContainer).BeginInit();
            _kdeSplitContainer.Panel1.SuspendLayout();
            _kdeSplitContainer.Panel2.SuspendLayout();
            _kdeSplitContainer.SuspendLayout();
            _scatterTabPage.SuspendLayout();
            ((ISupportInitialize)_scatterSplitContainer1).BeginInit();
            _scatterSplitContainer1.Panel1.SuspendLayout();
            _scatterSplitContainer1.Panel2.SuspendLayout();
            _scatterSplitContainer1.SuspendLayout();
            ((ISupportInitialize)_scatterSplitContainer2).BeginInit();
            _scatterSplitContainer2.Panel1.SuspendLayout();
            _scatterSplitContainer2.Panel2.SuspendLayout();
            _scatterSplitContainer2.SuspendLayout();
            _scatterXAxisGroupBox.SuspendLayout();
            _scatterYAxisGroupBox.SuspendLayout();
            _classificationTabPage.SuspendLayout();
            _classificationTabControl.SuspendLayout();
            _primaryClassificationTabPage.SuspendLayout();
            ((ISupportInitialize)_primaryClassificationSplitContainer).BeginInit();
            _primaryClassificationSplitContainer.Panel1.SuspendLayout();
            _primaryClassificationSplitContainer.Panel2.SuspendLayout();
            _primaryClassificationSplitContainer.SuspendLayout();
            ((ISupportInitialize)_primaryClassificationRuleSplitContainer).BeginInit();
            _primaryClassificationRuleSplitContainer.Panel1.SuspendLayout();
            _primaryClassificationRuleSplitContainer.Panel2.SuspendLayout();
            _primaryClassificationRuleSplitContainer.SuspendLayout();
            ((ISupportInitialize)_primaryRuleDataGridView).BeginInit();
            _primaryClassificationRuleToolStrip.SuspendLayout();
            _primaryClassificationTabControl.SuspendLayout();
            _primaryClassificationViewportTabPage.SuspendLayout();
            _primaryClassificationTableTabPage.SuspendLayout();
            ((ISupportInitialize)_primaryClassificationDataGridView).BeginInit();
            _primaryClassificationToolStrip.SuspendLayout();
            _secondaryClassificationTabPage.SuspendLayout();
            ((ISupportInitialize)_secondaryClassificationSplitContainer).BeginInit();
            _secondaryClassificationSplitContainer.Panel1.SuspendLayout();
            _secondaryClassificationSplitContainer.Panel2.SuspendLayout();
            _secondaryClassificationSplitContainer.SuspendLayout();
            ((ISupportInitialize)_secondaryClassificationRuleSplitContainer).BeginInit();
            _secondaryClassificationRuleSplitContainer.Panel1.SuspendLayout();
            _secondaryClassificationRuleSplitContainer.Panel2.SuspendLayout();
            _secondaryClassificationRuleSplitContainer.SuspendLayout();
            ((ISupportInitialize)_secondaryRuleDataGridView).BeginInit();
            _secondaryClassificationRuleToolStrip.SuspendLayout();
            _secondaryClassificationTabControl.SuspendLayout();
            _secondaryClassificationViewportTabPage.SuspendLayout();
            _secondaryClassificationTableTabPage.SuspendLayout();
            ((ISupportInitialize)_secondaryClassificationDataGridView).BeginInit();
            _secondaryClassificationToolStrip.SuspendLayout();
            _ruleContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _menuStrip
            // 
            _menuStrip.ImageScalingSize = new Size(24, 24);
            _menuStrip.Items.AddRange(new ToolStripItem[] { _fileToolStripMenuItem });
            _menuStrip.Location = new Point(0, 0);
            _menuStrip.Name = "_menuStrip";
            _menuStrip.Size = new Size(1178, 33);
            _menuStrip.TabIndex = 0;
            _menuStrip.Text = "_menuStrip";
            // 
            // _fileToolStripMenuItem
            // 
            _fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _openToolStripMenuItem, _exportGraphToolStripMenuItem, _exportClassificationToolStripMenuItem, toolStripSeparator1, _exitToolStripMenuItem });
            _fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            _fileToolStripMenuItem.Size = new Size(54, 29);
            _fileToolStripMenuItem.Text = "File";
            // 
            // _openToolStripMenuItem
            // 
            _openToolStripMenuItem.Name = "_openToolStripMenuItem";
            _openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            _openToolStripMenuItem.Size = new Size(345, 34);
            _openToolStripMenuItem.Text = "Open";
            _openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // _exportGraphToolStripMenuItem
            // 
            _exportGraphToolStripMenuItem.Name = "_exportGraphToolStripMenuItem";
            _exportGraphToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.G;
            _exportGraphToolStripMenuItem.Size = new Size(345, 34);
            _exportGraphToolStripMenuItem.Text = "Export Graph…";
            _exportGraphToolStripMenuItem.Click += ExportActivePlot;
            // 
            // _exportClassificationToolStripMenuItem
            // 
            _exportClassificationToolStripMenuItem.Name = "_exportClassificationToolStripMenuItem";
            _exportClassificationToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            _exportClassificationToolStripMenuItem.Size = new Size(345, 34);
            _exportClassificationToolStripMenuItem.Text = "Export Classification…";
            _exportClassificationToolStripMenuItem.Click += ExportClassification;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(342, 6);
            // 
            // _exitToolStripMenuItem
            // 
            _exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
            _exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            _exitToolStripMenuItem.Size = new Size(345, 34);
            _exitToolStripMenuItem.Text = "Exit";
            _exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // _statusStrip
            // 
            _statusStrip.ImageScalingSize = new Size(24, 24);
            _statusStrip.Items.AddRange(new ToolStripItem[] { _mainStatusLabel, _mainProgressBar });
            _statusStrip.Location = new Point(0, 712);
            _statusStrip.Name = "_statusStrip";
            _statusStrip.Size = new Size(1178, 32);
            _statusStrip.TabIndex = 2;
            _statusStrip.Text = "_statusStrip";
            // 
            // _mainStatusLabel
            // 
            _mainStatusLabel.Name = "_mainStatusLabel";
            _mainStatusLabel.Size = new Size(60, 25);
            _mainStatusLabel.Text = "Status";
            // 
            // _mainProgressBar
            // 
            _mainProgressBar.Name = "_mainProgressBar";
            _mainProgressBar.Size = new Size(100, 24);
            // 
            // _bandListBox
            // 
            _bandListBox.Dock = DockStyle.Fill;
            _bandListBox.FormattingEnabled = true;
            _bandListBox.Location = new Point(3, 27);
            _bandListBox.Name = "_bandListBox";
            _bandListBox.Size = new Size(294, 110);
            _bandListBox.TabIndex = 3;
            _bandListBox.SelectedIndexChanged += bandListBox_SelectedIndexChanged;
            // 
            // _bandPropertyGrid
            // 
            _bandPropertyGrid.BackColor = SystemColors.Control;
            _bandPropertyGrid.Dock = DockStyle.Fill;
            _bandPropertyGrid.Location = new Point(3, 27);
            _bandPropertyGrid.Name = "_bandPropertyGrid";
            _bandPropertyGrid.Size = new Size(294, 461);
            _bandPropertyGrid.TabIndex = 4;
            // 
            // _bandsDataGroupBox
            // 
            _bandsDataGroupBox.Controls.Add(_bandListBox);
            _bandsDataGroupBox.Dock = DockStyle.Fill;
            _bandsDataGroupBox.Location = new Point(0, 0);
            _bandsDataGroupBox.Name = "_bandsDataGroupBox";
            _bandsDataGroupBox.Size = new Size(300, 140);
            _bandsDataGroupBox.TabIndex = 5;
            _bandsDataGroupBox.TabStop = false;
            _bandsDataGroupBox.Text = "Bands";
            // 
            // _bandsDataPropertiesGroupBox
            // 
            _bandsDataPropertiesGroupBox.Controls.Add(_bandPropertyGrid);
            _bandsDataPropertiesGroupBox.Dock = DockStyle.Fill;
            _bandsDataPropertiesGroupBox.Location = new Point(0, 0);
            _bandsDataPropertiesGroupBox.Name = "_bandsDataPropertiesGroupBox";
            _bandsDataPropertiesGroupBox.Size = new Size(300, 491);
            _bandsDataPropertiesGroupBox.TabIndex = 6;
            _bandsDataPropertiesGroupBox.TabStop = false;
            _bandsDataPropertiesGroupBox.Text = "Properties";
            // 
            // _mainTabControl
            // 
            _mainTabControl.Controls.Add(_dataTabPage);
            _mainTabControl.Controls.Add(_explorationTabPage);
            _mainTabControl.Controls.Add(_classificationTabPage);
            _mainTabControl.Dock = DockStyle.Fill;
            _mainTabControl.ItemSize = new Size(150, 30);
            _mainTabControl.Location = new Point(0, 33);
            _mainTabControl.Multiline = true;
            _mainTabControl.Name = "_mainTabControl";
            _mainTabControl.SelectedIndex = 0;
            _mainTabControl.Size = new Size(1178, 679);
            _mainTabControl.SizeMode = TabSizeMode.Fixed;
            _mainTabControl.TabIndex = 7;
            _mainTabControl.Selected += TabControl_Selected;
            // 
            // _dataTabPage
            // 
            _dataTabPage.Controls.Add(_dataSplitContainer);
            _dataTabPage.Location = new Point(4, 34);
            _dataTabPage.Name = "_dataTabPage";
            _dataTabPage.Padding = new Padding(3);
            _dataTabPage.Size = new Size(1170, 641);
            _dataTabPage.TabIndex = 0;
            _dataTabPage.Text = "Data";
            _dataTabPage.UseVisualStyleBackColor = true;
            // 
            // _dataSplitContainer
            // 
            _dataSplitContainer.Dock = DockStyle.Fill;
            _dataSplitContainer.FixedPanel = FixedPanel.Panel1;
            _dataSplitContainer.Location = new Point(3, 3);
            _dataSplitContainer.Name = "_dataSplitContainer";
            // 
            // _dataSplitContainer.Panel1
            // 
            _dataSplitContainer.Panel1.Controls.Add(_bandsDataSplitContainer);
            // 
            // _dataSplitContainer.Panel2
            // 
            _dataSplitContainer.Panel2.Controls.Add(_dataTabControl);
            _dataSplitContainer.Size = new Size(1164, 635);
            _dataSplitContainer.SplitterDistance = 300;
            _dataSplitContainer.TabIndex = 7;
            // 
            // _bandsDataSplitContainer
            // 
            _bandsDataSplitContainer.Dock = DockStyle.Fill;
            _bandsDataSplitContainer.Location = new Point(0, 0);
            _bandsDataSplitContainer.Name = "_bandsDataSplitContainer";
            _bandsDataSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // _bandsDataSplitContainer.Panel1
            // 
            _bandsDataSplitContainer.Panel1.Controls.Add(_bandsDataGroupBox);
            // 
            // _bandsDataSplitContainer.Panel2
            // 
            _bandsDataSplitContainer.Panel2.Controls.Add(_bandsDataPropertiesGroupBox);
            _bandsDataSplitContainer.Size = new Size(300, 635);
            _bandsDataSplitContainer.SplitterDistance = 140;
            _bandsDataSplitContainer.TabIndex = 7;
            // 
            // _dataTabControl
            // 
            _dataTabControl.Alignment = TabAlignment.Bottom;
            _dataTabControl.Controls.Add(_dataViewportTabPage);
            _dataTabControl.Controls.Add(_dataHistogramTabPage);
            _dataTabControl.Dock = DockStyle.Fill;
            _dataTabControl.ItemSize = new Size(150, 30);
            _dataTabControl.Location = new Point(0, 0);
            _dataTabControl.Multiline = true;
            _dataTabControl.Name = "_dataTabControl";
            _dataTabControl.SelectedIndex = 0;
            _dataTabControl.Size = new Size(860, 635);
            _dataTabControl.SizeMode = TabSizeMode.Fixed;
            _dataTabControl.TabIndex = 0;
            _dataTabControl.Selected += TabControl_Selected;
            // 
            // _dataViewportTabPage
            // 
            _dataViewportTabPage.Controls.Add(_dataViewport);
            _dataViewportTabPage.Controls.Add(_dataViewportToolStrip);
            _dataViewportTabPage.Location = new Point(4, 4);
            _dataViewportTabPage.Name = "_dataViewportTabPage";
            _dataViewportTabPage.Padding = new Padding(3);
            _dataViewportTabPage.Size = new Size(852, 597);
            _dataViewportTabPage.TabIndex = 0;
            _dataViewportTabPage.Text = "Viewport";
            _dataViewportTabPage.UseVisualStyleBackColor = true;
            // 
            // _dataViewport
            // 
            _dataViewport.Dock = DockStyle.Fill;
            _dataViewport.Location = new Point(3, 37);
            _dataViewport.Name = "_dataViewport";
            _dataViewport.Size = new Size(846, 557);
            _dataViewport.TabIndex = 0;
            // 
            // _dataViewportToolStrip
            // 
            _dataViewportToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _dataViewportToolStrip.ImageScalingSize = new Size(24, 24);
            _dataViewportToolStrip.Items.AddRange(new ToolStripItem[] { _redToolStripDropDownButton, _greenToolStripDropDownButton, _blueToolStripDropDownButton });
            _dataViewportToolStrip.Location = new Point(3, 3);
            _dataViewportToolStrip.Name = "_dataViewportToolStrip";
            _dataViewportToolStrip.Size = new Size(846, 34);
            _dataViewportToolStrip.TabIndex = 0;
            // 
            // _redToolStripDropDownButton
            // 
            _redToolStripDropDownButton.Image = Properties.Resources.red_sqaure;
            _redToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            _redToolStripDropDownButton.Name = "_redToolStripDropDownButton";
            _redToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
            _redToolStripDropDownButton.Size = new Size(84, 29);
            _redToolStripDropDownButton.Text = "Red";
            // 
            // _greenToolStripDropDownButton
            // 
            _greenToolStripDropDownButton.Image = Properties.Resources.green_sqaure;
            _greenToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            _greenToolStripDropDownButton.Name = "_greenToolStripDropDownButton";
            _greenToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
            _greenToolStripDropDownButton.Size = new Size(100, 29);
            _greenToolStripDropDownButton.Text = "Green";
            // 
            // _blueToolStripDropDownButton
            // 
            _blueToolStripDropDownButton.Image = Properties.Resources.blue_sqaure;
            _blueToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            _blueToolStripDropDownButton.Name = "_blueToolStripDropDownButton";
            _blueToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
            _blueToolStripDropDownButton.Size = new Size(87, 29);
            _blueToolStripDropDownButton.Text = "Blue";
            // 
            // _dataHistogramTabPage
            // 
            _dataHistogramTabPage.Controls.Add(_histogramPlotView);
            _dataHistogramTabPage.Location = new Point(4, 4);
            _dataHistogramTabPage.Name = "_dataHistogramTabPage";
            _dataHistogramTabPage.Padding = new Padding(3);
            _dataHistogramTabPage.Size = new Size(852, 597);
            _dataHistogramTabPage.TabIndex = 1;
            _dataHistogramTabPage.Text = "Histogram";
            _dataHistogramTabPage.UseVisualStyleBackColor = true;
            // 
            // _histogramPlotView
            // 
            _histogramPlotView.Dock = DockStyle.Fill;
            _histogramPlotView.Location = new Point(3, 3);
            _histogramPlotView.Name = "_histogramPlotView";
            _histogramPlotView.PanCursor = Cursors.Hand;
            _histogramPlotView.Size = new Size(846, 591);
            _histogramPlotView.TabIndex = 0;
            _histogramPlotView.Text = "_histogramPlotView";
            _histogramPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _histogramPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _histogramPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _histogramPlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // _explorationTabPage
            // 
            _explorationTabPage.Controls.Add(_explorationTabControl);
            _explorationTabPage.Location = new Point(4, 34);
            _explorationTabPage.Name = "_explorationTabPage";
            _explorationTabPage.Padding = new Padding(3);
            _explorationTabPage.Size = new Size(1170, 641);
            _explorationTabPage.TabIndex = 1;
            _explorationTabPage.Text = "Exploration";
            _explorationTabPage.UseVisualStyleBackColor = true;
            // 
            // _explorationTabControl
            // 
            _explorationTabControl.Controls.Add(_correlationTabPage);
            _explorationTabControl.Controls.Add(_kdeTabPage);
            _explorationTabControl.Controls.Add(_scatterTabPage);
            _explorationTabControl.Dock = DockStyle.Fill;
            _explorationTabControl.ItemSize = new Size(135, 30);
            _explorationTabControl.Location = new Point(3, 3);
            _explorationTabControl.Name = "_explorationTabControl";
            _explorationTabControl.SelectedIndex = 0;
            _explorationTabControl.Size = new Size(1164, 635);
            _explorationTabControl.SizeMode = TabSizeMode.Fixed;
            _explorationTabControl.TabIndex = 0;
            // 
            // _correlationTabPage
            // 
            _correlationTabPage.Controls.Add(_correlationDataGridView);
            _correlationTabPage.Location = new Point(4, 34);
            _correlationTabPage.Name = "_correlationTabPage";
            _correlationTabPage.Padding = new Padding(3);
            _correlationTabPage.Size = new Size(1156, 597);
            _correlationTabPage.TabIndex = 0;
            _correlationTabPage.Text = "Correlation";
            _correlationTabPage.UseVisualStyleBackColor = true;
            // 
            // _correlationDataGridView
            // 
            _correlationDataGridView.AllowUserToAddRows = false;
            _correlationDataGridView.AllowUserToDeleteRows = false;
            _correlationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _correlationDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            _correlationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _correlationDataGridView.Dock = DockStyle.Fill;
            _correlationDataGridView.Location = new Point(3, 3);
            _correlationDataGridView.Name = "_correlationDataGridView";
            _correlationDataGridView.ReadOnly = true;
            _correlationDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            _correlationDataGridView.Size = new Size(1150, 591);
            _correlationDataGridView.TabIndex = 0;
            // 
            // _kdeTabPage
            // 
            _kdeTabPage.Controls.Add(_kdeSplitContainer);
            _kdeTabPage.Location = new Point(4, 34);
            _kdeTabPage.Name = "_kdeTabPage";
            _kdeTabPage.Padding = new Padding(3);
            _kdeTabPage.Size = new Size(1156, 597);
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
            _kdeSplitContainer.Panel1.Controls.Add(_kdeBandsListBox);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeSingleButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeProductButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeMultivariateButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeClearButton);
            // 
            // _kdeSplitContainer.Panel2
            // 
            _kdeSplitContainer.Panel2.Controls.Add(_kdePlotView);
            _kdeSplitContainer.Size = new Size(1150, 591);
            _kdeSplitContainer.SplitterDistance = 300;
            _kdeSplitContainer.TabIndex = 0;
            // 
            // _kdeBandsListBox
            // 
            _kdeBandsListBox.Dock = DockStyle.Fill;
            _kdeBandsListBox.FormattingEnabled = true;
            _kdeBandsListBox.Location = new Point(0, 0);
            _kdeBandsListBox.Name = "_kdeBandsListBox";
            _kdeBandsListBox.SelectionMode = SelectionMode.MultiExtended;
            _kdeBandsListBox.Size = new Size(300, 431);
            _kdeBandsListBox.TabIndex = 0;
            // 
            // _kdeSingleButton
            // 
            _kdeSingleButton.AutoSize = true;
            _kdeSingleButton.Dock = DockStyle.Bottom;
            _kdeSingleButton.Location = new Point(0, 431);
            _kdeSingleButton.Name = "_kdeSingleButton";
            _kdeSingleButton.Size = new Size(300, 40);
            _kdeSingleButton.TabIndex = 1;
            _kdeSingleButton.Text = "Single";
            _kdeSingleButton.UseVisualStyleBackColor = true;
            _kdeSingleButton.Click += KdeSingle;
            // 
            // _kdeProductButton
            // 
            _kdeProductButton.Dock = DockStyle.Bottom;
            _kdeProductButton.Location = new Point(0, 471);
            _kdeProductButton.Name = "_kdeProductButton";
            _kdeProductButton.Size = new Size(300, 40);
            _kdeProductButton.TabIndex = 2;
            _kdeProductButton.Text = "Product";
            _kdeProductButton.UseVisualStyleBackColor = true;
            _kdeProductButton.Click += KdeProduct;
            // 
            // _kdeMultivariateButton
            // 
            _kdeMultivariateButton.Dock = DockStyle.Bottom;
            _kdeMultivariateButton.Location = new Point(0, 511);
            _kdeMultivariateButton.Name = "_kdeMultivariateButton";
            _kdeMultivariateButton.Size = new Size(300, 40);
            _kdeMultivariateButton.TabIndex = 8;
            _kdeMultivariateButton.Text = "Multivar";
            _kdeMultivariateButton.UseVisualStyleBackColor = true;
            _kdeMultivariateButton.Click += KdeMultivariate;
            // 
            // _kdeClearButton
            // 
            _kdeClearButton.Dock = DockStyle.Bottom;
            _kdeClearButton.Location = new Point(0, 551);
            _kdeClearButton.Name = "_kdeClearButton";
            _kdeClearButton.Size = new Size(300, 40);
            _kdeClearButton.TabIndex = 9;
            _kdeClearButton.Text = "Clear";
            _kdeClearButton.UseVisualStyleBackColor = true;
            _kdeClearButton.Click += ClearKdePlot;
            // 
            // _kdePlotView
            // 
            _kdePlotView.Dock = DockStyle.Fill;
            _kdePlotView.Location = new Point(0, 0);
            _kdePlotView.Name = "_kdePlotView";
            _kdePlotView.PanCursor = Cursors.Hand;
            _kdePlotView.Size = new Size(846, 591);
            _kdePlotView.TabIndex = 1;
            _kdePlotView.Text = "plotView1";
            _kdePlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _kdePlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _kdePlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _kdePlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // _scatterTabPage
            // 
            _scatterTabPage.Controls.Add(_scatterSplitContainer1);
            _scatterTabPage.Location = new Point(4, 34);
            _scatterTabPage.Name = "_scatterTabPage";
            _scatterTabPage.Padding = new Padding(3);
            _scatterTabPage.Size = new Size(1156, 597);
            _scatterTabPage.TabIndex = 2;
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
            // 
            // _scatterSplitContainer1.Panel2
            // 
            _scatterSplitContainer1.Panel2.Controls.Add(_scatterPlotView);
            _scatterSplitContainer1.Size = new Size(1150, 591);
            _scatterSplitContainer1.SplitterDistance = 300;
            _scatterSplitContainer1.TabIndex = 0;
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
            _scatterSplitContainer2.Size = new Size(300, 551);
            _scatterSplitContainer2.SplitterDistance = 267;
            _scatterSplitContainer2.TabIndex = 3;
            // 
            // _scatterXAxisGroupBox
            // 
            _scatterXAxisGroupBox.Controls.Add(_scatterXListBox);
            _scatterXAxisGroupBox.Dock = DockStyle.Fill;
            _scatterXAxisGroupBox.Location = new Point(0, 0);
            _scatterXAxisGroupBox.Name = "_scatterXAxisGroupBox";
            _scatterXAxisGroupBox.Size = new Size(300, 267);
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
            _scatterXListBox.Size = new Size(294, 237);
            _scatterXListBox.TabIndex = 0;
            // 
            // _scatterYAxisGroupBox
            // 
            _scatterYAxisGroupBox.Controls.Add(_scatterYListBox);
            _scatterYAxisGroupBox.Dock = DockStyle.Fill;
            _scatterYAxisGroupBox.Location = new Point(0, 0);
            _scatterYAxisGroupBox.Name = "_scatterYAxisGroupBox";
            _scatterYAxisGroupBox.Size = new Size(300, 280);
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
            _scatterYListBox.Size = new Size(294, 250);
            _scatterYListBox.TabIndex = 0;
            // 
            // _buildScatterButton
            // 
            _buildScatterButton.Dock = DockStyle.Bottom;
            _buildScatterButton.Location = new Point(0, 551);
            _buildScatterButton.Name = "_buildScatterButton";
            _buildScatterButton.Size = new Size(300, 40);
            _buildScatterButton.TabIndex = 2;
            _buildScatterButton.Text = "Build";
            _buildScatterButton.UseVisualStyleBackColor = true;
            _buildScatterButton.Click += BuildScatterPlot;
            // 
            // _scatterPlotView
            // 
            _scatterPlotView.Dock = DockStyle.Fill;
            _scatterPlotView.Location = new Point(0, 0);
            _scatterPlotView.Name = "_scatterPlotView";
            _scatterPlotView.PanCursor = Cursors.Hand;
            _scatterPlotView.Size = new Size(846, 591);
            _scatterPlotView.TabIndex = 2;
            _scatterPlotView.Text = "plotView1";
            _scatterPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _scatterPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _scatterPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _scatterPlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // _classificationTabPage
            // 
            _classificationTabPage.Controls.Add(_classificationTabControl);
            _classificationTabPage.Controls.Add(_classificationAbortButton);
            _classificationTabPage.Location = new Point(4, 34);
            _classificationTabPage.Name = "_classificationTabPage";
            _classificationTabPage.Padding = new Padding(3);
            _classificationTabPage.Size = new Size(1170, 641);
            _classificationTabPage.TabIndex = 2;
            _classificationTabPage.Text = "Classification";
            _classificationTabPage.UseVisualStyleBackColor = true;
            // 
            // _classificationTabControl
            // 
            _classificationTabControl.Controls.Add(_primaryClassificationTabPage);
            _classificationTabControl.Controls.Add(_secondaryClassificationTabPage);
            _classificationTabControl.Dock = DockStyle.Fill;
            _classificationTabControl.ItemSize = new Size(125, 30);
            _classificationTabControl.Location = new Point(3, 3);
            _classificationTabControl.Name = "_classificationTabControl";
            _classificationTabControl.SelectedIndex = 0;
            _classificationTabControl.Size = new Size(1164, 600);
            _classificationTabControl.SizeMode = TabSizeMode.Fixed;
            _classificationTabControl.TabIndex = 1;
            // 
            // _primaryClassificationTabPage
            // 
            _primaryClassificationTabPage.Controls.Add(_primaryClassificationSplitContainer);
            _primaryClassificationTabPage.Controls.Add(_primaryClassificationToolStrip);
            _primaryClassificationTabPage.Location = new Point(4, 34);
            _primaryClassificationTabPage.Name = "_primaryClassificationTabPage";
            _primaryClassificationTabPage.Padding = new Padding(3);
            _primaryClassificationTabPage.Size = new Size(1156, 562);
            _primaryClassificationTabPage.TabIndex = 0;
            _primaryClassificationTabPage.Text = "Primary";
            _primaryClassificationTabPage.UseVisualStyleBackColor = true;
            // 
            // _primaryClassificationSplitContainer
            // 
            _primaryClassificationSplitContainer.Dock = DockStyle.Fill;
            _primaryClassificationSplitContainer.FixedPanel = FixedPanel.Panel1;
            _primaryClassificationSplitContainer.Location = new Point(3, 37);
            _primaryClassificationSplitContainer.Name = "_primaryClassificationSplitContainer";
            // 
            // _primaryClassificationSplitContainer.Panel1
            // 
            _primaryClassificationSplitContainer.Panel1.Controls.Add(_primaryClassificationRuleSplitContainer);
            _primaryClassificationSplitContainer.Panel1.Controls.Add(_primaryClassificationRuleToolStrip);
            // 
            // _primaryClassificationSplitContainer.Panel2
            // 
            _primaryClassificationSplitContainer.Panel2.Controls.Add(_primaryClassificationTabControl);
            _primaryClassificationSplitContainer.Size = new Size(1150, 522);
            _primaryClassificationSplitContainer.SplitterDistance = 300;
            _primaryClassificationSplitContainer.TabIndex = 1;
            // 
            // _primaryClassificationRuleSplitContainer
            // 
            _primaryClassificationRuleSplitContainer.Dock = DockStyle.Fill;
            _primaryClassificationRuleSplitContainer.FixedPanel = FixedPanel.Panel2;
            _primaryClassificationRuleSplitContainer.Location = new Point(0, 34);
            _primaryClassificationRuleSplitContainer.Name = "_primaryClassificationRuleSplitContainer";
            _primaryClassificationRuleSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // _primaryClassificationRuleSplitContainer.Panel1
            // 
            _primaryClassificationRuleSplitContainer.Panel1.Controls.Add(_primaryRuleDataGridView);
            // 
            // _primaryClassificationRuleSplitContainer.Panel2
            // 
            _primaryClassificationRuleSplitContainer.Panel2.Controls.Add(_primaryClassificationRichTextBox);
            _primaryClassificationRuleSplitContainer.Size = new Size(300, 488);
            _primaryClassificationRuleSplitContainer.SplitterDistance = 365;
            _primaryClassificationRuleSplitContainer.TabIndex = 10;
            // 
            // _primaryRuleDataGridView
            // 
            _primaryRuleDataGridView.AllowUserToAddRows = false;
            _primaryRuleDataGridView.AllowUserToDeleteRows = false;
            _primaryRuleDataGridView.AllowUserToResizeColumns = false;
            _primaryRuleDataGridView.AllowUserToResizeRows = false;
            _primaryRuleDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _primaryRuleDataGridView.ColumnHeadersVisible = false;
            _primaryRuleDataGridView.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewButtonColumn1 });
            _primaryRuleDataGridView.Dock = DockStyle.Fill;
            _primaryRuleDataGridView.Location = new Point(0, 0);
            _primaryRuleDataGridView.MultiSelect = false;
            _primaryRuleDataGridView.Name = "_primaryRuleDataGridView";
            _primaryRuleDataGridView.ReadOnly = true;
            _primaryRuleDataGridView.RowHeadersWidth = 62;
            _primaryRuleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _primaryRuleDataGridView.Size = new Size(300, 365);
            _primaryRuleDataGridView.TabIndex = 8;
            _primaryRuleDataGridView.CellClick += RuleDataGridView_CellClick;
            _primaryRuleDataGridView.CellMouseClick += RuleDataGridView_CellMouseClick;
            _primaryRuleDataGridView.RowPrePaint += DataGridView_RowPrePaint;
            _primaryRuleDataGridView.SelectionChanged += RuleDataGridView_SelectionChanged;
            // 
            // _primaryClassificationRichTextBox
            // 
            _primaryClassificationRichTextBox.BorderStyle = BorderStyle.FixedSingle;
            _primaryClassificationRichTextBox.Dock = DockStyle.Fill;
            _primaryClassificationRichTextBox.Location = new Point(0, 0);
            _primaryClassificationRichTextBox.Name = "_primaryClassificationRichTextBox";
            _primaryClassificationRichTextBox.ReadOnly = true;
            _primaryClassificationRichTextBox.Size = new Size(300, 119);
            _primaryClassificationRichTextBox.TabIndex = 0;
            _primaryClassificationRichTextBox.Text = "No rule select";
            // 
            // _primaryClassificationRuleToolStrip
            // 
            _primaryClassificationRuleToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _primaryClassificationRuleToolStrip.ImageScalingSize = new Size(24, 24);
            _primaryClassificationRuleToolStrip.Items.AddRange(new ToolStripItem[] { _primaryClassificationAddRuleToolStripButton, _primaryClassificationDeleteRuleToolStripButton, toolStripSeparator7, _primaryClassificationMoveRuleUpToolStripButton, _primaryClassificationMoveRuleDownToolStripButton, _primaryClassificationAutoGenerateToolStripButton });
            _primaryClassificationRuleToolStrip.Location = new Point(0, 0);
            _primaryClassificationRuleToolStrip.Name = "_primaryClassificationRuleToolStrip";
            _primaryClassificationRuleToolStrip.Size = new Size(300, 34);
            _primaryClassificationRuleToolStrip.TabIndex = 9;
            _primaryClassificationRuleToolStrip.Text = "_primaryClassificationRuleToolStrip";
            // 
            // _primaryClassificationAddRuleToolStripButton
            // 
            _primaryClassificationAddRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationAddRuleToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationAddRuleToolStripButton.Name = "_primaryClassificationAddRuleToolStripButton";
            _primaryClassificationAddRuleToolStripButton.Size = new Size(34, 29);
            _primaryClassificationAddRuleToolStripButton.Text = "+";
            _primaryClassificationAddRuleToolStripButton.Click += AddClassificationRule;
            // 
            // _primaryClassificationDeleteRuleToolStripButton
            // 
            _primaryClassificationDeleteRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationDeleteRuleToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationDeleteRuleToolStripButton.Name = "_primaryClassificationDeleteRuleToolStripButton";
            _primaryClassificationDeleteRuleToolStripButton.Size = new Size(34, 29);
            _primaryClassificationDeleteRuleToolStripButton.Text = "-";
            _primaryClassificationDeleteRuleToolStripButton.Click += DeleteClassificationRule;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(6, 34);
            // 
            // _primaryClassificationMoveRuleUpToolStripButton
            // 
            _primaryClassificationMoveRuleUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationMoveRuleUpToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationMoveRuleUpToolStripButton.Name = "_primaryClassificationMoveRuleUpToolStripButton";
            _primaryClassificationMoveRuleUpToolStripButton.Size = new Size(34, 29);
            _primaryClassificationMoveRuleUpToolStripButton.Text = "↑";
            _primaryClassificationMoveRuleUpToolStripButton.Click += MoveRuleUp;
            // 
            // _primaryClassificationMoveRuleDownToolStripButton
            // 
            _primaryClassificationMoveRuleDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationMoveRuleDownToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationMoveRuleDownToolStripButton.Name = "_primaryClassificationMoveRuleDownToolStripButton";
            _primaryClassificationMoveRuleDownToolStripButton.Size = new Size(34, 29);
            _primaryClassificationMoveRuleDownToolStripButton.Text = "↓";
            _primaryClassificationMoveRuleDownToolStripButton.Click += MoveRuleDown;
            // 
            // _primaryClassificationAutoGenerateToolStripButton
            // 
            _primaryClassificationAutoGenerateToolStripButton.Alignment = ToolStripItemAlignment.Right;
            _primaryClassificationAutoGenerateToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationAutoGenerateToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationAutoGenerateToolStripButton.Name = "_primaryClassificationAutoGenerateToolStripButton";
            _primaryClassificationAutoGenerateToolStripButton.Size = new Size(130, 29);
            _primaryClassificationAutoGenerateToolStripButton.Text = "Auto Generate";
            _primaryClassificationAutoGenerateToolStripButton.Click += AutoButton_Click;
            // 
            // _primaryClassificationTabControl
            // 
            _primaryClassificationTabControl.Alignment = TabAlignment.Bottom;
            _primaryClassificationTabControl.Controls.Add(_primaryClassificationViewportTabPage);
            _primaryClassificationTabControl.Controls.Add(_primaryClassificationTableTabPage);
            _primaryClassificationTabControl.Dock = DockStyle.Fill;
            _primaryClassificationTabControl.ItemSize = new Size(150, 30);
            _primaryClassificationTabControl.Location = new Point(0, 0);
            _primaryClassificationTabControl.Name = "_primaryClassificationTabControl";
            _primaryClassificationTabControl.SelectedIndex = 0;
            _primaryClassificationTabControl.Size = new Size(846, 522);
            _primaryClassificationTabControl.SizeMode = TabSizeMode.Fixed;
            _primaryClassificationTabControl.TabIndex = 1;
            // 
            // _primaryClassificationViewportTabPage
            // 
            _primaryClassificationViewportTabPage.Controls.Add(_primaryClassificationViewport);
            _primaryClassificationViewportTabPage.Location = new Point(4, 4);
            _primaryClassificationViewportTabPage.Name = "_primaryClassificationViewportTabPage";
            _primaryClassificationViewportTabPage.Padding = new Padding(3);
            _primaryClassificationViewportTabPage.Size = new Size(838, 484);
            _primaryClassificationViewportTabPage.TabIndex = 0;
            _primaryClassificationViewportTabPage.Text = "Viewport";
            _primaryClassificationViewportTabPage.UseVisualStyleBackColor = true;
            // 
            // _primaryClassificationViewport
            // 
            _primaryClassificationViewport.Dock = DockStyle.Fill;
            _primaryClassificationViewport.Location = new Point(3, 3);
            _primaryClassificationViewport.Name = "_primaryClassificationViewport";
            _primaryClassificationViewport.Size = new Size(832, 478);
            _primaryClassificationViewport.TabIndex = 0;
            // 
            // _primaryClassificationTableTabPage
            // 
            _primaryClassificationTableTabPage.Controls.Add(_primaryClassificationDataGridView);
            _primaryClassificationTableTabPage.Location = new Point(4, 4);
            _primaryClassificationTableTabPage.Name = "_primaryClassificationTableTabPage";
            _primaryClassificationTableTabPage.Padding = new Padding(3);
            _primaryClassificationTableTabPage.Size = new Size(838, 484);
            _primaryClassificationTableTabPage.TabIndex = 1;
            _primaryClassificationTableTabPage.Text = "Table";
            _primaryClassificationTableTabPage.UseVisualStyleBackColor = true;
            // 
            // _primaryClassificationDataGridView
            // 
            _primaryClassificationDataGridView.AllowUserToAddRows = false;
            _primaryClassificationDataGridView.AllowUserToDeleteRows = false;
            _primaryClassificationDataGridView.AllowUserToResizeRows = false;
            _primaryClassificationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _primaryClassificationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _primaryClassificationDataGridView.Columns.AddRange(new DataGridViewColumn[] { _primaryClassificationColorCol, _primaryClassificationNameCol, _primaryClassificationCountCol, _primaryClassificationEditCol });
            _primaryClassificationDataGridView.Dock = DockStyle.Fill;
            _primaryClassificationDataGridView.Location = new Point(3, 3);
            _primaryClassificationDataGridView.Name = "_primaryClassificationDataGridView";
            _primaryClassificationDataGridView.ReadOnly = true;
            _primaryClassificationDataGridView.RowHeadersVisible = false;
            _primaryClassificationDataGridView.RowHeadersWidth = 62;
            _primaryClassificationDataGridView.Size = new Size(832, 478);
            _primaryClassificationDataGridView.TabIndex = 0;
            _primaryClassificationDataGridView.CellClick += ClassificationGrid_CellClick;
            // 
            // _primaryClassificationToolStrip
            // 
            _primaryClassificationToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _primaryClassificationToolStrip.ImageScalingSize = new Size(24, 24);
            _primaryClassificationToolStrip.Items.AddRange(new ToolStripItem[] { _primaryClassificationCompareToolStripButton, _primaryClassificationClassifyToolStripButton, toolStripSeparator2, toolStripLabel1, _primaryClassificationModeToolStripComboBox, toolStripSeparator8, _primaryClassificationExportToolStripButton });
            _primaryClassificationToolStrip.Location = new Point(3, 3);
            _primaryClassificationToolStrip.Name = "_primaryClassificationToolStrip";
            _primaryClassificationToolStrip.Size = new Size(1150, 34);
            _primaryClassificationToolStrip.TabIndex = 0;
            // 
            // _primaryClassificationCompareToolStripButton
            // 
            _primaryClassificationCompareToolStripButton.Alignment = ToolStripItemAlignment.Right;
            _primaryClassificationCompareToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationCompareToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationCompareToolStripButton.Name = "_primaryClassificationCompareToolStripButton";
            _primaryClassificationCompareToolStripButton.Size = new Size(89, 29);
            _primaryClassificationCompareToolStripButton.Text = "Compare";
            _primaryClassificationCompareToolStripButton.Click += CompareToolStripButton_Click;
            // 
            // _primaryClassificationClassifyToolStripButton
            // 
            _primaryClassificationClassifyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationClassifyToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationClassifyToolStripButton.Name = "_primaryClassificationClassifyToolStripButton";
            _primaryClassificationClassifyToolStripButton.Size = new Size(75, 29);
            _primaryClassificationClassifyToolStripButton.Text = "Classify";
            _primaryClassificationClassifyToolStripButton.Click += Classify_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 34);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(175, 29);
            toolStripLabel1.Text = "Classification Mode: ";
            // 
            // _primaryClassificationModeToolStripComboBox
            // 
            _primaryClassificationModeToolStripComboBox.Items.AddRange(new object[] { "Rule Per Class", "Direct Check" });
            _primaryClassificationModeToolStripComboBox.Name = "_primaryClassificationModeToolStripComboBox";
            _primaryClassificationModeToolStripComboBox.Size = new Size(140, 34);
            _primaryClassificationModeToolStripComboBox.Text = "Rule Per Class";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 34);
            // 
            // _primaryClassificationExportToolStripButton
            // 
            _primaryClassificationExportToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _primaryClassificationExportToolStripButton.ImageTransparentColor = Color.Magenta;
            _primaryClassificationExportToolStripButton.Name = "_primaryClassificationExportToolStripButton";
            _primaryClassificationExportToolStripButton.Size = new Size(67, 29);
            _primaryClassificationExportToolStripButton.Text = "Export";
            _primaryClassificationExportToolStripButton.Click += ExportClassification;
            // 
            // _secondaryClassificationTabPage
            // 
            _secondaryClassificationTabPage.Controls.Add(_secondaryClassificationSplitContainer);
            _secondaryClassificationTabPage.Controls.Add(_secondaryClassificationToolStrip);
            _secondaryClassificationTabPage.Location = new Point(4, 34);
            _secondaryClassificationTabPage.Name = "_secondaryClassificationTabPage";
            _secondaryClassificationTabPage.Padding = new Padding(3);
            _secondaryClassificationTabPage.Size = new Size(1156, 562);
            _secondaryClassificationTabPage.TabIndex = 1;
            _secondaryClassificationTabPage.Text = "Secondary";
            _secondaryClassificationTabPage.UseVisualStyleBackColor = true;
            // 
            // _secondaryClassificationSplitContainer
            // 
            _secondaryClassificationSplitContainer.Dock = DockStyle.Fill;
            _secondaryClassificationSplitContainer.FixedPanel = FixedPanel.Panel1;
            _secondaryClassificationSplitContainer.Location = new Point(3, 37);
            _secondaryClassificationSplitContainer.Name = "_secondaryClassificationSplitContainer";
            // 
            // _secondaryClassificationSplitContainer.Panel1
            // 
            _secondaryClassificationSplitContainer.Panel1.Controls.Add(_secondaryClassificationRuleSplitContainer);
            _secondaryClassificationSplitContainer.Panel1.Controls.Add(_secondaryClassificationRuleToolStrip);
            // 
            // _secondaryClassificationSplitContainer.Panel2
            // 
            _secondaryClassificationSplitContainer.Panel2.Controls.Add(_secondaryClassificationTabControl);
            _secondaryClassificationSplitContainer.Size = new Size(1150, 522);
            _secondaryClassificationSplitContainer.SplitterDistance = 300;
            _secondaryClassificationSplitContainer.TabIndex = 0;
            // 
            // _secondaryClassificationRuleSplitContainer
            // 
            _secondaryClassificationRuleSplitContainer.Dock = DockStyle.Fill;
            _secondaryClassificationRuleSplitContainer.FixedPanel = FixedPanel.Panel2;
            _secondaryClassificationRuleSplitContainer.Location = new Point(0, 34);
            _secondaryClassificationRuleSplitContainer.Name = "_secondaryClassificationRuleSplitContainer";
            _secondaryClassificationRuleSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // _secondaryClassificationRuleSplitContainer.Panel1
            // 
            _secondaryClassificationRuleSplitContainer.Panel1.Controls.Add(_secondaryRuleDataGridView);
            // 
            // _secondaryClassificationRuleSplitContainer.Panel2
            // 
            _secondaryClassificationRuleSplitContainer.Panel2.Controls.Add(_secondaryClassificationRichTextBox);
            _secondaryClassificationRuleSplitContainer.Size = new Size(300, 488);
            _secondaryClassificationRuleSplitContainer.SplitterDistance = 365;
            _secondaryClassificationRuleSplitContainer.TabIndex = 10;
            // 
            // _secondaryRuleDataGridView
            // 
            _secondaryRuleDataGridView.AllowUserToAddRows = false;
            _secondaryRuleDataGridView.AllowUserToDeleteRows = false;
            _secondaryRuleDataGridView.AllowUserToResizeColumns = false;
            _secondaryRuleDataGridView.AllowUserToResizeRows = false;
            _secondaryRuleDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _secondaryRuleDataGridView.ColumnHeadersVisible = false;
            _secondaryRuleDataGridView.Columns.AddRange(new DataGridViewColumn[] { NameColumn, EditColumn });
            _secondaryRuleDataGridView.Dock = DockStyle.Fill;
            _secondaryRuleDataGridView.Location = new Point(0, 0);
            _secondaryRuleDataGridView.MultiSelect = false;
            _secondaryRuleDataGridView.Name = "_secondaryRuleDataGridView";
            _secondaryRuleDataGridView.ReadOnly = true;
            _secondaryRuleDataGridView.RowHeadersWidth = 62;
            _secondaryRuleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _secondaryRuleDataGridView.Size = new Size(300, 365);
            _secondaryRuleDataGridView.TabIndex = 8;
            _secondaryRuleDataGridView.CellClick += RuleDataGridView_CellClick;
            _secondaryRuleDataGridView.CellMouseClick += RuleDataGridView_CellMouseClick;
            _secondaryRuleDataGridView.RowPrePaint += DataGridView_RowPrePaint;
            _secondaryRuleDataGridView.SelectionChanged += RuleDataGridView_SelectionChanged;
            // 
            // _secondaryClassificationRichTextBox
            // 
            _secondaryClassificationRichTextBox.BorderStyle = BorderStyle.FixedSingle;
            _secondaryClassificationRichTextBox.Dock = DockStyle.Fill;
            _secondaryClassificationRichTextBox.Location = new Point(0, 0);
            _secondaryClassificationRichTextBox.Name = "_secondaryClassificationRichTextBox";
            _secondaryClassificationRichTextBox.ReadOnly = true;
            _secondaryClassificationRichTextBox.Size = new Size(300, 119);
            _secondaryClassificationRichTextBox.TabIndex = 0;
            _secondaryClassificationRichTextBox.Text = "No rule select";
            // 
            // _secondaryClassificationRuleToolStrip
            // 
            _secondaryClassificationRuleToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _secondaryClassificationRuleToolStrip.ImageScalingSize = new Size(24, 24);
            _secondaryClassificationRuleToolStrip.Items.AddRange(new ToolStripItem[] { _secondaryClassificationAddRuleToolStripButton, _secondaryClassificationDeleteRuleToolStripButton, toolStripSeparator6, _secondaryClassificationMoveRuleUpToolStripButton, _secondaryClassificationMoveRuleDownToolStripButton });
            _secondaryClassificationRuleToolStrip.Location = new Point(0, 0);
            _secondaryClassificationRuleToolStrip.Name = "_secondaryClassificationRuleToolStrip";
            _secondaryClassificationRuleToolStrip.Size = new Size(300, 34);
            _secondaryClassificationRuleToolStrip.TabIndex = 9;
            _secondaryClassificationRuleToolStrip.Text = "_secondaryClassificationRuleToolStrip";
            // 
            // _secondaryClassificationAddRuleToolStripButton
            // 
            _secondaryClassificationAddRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationAddRuleToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationAddRuleToolStripButton.Name = "_secondaryClassificationAddRuleToolStripButton";
            _secondaryClassificationAddRuleToolStripButton.Size = new Size(34, 29);
            _secondaryClassificationAddRuleToolStripButton.Text = "+";
            _secondaryClassificationAddRuleToolStripButton.Click += AddClassificationRule;
            // 
            // _secondaryClassificationDeleteRuleToolStripButton
            // 
            _secondaryClassificationDeleteRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationDeleteRuleToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationDeleteRuleToolStripButton.Name = "_secondaryClassificationDeleteRuleToolStripButton";
            _secondaryClassificationDeleteRuleToolStripButton.Size = new Size(34, 29);
            _secondaryClassificationDeleteRuleToolStripButton.Text = "-";
            _secondaryClassificationDeleteRuleToolStripButton.Click += DeleteClassificationRule;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 34);
            // 
            // _secondaryClassificationMoveRuleUpToolStripButton
            // 
            _secondaryClassificationMoveRuleUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationMoveRuleUpToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationMoveRuleUpToolStripButton.Name = "_secondaryClassificationMoveRuleUpToolStripButton";
            _secondaryClassificationMoveRuleUpToolStripButton.Size = new Size(34, 29);
            _secondaryClassificationMoveRuleUpToolStripButton.Text = "↑";
            _secondaryClassificationMoveRuleUpToolStripButton.Click += MoveRuleUp;
            // 
            // _secondaryClassificationMoveRuleDownToolStripButton
            // 
            _secondaryClassificationMoveRuleDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationMoveRuleDownToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationMoveRuleDownToolStripButton.Name = "_secondaryClassificationMoveRuleDownToolStripButton";
            _secondaryClassificationMoveRuleDownToolStripButton.Size = new Size(34, 29);
            _secondaryClassificationMoveRuleDownToolStripButton.Text = "↓";
            _secondaryClassificationMoveRuleDownToolStripButton.Click += MoveRuleDown;
            // 
            // _secondaryClassificationTabControl
            // 
            _secondaryClassificationTabControl.Alignment = TabAlignment.Bottom;
            _secondaryClassificationTabControl.Controls.Add(_secondaryClassificationViewportTabPage);
            _secondaryClassificationTabControl.Controls.Add(_secondaryClassificationTableTabPage);
            _secondaryClassificationTabControl.Dock = DockStyle.Fill;
            _secondaryClassificationTabControl.ItemSize = new Size(150, 30);
            _secondaryClassificationTabControl.Location = new Point(0, 0);
            _secondaryClassificationTabControl.Name = "_secondaryClassificationTabControl";
            _secondaryClassificationTabControl.SelectedIndex = 0;
            _secondaryClassificationTabControl.Size = new Size(846, 522);
            _secondaryClassificationTabControl.SizeMode = TabSizeMode.Fixed;
            _secondaryClassificationTabControl.TabIndex = 1;
            // 
            // _secondaryClassificationViewportTabPage
            // 
            _secondaryClassificationViewportTabPage.Controls.Add(_secondaryClassificationViewport);
            _secondaryClassificationViewportTabPage.Location = new Point(4, 4);
            _secondaryClassificationViewportTabPage.Name = "_secondaryClassificationViewportTabPage";
            _secondaryClassificationViewportTabPage.Padding = new Padding(3);
            _secondaryClassificationViewportTabPage.Size = new Size(838, 484);
            _secondaryClassificationViewportTabPage.TabIndex = 0;
            _secondaryClassificationViewportTabPage.Text = "Viewport";
            _secondaryClassificationViewportTabPage.UseVisualStyleBackColor = true;
            // 
            // _secondaryClassificationViewport
            // 
            _secondaryClassificationViewport.Dock = DockStyle.Fill;
            _secondaryClassificationViewport.Location = new Point(3, 3);
            _secondaryClassificationViewport.Name = "_secondaryClassificationViewport";
            _secondaryClassificationViewport.Size = new Size(832, 478);
            _secondaryClassificationViewport.TabIndex = 0;
            // 
            // _secondaryClassificationTableTabPage
            // 
            _secondaryClassificationTableTabPage.Controls.Add(_secondaryClassificationDataGridView);
            _secondaryClassificationTableTabPage.Location = new Point(4, 4);
            _secondaryClassificationTableTabPage.Name = "_secondaryClassificationTableTabPage";
            _secondaryClassificationTableTabPage.Padding = new Padding(3);
            _secondaryClassificationTableTabPage.Size = new Size(838, 484);
            _secondaryClassificationTableTabPage.TabIndex = 1;
            _secondaryClassificationTableTabPage.Text = "Table";
            _secondaryClassificationTableTabPage.UseVisualStyleBackColor = true;
            // 
            // _secondaryClassificationDataGridView
            // 
            _secondaryClassificationDataGridView.AllowUserToAddRows = false;
            _secondaryClassificationDataGridView.AllowUserToDeleteRows = false;
            _secondaryClassificationDataGridView.AllowUserToResizeRows = false;
            _secondaryClassificationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _secondaryClassificationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _secondaryClassificationDataGridView.Columns.AddRange(new DataGridViewColumn[] { _secondaryClassificationColorCol, _secondaryClassificationNameCol, _secondaryClassificationCountCol, _secondaryClassificationEditCol });
            _secondaryClassificationDataGridView.Dock = DockStyle.Fill;
            _secondaryClassificationDataGridView.Location = new Point(3, 3);
            _secondaryClassificationDataGridView.Name = "_secondaryClassificationDataGridView";
            _secondaryClassificationDataGridView.ReadOnly = true;
            _secondaryClassificationDataGridView.RowHeadersVisible = false;
            _secondaryClassificationDataGridView.RowHeadersWidth = 62;
            _secondaryClassificationDataGridView.Size = new Size(832, 478);
            _secondaryClassificationDataGridView.TabIndex = 0;
            _secondaryClassificationDataGridView.CellClick += ClassificationGrid_CellClick;
            // 
            // _secondaryClassificationToolStrip
            // 
            _secondaryClassificationToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _secondaryClassificationToolStrip.ImageScalingSize = new Size(24, 24);
            _secondaryClassificationToolStrip.Items.AddRange(new ToolStripItem[] { _secondaryClassificationCompareToolStripButton, _secondaryClassificationClassifyToolStripButton, toolStripSeparator3, _secondaryClassificationExportToolStripButton });
            _secondaryClassificationToolStrip.Location = new Point(3, 3);
            _secondaryClassificationToolStrip.Name = "_secondaryClassificationToolStrip";
            _secondaryClassificationToolStrip.Size = new Size(1150, 34);
            _secondaryClassificationToolStrip.TabIndex = 0;
            // 
            // _secondaryClassificationCompareToolStripButton
            // 
            _secondaryClassificationCompareToolStripButton.Alignment = ToolStripItemAlignment.Right;
            _secondaryClassificationCompareToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationCompareToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationCompareToolStripButton.Name = "_secondaryClassificationCompareToolStripButton";
            _secondaryClassificationCompareToolStripButton.Size = new Size(89, 29);
            _secondaryClassificationCompareToolStripButton.Text = "Compare";
            _secondaryClassificationCompareToolStripButton.Click += CompareToolStripButton_Click;
            // 
            // _secondaryClassificationClassifyToolStripButton
            // 
            _secondaryClassificationClassifyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationClassifyToolStripButton.Image = (Image)resources.GetObject("_secondaryClassificationClassifyToolStripButton.Image");
            _secondaryClassificationClassifyToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationClassifyToolStripButton.Name = "_secondaryClassificationClassifyToolStripButton";
            _secondaryClassificationClassifyToolStripButton.Size = new Size(75, 29);
            _secondaryClassificationClassifyToolStripButton.Text = "Classify";
            _secondaryClassificationClassifyToolStripButton.Click += Classify_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 34);
            // 
            // _secondaryClassificationExportToolStripButton
            // 
            _secondaryClassificationExportToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _secondaryClassificationExportToolStripButton.Image = (Image)resources.GetObject("_secondaryClassificationExportToolStripButton.Image");
            _secondaryClassificationExportToolStripButton.ImageTransparentColor = Color.Magenta;
            _secondaryClassificationExportToolStripButton.Name = "_secondaryClassificationExportToolStripButton";
            _secondaryClassificationExportToolStripButton.Size = new Size(67, 29);
            _secondaryClassificationExportToolStripButton.Text = "Export";
            _secondaryClassificationExportToolStripButton.Click += ExportClassification;
            // 
            // _classificationAbortButton
            // 
            _classificationAbortButton.AutoSize = true;
            _classificationAbortButton.Dock = DockStyle.Bottom;
            _classificationAbortButton.Location = new Point(3, 603);
            _classificationAbortButton.Name = "_classificationAbortButton";
            _classificationAbortButton.Size = new Size(1164, 35);
            _classificationAbortButton.TabIndex = 2;
            _classificationAbortButton.Text = "Abort";
            _classificationAbortButton.UseVisualStyleBackColor = true;
            _classificationAbortButton.Visible = false;
            _classificationAbortButton.Click += AbortClassification_Click;
            // 
            // _ruleContextMenuStrip
            // 
            _ruleContextMenuStrip.ImageScalingSize = new Size(24, 24);
            _ruleContextMenuStrip.Items.AddRange(new ToolStripItem[] { _moveUpToolStripMenuItem, _moveDownToolStripMenuItem, toolStripSeparator4, _editToolStripMenuItem, _cloneToolStripMenuItem, toolStripSeparator5, _removeToolStripMenuItem });
            _ruleContextMenuStrip.Name = "_ruleContextMenuStrip";
            _ruleContextMenuStrip.Size = new Size(180, 176);
            // 
            // _moveUpToolStripMenuItem
            // 
            _moveUpToolStripMenuItem.Name = "_moveUpToolStripMenuItem";
            _moveUpToolStripMenuItem.Size = new Size(179, 32);
            _moveUpToolStripMenuItem.Text = "Move up";
            _moveUpToolStripMenuItem.Click += MoveRuleUp;
            // 
            // _moveDownToolStripMenuItem
            // 
            _moveDownToolStripMenuItem.Name = "_moveDownToolStripMenuItem";
            _moveDownToolStripMenuItem.Size = new Size(179, 32);
            _moveDownToolStripMenuItem.Text = "Move down";
            _moveDownToolStripMenuItem.Click += MoveRuleDown;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(176, 6);
            // 
            // _editToolStripMenuItem
            // 
            _editToolStripMenuItem.Name = "_editToolStripMenuItem";
            _editToolStripMenuItem.Size = new Size(179, 32);
            _editToolStripMenuItem.Text = "Edit";
            _editToolStripMenuItem.Click += EditClassificationRule;
            // 
            // _cloneToolStripMenuItem
            // 
            _cloneToolStripMenuItem.Name = "_cloneToolStripMenuItem";
            _cloneToolStripMenuItem.Size = new Size(179, 32);
            _cloneToolStripMenuItem.Text = "Clone";
            _cloneToolStripMenuItem.Click += CloneClassificationRule;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(176, 6);
            // 
            // _removeToolStripMenuItem
            // 
            _removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
            _removeToolStripMenuItem.Size = new Size(179, 32);
            _removeToolStripMenuItem.Text = "Remove";
            _removeToolStripMenuItem.Click += DeleteClassificationRule;
            // 
            // _classifyWorker
            // 
            _classifyWorker.WorkerReportsProgress = true;
            _classifyWorker.WorkerSupportsCancellation = true;
            _classifyWorker.DoWork += ClassifyWorker_DoWork;
            _classifyWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _classifyWorker.RunWorkerCompleted += ClassifyWorker_RunWorkerCompleted;
            // 
            // _statsWorker
            // 
            _statsWorker.WorkerReportsProgress = true;
            _statsWorker.DoWork += StatsWorker_DoWork;
            _statsWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _statsWorker.RunWorkerCompleted += StatsWorker_RunWorkerCompleted;
            // 
            // _kdeWorker
            // 
            _kdeWorker.WorkerReportsProgress = true;
            _kdeWorker.DoWork += KdeWorker_DoWork;
            _kdeWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _kdeWorker.RunWorkerCompleted += KdeWorker_RunWorkerCompleted;
            // 
            // _scatterWorker
            // 
            _scatterWorker.WorkerReportsProgress = true;
            _scatterWorker.DoWork += ScatterWorker_DoWork;
            _scatterWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _scatterWorker.RunWorkerCompleted += ScatterWorker_RunWorkerCompleted;
            // 
            // _loadWorker
            // 
            _loadWorker.WorkerReportsProgress = true;
            _loadWorker.DoWork += LoadWorker_DoWork;
            _loadWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _loadWorker.RunWorkerCompleted += LoadWorker_RunWorkerCompleted;
            // 
            // _secondaryClassificationColorCol
            // 
            _secondaryClassificationColorCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            _secondaryClassificationColorCol.HeaderText = "Color";
            _secondaryClassificationColorCol.MinimumWidth = 8;
            _secondaryClassificationColorCol.Name = "_secondaryClassificationColorCol";
            _secondaryClassificationColorCol.ReadOnly = true;
            _secondaryClassificationColorCol.Width = 61;
            // 
            // _secondaryClassificationNameCol
            // 
            _secondaryClassificationNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _secondaryClassificationNameCol.HeaderText = "Class";
            _secondaryClassificationNameCol.MinimumWidth = 8;
            _secondaryClassificationNameCol.Name = "_secondaryClassificationNameCol";
            _secondaryClassificationNameCol.ReadOnly = true;
            // 
            // _secondaryClassificationCountCol
            // 
            _secondaryClassificationCountCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _secondaryClassificationCountCol.HeaderText = "Pixels";
            _secondaryClassificationCountCol.MinimumWidth = 8;
            _secondaryClassificationCountCol.Name = "_secondaryClassificationCountCol";
            _secondaryClassificationCountCol.ReadOnly = true;
            _secondaryClassificationCountCol.Width = 91;
            // 
            // _secondaryClassificationEditCol
            // 
            _secondaryClassificationEditCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _secondaryClassificationEditCol.HeaderText = "";
            _secondaryClassificationEditCol.MinimumWidth = 8;
            _secondaryClassificationEditCol.Name = "_secondaryClassificationEditCol";
            _secondaryClassificationEditCol.ReadOnly = true;
            _secondaryClassificationEditCol.Text = "More";
            _secondaryClassificationEditCol.UseColumnTextForButtonValue = true;
            _secondaryClassificationEditCol.Width = 8;
            // 
            // NameColumn
            // 
            NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            NameColumn.HeaderText = "Name";
            NameColumn.MinimumWidth = 8;
            NameColumn.Name = "NameColumn";
            NameColumn.ReadOnly = true;
            NameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // EditColumn
            // 
            EditColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            EditColumn.HeaderText = "Edit";
            EditColumn.MinimumWidth = 8;
            EditColumn.Name = "EditColumn";
            EditColumn.ReadOnly = true;
            EditColumn.Text = "Edit";
            EditColumn.Width = 32;
            // 
            // _primaryClassificationColorCol
            // 
            _primaryClassificationColorCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            _primaryClassificationColorCol.HeaderText = "Color";
            _primaryClassificationColorCol.MinimumWidth = 8;
            _primaryClassificationColorCol.Name = "_primaryClassificationColorCol";
            _primaryClassificationColorCol.ReadOnly = true;
            _primaryClassificationColorCol.Width = 61;
            // 
            // _primaryClassificationNameCol
            // 
            _primaryClassificationNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _primaryClassificationNameCol.HeaderText = "Class";
            _primaryClassificationNameCol.MinimumWidth = 8;
            _primaryClassificationNameCol.Name = "_primaryClassificationNameCol";
            _primaryClassificationNameCol.ReadOnly = true;
            // 
            // _primaryClassificationCountCol
            // 
            _primaryClassificationCountCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _primaryClassificationCountCol.HeaderText = "Pixels";
            _primaryClassificationCountCol.MinimumWidth = 8;
            _primaryClassificationCountCol.Name = "_primaryClassificationCountCol";
            _primaryClassificationCountCol.ReadOnly = true;
            _primaryClassificationCountCol.Width = 91;
            // 
            // _primaryClassificationEditCol
            // 
            _primaryClassificationEditCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _primaryClassificationEditCol.HeaderText = "";
            _primaryClassificationEditCol.MinimumWidth = 8;
            _primaryClassificationEditCol.Name = "_primaryClassificationEditCol";
            _primaryClassificationEditCol.ReadOnly = true;
            _primaryClassificationEditCol.Text = "More";
            _primaryClassificationEditCol.UseColumnTextForButtonValue = true;
            _primaryClassificationEditCol.Width = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn1.HeaderText = "Name";
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewButtonColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewButtonColumn1.HeaderText = "Edit";
            dataGridViewButtonColumn1.MinimumWidth = 8;
            dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            dataGridViewButtonColumn1.ReadOnly = true;
            dataGridViewButtonColumn1.Text = "Edit";
            dataGridViewButtonColumn1.Width = 32;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 744);
            Controls.Add(_mainTabControl);
            Controls.Add(_statusStrip);
            Controls.Add(_menuStrip);
            MainMenuStrip = _menuStrip;
            Name = "MainForm";
            Text = "Main";
            Load += MainForm_Load;
            _menuStrip.ResumeLayout(false);
            _menuStrip.PerformLayout();
            _statusStrip.ResumeLayout(false);
            _statusStrip.PerformLayout();
            _bandsDataGroupBox.ResumeLayout(false);
            _bandsDataPropertiesGroupBox.ResumeLayout(false);
            _mainTabControl.ResumeLayout(false);
            _dataTabPage.ResumeLayout(false);
            _dataSplitContainer.Panel1.ResumeLayout(false);
            _dataSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_dataSplitContainer).EndInit();
            _dataSplitContainer.ResumeLayout(false);
            _bandsDataSplitContainer.Panel1.ResumeLayout(false);
            _bandsDataSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_bandsDataSplitContainer).EndInit();
            _bandsDataSplitContainer.ResumeLayout(false);
            _dataTabControl.ResumeLayout(false);
            _dataViewportTabPage.ResumeLayout(false);
            _dataViewportTabPage.PerformLayout();
            _dataViewportToolStrip.ResumeLayout(false);
            _dataViewportToolStrip.PerformLayout();
            _dataHistogramTabPage.ResumeLayout(false);
            _explorationTabPage.ResumeLayout(false);
            _explorationTabControl.ResumeLayout(false);
            _correlationTabPage.ResumeLayout(false);
            ((ISupportInitialize)_correlationDataGridView).EndInit();
            _kdeTabPage.ResumeLayout(false);
            _kdeSplitContainer.Panel1.ResumeLayout(false);
            _kdeSplitContainer.Panel1.PerformLayout();
            _kdeSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_kdeSplitContainer).EndInit();
            _kdeSplitContainer.ResumeLayout(false);
            _scatterTabPage.ResumeLayout(false);
            _scatterSplitContainer1.Panel1.ResumeLayout(false);
            _scatterSplitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_scatterSplitContainer1).EndInit();
            _scatterSplitContainer1.ResumeLayout(false);
            _scatterSplitContainer2.Panel1.ResumeLayout(false);
            _scatterSplitContainer2.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_scatterSplitContainer2).EndInit();
            _scatterSplitContainer2.ResumeLayout(false);
            _scatterXAxisGroupBox.ResumeLayout(false);
            _scatterYAxisGroupBox.ResumeLayout(false);
            _classificationTabPage.ResumeLayout(false);
            _classificationTabPage.PerformLayout();
            _classificationTabControl.ResumeLayout(false);
            _primaryClassificationTabPage.ResumeLayout(false);
            _primaryClassificationTabPage.PerformLayout();
            _primaryClassificationSplitContainer.Panel1.ResumeLayout(false);
            _primaryClassificationSplitContainer.Panel1.PerformLayout();
            _primaryClassificationSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_primaryClassificationSplitContainer).EndInit();
            _primaryClassificationSplitContainer.ResumeLayout(false);
            _primaryClassificationRuleSplitContainer.Panel1.ResumeLayout(false);
            _primaryClassificationRuleSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_primaryClassificationRuleSplitContainer).EndInit();
            _primaryClassificationRuleSplitContainer.ResumeLayout(false);
            ((ISupportInitialize)_primaryRuleDataGridView).EndInit();
            _primaryClassificationRuleToolStrip.ResumeLayout(false);
            _primaryClassificationRuleToolStrip.PerformLayout();
            _primaryClassificationTabControl.ResumeLayout(false);
            _primaryClassificationViewportTabPage.ResumeLayout(false);
            _primaryClassificationTableTabPage.ResumeLayout(false);
            ((ISupportInitialize)_primaryClassificationDataGridView).EndInit();
            _primaryClassificationToolStrip.ResumeLayout(false);
            _primaryClassificationToolStrip.PerformLayout();
            _secondaryClassificationTabPage.ResumeLayout(false);
            _secondaryClassificationTabPage.PerformLayout();
            _secondaryClassificationSplitContainer.Panel1.ResumeLayout(false);
            _secondaryClassificationSplitContainer.Panel1.PerformLayout();
            _secondaryClassificationSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_secondaryClassificationSplitContainer).EndInit();
            _secondaryClassificationSplitContainer.ResumeLayout(false);
            _secondaryClassificationRuleSplitContainer.Panel1.ResumeLayout(false);
            _secondaryClassificationRuleSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_secondaryClassificationRuleSplitContainer).EndInit();
            _secondaryClassificationRuleSplitContainer.ResumeLayout(false);
            ((ISupportInitialize)_secondaryRuleDataGridView).EndInit();
            _secondaryClassificationRuleToolStrip.ResumeLayout(false);
            _secondaryClassificationRuleToolStrip.PerformLayout();
            _secondaryClassificationTabControl.ResumeLayout(false);
            _secondaryClassificationViewportTabPage.ResumeLayout(false);
            _secondaryClassificationTableTabPage.ResumeLayout(false);
            ((ISupportInitialize)_secondaryClassificationDataGridView).EndInit();
            _secondaryClassificationToolStrip.ResumeLayout(false);
            _secondaryClassificationToolStrip.PerformLayout();
            _ruleContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip _menuStrip;
        private ToolStripMenuItem _fileToolStripMenuItem;
        private ToolStripMenuItem _exitToolStripMenuItem;
        private ToolStripMenuItem _openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private OpenFileDialog _openFileDialog;
        private StatusStrip _statusStrip;
        private ListBox _bandListBox;
        private PropertyGrid _bandPropertyGrid;
        private GroupBox _bandsDataGroupBox;
        private GroupBox _bandsDataPropertiesGroupBox;
        private TabControl _mainTabControl;
        private TabPage _dataTabPage;
        private SplitContainer _dataSplitContainer;
        private SplitContainer _bandsDataSplitContainer;
        private TabPage _explorationTabPage;
        private TabControl _dataTabControl;
        private TabPage _dataViewportTabPage;
        private TabPage _dataHistogramTabPage;
        private ToolStripStatusLabel _mainStatusLabel;
        private ToolStripProgressBar _mainProgressBar;
        private BackgroundWorker _classifyWorker;
        private BackgroundWorker _statsWorker;
        private BackgroundWorker _kdeWorker;
        private BackgroundWorker _scatterWorker;
        private BackgroundWorker _loadWorker;
        private ToolStrip _dataViewportToolStrip;
        private ToolStripDropDownButton _redToolStripDropDownButton;
        private ToolStripDropDownButton _greenToolStripDropDownButton;
        private ToolStripDropDownButton _blueToolStripDropDownButton;
        private OxyPlot.WindowsForms.PlotView _histogramPlotView;
        private TabPage _classificationTabPage;
        private Forms.Viewport _dataViewport;
        private SplitContainer _secondaryClassificationSplitContainer;
        private Forms.Viewport _secondaryClassificationViewport;
        private TabControl _explorationTabControl;
        private TabPage _correlationTabPage;
        private TabPage _kdeTabPage;
        private TabPage _scatterTabPage;
        private DataGridView _correlationDataGridView;
        private SplitContainer _kdeSplitContainer;
        private ListBox _kdeBandsListBox;
        private Button _kdeProductButton;
        private Button _kdeSingleButton;
        private Button _kdeMultivariateButton;
        private Button _kdeClearButton;
        private OxyPlot.WindowsForms.PlotView _kdePlotView;
        private SplitContainer _scatterSplitContainer1;
        private Button _buildScatterButton;
        private OxyPlot.WindowsForms.PlotView _scatterPlotView;
        private SplitContainer _scatterSplitContainer2;
        private GroupBox _scatterXAxisGroupBox;
        private GroupBox _scatterYAxisGroupBox;
        private ListBox _scatterXListBox;
        private ListBox _scatterYListBox;
        private TabControl _secondaryClassificationTabControl;
        private TabPage _secondaryClassificationViewportTabPage;
        private TabPage _secondaryClassificationTableTabPage;
        private ToolStrip _secondaryClassificationToolStrip;
        private DataGridView _secondaryRuleDataGridView;
        private ToolStrip _secondaryClassificationRuleToolStrip;
        private ToolStripButton _secondaryClassificationAddRuleToolStripButton;
        private ToolStripButton _secondaryClassificationDeleteRuleToolStripButton;
        private ToolStripButton _secondaryClassificationMoveRuleUpToolStripButton;
        private ToolStripButton _secondaryClassificationMoveRuleDownToolStripButton;
        private SplitContainer _secondaryClassificationRuleSplitContainer;
        private RichTextBox _secondaryClassificationRichTextBox;
        private ToolStripButton _secondaryClassificationCompareToolStripButton;
        private ContextMenuStrip _ruleContextMenuStrip;
        private ToolStripMenuItem _editToolStripMenuItem;
        private ToolStripMenuItem _removeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem _moveUpToolStripMenuItem;
        private ToolStripMenuItem _moveDownToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem _cloneToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private TabControl _classificationTabControl;
        private TabPage _primaryClassificationTabPage;
        private SplitContainer _primaryClassificationSplitContainer;
        private SplitContainer _primaryClassificationRuleSplitContainer;
        private DataGridView _primaryRuleDataGridView;
        private RichTextBox _primaryClassificationRichTextBox;
        private ToolStrip _primaryClassificationRuleToolStrip;
        private ToolStripButton _primaryClassificationAddRuleToolStripButton;
        private ToolStripButton _primaryClassificationDeleteRuleToolStripButton;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripButton _primaryClassificationMoveRuleUpToolStripButton;
        private ToolStripButton _primaryClassificationMoveRuleDownToolStripButton;
        private TabControl _primaryClassificationTabControl;
        private TabPage _primaryClassificationViewportTabPage;
        private Forms.Viewport _primaryClassificationViewport;
        private ToolStrip _primaryClassificationToolStrip;
        private ToolStripButton _primaryClassificationCompareToolStripButton;
        private TabPage _primaryClassificationTableTabPage;
        private TabPage _secondaryClassificationTabPage;
        private ToolStripButton _primaryClassificationAutoGenerateToolStripButton;
        private SaveFileDialog _saveFileDialog;
        private ToolStripMenuItem _exportGraphToolStripMenuItem;
        private ToolStripMenuItem _exportClassificationToolStripMenuItem;
        private DataGridView _primaryClassificationDataGridView;
        private DataGridView _secondaryClassificationDataGridView;
        private ToolStripButton _primaryClassificationClassifyToolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox _primaryClassificationModeToolStripComboBox;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton _primaryClassificationExportToolStripButton;
        private ToolStripButton _secondaryClassificationClassifyToolStripButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton _secondaryClassificationExportToolStripButton;
        private Button _classificationAbortButton;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewButtonColumn dataGridViewButtonColumn1;
        private DataGridViewImageColumn _primaryClassificationColorCol;
        private DataGridViewTextBoxColumn _primaryClassificationNameCol;
        private DataGridViewTextBoxColumn _primaryClassificationCountCol;
        private DataGridViewButtonColumn _primaryClassificationEditCol;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewButtonColumn EditColumn;
        private DataGridViewImageColumn _secondaryClassificationColorCol;
        private DataGridViewTextBoxColumn _secondaryClassificationNameCol;
        private DataGridViewTextBoxColumn _secondaryClassificationCountCol;
        private DataGridViewButtonColumn _secondaryClassificationEditCol;
    }
}
