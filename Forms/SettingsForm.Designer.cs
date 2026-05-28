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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
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
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(_cancelButton);
            flowLayoutPanel1.Controls.Add(_okButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // _cancelButton
            // 
            resources.ApplyResources(_cancelButton, "_cancelButton");
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Name = "_cancelButton";
            _cancelButton.UseVisualStyleBackColor = true;
            _cancelButton.Click += _cancelButton_Click;
            // 
            // _okButton
            // 
            resources.ApplyResources(_okButton, "_okButton");
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Name = "_okButton";
            _okButton.UseVisualStyleBackColor = true;
            _okButton.Click += _okButton_Click;
            // 
            // tabControl1
            // 
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(_languageCombo, 1, 2);
            tableLayoutPanel1.Controls.Add(_bandwidthMethodCombo, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(_defaultKernelCombo, 1, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // _languageCombo
            // 
            resources.ApplyResources(_languageCombo, "_languageCombo");
            _languageCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _languageCombo.FormattingEnabled = true;
            _languageCombo.Name = "_languageCombo";
            // 
            // _bandwidthMethodCombo
            // 
            resources.ApplyResources(_bandwidthMethodCombo, "_bandwidthMethodCombo");
            _bandwidthMethodCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _bandwidthMethodCombo.FormattingEnabled = true;
            _bandwidthMethodCombo.Name = "_bandwidthMethodCombo";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // _defaultKernelCombo
            // 
            resources.ApplyResources(_defaultKernelCombo, "_defaultKernelCombo");
            _defaultKernelCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _defaultKernelCombo.FormattingEnabled = true;
            _defaultKernelCombo.Name = "_defaultKernelCombo";
            // 
            // tabPage2
            // 
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Controls.Add(tableLayoutPanel2);
            tabPage2.Name = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
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
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // _binsRuleCombo
            // 
            resources.ApplyResources(_binsRuleCombo, "_binsRuleCombo");
            tableLayoutPanel2.SetColumnSpan(_binsRuleCombo, 2);
            _binsRuleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _binsRuleCombo.FormattingEnabled = true;
            _binsRuleCombo.Name = "_binsRuleCombo";
            _binsRuleCombo.SelectedIndexChanged += _binsRuleCombo_SelectedIndexChanged;
            // 
            // _customBinsNud
            // 
            resources.ApplyResources(_customBinsNud, "_customBinsNud");
            tableLayoutPanel2.SetColumnSpan(_customBinsNud, 2);
            _customBinsNud.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            _customBinsNud.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            _customBinsNud.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            _customBinsNud.Name = "_customBinsNud";
            _customBinsNud.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // _showCumulativeCheck
            // 
            resources.ApplyResources(_showCumulativeCheck, "_showCumulativeCheck");
            tableLayoutPanel2.SetColumnSpan(_showCumulativeCheck, 3);
            _showCumulativeCheck.Name = "_showCumulativeCheck";
            _showCumulativeCheck.UseVisualStyleBackColor = true;
            // 
            // _barColorSwatch
            // 
            resources.ApplyResources(_barColorSwatch, "_barColorSwatch");
            _barColorSwatch.Name = "_barColorSwatch";
            _barColorSwatch.UseVisualStyleBackColor = true;
            _barColorSwatch.Click += _barColorPickBtn_Click;
            // 
            // _lineColorSwatch
            // 
            resources.ApplyResources(_lineColorSwatch, "_lineColorSwatch");
            _lineColorSwatch.Name = "_lineColorSwatch";
            _lineColorSwatch.UseVisualStyleBackColor = true;
            _lineColorSwatch.Click += _lineColorPickBtn_Click;
            // 
            // _barColorTextBox
            // 
            resources.ApplyResources(_barColorTextBox, "_barColorTextBox");
            _barColorTextBox.Name = "_barColorTextBox";
            // 
            // _lineColorTextBox
            // 
            resources.ApplyResources(_lineColorTextBox, "_lineColorTextBox");
            _lineColorTextBox.Name = "_lineColorTextBox";
            // 
            // tabPage3
            // 
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.Controls.Add(tableLayoutPanel3);
            tabPage3.Name = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(tableLayoutPanel3, "tableLayoutPanel3");
            tableLayoutPanel3.Controls.Add(_blueBandNud, 1, 2);
            tableLayoutPanel3.Controls.Add(_greenBandNud, 1, 1);
            tableLayoutPanel3.Controls.Add(label8, 0, 0);
            tableLayoutPanel3.Controls.Add(label9, 0, 1);
            tableLayoutPanel3.Controls.Add(label10, 0, 2);
            tableLayoutPanel3.Controls.Add(label11, 0, 3);
            tableLayoutPanel3.Controls.Add(_redBandNud, 1, 0);
            tableLayoutPanel3.Controls.Add(_interpolationCombo, 1, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // _blueBandNud
            // 
            resources.ApplyResources(_blueBandNud, "_blueBandNud");
            _blueBandNud.Name = "_blueBandNud";
            // 
            // _greenBandNud
            // 
            resources.ApplyResources(_greenBandNud, "_greenBandNud");
            _greenBandNud.Name = "_greenBandNud";
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // _redBandNud
            // 
            resources.ApplyResources(_redBandNud, "_redBandNud");
            _redBandNud.Name = "_redBandNud";
            // 
            // _interpolationCombo
            // 
            resources.ApplyResources(_interpolationCombo, "_interpolationCombo");
            _interpolationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _interpolationCombo.FormattingEnabled = true;
            _interpolationCombo.Name = "_interpolationCombo";
            // 
            // tabPage4
            // 
            resources.ApplyResources(tabPage4, "tabPage4");
            tabPage4.Controls.Add(tableLayoutPanel4);
            tabPage4.Name = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(tableLayoutPanel4, "tableLayoutPanel4");
            tableLayoutPanel4.Controls.Add(_showLegendCheck, 0, 1);
            tableLayoutPanel4.Controls.Add(_showAxisLabelsCheck, 0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // _showLegendCheck
            // 
            resources.ApplyResources(_showLegendCheck, "_showLegendCheck");
            _showLegendCheck.Name = "_showLegendCheck";
            _showLegendCheck.UseVisualStyleBackColor = true;
            // 
            // _showAxisLabelsCheck
            // 
            resources.ApplyResources(_showAxisLabelsCheck, "_showAxisLabelsCheck");
            _showAxisLabelsCheck.Name = "_showAxisLabelsCheck";
            _showAxisLabelsCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AcceptButton = _okButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cancelButton;
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            ShowIcon = false;
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