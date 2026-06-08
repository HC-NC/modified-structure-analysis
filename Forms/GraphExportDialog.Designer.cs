namespace modified_structure_analysis.Forms
{
    partial class GraphExportDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphExportDialog));
            _layout = new TableLayoutPanel();
            _formatLabel = new Label();
            _formatCombo = new ComboBox();
            _widthLabel = new Label();
            _widthUpDown = new NumericUpDown();
            _heightLabel = new Label();
            _heightUpDown = new NumericUpDown();
            _dpiLabel = new Label();
            _dpiUpDown = new NumericUpDown();
            _qualityLabel = new Label();
            _qualityUpDown = new NumericUpDown();
            _titleLabel = new Label();
            _titleTextBox = new TextBox();
            _grayscaleCheckBox = new CheckBox();
            _saveLabel = new Label();
            _pathTextBox = new TextBox();
            _browseButton = new Button();
            _buttonPanel = new FlowLayoutPanel();
            _cancelButton = new Button();
            _okButton = new Button();
            _layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_widthUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_heightUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_dpiUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_qualityUpDown).BeginInit();
            _buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _layout
            // 
            resources.ApplyResources(_layout, "_layout");
            _layout.Controls.Add(_formatLabel, 0, 0);
            _layout.Controls.Add(_formatCombo, 1, 0);
            _layout.Controls.Add(_widthLabel, 0, 1);
            _layout.Controls.Add(_widthUpDown, 1, 1);
            _layout.Controls.Add(_heightLabel, 0, 2);
            _layout.Controls.Add(_heightUpDown, 1, 2);
            _layout.Controls.Add(_dpiLabel, 0, 3);
            _layout.Controls.Add(_dpiUpDown, 1, 3);
            _layout.Controls.Add(_qualityLabel, 0, 4);
            _layout.Controls.Add(_qualityUpDown, 1, 4);
            _layout.Controls.Add(_titleLabel, 0, 5);
            _layout.Controls.Add(_titleTextBox, 1, 5);
            _layout.Controls.Add(_grayscaleCheckBox, 0, 6);
            _layout.Controls.Add(_saveLabel, 0, 7);
            _layout.Controls.Add(_pathTextBox, 1, 7);
            _layout.Controls.Add(_browseButton, 2, 7);
            _layout.Controls.Add(_buttonPanel, 0, 8);
            _layout.Name = "_layout";
            // 
            // _formatLabel
            // 
            resources.ApplyResources(_formatLabel, "_formatLabel");
            _formatLabel.Name = "_formatLabel";
            // 
            // _formatCombo
            // 
            resources.ApplyResources(_formatCombo, "_formatCombo");
            _layout.SetColumnSpan(_formatCombo, 2);
            _formatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _formatCombo.Name = "_formatCombo";
            _formatCombo.SelectedIndexChanged += UpdateQualityVisibility;
            // 
            // _widthLabel
            // 
            resources.ApplyResources(_widthLabel, "_widthLabel");
            _widthLabel.Name = "_widthLabel";
            // 
            // _widthUpDown
            // 
            resources.ApplyResources(_widthUpDown, "_widthUpDown");
            _layout.SetColumnSpan(_widthUpDown, 2);
            _widthUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            _widthUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            _widthUpDown.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            _widthUpDown.Name = "_widthUpDown";
            _widthUpDown.Value = new decimal(new int[] { 800, 0, 0, 0 });
            // 
            // _heightLabel
            // 
            resources.ApplyResources(_heightLabel, "_heightLabel");
            _heightLabel.Name = "_heightLabel";
            // 
            // _heightUpDown
            // 
            resources.ApplyResources(_heightUpDown, "_heightUpDown");
            _layout.SetColumnSpan(_heightUpDown, 2);
            _heightUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            _heightUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            _heightUpDown.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            _heightUpDown.Name = "_heightUpDown";
            _heightUpDown.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // _dpiLabel
            // 
            resources.ApplyResources(_dpiLabel, "_dpiLabel");
            _dpiLabel.Name = "_dpiLabel";
            // 
            // _dpiUpDown
            // 
            resources.ApplyResources(_dpiUpDown, "_dpiUpDown");
            _layout.SetColumnSpan(_dpiUpDown, 2);
            _dpiUpDown.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            _dpiUpDown.Maximum = new decimal(new int[] { 1200, 0, 0, 0 });
            _dpiUpDown.Minimum = new decimal(new int[] { 72, 0, 0, 0 });
            _dpiUpDown.Name = "_dpiUpDown";
            _dpiUpDown.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // 
            // _qualityLabel
            // 
            resources.ApplyResources(_qualityLabel, "_qualityLabel");
            _qualityLabel.Name = "_qualityLabel";
            // 
            // _qualityUpDown
            // 
            resources.ApplyResources(_qualityUpDown, "_qualityUpDown");
            _layout.SetColumnSpan(_qualityUpDown, 2);
            _qualityUpDown.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            _qualityUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            _qualityUpDown.Name = "_qualityUpDown";
            _qualityUpDown.Value = new decimal(new int[] { 90, 0, 0, 0 });
            // 
            // _titleLabel
            // 
            resources.ApplyResources(_titleLabel, "_titleLabel");
            _titleLabel.Name = "_titleLabel";
            // 
            // _titleTextBox
            // 
            resources.ApplyResources(_titleTextBox, "_titleTextBox");
            _layout.SetColumnSpan(_titleTextBox, 2);
            _titleTextBox.Name = "_titleTextBox";
            // 
            // _grayscaleCheckBox
            // 
            resources.ApplyResources(_grayscaleCheckBox, "_grayscaleCheckBox");
            _layout.SetColumnSpan(_grayscaleCheckBox, 3);
            _grayscaleCheckBox.Name = "_grayscaleCheckBox";
            // 
            // _saveLabel
            // 
            resources.ApplyResources(_saveLabel, "_saveLabel");
            _saveLabel.Name = "_saveLabel";
            // 
            // _pathTextBox
            // 
            resources.ApplyResources(_pathTextBox, "_pathTextBox");
            _pathTextBox.Name = "_pathTextBox";
            // 
            // _browseButton
            // 
            resources.ApplyResources(_browseButton, "_browseButton");
            _browseButton.Name = "_browseButton";
            _browseButton.Click += BrowseClick;
            // 
            // _buttonPanel
            // 
            resources.ApplyResources(_buttonPanel, "_buttonPanel");
            _layout.SetColumnSpan(_buttonPanel, 3);
            _buttonPanel.Controls.Add(_cancelButton);
            _buttonPanel.Controls.Add(_okButton);
            _buttonPanel.Name = "_buttonPanel";
            // 
            // _cancelButton
            // 
            resources.ApplyResources(_cancelButton, "_cancelButton");
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Name = "_cancelButton";
            // 
            // _okButton
            // 
            resources.ApplyResources(_okButton, "_okButton");
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Name = "_okButton";
            _okButton.Click += OkClick;
            // 
            // GraphExportDialog
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_layout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GraphExportDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            Load += UpdateQualityVisibility;
            _layout.ResumeLayout(false);
            _layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_widthUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)_heightUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)_dpiUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)_qualityUpDown).EndInit();
            _buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox _formatCombo;
        private NumericUpDown _widthUpDown;
        private NumericUpDown _heightUpDown;
        private NumericUpDown _dpiUpDown;
        private NumericUpDown _qualityUpDown;
        private Label _formatLabel;
        private Label _widthLabel;
        private Label _heightLabel;
        private Label _dpiLabel;
        private Label _qualityLabel;
        private Label _titleLabel;
        private Label _saveLabel;
        private TextBox _titleTextBox;
        private CheckBox _grayscaleCheckBox;
        private TextBox _pathTextBox;
        private Button _browseButton;
        private TableLayoutPanel _layout;
        private FlowLayoutPanel _buttonPanel;
        private Button _okButton;
        private Button _cancelButton;
    }
}