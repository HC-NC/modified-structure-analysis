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
            _dataSplitContainer = new SplitContainer();
            _bandsDataSplitContainer = new SplitContainer();
            _bandsDataGroupBox = new GroupBox();
            _bandListBox = new ListBox();
            _bandsDataPropertiesGroupBox = new GroupBox();
            _bandPropertyGrid = new PropertyGrid();
            _dataTabControl = new TabControl();
            _dataViewportTabPage = new TabPage();
            _dataViewport = new Viewport();
            _dataViewportToolStrip = new ToolStrip();
            _redToolStripDropDownButton = new ToolStripDropDownButton();
            _greenToolStripDropDownButton = new ToolStripDropDownButton();
            _blueToolStripDropDownButton = new ToolStripDropDownButton();
            toolStripSeparator9 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            _dataUpToolStripButton = new ToolStripButton();
            _dataDownToolStripButton = new ToolStripButton();
            toolStripSeparator10 = new ToolStripSeparator();
            _dataLeftToolStripButton = new ToolStripButton();
            _dataRightToolStripButton = new ToolStripButton();
            toolStripSeparator11 = new ToolStripSeparator();
            _dataZoomInToolStripButton = new ToolStripButton();
            _dataZoomOutToolStripButton = new ToolStripButton();
            _dataHistogramTabPage = new TabPage();
            _histogramPlotView = new OxyPlot.WindowsForms.PlotView();
            _kdeSplitContainer = new SplitContainer();
            _kdeBandsListBox = new ListBox();
            _kdeSingleButton = new Button();
            _kdeProductButton = new Button();
            _kdeMultivariateButton = new Button();
            _kdeClearButton = new Button();
            _kdePlotView = new OxyPlot.WindowsForms.PlotView();
            _scatterSplitContainer1 = new SplitContainer();
            _scatterSplitContainer2 = new SplitContainer();
            _scatterXAxisGroupBox = new GroupBox();
            _scatterXListBox = new ListBox();
            _scatterYAxisGroupBox = new GroupBox();
            _scatterYListBox = new ListBox();
            _buildScatterButton = new Button();
            _scatterPlotView = new OxyPlot.WindowsForms.PlotView();
            _primaryClassificationSplitContainer = new SplitContainer();
            _primaryClassificationRuleSplitContainer = new SplitContainer();
            _primaryRuleDataGridView = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewButtonColumn1 = new DataGridViewButtonColumn();
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
            toolStrip1 = new ToolStrip();
            toolStripLabel3 = new ToolStripLabel();
            _primaryUpToolStripButton = new ToolStripButton();
            _primaryDownToolStripButton = new ToolStripButton();
            toolStripSeparator13 = new ToolStripSeparator();
            _primaryLeftToolStripButton = new ToolStripButton();
            _primaryRightToolStripButton = new ToolStripButton();
            toolStripSeparator14 = new ToolStripSeparator();
            _primaryZoomInToolStripButton = new ToolStripButton();
            _primaryZoomOutToolStripButton = new ToolStripButton();
            _primaryClassificationTableTabPage = new TabPage();
            _primaryClassificationDataGridView = new DataGridView();
            _primaryClassificationColorCol = new DataGridViewImageColumn();
            _primaryClassificationNameCol = new DataGridViewTextBoxColumn();
            _primaryClassificationCountCol = new DataGridViewTextBoxColumn();
            _primaryClassificationEditCol = new DataGridViewButtonColumn();
            _secondaryClassificationSplitContainer = new SplitContainer();
            _secondaryClassificationRuleSplitContainer = new SplitContainer();
            _secondaryRuleDataGridView = new DataGridView();
            NameColumn = new DataGridViewTextBoxColumn();
            EditColumn = new DataGridViewButtonColumn();
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
            toolStrip2 = new ToolStrip();
            toolStripLabel4 = new ToolStripLabel();
            _secondaryUpToolStripButton = new ToolStripButton();
            _secondaryDownToolStripButton = new ToolStripButton();
            toolStripSeparator12 = new ToolStripSeparator();
            _secondaryLeftToolStripButton = new ToolStripButton();
            _secondaryRightToolStripButton = new ToolStripButton();
            toolStripSeparator15 = new ToolStripSeparator();
            _secondaryZoomInToolStripButton = new ToolStripButton();
            _secondaryZoomOutToolStripButton = new ToolStripButton();
            _secondaryClassificationTableTabPage = new TabPage();
            _secondaryClassificationDataGridView = new DataGridView();
            _secondaryClassificationColorCol = new DataGridViewImageColumn();
            _secondaryClassificationNameCol = new DataGridViewTextBoxColumn();
            _secondaryClassificationCountCol = new DataGridViewTextBoxColumn();
            _secondaryClassificationEditCol = new DataGridViewButtonColumn();
            _menuStrip = new MenuStrip();
            _fileToolStripMenuItem = new ToolStripMenuItem();
            _openToolStripMenuItem = new ToolStripMenuItem();
            _exportGraphToolStripMenuItem = new ToolStripMenuItem();
            _exportClassificationToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            _exitToolStripMenuItem = new ToolStripMenuItem();
            _toolsToolStripMenuItem = new ToolStripMenuItem();
            _settingsToolStripMenuItem = new ToolStripMenuItem();
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();
            _statusStrip = new StatusStrip();
            _mainStatusLabel = new ToolStripStatusLabel();
            _mainProgressBar = new ToolStripProgressBar();
            _mainTabControl = new TabControl();
            _dataTabPage = new TabPage();
            _explorationTabPage = new TabPage();
            _explorationTabControl = new TabControl();
            _correlationTabPage = new TabPage();
            _correlationDataGridView = new DataGridView();
            _kdeTabPage = new TabPage();
            _scatterTabPage = new TabPage();
            _classificationTabPage = new TabPage();
            _classificationTabControl = new TabControl();
            _primaryClassificationTabPage = new TabPage();
            _primaryClassificationToolStrip = new ToolStrip();
            _primaryClassificationCompareToolStripButton = new ToolStripButton();
            _primaryClassificationClassifyToolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            _primaryClassificationModeToolStripComboBox = new ToolStripComboBox();
            toolStripSeparator8 = new ToolStripSeparator();
            _primaryClassificationExportToolStripButton = new ToolStripButton();
            _secondaryClassificationTabPage = new TabPage();
            _secondaryClassificationToolStrip = new ToolStrip();
            _secondaryClassificationCompareToolStripButton = new ToolStripButton();
            _secondaryClassificationClassifyToolStripButton = new ToolStripButton();
            toolStripSeparator16 = new ToolStripSeparator();
            filterToolStripButton = new ToolStripButton();
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
            ((ISupportInitialize)_dataSplitContainer).BeginInit();
            _dataSplitContainer.Panel1.SuspendLayout();
            _dataSplitContainer.Panel2.SuspendLayout();
            _dataSplitContainer.SuspendLayout();
            ((ISupportInitialize)_bandsDataSplitContainer).BeginInit();
            _bandsDataSplitContainer.Panel1.SuspendLayout();
            _bandsDataSplitContainer.Panel2.SuspendLayout();
            _bandsDataSplitContainer.SuspendLayout();
            _bandsDataGroupBox.SuspendLayout();
            _bandsDataPropertiesGroupBox.SuspendLayout();
            _dataTabControl.SuspendLayout();
            _dataViewportTabPage.SuspendLayout();
            _dataViewportToolStrip.SuspendLayout();
            _dataHistogramTabPage.SuspendLayout();
            ((ISupportInitialize)_kdeSplitContainer).BeginInit();
            _kdeSplitContainer.Panel1.SuspendLayout();
            _kdeSplitContainer.Panel2.SuspendLayout();
            _kdeSplitContainer.SuspendLayout();
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
            toolStrip1.SuspendLayout();
            _primaryClassificationTableTabPage.SuspendLayout();
            ((ISupportInitialize)_primaryClassificationDataGridView).BeginInit();
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
            toolStrip2.SuspendLayout();
            _secondaryClassificationTableTabPage.SuspendLayout();
            ((ISupportInitialize)_secondaryClassificationDataGridView).BeginInit();
            _menuStrip.SuspendLayout();
            _statusStrip.SuspendLayout();
            _mainTabControl.SuspendLayout();
            _dataTabPage.SuspendLayout();
            _explorationTabPage.SuspendLayout();
            _explorationTabControl.SuspendLayout();
            _correlationTabPage.SuspendLayout();
            ((ISupportInitialize)_correlationDataGridView).BeginInit();
            _kdeTabPage.SuspendLayout();
            _scatterTabPage.SuspendLayout();
            _classificationTabPage.SuspendLayout();
            _classificationTabControl.SuspendLayout();
            _primaryClassificationTabPage.SuspendLayout();
            _primaryClassificationToolStrip.SuspendLayout();
            _secondaryClassificationTabPage.SuspendLayout();
            _secondaryClassificationToolStrip.SuspendLayout();
            _ruleContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _dataSplitContainer
            // 
            resources.ApplyResources(_dataSplitContainer, "_dataSplitContainer");
            _dataSplitContainer.FixedPanel = FixedPanel.Panel1;
            _dataSplitContainer.Name = "_dataSplitContainer";
            // 
            // _dataSplitContainer.Panel1
            // 
            _dataSplitContainer.Panel1.Controls.Add(_bandsDataSplitContainer);
            // 
            // _dataSplitContainer.Panel2
            // 
            _dataSplitContainer.Panel2.Controls.Add(_dataTabControl);
            // 
            // _bandsDataSplitContainer
            // 
            resources.ApplyResources(_bandsDataSplitContainer, "_bandsDataSplitContainer");
            _bandsDataSplitContainer.Name = "_bandsDataSplitContainer";
            // 
            // _bandsDataSplitContainer.Panel1
            // 
            _bandsDataSplitContainer.Panel1.Controls.Add(_bandsDataGroupBox);
            // 
            // _bandsDataSplitContainer.Panel2
            // 
            _bandsDataSplitContainer.Panel2.Controls.Add(_bandsDataPropertiesGroupBox);
            // 
            // _bandsDataGroupBox
            // 
            _bandsDataGroupBox.Controls.Add(_bandListBox);
            resources.ApplyResources(_bandsDataGroupBox, "_bandsDataGroupBox");
            _bandsDataGroupBox.Name = "_bandsDataGroupBox";
            _bandsDataGroupBox.TabStop = false;
            // 
            // _bandListBox
            // 
            resources.ApplyResources(_bandListBox, "_bandListBox");
            _bandListBox.FormattingEnabled = true;
            _bandListBox.Name = "_bandListBox";
            _bandListBox.SelectedIndexChanged += bandListBox_SelectedIndexChanged;
            // 
            // _bandsDataPropertiesGroupBox
            // 
            _bandsDataPropertiesGroupBox.Controls.Add(_bandPropertyGrid);
            resources.ApplyResources(_bandsDataPropertiesGroupBox, "_bandsDataPropertiesGroupBox");
            _bandsDataPropertiesGroupBox.Name = "_bandsDataPropertiesGroupBox";
            _bandsDataPropertiesGroupBox.TabStop = false;
            // 
            // _bandPropertyGrid
            // 
            _bandPropertyGrid.BackColor = SystemColors.Control;
            resources.ApplyResources(_bandPropertyGrid, "_bandPropertyGrid");
            _bandPropertyGrid.Name = "_bandPropertyGrid";
            // 
            // _dataTabControl
            // 
            resources.ApplyResources(_dataTabControl, "_dataTabControl");
            _dataTabControl.Controls.Add(_dataViewportTabPage);
            _dataTabControl.Controls.Add(_dataHistogramTabPage);
            _dataTabControl.Multiline = true;
            _dataTabControl.Name = "_dataTabControl";
            _dataTabControl.SelectedIndex = 0;
            _dataTabControl.SizeMode = TabSizeMode.Fixed;
            _dataTabControl.Selected += TabControl_Selected;
            // 
            // _dataViewportTabPage
            // 
            _dataViewportTabPage.Controls.Add(_dataViewport);
            _dataViewportTabPage.Controls.Add(_dataViewportToolStrip);
            resources.ApplyResources(_dataViewportTabPage, "_dataViewportTabPage");
            _dataViewportTabPage.Name = "_dataViewportTabPage";
            _dataViewportTabPage.UseVisualStyleBackColor = true;
            // 
            // _dataViewport
            // 
            resources.ApplyResources(_dataViewport, "_dataViewport");
            _dataViewport.Name = "_dataViewport";
            // 
            // _dataViewportToolStrip
            // 
            _dataViewportToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _dataViewportToolStrip.ImageScalingSize = new Size(24, 24);
            _dataViewportToolStrip.Items.AddRange(new ToolStripItem[] { _redToolStripDropDownButton, _greenToolStripDropDownButton, _blueToolStripDropDownButton, toolStripSeparator9, toolStripLabel2, _dataUpToolStripButton, _dataDownToolStripButton, toolStripSeparator10, _dataLeftToolStripButton, _dataRightToolStripButton, toolStripSeparator11, _dataZoomInToolStripButton, _dataZoomOutToolStripButton });
            resources.ApplyResources(_dataViewportToolStrip, "_dataViewportToolStrip");
            _dataViewportToolStrip.Name = "_dataViewportToolStrip";
            // 
            // _redToolStripDropDownButton
            // 
            _redToolStripDropDownButton.Image = Properties.Resources.red_sqaure;
            resources.ApplyResources(_redToolStripDropDownButton, "_redToolStripDropDownButton");
            _redToolStripDropDownButton.Name = "_redToolStripDropDownButton";
            _redToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
            // 
            // _greenToolStripDropDownButton
            // 
            _greenToolStripDropDownButton.Image = Properties.Resources.green_sqaure;
            resources.ApplyResources(_greenToolStripDropDownButton, "_greenToolStripDropDownButton");
            _greenToolStripDropDownButton.Name = "_greenToolStripDropDownButton";
            _greenToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
            // 
            // _blueToolStripDropDownButton
            // 
            _blueToolStripDropDownButton.Image = Properties.Resources.blue_sqaure;
            resources.ApplyResources(_blueToolStripDropDownButton, "_blueToolStripDropDownButton");
            _blueToolStripDropDownButton.Name = "_blueToolStripDropDownButton";
            _blueToolStripDropDownButton.Overflow = ToolStripItemOverflow.Never;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(toolStripSeparator9, "toolStripSeparator9");
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            resources.ApplyResources(toolStripLabel2, "toolStripLabel2");
            // 
            // _dataUpToolStripButton
            // 
            _dataUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_dataUpToolStripButton, "_dataUpToolStripButton");
            _dataUpToolStripButton.Name = "_dataUpToolStripButton";
            // 
            // _dataDownToolStripButton
            // 
            _dataDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_dataDownToolStripButton, "_dataDownToolStripButton");
            _dataDownToolStripButton.Name = "_dataDownToolStripButton";
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            resources.ApplyResources(toolStripSeparator10, "toolStripSeparator10");
            // 
            // _dataLeftToolStripButton
            // 
            _dataLeftToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_dataLeftToolStripButton, "_dataLeftToolStripButton");
            _dataLeftToolStripButton.Name = "_dataLeftToolStripButton";
            // 
            // _dataRightToolStripButton
            // 
            _dataRightToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_dataRightToolStripButton, "_dataRightToolStripButton");
            _dataRightToolStripButton.Name = "_dataRightToolStripButton";
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            resources.ApplyResources(toolStripSeparator11, "toolStripSeparator11");
            // 
            // _dataZoomInToolStripButton
            // 
            _dataZoomInToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_dataZoomInToolStripButton, "_dataZoomInToolStripButton");
            _dataZoomInToolStripButton.Name = "_dataZoomInToolStripButton";
            // 
            // _dataZoomOutToolStripButton
            // 
            _dataZoomOutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_dataZoomOutToolStripButton, "_dataZoomOutToolStripButton");
            _dataZoomOutToolStripButton.Name = "_dataZoomOutToolStripButton";
            // 
            // _dataHistogramTabPage
            // 
            _dataHistogramTabPage.Controls.Add(_histogramPlotView);
            resources.ApplyResources(_dataHistogramTabPage, "_dataHistogramTabPage");
            _dataHistogramTabPage.Name = "_dataHistogramTabPage";
            _dataHistogramTabPage.UseVisualStyleBackColor = true;
            // 
            // _histogramPlotView
            // 
            resources.ApplyResources(_histogramPlotView, "_histogramPlotView");
            _histogramPlotView.Name = "_histogramPlotView";
            _histogramPlotView.PanCursor = Cursors.Hand;
            _histogramPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _histogramPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _histogramPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _histogramPlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // _kdeSplitContainer
            // 
            resources.ApplyResources(_kdeSplitContainer, "_kdeSplitContainer");
            _kdeSplitContainer.FixedPanel = FixedPanel.Panel1;
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
            // 
            // _kdeBandsListBox
            // 
            resources.ApplyResources(_kdeBandsListBox, "_kdeBandsListBox");
            _kdeBandsListBox.FormattingEnabled = true;
            _kdeBandsListBox.Name = "_kdeBandsListBox";
            _kdeBandsListBox.SelectionMode = SelectionMode.MultiExtended;
            // 
            // _kdeSingleButton
            // 
            resources.ApplyResources(_kdeSingleButton, "_kdeSingleButton");
            _kdeSingleButton.Name = "_kdeSingleButton";
            _kdeSingleButton.UseVisualStyleBackColor = true;
            _kdeSingleButton.Click += KdeSingle;
            // 
            // _kdeProductButton
            // 
            resources.ApplyResources(_kdeProductButton, "_kdeProductButton");
            _kdeProductButton.Name = "_kdeProductButton";
            _kdeProductButton.UseVisualStyleBackColor = true;
            _kdeProductButton.Click += KdeProduct;
            // 
            // _kdeMultivariateButton
            // 
            resources.ApplyResources(_kdeMultivariateButton, "_kdeMultivariateButton");
            _kdeMultivariateButton.Name = "_kdeMultivariateButton";
            _kdeMultivariateButton.UseVisualStyleBackColor = true;
            _kdeMultivariateButton.Click += KdeMultivariate;
            // 
            // _kdeClearButton
            // 
            resources.ApplyResources(_kdeClearButton, "_kdeClearButton");
            _kdeClearButton.Name = "_kdeClearButton";
            _kdeClearButton.UseVisualStyleBackColor = true;
            _kdeClearButton.Click += ClearKdePlot;
            // 
            // _kdePlotView
            // 
            resources.ApplyResources(_kdePlotView, "_kdePlotView");
            _kdePlotView.Name = "_kdePlotView";
            _kdePlotView.PanCursor = Cursors.Hand;
            _kdePlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _kdePlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _kdePlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _kdePlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // _scatterSplitContainer1
            // 
            resources.ApplyResources(_scatterSplitContainer1, "_scatterSplitContainer1");
            _scatterSplitContainer1.FixedPanel = FixedPanel.Panel1;
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
            // 
            // _scatterSplitContainer2
            // 
            resources.ApplyResources(_scatterSplitContainer2, "_scatterSplitContainer2");
            _scatterSplitContainer2.Name = "_scatterSplitContainer2";
            // 
            // _scatterSplitContainer2.Panel1
            // 
            _scatterSplitContainer2.Panel1.Controls.Add(_scatterXAxisGroupBox);
            // 
            // _scatterSplitContainer2.Panel2
            // 
            _scatterSplitContainer2.Panel2.Controls.Add(_scatterYAxisGroupBox);
            // 
            // _scatterXAxisGroupBox
            // 
            _scatterXAxisGroupBox.Controls.Add(_scatterXListBox);
            resources.ApplyResources(_scatterXAxisGroupBox, "_scatterXAxisGroupBox");
            _scatterXAxisGroupBox.Name = "_scatterXAxisGroupBox";
            _scatterXAxisGroupBox.TabStop = false;
            // 
            // _scatterXListBox
            // 
            resources.ApplyResources(_scatterXListBox, "_scatterXListBox");
            _scatterXListBox.FormattingEnabled = true;
            _scatterXListBox.Name = "_scatterXListBox";
            // 
            // _scatterYAxisGroupBox
            // 
            _scatterYAxisGroupBox.Controls.Add(_scatterYListBox);
            resources.ApplyResources(_scatterYAxisGroupBox, "_scatterYAxisGroupBox");
            _scatterYAxisGroupBox.Name = "_scatterYAxisGroupBox";
            _scatterYAxisGroupBox.TabStop = false;
            // 
            // _scatterYListBox
            // 
            resources.ApplyResources(_scatterYListBox, "_scatterYListBox");
            _scatterYListBox.FormattingEnabled = true;
            _scatterYListBox.Name = "_scatterYListBox";
            // 
            // _buildScatterButton
            // 
            resources.ApplyResources(_buildScatterButton, "_buildScatterButton");
            _buildScatterButton.Name = "_buildScatterButton";
            _buildScatterButton.UseVisualStyleBackColor = true;
            _buildScatterButton.Click += BuildScatterPlot;
            // 
            // _scatterPlotView
            // 
            resources.ApplyResources(_scatterPlotView, "_scatterPlotView");
            _scatterPlotView.Name = "_scatterPlotView";
            _scatterPlotView.PanCursor = Cursors.Hand;
            _scatterPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _scatterPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _scatterPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _scatterPlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // _primaryClassificationSplitContainer
            // 
            resources.ApplyResources(_primaryClassificationSplitContainer, "_primaryClassificationSplitContainer");
            _primaryClassificationSplitContainer.FixedPanel = FixedPanel.Panel1;
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
            // 
            // _primaryClassificationRuleSplitContainer
            // 
            resources.ApplyResources(_primaryClassificationRuleSplitContainer, "_primaryClassificationRuleSplitContainer");
            _primaryClassificationRuleSplitContainer.FixedPanel = FixedPanel.Panel2;
            _primaryClassificationRuleSplitContainer.Name = "_primaryClassificationRuleSplitContainer";
            // 
            // _primaryClassificationRuleSplitContainer.Panel1
            // 
            _primaryClassificationRuleSplitContainer.Panel1.Controls.Add(_primaryRuleDataGridView);
            // 
            // _primaryClassificationRuleSplitContainer.Panel2
            // 
            _primaryClassificationRuleSplitContainer.Panel2.Controls.Add(_primaryClassificationRichTextBox);
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
            resources.ApplyResources(_primaryRuleDataGridView, "_primaryRuleDataGridView");
            _primaryRuleDataGridView.MultiSelect = false;
            _primaryRuleDataGridView.Name = "_primaryRuleDataGridView";
            _primaryRuleDataGridView.ReadOnly = true;
            _primaryRuleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _primaryRuleDataGridView.CellClick += RuleDataGridView_CellClick;
            _primaryRuleDataGridView.CellMouseClick += RuleDataGridView_CellMouseClick;
            _primaryRuleDataGridView.RowPrePaint += DataGridView_RowPrePaint;
            _primaryRuleDataGridView.SelectionChanged += RuleDataGridView_SelectionChanged;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewButtonColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(dataGridViewButtonColumn1, "dataGridViewButtonColumn1");
            dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            dataGridViewButtonColumn1.ReadOnly = true;
            dataGridViewButtonColumn1.Text = "⚙";
            dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // _primaryClassificationRichTextBox
            // 
            _primaryClassificationRichTextBox.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(_primaryClassificationRichTextBox, "_primaryClassificationRichTextBox");
            _primaryClassificationRichTextBox.Name = "_primaryClassificationRichTextBox";
            _primaryClassificationRichTextBox.ReadOnly = true;
            // 
            // _primaryClassificationRuleToolStrip
            // 
            _primaryClassificationRuleToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _primaryClassificationRuleToolStrip.ImageScalingSize = new Size(24, 24);
            _primaryClassificationRuleToolStrip.Items.AddRange(new ToolStripItem[] { _primaryClassificationAddRuleToolStripButton, _primaryClassificationDeleteRuleToolStripButton, toolStripSeparator7, _primaryClassificationMoveRuleUpToolStripButton, _primaryClassificationMoveRuleDownToolStripButton, _primaryClassificationAutoGenerateToolStripButton });
            resources.ApplyResources(_primaryClassificationRuleToolStrip, "_primaryClassificationRuleToolStrip");
            _primaryClassificationRuleToolStrip.Name = "_primaryClassificationRuleToolStrip";
            // 
            // _primaryClassificationAddRuleToolStripButton
            // 
            _primaryClassificationAddRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationAddRuleToolStripButton, "_primaryClassificationAddRuleToolStripButton");
            _primaryClassificationAddRuleToolStripButton.Name = "_primaryClassificationAddRuleToolStripButton";
            _primaryClassificationAddRuleToolStripButton.Click += AddClassificationRule;
            // 
            // _primaryClassificationDeleteRuleToolStripButton
            // 
            _primaryClassificationDeleteRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationDeleteRuleToolStripButton, "_primaryClassificationDeleteRuleToolStripButton");
            _primaryClassificationDeleteRuleToolStripButton.Name = "_primaryClassificationDeleteRuleToolStripButton";
            _primaryClassificationDeleteRuleToolStripButton.Click += DeleteClassificationRule;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
            // 
            // _primaryClassificationMoveRuleUpToolStripButton
            // 
            _primaryClassificationMoveRuleUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationMoveRuleUpToolStripButton, "_primaryClassificationMoveRuleUpToolStripButton");
            _primaryClassificationMoveRuleUpToolStripButton.Name = "_primaryClassificationMoveRuleUpToolStripButton";
            _primaryClassificationMoveRuleUpToolStripButton.Click += MoveRuleUp;
            // 
            // _primaryClassificationMoveRuleDownToolStripButton
            // 
            _primaryClassificationMoveRuleDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationMoveRuleDownToolStripButton, "_primaryClassificationMoveRuleDownToolStripButton");
            _primaryClassificationMoveRuleDownToolStripButton.Name = "_primaryClassificationMoveRuleDownToolStripButton";
            _primaryClassificationMoveRuleDownToolStripButton.Click += MoveRuleDown;
            // 
            // _primaryClassificationAutoGenerateToolStripButton
            // 
            _primaryClassificationAutoGenerateToolStripButton.Alignment = ToolStripItemAlignment.Right;
            _primaryClassificationAutoGenerateToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationAutoGenerateToolStripButton, "_primaryClassificationAutoGenerateToolStripButton");
            _primaryClassificationAutoGenerateToolStripButton.Name = "_primaryClassificationAutoGenerateToolStripButton";
            _primaryClassificationAutoGenerateToolStripButton.Click += AutoButton_Click;
            // 
            // _primaryClassificationTabControl
            // 
            resources.ApplyResources(_primaryClassificationTabControl, "_primaryClassificationTabControl");
            _primaryClassificationTabControl.Controls.Add(_primaryClassificationViewportTabPage);
            _primaryClassificationTabControl.Controls.Add(_primaryClassificationTableTabPage);
            _primaryClassificationTabControl.Name = "_primaryClassificationTabControl";
            _primaryClassificationTabControl.SelectedIndex = 0;
            _primaryClassificationTabControl.SizeMode = TabSizeMode.Fixed;
            // 
            // _primaryClassificationViewportTabPage
            // 
            _primaryClassificationViewportTabPage.Controls.Add(_primaryClassificationViewport);
            _primaryClassificationViewportTabPage.Controls.Add(toolStrip1);
            resources.ApplyResources(_primaryClassificationViewportTabPage, "_primaryClassificationViewportTabPage");
            _primaryClassificationViewportTabPage.Name = "_primaryClassificationViewportTabPage";
            _primaryClassificationViewportTabPage.UseVisualStyleBackColor = true;
            // 
            // _primaryClassificationViewport
            // 
            resources.ApplyResources(_primaryClassificationViewport, "_primaryClassificationViewport");
            _primaryClassificationViewport.Name = "_primaryClassificationViewport";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel3, _primaryUpToolStripButton, _primaryDownToolStripButton, toolStripSeparator13, _primaryLeftToolStripButton, _primaryRightToolStripButton, toolStripSeparator14, _primaryZoomInToolStripButton, _primaryZoomOutToolStripButton });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            resources.ApplyResources(toolStripLabel3, "toolStripLabel3");
            // 
            // _primaryUpToolStripButton
            // 
            _primaryUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryUpToolStripButton, "_primaryUpToolStripButton");
            _primaryUpToolStripButton.Name = "_primaryUpToolStripButton";
            // 
            // _primaryDownToolStripButton
            // 
            _primaryDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryDownToolStripButton, "_primaryDownToolStripButton");
            _primaryDownToolStripButton.Name = "_primaryDownToolStripButton";
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            resources.ApplyResources(toolStripSeparator13, "toolStripSeparator13");
            // 
            // _primaryLeftToolStripButton
            // 
            _primaryLeftToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryLeftToolStripButton, "_primaryLeftToolStripButton");
            _primaryLeftToolStripButton.Name = "_primaryLeftToolStripButton";
            // 
            // _primaryRightToolStripButton
            // 
            _primaryRightToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryRightToolStripButton, "_primaryRightToolStripButton");
            _primaryRightToolStripButton.Name = "_primaryRightToolStripButton";
            // 
            // toolStripSeparator14
            // 
            toolStripSeparator14.Name = "toolStripSeparator14";
            resources.ApplyResources(toolStripSeparator14, "toolStripSeparator14");
            // 
            // _primaryZoomInToolStripButton
            // 
            _primaryZoomInToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryZoomInToolStripButton, "_primaryZoomInToolStripButton");
            _primaryZoomInToolStripButton.Name = "_primaryZoomInToolStripButton";
            // 
            // _primaryZoomOutToolStripButton
            // 
            _primaryZoomOutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryZoomOutToolStripButton, "_primaryZoomOutToolStripButton");
            _primaryZoomOutToolStripButton.Name = "_primaryZoomOutToolStripButton";
            // 
            // _primaryClassificationTableTabPage
            // 
            _primaryClassificationTableTabPage.Controls.Add(_primaryClassificationDataGridView);
            resources.ApplyResources(_primaryClassificationTableTabPage, "_primaryClassificationTableTabPage");
            _primaryClassificationTableTabPage.Name = "_primaryClassificationTableTabPage";
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
            resources.ApplyResources(_primaryClassificationDataGridView, "_primaryClassificationDataGridView");
            _primaryClassificationDataGridView.Name = "_primaryClassificationDataGridView";
            _primaryClassificationDataGridView.ReadOnly = true;
            _primaryClassificationDataGridView.RowHeadersVisible = false;
            _primaryClassificationDataGridView.CellClick += ClassificationGrid_CellClick;
            // 
            // _primaryClassificationColorCol
            // 
            _primaryClassificationColorCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(_primaryClassificationColorCol, "_primaryClassificationColorCol");
            _primaryClassificationColorCol.Name = "_primaryClassificationColorCol";
            _primaryClassificationColorCol.ReadOnly = true;
            // 
            // _primaryClassificationNameCol
            // 
            _primaryClassificationNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_primaryClassificationNameCol, "_primaryClassificationNameCol");
            _primaryClassificationNameCol.Name = "_primaryClassificationNameCol";
            _primaryClassificationNameCol.ReadOnly = true;
            // 
            // _primaryClassificationCountCol
            // 
            _primaryClassificationCountCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(_primaryClassificationCountCol, "_primaryClassificationCountCol");
            _primaryClassificationCountCol.Name = "_primaryClassificationCountCol";
            _primaryClassificationCountCol.ReadOnly = true;
            // 
            // _primaryClassificationEditCol
            // 
            _primaryClassificationEditCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(_primaryClassificationEditCol, "_primaryClassificationEditCol");
            _primaryClassificationEditCol.Name = "_primaryClassificationEditCol";
            _primaryClassificationEditCol.ReadOnly = true;
            _primaryClassificationEditCol.Text = "→";
            _primaryClassificationEditCol.UseColumnTextForButtonValue = true;
            // 
            // _secondaryClassificationSplitContainer
            // 
            resources.ApplyResources(_secondaryClassificationSplitContainer, "_secondaryClassificationSplitContainer");
            _secondaryClassificationSplitContainer.FixedPanel = FixedPanel.Panel1;
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
            // 
            // _secondaryClassificationRuleSplitContainer
            // 
            resources.ApplyResources(_secondaryClassificationRuleSplitContainer, "_secondaryClassificationRuleSplitContainer");
            _secondaryClassificationRuleSplitContainer.FixedPanel = FixedPanel.Panel2;
            _secondaryClassificationRuleSplitContainer.Name = "_secondaryClassificationRuleSplitContainer";
            // 
            // _secondaryClassificationRuleSplitContainer.Panel1
            // 
            _secondaryClassificationRuleSplitContainer.Panel1.Controls.Add(_secondaryRuleDataGridView);
            // 
            // _secondaryClassificationRuleSplitContainer.Panel2
            // 
            _secondaryClassificationRuleSplitContainer.Panel2.Controls.Add(_secondaryClassificationRichTextBox);
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
            resources.ApplyResources(_secondaryRuleDataGridView, "_secondaryRuleDataGridView");
            _secondaryRuleDataGridView.MultiSelect = false;
            _secondaryRuleDataGridView.Name = "_secondaryRuleDataGridView";
            _secondaryRuleDataGridView.ReadOnly = true;
            _secondaryRuleDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _secondaryRuleDataGridView.CellClick += RuleDataGridView_CellClick;
            _secondaryRuleDataGridView.CellMouseClick += RuleDataGridView_CellMouseClick;
            _secondaryRuleDataGridView.RowPrePaint += DataGridView_RowPrePaint;
            _secondaryRuleDataGridView.SelectionChanged += RuleDataGridView_SelectionChanged;
            // 
            // NameColumn
            // 
            NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(NameColumn, "NameColumn");
            NameColumn.Name = "NameColumn";
            NameColumn.ReadOnly = true;
            NameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // EditColumn
            // 
            EditColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(EditColumn, "EditColumn");
            EditColumn.Name = "EditColumn";
            EditColumn.ReadOnly = true;
            EditColumn.Text = "⚙";
            EditColumn.UseColumnTextForButtonValue = true;
            // 
            // _secondaryClassificationRichTextBox
            // 
            _secondaryClassificationRichTextBox.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(_secondaryClassificationRichTextBox, "_secondaryClassificationRichTextBox");
            _secondaryClassificationRichTextBox.Name = "_secondaryClassificationRichTextBox";
            _secondaryClassificationRichTextBox.ReadOnly = true;
            // 
            // _secondaryClassificationRuleToolStrip
            // 
            _secondaryClassificationRuleToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _secondaryClassificationRuleToolStrip.ImageScalingSize = new Size(24, 24);
            _secondaryClassificationRuleToolStrip.Items.AddRange(new ToolStripItem[] { _secondaryClassificationAddRuleToolStripButton, _secondaryClassificationDeleteRuleToolStripButton, toolStripSeparator6, _secondaryClassificationMoveRuleUpToolStripButton, _secondaryClassificationMoveRuleDownToolStripButton });
            resources.ApplyResources(_secondaryClassificationRuleToolStrip, "_secondaryClassificationRuleToolStrip");
            _secondaryClassificationRuleToolStrip.Name = "_secondaryClassificationRuleToolStrip";
            // 
            // _secondaryClassificationAddRuleToolStripButton
            // 
            _secondaryClassificationAddRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationAddRuleToolStripButton, "_secondaryClassificationAddRuleToolStripButton");
            _secondaryClassificationAddRuleToolStripButton.Name = "_secondaryClassificationAddRuleToolStripButton";
            _secondaryClassificationAddRuleToolStripButton.Click += AddClassificationRule;
            // 
            // _secondaryClassificationDeleteRuleToolStripButton
            // 
            _secondaryClassificationDeleteRuleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationDeleteRuleToolStripButton, "_secondaryClassificationDeleteRuleToolStripButton");
            _secondaryClassificationDeleteRuleToolStripButton.Name = "_secondaryClassificationDeleteRuleToolStripButton";
            _secondaryClassificationDeleteRuleToolStripButton.Click += DeleteClassificationRule;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            // 
            // _secondaryClassificationMoveRuleUpToolStripButton
            // 
            _secondaryClassificationMoveRuleUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationMoveRuleUpToolStripButton, "_secondaryClassificationMoveRuleUpToolStripButton");
            _secondaryClassificationMoveRuleUpToolStripButton.Name = "_secondaryClassificationMoveRuleUpToolStripButton";
            _secondaryClassificationMoveRuleUpToolStripButton.Click += MoveRuleUp;
            // 
            // _secondaryClassificationMoveRuleDownToolStripButton
            // 
            _secondaryClassificationMoveRuleDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationMoveRuleDownToolStripButton, "_secondaryClassificationMoveRuleDownToolStripButton");
            _secondaryClassificationMoveRuleDownToolStripButton.Name = "_secondaryClassificationMoveRuleDownToolStripButton";
            _secondaryClassificationMoveRuleDownToolStripButton.Click += MoveRuleDown;
            // 
            // _secondaryClassificationTabControl
            // 
            resources.ApplyResources(_secondaryClassificationTabControl, "_secondaryClassificationTabControl");
            _secondaryClassificationTabControl.Controls.Add(_secondaryClassificationViewportTabPage);
            _secondaryClassificationTabControl.Controls.Add(_secondaryClassificationTableTabPage);
            _secondaryClassificationTabControl.Name = "_secondaryClassificationTabControl";
            _secondaryClassificationTabControl.SelectedIndex = 0;
            _secondaryClassificationTabControl.SizeMode = TabSizeMode.Fixed;
            // 
            // _secondaryClassificationViewportTabPage
            // 
            _secondaryClassificationViewportTabPage.Controls.Add(_secondaryClassificationViewport);
            _secondaryClassificationViewportTabPage.Controls.Add(toolStrip2);
            resources.ApplyResources(_secondaryClassificationViewportTabPage, "_secondaryClassificationViewportTabPage");
            _secondaryClassificationViewportTabPage.Name = "_secondaryClassificationViewportTabPage";
            _secondaryClassificationViewportTabPage.UseVisualStyleBackColor = true;
            // 
            // _secondaryClassificationViewport
            // 
            resources.ApplyResources(_secondaryClassificationViewport, "_secondaryClassificationViewport");
            _secondaryClassificationViewport.Name = "_secondaryClassificationViewport";
            // 
            // toolStrip2
            // 
            toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip2.ImageScalingSize = new Size(24, 24);
            toolStrip2.Items.AddRange(new ToolStripItem[] { toolStripLabel4, _secondaryUpToolStripButton, _secondaryDownToolStripButton, toolStripSeparator12, _secondaryLeftToolStripButton, _secondaryRightToolStripButton, toolStripSeparator15, _secondaryZoomInToolStripButton, _secondaryZoomOutToolStripButton });
            resources.ApplyResources(toolStrip2, "toolStrip2");
            toolStrip2.Name = "toolStrip2";
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.Name = "toolStripLabel4";
            resources.ApplyResources(toolStripLabel4, "toolStripLabel4");
            // 
            // _secondaryUpToolStripButton
            // 
            _secondaryUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryUpToolStripButton, "_secondaryUpToolStripButton");
            _secondaryUpToolStripButton.Name = "_secondaryUpToolStripButton";
            // 
            // _secondaryDownToolStripButton
            // 
            _secondaryDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryDownToolStripButton, "_secondaryDownToolStripButton");
            _secondaryDownToolStripButton.Name = "_secondaryDownToolStripButton";
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            resources.ApplyResources(toolStripSeparator12, "toolStripSeparator12");
            // 
            // _secondaryLeftToolStripButton
            // 
            _secondaryLeftToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryLeftToolStripButton, "_secondaryLeftToolStripButton");
            _secondaryLeftToolStripButton.Name = "_secondaryLeftToolStripButton";
            // 
            // _secondaryRightToolStripButton
            // 
            _secondaryRightToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryRightToolStripButton, "_secondaryRightToolStripButton");
            _secondaryRightToolStripButton.Name = "_secondaryRightToolStripButton";
            // 
            // toolStripSeparator15
            // 
            toolStripSeparator15.Name = "toolStripSeparator15";
            resources.ApplyResources(toolStripSeparator15, "toolStripSeparator15");
            // 
            // _secondaryZoomInToolStripButton
            // 
            _secondaryZoomInToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryZoomInToolStripButton, "_secondaryZoomInToolStripButton");
            _secondaryZoomInToolStripButton.Name = "_secondaryZoomInToolStripButton";
            // 
            // _secondaryZoomOutToolStripButton
            // 
            _secondaryZoomOutToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryZoomOutToolStripButton, "_secondaryZoomOutToolStripButton");
            _secondaryZoomOutToolStripButton.Name = "_secondaryZoomOutToolStripButton";
            // 
            // _secondaryClassificationTableTabPage
            // 
            _secondaryClassificationTableTabPage.Controls.Add(_secondaryClassificationDataGridView);
            resources.ApplyResources(_secondaryClassificationTableTabPage, "_secondaryClassificationTableTabPage");
            _secondaryClassificationTableTabPage.Name = "_secondaryClassificationTableTabPage";
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
            resources.ApplyResources(_secondaryClassificationDataGridView, "_secondaryClassificationDataGridView");
            _secondaryClassificationDataGridView.Name = "_secondaryClassificationDataGridView";
            _secondaryClassificationDataGridView.ReadOnly = true;
            _secondaryClassificationDataGridView.RowHeadersVisible = false;
            _secondaryClassificationDataGridView.CellClick += ClassificationGrid_CellClick;
            // 
            // _secondaryClassificationColorCol
            // 
            _secondaryClassificationColorCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(_secondaryClassificationColorCol, "_secondaryClassificationColorCol");
            _secondaryClassificationColorCol.Name = "_secondaryClassificationColorCol";
            _secondaryClassificationColorCol.ReadOnly = true;
            // 
            // _secondaryClassificationNameCol
            // 
            _secondaryClassificationNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_secondaryClassificationNameCol, "_secondaryClassificationNameCol");
            _secondaryClassificationNameCol.Name = "_secondaryClassificationNameCol";
            _secondaryClassificationNameCol.ReadOnly = true;
            // 
            // _secondaryClassificationCountCol
            // 
            _secondaryClassificationCountCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(_secondaryClassificationCountCol, "_secondaryClassificationCountCol");
            _secondaryClassificationCountCol.Name = "_secondaryClassificationCountCol";
            _secondaryClassificationCountCol.ReadOnly = true;
            // 
            // _secondaryClassificationEditCol
            // 
            _secondaryClassificationEditCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(_secondaryClassificationEditCol, "_secondaryClassificationEditCol");
            _secondaryClassificationEditCol.Name = "_secondaryClassificationEditCol";
            _secondaryClassificationEditCol.ReadOnly = true;
            _secondaryClassificationEditCol.Text = "→";
            _secondaryClassificationEditCol.UseColumnTextForButtonValue = true;
            // 
            // _menuStrip
            // 
            _menuStrip.ImageScalingSize = new Size(24, 24);
            _menuStrip.Items.AddRange(new ToolStripItem[] { _fileToolStripMenuItem, _toolsToolStripMenuItem });
            resources.ApplyResources(_menuStrip, "_menuStrip");
            _menuStrip.Name = "_menuStrip";
            // 
            // _fileToolStripMenuItem
            // 
            _fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _openToolStripMenuItem, _exportGraphToolStripMenuItem, _exportClassificationToolStripMenuItem, toolStripSeparator1, _exitToolStripMenuItem });
            _fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            resources.ApplyResources(_fileToolStripMenuItem, "_fileToolStripMenuItem");
            // 
            // _openToolStripMenuItem
            // 
            _openToolStripMenuItem.Name = "_openToolStripMenuItem";
            resources.ApplyResources(_openToolStripMenuItem, "_openToolStripMenuItem");
            _openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // _exportGraphToolStripMenuItem
            // 
            _exportGraphToolStripMenuItem.Name = "_exportGraphToolStripMenuItem";
            resources.ApplyResources(_exportGraphToolStripMenuItem, "_exportGraphToolStripMenuItem");
            _exportGraphToolStripMenuItem.Click += ExportActivePlot;
            // 
            // _exportClassificationToolStripMenuItem
            // 
            _exportClassificationToolStripMenuItem.Name = "_exportClassificationToolStripMenuItem";
            resources.ApplyResources(_exportClassificationToolStripMenuItem, "_exportClassificationToolStripMenuItem");
            _exportClassificationToolStripMenuItem.Click += ExportClassification;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // _exitToolStripMenuItem
            // 
            _exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
            resources.ApplyResources(_exitToolStripMenuItem, "_exitToolStripMenuItem");
            _exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // _toolsToolStripMenuItem
            // 
            _toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _settingsToolStripMenuItem });
            _toolsToolStripMenuItem.Name = "_toolsToolStripMenuItem";
            resources.ApplyResources(_toolsToolStripMenuItem, "_toolsToolStripMenuItem");
            // 
            // _settingsToolStripMenuItem
            // 
            _settingsToolStripMenuItem.Name = "_settingsToolStripMenuItem";
            resources.ApplyResources(_settingsToolStripMenuItem, "_settingsToolStripMenuItem");
            _settingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
            // 
            // _statusStrip
            // 
            _statusStrip.ImageScalingSize = new Size(24, 24);
            _statusStrip.Items.AddRange(new ToolStripItem[] { _mainStatusLabel, _mainProgressBar });
            resources.ApplyResources(_statusStrip, "_statusStrip");
            _statusStrip.Name = "_statusStrip";
            // 
            // _mainStatusLabel
            // 
            _mainStatusLabel.Name = "_mainStatusLabel";
            resources.ApplyResources(_mainStatusLabel, "_mainStatusLabel");
            // 
            // _mainProgressBar
            // 
            _mainProgressBar.Name = "_mainProgressBar";
            resources.ApplyResources(_mainProgressBar, "_mainProgressBar");
            // 
            // _mainTabControl
            // 
            _mainTabControl.Controls.Add(_dataTabPage);
            _mainTabControl.Controls.Add(_explorationTabPage);
            _mainTabControl.Controls.Add(_classificationTabPage);
            resources.ApplyResources(_mainTabControl, "_mainTabControl");
            _mainTabControl.Multiline = true;
            _mainTabControl.Name = "_mainTabControl";
            _mainTabControl.SelectedIndex = 0;
            _mainTabControl.SizeMode = TabSizeMode.Fixed;
            _mainTabControl.Selected += TabControl_Selected;
            // 
            // _dataTabPage
            // 
            _dataTabPage.Controls.Add(_dataSplitContainer);
            resources.ApplyResources(_dataTabPage, "_dataTabPage");
            _dataTabPage.Name = "_dataTabPage";
            _dataTabPage.UseVisualStyleBackColor = true;
            // 
            // _explorationTabPage
            // 
            _explorationTabPage.Controls.Add(_explorationTabControl);
            resources.ApplyResources(_explorationTabPage, "_explorationTabPage");
            _explorationTabPage.Name = "_explorationTabPage";
            _explorationTabPage.UseVisualStyleBackColor = true;
            // 
            // _explorationTabControl
            // 
            _explorationTabControl.Controls.Add(_correlationTabPage);
            _explorationTabControl.Controls.Add(_kdeTabPage);
            _explorationTabControl.Controls.Add(_scatterTabPage);
            resources.ApplyResources(_explorationTabControl, "_explorationTabControl");
            _explorationTabControl.Name = "_explorationTabControl";
            _explorationTabControl.SelectedIndex = 0;
            // 
            // _correlationTabPage
            // 
            _correlationTabPage.Controls.Add(_correlationDataGridView);
            resources.ApplyResources(_correlationTabPage, "_correlationTabPage");
            _correlationTabPage.Name = "_correlationTabPage";
            _correlationTabPage.UseVisualStyleBackColor = true;
            // 
            // _correlationDataGridView
            // 
            _correlationDataGridView.AllowUserToAddRows = false;
            _correlationDataGridView.AllowUserToDeleteRows = false;
            _correlationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _correlationDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            _correlationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(_correlationDataGridView, "_correlationDataGridView");
            _correlationDataGridView.Name = "_correlationDataGridView";
            _correlationDataGridView.ReadOnly = true;
            _correlationDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            // 
            // _kdeTabPage
            // 
            _kdeTabPage.Controls.Add(_kdeSplitContainer);
            resources.ApplyResources(_kdeTabPage, "_kdeTabPage");
            _kdeTabPage.Name = "_kdeTabPage";
            _kdeTabPage.UseVisualStyleBackColor = true;
            // 
            // _scatterTabPage
            // 
            _scatterTabPage.Controls.Add(_scatterSplitContainer1);
            resources.ApplyResources(_scatterTabPage, "_scatterTabPage");
            _scatterTabPage.Name = "_scatterTabPage";
            _scatterTabPage.UseVisualStyleBackColor = true;
            // 
            // _classificationTabPage
            // 
            _classificationTabPage.Controls.Add(_classificationTabControl);
            _classificationTabPage.Controls.Add(_classificationAbortButton);
            resources.ApplyResources(_classificationTabPage, "_classificationTabPage");
            _classificationTabPage.Name = "_classificationTabPage";
            _classificationTabPage.UseVisualStyleBackColor = true;
            // 
            // _classificationTabControl
            // 
            _classificationTabControl.Controls.Add(_primaryClassificationTabPage);
            _classificationTabControl.Controls.Add(_secondaryClassificationTabPage);
            resources.ApplyResources(_classificationTabControl, "_classificationTabControl");
            _classificationTabControl.Name = "_classificationTabControl";
            _classificationTabControl.SelectedIndex = 0;
            _classificationTabControl.SizeMode = TabSizeMode.Fixed;
            // 
            // _primaryClassificationTabPage
            // 
            _primaryClassificationTabPage.Controls.Add(_primaryClassificationSplitContainer);
            _primaryClassificationTabPage.Controls.Add(_primaryClassificationToolStrip);
            resources.ApplyResources(_primaryClassificationTabPage, "_primaryClassificationTabPage");
            _primaryClassificationTabPage.Name = "_primaryClassificationTabPage";
            _primaryClassificationTabPage.UseVisualStyleBackColor = true;
            // 
            // _primaryClassificationToolStrip
            // 
            _primaryClassificationToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _primaryClassificationToolStrip.ImageScalingSize = new Size(24, 24);
            _primaryClassificationToolStrip.Items.AddRange(new ToolStripItem[] { _primaryClassificationCompareToolStripButton, _primaryClassificationClassifyToolStripButton, toolStripSeparator2, toolStripLabel1, _primaryClassificationModeToolStripComboBox, toolStripSeparator8, _primaryClassificationExportToolStripButton });
            resources.ApplyResources(_primaryClassificationToolStrip, "_primaryClassificationToolStrip");
            _primaryClassificationToolStrip.Name = "_primaryClassificationToolStrip";
            // 
            // _primaryClassificationCompareToolStripButton
            // 
            _primaryClassificationCompareToolStripButton.Alignment = ToolStripItemAlignment.Right;
            _primaryClassificationCompareToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationCompareToolStripButton, "_primaryClassificationCompareToolStripButton");
            _primaryClassificationCompareToolStripButton.Name = "_primaryClassificationCompareToolStripButton";
            _primaryClassificationCompareToolStripButton.Click += CompareToolStripButton_Click;
            // 
            // _primaryClassificationClassifyToolStripButton
            // 
            _primaryClassificationClassifyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationClassifyToolStripButton, "_primaryClassificationClassifyToolStripButton");
            _primaryClassificationClassifyToolStripButton.Name = "_primaryClassificationClassifyToolStripButton";
            _primaryClassificationClassifyToolStripButton.Click += Classify_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(toolStripLabel1, "toolStripLabel1");
            // 
            // _primaryClassificationModeToolStripComboBox
            // 
            _primaryClassificationModeToolStripComboBox.Items.AddRange(new object[] { resources.GetString("_primaryClassificationModeToolStripComboBox.Items"), resources.GetString("_primaryClassificationModeToolStripComboBox.Items1") });
            _primaryClassificationModeToolStripComboBox.Name = "_primaryClassificationModeToolStripComboBox";
            resources.ApplyResources(_primaryClassificationModeToolStripComboBox, "_primaryClassificationModeToolStripComboBox");
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(toolStripSeparator8, "toolStripSeparator8");
            // 
            // _primaryClassificationExportToolStripButton
            // 
            _primaryClassificationExportToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_primaryClassificationExportToolStripButton, "_primaryClassificationExportToolStripButton");
            _primaryClassificationExportToolStripButton.Name = "_primaryClassificationExportToolStripButton";
            _primaryClassificationExportToolStripButton.Click += ExportClassification;
            // 
            // _secondaryClassificationTabPage
            // 
            _secondaryClassificationTabPage.Controls.Add(_secondaryClassificationSplitContainer);
            _secondaryClassificationTabPage.Controls.Add(_secondaryClassificationToolStrip);
            resources.ApplyResources(_secondaryClassificationTabPage, "_secondaryClassificationTabPage");
            _secondaryClassificationTabPage.Name = "_secondaryClassificationTabPage";
            _secondaryClassificationTabPage.UseVisualStyleBackColor = true;
            // 
            // _secondaryClassificationToolStrip
            // 
            _secondaryClassificationToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            _secondaryClassificationToolStrip.ImageScalingSize = new Size(24, 24);
            _secondaryClassificationToolStrip.Items.AddRange(new ToolStripItem[] { _secondaryClassificationCompareToolStripButton, _secondaryClassificationClassifyToolStripButton, toolStripSeparator16, filterToolStripButton, toolStripSeparator3, _secondaryClassificationExportToolStripButton });
            resources.ApplyResources(_secondaryClassificationToolStrip, "_secondaryClassificationToolStrip");
            _secondaryClassificationToolStrip.Name = "_secondaryClassificationToolStrip";
            // 
            // _secondaryClassificationCompareToolStripButton
            // 
            _secondaryClassificationCompareToolStripButton.Alignment = ToolStripItemAlignment.Right;
            _secondaryClassificationCompareToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationCompareToolStripButton, "_secondaryClassificationCompareToolStripButton");
            _secondaryClassificationCompareToolStripButton.Name = "_secondaryClassificationCompareToolStripButton";
            _secondaryClassificationCompareToolStripButton.Click += CompareToolStripButton_Click;
            // 
            // _secondaryClassificationClassifyToolStripButton
            // 
            _secondaryClassificationClassifyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationClassifyToolStripButton, "_secondaryClassificationClassifyToolStripButton");
            _secondaryClassificationClassifyToolStripButton.Name = "_secondaryClassificationClassifyToolStripButton";
            _secondaryClassificationClassifyToolStripButton.Click += Classify_Click;
            // 
            // toolStripSeparator16
            // 
            toolStripSeparator16.Name = "toolStripSeparator16";
            resources.ApplyResources(toolStripSeparator16, "toolStripSeparator16");
            // 
            // filterToolStripButton
            // 
            filterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(filterToolStripButton, "filterToolStripButton");
            filterToolStripButton.Name = "filterToolStripButton";
            filterToolStripButton.Click += filterToolStripButton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // _secondaryClassificationExportToolStripButton
            // 
            _secondaryClassificationExportToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(_secondaryClassificationExportToolStripButton, "_secondaryClassificationExportToolStripButton");
            _secondaryClassificationExportToolStripButton.Name = "_secondaryClassificationExportToolStripButton";
            _secondaryClassificationExportToolStripButton.Click += ExportClassification;
            // 
            // _classificationAbortButton
            // 
            resources.ApplyResources(_classificationAbortButton, "_classificationAbortButton");
            _classificationAbortButton.Name = "_classificationAbortButton";
            _classificationAbortButton.UseVisualStyleBackColor = true;
            _classificationAbortButton.Click += AbortClassification_Click;
            // 
            // _ruleContextMenuStrip
            // 
            _ruleContextMenuStrip.ImageScalingSize = new Size(24, 24);
            _ruleContextMenuStrip.Items.AddRange(new ToolStripItem[] { _moveUpToolStripMenuItem, _moveDownToolStripMenuItem, toolStripSeparator4, _editToolStripMenuItem, _cloneToolStripMenuItem, toolStripSeparator5, _removeToolStripMenuItem });
            _ruleContextMenuStrip.Name = "_ruleContextMenuStrip";
            resources.ApplyResources(_ruleContextMenuStrip, "_ruleContextMenuStrip");
            // 
            // _moveUpToolStripMenuItem
            // 
            _moveUpToolStripMenuItem.Name = "_moveUpToolStripMenuItem";
            resources.ApplyResources(_moveUpToolStripMenuItem, "_moveUpToolStripMenuItem");
            _moveUpToolStripMenuItem.Click += MoveRuleUp;
            // 
            // _moveDownToolStripMenuItem
            // 
            _moveDownToolStripMenuItem.Name = "_moveDownToolStripMenuItem";
            resources.ApplyResources(_moveDownToolStripMenuItem, "_moveDownToolStripMenuItem");
            _moveDownToolStripMenuItem.Click += MoveRuleDown;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            // 
            // _editToolStripMenuItem
            // 
            _editToolStripMenuItem.Name = "_editToolStripMenuItem";
            resources.ApplyResources(_editToolStripMenuItem, "_editToolStripMenuItem");
            _editToolStripMenuItem.Click += EditClassificationRule;
            // 
            // _cloneToolStripMenuItem
            // 
            _cloneToolStripMenuItem.Name = "_cloneToolStripMenuItem";
            resources.ApplyResources(_cloneToolStripMenuItem, "_cloneToolStripMenuItem");
            _cloneToolStripMenuItem.Click += CloneClassificationRule;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // _removeToolStripMenuItem
            // 
            _removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
            resources.ApplyResources(_removeToolStripMenuItem, "_removeToolStripMenuItem");
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
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_mainTabControl);
            Controls.Add(_statusStrip);
            Controls.Add(_menuStrip);
            MainMenuStrip = _menuStrip;
            Name = "MainForm";
            Load += MainForm_Load;
            _dataSplitContainer.Panel1.ResumeLayout(false);
            _dataSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_dataSplitContainer).EndInit();
            _dataSplitContainer.ResumeLayout(false);
            _bandsDataSplitContainer.Panel1.ResumeLayout(false);
            _bandsDataSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_bandsDataSplitContainer).EndInit();
            _bandsDataSplitContainer.ResumeLayout(false);
            _bandsDataGroupBox.ResumeLayout(false);
            _bandsDataPropertiesGroupBox.ResumeLayout(false);
            _dataTabControl.ResumeLayout(false);
            _dataViewportTabPage.ResumeLayout(false);
            _dataViewportTabPage.PerformLayout();
            _dataViewportToolStrip.ResumeLayout(false);
            _dataViewportToolStrip.PerformLayout();
            _dataHistogramTabPage.ResumeLayout(false);
            _kdeSplitContainer.Panel1.ResumeLayout(false);
            _kdeSplitContainer.Panel1.PerformLayout();
            _kdeSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)_kdeSplitContainer).EndInit();
            _kdeSplitContainer.ResumeLayout(false);
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
            _primaryClassificationViewportTabPage.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            _primaryClassificationTableTabPage.ResumeLayout(false);
            ((ISupportInitialize)_primaryClassificationDataGridView).EndInit();
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
            _secondaryClassificationViewportTabPage.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            _secondaryClassificationTableTabPage.ResumeLayout(false);
            ((ISupportInitialize)_secondaryClassificationDataGridView).EndInit();
            _menuStrip.ResumeLayout(false);
            _menuStrip.PerformLayout();
            _statusStrip.ResumeLayout(false);
            _statusStrip.PerformLayout();
            _mainTabControl.ResumeLayout(false);
            _dataTabPage.ResumeLayout(false);
            _explorationTabPage.ResumeLayout(false);
            _explorationTabControl.ResumeLayout(false);
            _correlationTabPage.ResumeLayout(false);
            ((ISupportInitialize)_correlationDataGridView).EndInit();
            _kdeTabPage.ResumeLayout(false);
            _scatterTabPage.ResumeLayout(false);
            _classificationTabPage.ResumeLayout(false);
            _classificationTabPage.PerformLayout();
            _classificationTabControl.ResumeLayout(false);
            _primaryClassificationTabPage.ResumeLayout(false);
            _primaryClassificationTabPage.PerformLayout();
            _primaryClassificationToolStrip.ResumeLayout(false);
            _primaryClassificationToolStrip.PerformLayout();
            _secondaryClassificationTabPage.ResumeLayout(false);
            _secondaryClassificationTabPage.PerformLayout();
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
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewButtonColumn EditColumn;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton _dataUpToolStripButton;
        private ToolStripButton _dataDownToolStripButton;
        private ToolStripButton _dataLeftToolStripButton;
        private ToolStripButton _dataRightToolStripButton;
        private ToolStripButton _dataZoomInToolStripButton;
        private ToolStripButton _dataZoomOutToolStripButton;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel3;
        private ToolStripButton _primaryUpToolStripButton;
        private ToolStripButton _primaryDownToolStripButton;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripButton _primaryLeftToolStripButton;
        private ToolStripButton _primaryRightToolStripButton;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripButton _primaryZoomInToolStripButton;
        private ToolStripButton _primaryZoomOutToolStripButton;
        private ToolStrip toolStrip2;
        private ToolStripLabel toolStripLabel4;
        private ToolStripButton _secondaryUpToolStripButton;
        private ToolStripButton _secondaryDownToolStripButton;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripButton _secondaryLeftToolStripButton;
        private ToolStripButton _secondaryRightToolStripButton;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripButton _secondaryZoomInToolStripButton;
        private ToolStripButton _secondaryZoomOutToolStripButton;
        private ToolStripMenuItem _toolsToolStripMenuItem;
        private ToolStripMenuItem _settingsToolStripMenuItem;
        private DataGridViewImageColumn _primaryClassificationColorCol;
        private DataGridViewTextBoxColumn _primaryClassificationNameCol;
        private DataGridViewTextBoxColumn _primaryClassificationCountCol;
        private DataGridViewButtonColumn _primaryClassificationEditCol;
        private DataGridViewImageColumn _secondaryClassificationColorCol;
        private DataGridViewTextBoxColumn _secondaryClassificationNameCol;
        private DataGridViewTextBoxColumn _secondaryClassificationCountCol;
        private DataGridViewButtonColumn _secondaryClassificationEditCol;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripButton filterToolStripButton;
    }
}
