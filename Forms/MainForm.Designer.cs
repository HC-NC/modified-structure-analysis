namespace modified_structure_analysis
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            statusStrip1 = new StatusStrip();
            mainStatusLabel = new ToolStripStatusLabel();
            mainProgressBar = new ToolStripProgressBar();
            bandListBox = new ListBox();
            bandPropertyGrid = new PropertyGrid();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            tabControl1 = new TabControl();
            dataTabPage = new TabPage();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            dataTabControl = new TabControl();
            viewportTabPage = new TabPage();
            toolStripContainer1 = new ToolStripContainer();
            viewport1 = new modified_structure_analysis.Forms.Viewport();
            toolStrip1 = new ToolStrip();
            redToolStripDropDownButton = new ToolStripDropDownButton();
            greenToolStripDropDownButton = new ToolStripDropDownButton();
            blueToolStripDropDownButton = new ToolStripDropDownButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton1 = new ToolStripButton();
            histogramTabPage = new TabPage();
            histogramPlotView = new OxyPlot.WindowsForms.PlotView();
            explorationTabPage = new TabPage();
            splitContainer3 = new SplitContainer();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            correlationDataGridView = new DataGridView();
            classificationTabPage = new TabPage();
            splitContainer4 = new SplitContainer();
            label3 = new Label();
            comboBox3 = new ComboBox();
            button1 = new Button();
            label2 = new Label();
            label1 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            viewport2 = new modified_structure_analysis.Forms.Viewport();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            tabControl1.SuspendLayout();
            dataTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            dataTabControl.SuspendLayout();
            viewportTabPage.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            histogramTabPage.SuspendLayout();
            explorationTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)correlationDataGridView).BeginInit();
            classificationTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1178, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(223, 34);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(220, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitToolStripMenuItem.Size = new Size(223, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { mainStatusLabel, mainProgressBar });
            statusStrip1.Location = new Point(0, 712);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1178, 32);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // mainStatusLabel
            // 
            mainStatusLabel.Name = "mainStatusLabel";
            mainStatusLabel.Size = new Size(60, 25);
            mainStatusLabel.Text = "Status";
            // 
            // mainProgressBar
            // 
            mainProgressBar.Name = "mainProgressBar";
            mainProgressBar.Size = new Size(100, 24);
            // 
            // bandListBox
            // 
            bandListBox.Dock = DockStyle.Fill;
            bandListBox.FormattingEnabled = true;
            bandListBox.Location = new Point(3, 27);
            bandListBox.Name = "bandListBox";
            bandListBox.Size = new Size(294, 110);
            bandListBox.TabIndex = 3;
            bandListBox.SelectedIndexChanged += bandListBox_SelectedIndexChanged;
            // 
            // bandPropertyGrid
            // 
            bandPropertyGrid.BackColor = SystemColors.Control;
            bandPropertyGrid.Dock = DockStyle.Fill;
            bandPropertyGrid.Location = new Point(3, 27);
            bandPropertyGrid.Name = "bandPropertyGrid";
            bandPropertyGrid.Size = new Size(294, 461);
            bandPropertyGrid.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bandListBox);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(300, 140);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Bands";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(bandPropertyGrid);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(300, 491);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Properties";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(dataTabPage);
            tabControl1.Controls.Add(explorationTabPage);
            tabControl1.Controls.Add(classificationTabPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size(150, 30);
            tabControl1.Location = new Point(0, 33);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1178, 679);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 7;
            tabControl1.Selected += TabControl_Selected;
            // 
            // dataTabPage
            // 
            dataTabPage.Controls.Add(splitContainer1);
            dataTabPage.Location = new Point(4, 34);
            dataTabPage.Name = "dataTabPage";
            dataTabPage.Padding = new Padding(3);
            dataTabPage.Size = new Size(1170, 641);
            dataTabPage.TabIndex = 0;
            dataTabPage.Text = "Data";
            dataTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataTabControl);
            splitContainer1.Size = new Size(1164, 635);
            splitContainer1.SplitterDistance = 300;
            splitContainer1.TabIndex = 7;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(groupBox2);
            splitContainer2.Size = new Size(300, 635);
            splitContainer2.SplitterDistance = 140;
            splitContainer2.TabIndex = 7;
            // 
            // dataTabControl
            // 
            dataTabControl.Alignment = TabAlignment.Bottom;
            dataTabControl.Controls.Add(viewportTabPage);
            dataTabControl.Controls.Add(histogramTabPage);
            dataTabControl.Dock = DockStyle.Fill;
            dataTabControl.ItemSize = new Size(150, 30);
            dataTabControl.Location = new Point(0, 0);
            dataTabControl.Multiline = true;
            dataTabControl.Name = "dataTabControl";
            dataTabControl.SelectedIndex = 0;
            dataTabControl.Size = new Size(860, 635);
            dataTabControl.SizeMode = TabSizeMode.Fixed;
            dataTabControl.TabIndex = 0;
            dataTabControl.Selected += TabControl_Selected;
            // 
            // viewportTabPage
            // 
            viewportTabPage.Controls.Add(toolStripContainer1);
            viewportTabPage.Location = new Point(4, 4);
            viewportTabPage.Name = "viewportTabPage";
            viewportTabPage.Padding = new Padding(3);
            viewportTabPage.Size = new Size(852, 597);
            viewportTabPage.TabIndex = 0;
            viewportTabPage.Text = "Viewport";
            viewportTabPage.UseVisualStyleBackColor = true;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(viewport1);
            toolStripContainer1.ContentPanel.Size = new Size(846, 557);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.LeftToolStripPanelVisible = false;
            toolStripContainer1.Location = new Point(3, 3);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.RightToolStripPanelVisible = false;
            toolStripContainer1.Size = new Size(846, 591);
            toolStripContainer1.TabIndex = 0;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // viewport1
            // 
            viewport1.Dock = DockStyle.Fill;
            viewport1.Location = new Point(0, 0);
            viewport1.Name = "viewport1";
            viewport1.Size = new Size(846, 557);
            viewport1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { redToolStripDropDownButton, greenToolStripDropDownButton, blueToolStripDropDownButton, toolStripSeparator2, toolStripButton1 });
            toolStrip1.Location = new Point(4, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(358, 34);
            toolStrip1.TabIndex = 0;
            // 
            // redToolStripDropDownButton
            // 
            redToolStripDropDownButton.Image = Properties.Resources.red_sqaure;
            redToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            redToolStripDropDownButton.Name = "redToolStripDropDownButton";
            redToolStripDropDownButton.Size = new Size(84, 29);
            redToolStripDropDownButton.Text = "Red";
            // 
            // greenToolStripDropDownButton
            // 
            greenToolStripDropDownButton.Image = Properties.Resources.green_sqaure;
            greenToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            greenToolStripDropDownButton.Name = "greenToolStripDropDownButton";
            greenToolStripDropDownButton.Size = new Size(100, 29);
            greenToolStripDropDownButton.Text = "Green";
            // 
            // blueToolStripDropDownButton
            // 
            blueToolStripDropDownButton.Image = Properties.Resources.blue_sqaure;
            blueToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            blueToolStripDropDownButton.Name = "blueToolStripDropDownButton";
            blueToolStripDropDownButton.Size = new Size(87, 29);
            blueToolStripDropDownButton.Text = "Blue";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 34);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(63, 29);
            toolStripButton1.Text = "Apply";
            toolStripButton1.Click += UpdateImage;
            // 
            // histogramTabPage
            // 
            histogramTabPage.Controls.Add(histogramPlotView);
            histogramTabPage.Location = new Point(4, 4);
            histogramTabPage.Name = "histogramTabPage";
            histogramTabPage.Padding = new Padding(3);
            histogramTabPage.Size = new Size(852, 597);
            histogramTabPage.TabIndex = 1;
            histogramTabPage.Text = "Histogram";
            histogramTabPage.UseVisualStyleBackColor = true;
            // 
            // histogramPlotView
            // 
            histogramPlotView.Dock = DockStyle.Fill;
            histogramPlotView.Location = new Point(3, 3);
            histogramPlotView.Name = "histogramPlotView";
            histogramPlotView.PanCursor = Cursors.Hand;
            histogramPlotView.Size = new Size(846, 591);
            histogramPlotView.TabIndex = 0;
            histogramPlotView.Text = "histogramPlotView";
            histogramPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            histogramPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            histogramPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            histogramPlotView.DoubleClick += PlotView_DoubleClick;
            // 
            // explorationTabPage
            // 
            explorationTabPage.Controls.Add(splitContainer3);
            explorationTabPage.Location = new Point(4, 34);
            explorationTabPage.Name = "explorationTabPage";
            explorationTabPage.Padding = new Padding(3);
            explorationTabPage.Size = new Size(1170, 641);
            explorationTabPage.TabIndex = 1;
            explorationTabPage.Text = "Exploration";
            explorationTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.FixedPanel = FixedPanel.Panel2;
            splitContainer3.Location = new Point(3, 3);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(plotView1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(correlationDataGridView);
            splitContainer3.Size = new Size(1164, 635);
            splitContainer3.SplitterDistance = 435;
            splitContainer3.TabIndex = 2;
            // 
            // plotView1
            // 
            plotView1.Dock = DockStyle.Fill;
            plotView1.Location = new Point(0, 0);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(1164, 435);
            plotView1.TabIndex = 0;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            plotView1.DoubleClick += PlotView_DoubleClick;
            // 
            // correlationDataGridView
            // 
            correlationDataGridView.AllowUserToAddRows = false;
            correlationDataGridView.AllowUserToDeleteRows = false;
            correlationDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            correlationDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            correlationDataGridView.Dock = DockStyle.Fill;
            correlationDataGridView.Location = new Point(0, 0);
            correlationDataGridView.Name = "correlationDataGridView";
            correlationDataGridView.ReadOnly = true;
            correlationDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            correlationDataGridView.Size = new Size(1164, 196);
            correlationDataGridView.TabIndex = 1;
            // 
            // classificationTabPage
            // 
            classificationTabPage.Controls.Add(splitContainer4);
            classificationTabPage.Location = new Point(4, 34);
            classificationTabPage.Name = "classificationTabPage";
            classificationTabPage.Padding = new Padding(3);
            classificationTabPage.Size = new Size(1170, 641);
            classificationTabPage.TabIndex = 2;
            classificationTabPage.Text = "Classification";
            classificationTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(3, 3);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(label3);
            splitContainer4.Panel1.Controls.Add(comboBox3);
            splitContainer4.Panel1.Controls.Add(button1);
            splitContainer4.Panel1.Controls.Add(label2);
            splitContainer4.Panel1.Controls.Add(label1);
            splitContainer4.Panel1.Controls.Add(comboBox2);
            splitContainer4.Panel1.Controls.Add(comboBox1);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(viewport2);
            splitContainer4.Size = new Size(1164, 635);
            splitContainer4.SplitterDistance = 257;
            splitContainer4.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 199);
            label3.Name = "label3";
            label3.Size = new Size(71, 25);
            label3.TabIndex = 6;
            label3.Text = "Param3";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(38, 227);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(182, 33);
            comboBox3.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(38, 306);
            button1.Name = "button1";
            button1.Size = new Size(182, 56);
            button1.TabIndex = 4;
            button1.Text = "Calc";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 120);
            label2.Name = "label2";
            label2.Size = new Size(71, 25);
            label2.TabIndex = 3;
            label2.Text = "Param2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 42);
            label1.Name = "label1";
            label1.Size = new Size(71, 25);
            label1.TabIndex = 2;
            label1.Text = "Param1";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(38, 148);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(182, 33);
            comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(38, 70);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 33);
            comboBox1.TabIndex = 0;
            // 
            // viewport2
            // 
            viewport2.Dock = DockStyle.Fill;
            viewport2.Location = new Point(0, 0);
            viewport2.Name = "viewport2";
            viewport2.Size = new Size(903, 635);
            viewport2.TabIndex = 0;
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 744);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Form1";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            dataTabPage.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            dataTabControl.ResumeLayout(false);
            viewportTabPage.ResumeLayout(false);
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            histogramTabPage.ResumeLayout(false);
            explorationTabPage.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)correlationDataGridView).EndInit();
            classificationTabPage.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel1.PerformLayout();
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private OpenFileDialog openFileDialog1;
        private StatusStrip statusStrip1;
        private ListBox bandListBox;
        private PropertyGrid bandPropertyGrid;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TabControl tabControl1;
        private TabPage dataTabPage;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TabPage explorationTabPage;
        private TabControl dataTabControl;
        private TabPage viewportTabPage;
        private TabPage histogramTabPage;
        private ToolStripStatusLabel mainStatusLabel;
        private ToolStripProgressBar mainProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton redToolStripDropDownButton;
        private ToolStripDropDownButton greenToolStripDropDownButton;
        private ToolStripDropDownButton blueToolStripDropDownButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton1;
        private OxyPlot.WindowsForms.PlotView histogramPlotView;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private DataGridView correlationDataGridView;
        private SplitContainer splitContainer3;
        private TabPage classificationTabPage;
        private Forms.Viewport viewport1;
        private SplitContainer splitContainer4;
        private Forms.Viewport viewport2;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label2;
        private Label label1;
        private Button button1;
        private Label label3;
        private ComboBox comboBox3;
    }
}
