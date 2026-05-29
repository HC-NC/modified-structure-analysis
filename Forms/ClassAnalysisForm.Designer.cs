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
            statusStrip1 = new StatusStrip();
            _summaryToolStripStatusLabel = new ToolStripStatusLabel();
            _saveFileDialog = new SaveFileDialog();
            _backgroundWorker = new System.ComponentModel.BackgroundWorker();
            _scatterWorker = new System.ComponentModel.BackgroundWorker();
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
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Controls.Add(toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            splitContainer1.Panel2.Controls.Add(tabControl1);
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(_classesListBox);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // _classesListBox
            // 
            resources.ApplyResources(_classesListBox, "_classesListBox");
            _classesListBox.FormattingEnabled = true;
            _classesListBox.Name = "_classesListBox";
            _classesListBox.SelectedIndexChanged += ClassesListBox_SelectedIndexChanged;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { _exportToolStripDropDownButton });
            toolStrip1.Name = "toolStrip1";
            // 
            // _exportToolStripDropDownButton
            // 
            resources.ApplyResources(_exportToolStripDropDownButton, "_exportToolStripDropDownButton");
            _exportToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _exportToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { _allStatsToolStripMenuItem, _pixelsToolStripMenuItem, _plotToolStripMenuItem });
            _exportToolStripDropDownButton.Name = "_exportToolStripDropDownButton";
            // 
            // _allStatsToolStripMenuItem
            // 
            resources.ApplyResources(_allStatsToolStripMenuItem, "_allStatsToolStripMenuItem");
            _allStatsToolStripMenuItem.Name = "_allStatsToolStripMenuItem";
            _allStatsToolStripMenuItem.Click += ExportToolStripButton_Click;
            // 
            // _pixelsToolStripMenuItem
            // 
            resources.ApplyResources(_pixelsToolStripMenuItem, "_pixelsToolStripMenuItem");
            _pixelsToolStripMenuItem.Name = "_pixelsToolStripMenuItem";
            _pixelsToolStripMenuItem.Click += ExportPixelsTable_Click;
            // 
            // _plotToolStripMenuItem
            // 
            resources.ApplyResources(_plotToolStripMenuItem, "_plotToolStripMenuItem");
            _plotToolStripMenuItem.Name = "_plotToolStripMenuItem";
            _plotToolStripMenuItem.Click += ExportActivePlot_Click;
            // 
            // tabControl1
            // 
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Controls.Add(_statsTabPage);
            tabControl1.Controls.Add(_kdeTabPage);
            tabControl1.Controls.Add(_scatterTabPage);
            tabControl1.Controls.Add(_pixelsTabPage);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // _statsTabPage
            // 
            resources.ApplyResources(_statsTabPage, "_statsTabPage");
            _statsTabPage.Controls.Add(splitContainer2);
            _statsTabPage.Name = "_statsTabPage";
            _statsTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(splitContainer2.Panel1, "splitContainer2.Panel1");
            splitContainer2.Panel1.Controls.Add(groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(splitContainer2.Panel2, "splitContainer2.Panel2");
            splitContainer2.Panel2.Controls.Add(groupBox3);
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(_bandsListBox);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // _bandsListBox
            // 
            resources.ApplyResources(_bandsListBox, "_bandsListBox");
            _bandsListBox.FormattingEnabled = true;
            _bandsListBox.Name = "_bandsListBox";
            _bandsListBox.SelectedIndexChanged += BandsListBox_SelectedIndexChanged;
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(_propertyGrid);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // _propertyGrid
            // 
            resources.ApplyResources(_propertyGrid, "_propertyGrid");
            _propertyGrid.BackColor = SystemColors.Control;
            _propertyGrid.Name = "_propertyGrid";
            // 
            // _kdeTabPage
            // 
            resources.ApplyResources(_kdeTabPage, "_kdeTabPage");
            _kdeTabPage.Controls.Add(_kdeSplitContainer);
            _kdeTabPage.Name = "_kdeTabPage";
            _kdeTabPage.UseVisualStyleBackColor = true;
            // 
            // _kdeSplitContainer
            // 
            resources.ApplyResources(_kdeSplitContainer, "_kdeSplitContainer");
            _kdeSplitContainer.FixedPanel = FixedPanel.Panel1;
            _kdeSplitContainer.Name = "_kdeSplitContainer";
            // 
            // _kdeSplitContainer.Panel1
            // 
            resources.ApplyResources(_kdeSplitContainer.Panel1, "_kdeSplitContainer.Panel1");
            _kdeSplitContainer.Panel1.Controls.Add(_kdeListBox);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeSingleButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeProductButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeMultivariateButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeClearButton);
            _kdeSplitContainer.Panel1.Controls.Add(_kdeProgressBar);
            // 
            // _kdeSplitContainer.Panel2
            // 
            resources.ApplyResources(_kdeSplitContainer.Panel2, "_kdeSplitContainer.Panel2");
            _kdeSplitContainer.Panel2.Controls.Add(_kdePlotView);
            // 
            // _kdeListBox
            // 
            resources.ApplyResources(_kdeListBox, "_kdeListBox");
            _kdeListBox.FormattingEnabled = true;
            _kdeListBox.Name = "_kdeListBox";
            _kdeListBox.SelectionMode = SelectionMode.MultiExtended;
            // 
            // _kdeSingleButton
            // 
            resources.ApplyResources(_kdeSingleButton, "_kdeSingleButton");
            _kdeSingleButton.Name = "_kdeSingleButton";
            _kdeSingleButton.UseVisualStyleBackColor = true;
            _kdeSingleButton.Click += KdeSingleButton_Click;
            // 
            // _kdeProductButton
            // 
            resources.ApplyResources(_kdeProductButton, "_kdeProductButton");
            _kdeProductButton.Name = "_kdeProductButton";
            _kdeProductButton.UseVisualStyleBackColor = true;
            _kdeProductButton.Click += KdeProductButton_Click;
            // 
            // _kdeMultivariateButton
            // 
            resources.ApplyResources(_kdeMultivariateButton, "_kdeMultivariateButton");
            _kdeMultivariateButton.Name = "_kdeMultivariateButton";
            _kdeMultivariateButton.UseVisualStyleBackColor = true;
            _kdeMultivariateButton.Click += KdeMultivariateButton_Click;
            // 
            // _kdeClearButton
            // 
            resources.ApplyResources(_kdeClearButton, "_kdeClearButton");
            _kdeClearButton.Name = "_kdeClearButton";
            _kdeClearButton.UseVisualStyleBackColor = true;
            _kdeClearButton.Click += KdeClearButton_Click;
            // 
            // _kdeProgressBar
            // 
            resources.ApplyResources(_kdeProgressBar, "_kdeProgressBar");
            _kdeProgressBar.Name = "_kdeProgressBar";
            // 
            // _kdePlotView
            // 
            resources.ApplyResources(_kdePlotView, "_kdePlotView");
            _kdePlotView.Name = "_kdePlotView";
            _kdePlotView.PanCursor = Cursors.Hand;
            _kdePlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _kdePlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _kdePlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _kdePlotView.DoubleClick += ResetPlotAxes;
            // 
            // _scatterTabPage
            // 
            resources.ApplyResources(_scatterTabPage, "_scatterTabPage");
            _scatterTabPage.Controls.Add(_scatterSplitContainer1);
            _scatterTabPage.Name = "_scatterTabPage";
            _scatterTabPage.UseVisualStyleBackColor = true;
            // 
            // _scatterSplitContainer1
            // 
            resources.ApplyResources(_scatterSplitContainer1, "_scatterSplitContainer1");
            _scatterSplitContainer1.FixedPanel = FixedPanel.Panel1;
            _scatterSplitContainer1.Name = "_scatterSplitContainer1";
            // 
            // _scatterSplitContainer1.Panel1
            // 
            resources.ApplyResources(_scatterSplitContainer1.Panel1, "_scatterSplitContainer1.Panel1");
            _scatterSplitContainer1.Panel1.Controls.Add(_scatterSplitContainer2);
            _scatterSplitContainer1.Panel1.Controls.Add(_buildScatterButton);
            _scatterSplitContainer1.Panel1.Controls.Add(_scatterProgressBar);
            // 
            // _scatterSplitContainer1.Panel2
            // 
            resources.ApplyResources(_scatterSplitContainer1.Panel2, "_scatterSplitContainer1.Panel2");
            _scatterSplitContainer1.Panel2.Controls.Add(_scatterPlotView);
            // 
            // _scatterSplitContainer2
            // 
            resources.ApplyResources(_scatterSplitContainer2, "_scatterSplitContainer2");
            _scatterSplitContainer2.Name = "_scatterSplitContainer2";
            // 
            // _scatterSplitContainer2.Panel1
            // 
            resources.ApplyResources(_scatterSplitContainer2.Panel1, "_scatterSplitContainer2.Panel1");
            _scatterSplitContainer2.Panel1.Controls.Add(_scatterXAxisGroupBox);
            // 
            // _scatterSplitContainer2.Panel2
            // 
            resources.ApplyResources(_scatterSplitContainer2.Panel2, "_scatterSplitContainer2.Panel2");
            _scatterSplitContainer2.Panel2.Controls.Add(_scatterYAxisGroupBox);
            // 
            // _scatterXAxisGroupBox
            // 
            resources.ApplyResources(_scatterXAxisGroupBox, "_scatterXAxisGroupBox");
            _scatterXAxisGroupBox.Controls.Add(_scatterXListBox);
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
            resources.ApplyResources(_scatterYAxisGroupBox, "_scatterYAxisGroupBox");
            _scatterYAxisGroupBox.Controls.Add(_scatterYListBox);
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
            _buildScatterButton.Click += BuildScatterButton_Click;
            // 
            // _scatterProgressBar
            // 
            resources.ApplyResources(_scatterProgressBar, "_scatterProgressBar");
            _scatterProgressBar.Name = "_scatterProgressBar";
            // 
            // _scatterPlotView
            // 
            resources.ApplyResources(_scatterPlotView, "_scatterPlotView");
            _scatterPlotView.Name = "_scatterPlotView";
            _scatterPlotView.PanCursor = Cursors.Hand;
            _scatterPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            _scatterPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            _scatterPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            _scatterPlotView.DoubleClick += ResetPlotAxes;
            // 
            // _pixelsTabPage
            // 
            resources.ApplyResources(_pixelsTabPage, "_pixelsTabPage");
            _pixelsTabPage.Controls.Add(dataGridView1);
            _pixelsTabPage.Name = "_pixelsTabPage";
            _pixelsTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            resources.ApplyResources(dataGridView1, "dataGridView1");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { _indexColumn, _colColumn, _rowColumn, _mapXColumn, _mapYColumn, _valueColumn, _zScoreColumn });
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            // 
            // _indexColumn
            // 
            _indexColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(_indexColumn, "_indexColumn");
            _indexColumn.Name = "_indexColumn";
            _indexColumn.ReadOnly = true;
            // 
            // _colColumn
            // 
            _colColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_colColumn, "_colColumn");
            _colColumn.Name = "_colColumn";
            _colColumn.ReadOnly = true;
            // 
            // _rowColumn
            // 
            _rowColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_rowColumn, "_rowColumn");
            _rowColumn.Name = "_rowColumn";
            _rowColumn.ReadOnly = true;
            // 
            // _mapXColumn
            // 
            _mapXColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_mapXColumn, "_mapXColumn");
            _mapXColumn.Name = "_mapXColumn";
            _mapXColumn.ReadOnly = true;
            // 
            // _mapYColumn
            // 
            _mapYColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_mapYColumn, "_mapYColumn");
            _mapYColumn.Name = "_mapYColumn";
            _mapYColumn.ReadOnly = true;
            // 
            // _valueColumn
            // 
            _valueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_valueColumn, "_valueColumn");
            _valueColumn.Name = "_valueColumn";
            _valueColumn.ReadOnly = true;
            // 
            // _zScoreColumn
            // 
            _zScoreColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(_zScoreColumn, "_zScoreColumn");
            _zScoreColumn.Name = "_zScoreColumn";
            _zScoreColumn.ReadOnly = true;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { _summaryToolStripStatusLabel });
            statusStrip1.Name = "statusStrip1";
            // 
            // _summaryToolStripStatusLabel
            // 
            resources.ApplyResources(_summaryToolStripStatusLabel, "_summaryToolStripStatusLabel");
            _summaryToolStripStatusLabel.Name = "_summaryToolStripStatusLabel";
            // 
            // _saveFileDialog
            // 
            resources.ApplyResources(_saveFileDialog, "_saveFileDialog");
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
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Name = "ClassAnalysisForm";
            ShowIcon = false;
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
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
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
        private DataGridViewTextBoxColumn _indexColumn;
        private DataGridViewTextBoxColumn _colColumn;
        private DataGridViewTextBoxColumn _rowColumn;
        private DataGridViewTextBoxColumn _mapXColumn;
        private DataGridViewTextBoxColumn _mapYColumn;
        private DataGridViewTextBoxColumn _valueColumn;
        private DataGridViewTextBoxColumn _zScoreColumn;
    }
}