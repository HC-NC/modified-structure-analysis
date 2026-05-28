namespace modified_structure_analysis.Forms
{
    partial class SettingsForm
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            _cancelButton = new Button();
            _okButton = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            _languageCombo = new ComboBox();
            _bandwidthMethodCombo = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            _defaultKernelCombo = new ComboBox();
            tabPage2 = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            _binsRuleCombo = new ComboBox();
            _customBinsNud = new NumericUpDown();
            _showCumulativeCheck = new CheckBox();
            _barColorSwatch = new Button();
            _lineColorSwatch = new Button();
            _barColorTextBox = new TextBox();
            _lineColorTextBox = new TextBox();
            tabPage3 = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            _blueBandNud = new NumericUpDown();
            _greenBandNud = new NumericUpDown();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            _redBandNud = new NumericUpDown();
            _interpolationCombo = new ComboBox();
            tabPage4 = new TabPage();
            tableLayoutPanel4 = new TableLayoutPanel();
            _showLegendCheck = new CheckBox();
            _showAxisLabelsCheck = new CheckBox();
            flowLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_customBinsNud).BeginInit();
            tabPage3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_blueBandNud).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_greenBandNud).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_redBandNud).BeginInit();
            tabPage4.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(_cancelButton);
            flowLayoutPanel1.Controls.Add(_okButton);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(0, 310);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(4);
            flowLayoutPanel1.Size = new Size(518, 54);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(387, 7);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(120, 40);
            _cancelButton.TabIndex = 2;
            _cancelButton.Text = "Cancel";
            _cancelButton.UseVisualStyleBackColor = true;
            _cancelButton.Click += _cancelButton_Click;
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(261, 7);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(120, 40);
            _okButton.TabIndex = 1;
            _okButton.Text = "OK";
            _okButton.UseVisualStyleBackColor = true;
            _okButton.Click += _okButton_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(518, 310);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(510, 272);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(_languageCombo, 1, 2);
            tableLayoutPanel1.Controls.Add(_bandwidthMethodCombo, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(_defaultKernelCombo, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(504, 266);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // _languageCombo
            // 
            _languageCombo.Dock = DockStyle.Fill;
            _languageCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _languageCombo.FormattingEnabled = true;
            _languageCombo.Location = new Point(204, 81);
            _languageCombo.Name = "_languageCombo";
            _languageCombo.Size = new Size(297, 33);
            _languageCombo.TabIndex = 5;
            // 
            // _bandwidthMethodCombo
            // 
            _bandwidthMethodCombo.Dock = DockStyle.Fill;
            _bandwidthMethodCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _bandwidthMethodCombo.FormattingEnabled = true;
            _bandwidthMethodCombo.Location = new Point(204, 42);
            _bandwidthMethodCombo.Name = "_bandwidthMethodCombo";
            _bandwidthMethodCombo.Size = new Size(297, 33);
            _bandwidthMethodCombo.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(195, 39);
            label1.TabIndex = 0;
            label1.Text = "Default kernel function:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 39);
            label2.Name = "label2";
            label2.Size = new Size(195, 39);
            label2.TabIndex = 1;
            label2.Text = "Bandwidth method:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 78);
            label3.Name = "label3";
            label3.Size = new Size(195, 39);
            label3.TabIndex = 2;
            label3.Text = "Language:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _defaultKernelCombo
            // 
            _defaultKernelCombo.Dock = DockStyle.Fill;
            _defaultKernelCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _defaultKernelCombo.FormattingEnabled = true;
            _defaultKernelCombo.Location = new Point(204, 3);
            _defaultKernelCombo.Name = "_defaultKernelCombo";
            _defaultKernelCombo.Size = new Size(297, 33);
            _defaultKernelCombo.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tableLayoutPanel2);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(510, 272);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Histogram";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(label4, 0, 0);
            tableLayoutPanel2.Controls.Add(label5, 0, 1);
            tableLayoutPanel2.Controls.Add(label6, 0, 3);
            tableLayoutPanel2.Controls.Add(label7, 0, 4);
            tableLayoutPanel2.Controls.Add(_binsRuleCombo, 1, 0);
            tableLayoutPanel2.Controls.Add(_customBinsNud, 1, 1);
            tableLayoutPanel2.Controls.Add(_showCumulativeCheck, 0, 2);
            tableLayoutPanel2.Controls.Add(_barColorSwatch, 1, 3);
            tableLayoutPanel2.Controls.Add(_lineColorSwatch, 1, 4);
            tableLayoutPanel2.Controls.Add(_barColorTextBox, 2, 3);
            tableLayoutPanel2.Controls.Add(_lineColorTextBox, 2, 4);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(504, 266);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(181, 39);
            label4.TabIndex = 0;
            label4.Text = "Bins calculation rule:";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 39);
            label5.Name = "label5";
            label5.Size = new Size(181, 37);
            label5.TabIndex = 1;
            label5.Text = "Custom bin count:";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 111);
            label6.Name = "label6";
            label6.Size = new Size(181, 37);
            label6.TabIndex = 2;
            label6.Text = "Bar color:";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(3, 148);
            label7.Name = "label7";
            label7.Size = new Size(181, 37);
            label7.TabIndex = 3;
            label7.Text = "Cumulative line color:";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _binsRuleCombo
            // 
            tableLayoutPanel2.SetColumnSpan(_binsRuleCombo, 2);
            _binsRuleCombo.Dock = DockStyle.Fill;
            _binsRuleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _binsRuleCombo.FormattingEnabled = true;
            _binsRuleCombo.Location = new Point(190, 3);
            _binsRuleCombo.Name = "_binsRuleCombo";
            _binsRuleCombo.Size = new Size(311, 33);
            _binsRuleCombo.TabIndex = 4;
            _binsRuleCombo.SelectedIndexChanged += _binsRuleCombo_SelectedIndexChanged;
            // 
            // _customBinsNud
            // 
            tableLayoutPanel2.SetColumnSpan(_customBinsNud, 2);
            _customBinsNud.Dock = DockStyle.Fill;
            _customBinsNud.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            _customBinsNud.Location = new Point(190, 42);
            _customBinsNud.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            _customBinsNud.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            _customBinsNud.Name = "_customBinsNud";
            _customBinsNud.Size = new Size(311, 31);
            _customBinsNud.TabIndex = 5;
            _customBinsNud.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // _showCumulativeCheck
            // 
            _showCumulativeCheck.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(_showCumulativeCheck, 3);
            _showCumulativeCheck.Dock = DockStyle.Fill;
            _showCumulativeCheck.Location = new Point(3, 79);
            _showCumulativeCheck.Name = "_showCumulativeCheck";
            _showCumulativeCheck.Size = new Size(498, 29);
            _showCumulativeCheck.TabIndex = 6;
            _showCumulativeCheck.Text = "Show cumulative line";
            _showCumulativeCheck.UseVisualStyleBackColor = true;
            // 
            // _barColorSwatch
            // 
            _barColorSwatch.FlatStyle = FlatStyle.Flat;
            _barColorSwatch.Location = new Point(190, 114);
            _barColorSwatch.Name = "_barColorSwatch";
            _barColorSwatch.Size = new Size(100, 31);
            _barColorSwatch.TabIndex = 7;
            _barColorSwatch.UseVisualStyleBackColor = true;
            _barColorSwatch.Click += _barColorPickBtn_Click;
            // 
            // _lineColorSwatch
            // 
            _lineColorSwatch.FlatStyle = FlatStyle.Flat;
            _lineColorSwatch.Location = new Point(190, 151);
            _lineColorSwatch.Name = "_lineColorSwatch";
            _lineColorSwatch.Size = new Size(100, 31);
            _lineColorSwatch.TabIndex = 8;
            _lineColorSwatch.UseVisualStyleBackColor = true;
            _lineColorSwatch.Click += _lineColorPickBtn_Click;
            // 
            // _barColorTextBox
            // 
            _barColorTextBox.Dock = DockStyle.Fill;
            _barColorTextBox.Location = new Point(296, 114);
            _barColorTextBox.Name = "_barColorTextBox";
            _barColorTextBox.Size = new Size(205, 31);
            _barColorTextBox.TabIndex = 9;
            // 
            // _lineColorTextBox
            // 
            _lineColorTextBox.Dock = DockStyle.Fill;
            _lineColorTextBox.Location = new Point(296, 151);
            _lineColorTextBox.Name = "_lineColorTextBox";
            _lineColorTextBox.Size = new Size(205, 31);
            _lineColorTextBox.TabIndex = 10;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tableLayoutPanel3);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(510, 272);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Viewport";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(_blueBandNud, 1, 2);
            tableLayoutPanel3.Controls.Add(_greenBandNud, 1, 1);
            tableLayoutPanel3.Controls.Add(label8, 0, 0);
            tableLayoutPanel3.Controls.Add(label9, 0, 1);
            tableLayoutPanel3.Controls.Add(label10, 0, 2);
            tableLayoutPanel3.Controls.Add(label11, 0, 3);
            tableLayoutPanel3.Controls.Add(_redBandNud, 1, 0);
            tableLayoutPanel3.Controls.Add(_interpolationCombo, 1, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(504, 266);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // _blueBandNud
            // 
            _blueBandNud.Dock = DockStyle.Fill;
            _blueBandNud.Location = new Point(179, 77);
            _blueBandNud.Name = "_blueBandNud";
            _blueBandNud.Size = new Size(322, 31);
            _blueBandNud.TabIndex = 6;
            // 
            // _greenBandNud
            // 
            _greenBandNud.Dock = DockStyle.Fill;
            _greenBandNud.Location = new Point(179, 40);
            _greenBandNud.Name = "_greenBandNud";
            _greenBandNud.Size = new Size(322, 31);
            _greenBandNud.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(3, 0);
            label8.Name = "label8";
            label8.Size = new Size(170, 37);
            label8.TabIndex = 0;
            label8.Text = "Default Red band:";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Fill;
            label9.Location = new Point(3, 37);
            label9.Name = "label9";
            label9.Size = new Size(170, 37);
            label9.TabIndex = 1;
            label9.Text = "Default Green band:";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Fill;
            label10.Location = new Point(3, 74);
            label10.Name = "label10";
            label10.Size = new Size(170, 37);
            label10.TabIndex = 2;
            label10.Text = "Default Blue band:";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Dock = DockStyle.Fill;
            label11.Location = new Point(3, 111);
            label11.Name = "label11";
            label11.Size = new Size(170, 39);
            label11.TabIndex = 3;
            label11.Text = "Interpolation:";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _redBandNud
            // 
            _redBandNud.Dock = DockStyle.Fill;
            _redBandNud.Location = new Point(179, 3);
            _redBandNud.Name = "_redBandNud";
            _redBandNud.Size = new Size(322, 31);
            _redBandNud.TabIndex = 4;
            // 
            // _interpolationCombo
            // 
            _interpolationCombo.Dock = DockStyle.Fill;
            _interpolationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _interpolationCombo.FormattingEnabled = true;
            _interpolationCombo.Location = new Point(179, 114);
            _interpolationCombo.Name = "_interpolationCombo";
            _interpolationCombo.Size = new Size(322, 33);
            _interpolationCombo.TabIndex = 7;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(tableLayoutPanel4);
            tabPage4.Location = new Point(4, 34);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(510, 272);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Graph";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(_showLegendCheck, 0, 1);
            tableLayoutPanel4.Controls.Add(_showAxisLabelsCheck, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(504, 266);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // _showLegendCheck
            // 
            _showLegendCheck.AutoSize = true;
            _showLegendCheck.Dock = DockStyle.Fill;
            _showLegendCheck.Location = new Point(3, 38);
            _showLegendCheck.Name = "_showLegendCheck";
            _showLegendCheck.Size = new Size(498, 29);
            _showLegendCheck.TabIndex = 1;
            _showLegendCheck.Text = "Show legend";
            _showLegendCheck.UseVisualStyleBackColor = true;
            // 
            // _showAxisLabelsCheck
            // 
            _showAxisLabelsCheck.AutoSize = true;
            _showAxisLabelsCheck.Dock = DockStyle.Fill;
            _showAxisLabelsCheck.Location = new Point(3, 3);
            _showAxisLabelsCheck.Name = "_showAxisLabelsCheck";
            _showAxisLabelsCheck.Size = new Size(498, 29);
            _showAxisLabelsCheck.TabIndex = 0;
            _showAxisLabelsCheck.Text = "Show axis labels";
            _showAxisLabelsCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AcceptButton = _okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cancelButton;
            ClientSize = new Size(518, 364);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            Load += SettingsForm_Load;
            flowLayoutPanel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_customBinsNud).EndInit();
            tabPage3.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_blueBandNud).EndInit();
            ((System.ComponentModel.ISupportInitialize)_greenBandNud).EndInit();
            ((System.ComponentModel.ISupportInitialize)_redBandNud).EndInit();
            tabPage4.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button _cancelButton;
        private Button _okButton;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox _languageCombo;
        private ComboBox _bandwidthMethodCombo;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox _defaultKernelCombo;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox _binsRuleCombo;
        private NumericUpDown _customBinsNud;
        private CheckBox _showCumulativeCheck;
        private Button _barColorSwatch;
        private Button _lineColorSwatch;
        private TextBox _barColorTextBox;
        private TextBox _lineColorTextBox;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private NumericUpDown _blueBandNud;
        private NumericUpDown _greenBandNud;
        private NumericUpDown _redBandNud;
        private ComboBox _interpolationCombo;
        private TableLayoutPanel tableLayoutPanel4;
        private CheckBox _showLegendCheck;
        private CheckBox _showAxisLabelsCheck;
    }
}