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
            _layout.AutoSize = true;
            _layout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _layout.ColumnCount = 3;
            _layout.ColumnStyles.Add(new ColumnStyle());
            _layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _layout.ColumnStyles.Add(new ColumnStyle());
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
            _layout.Controls.Add(_grayscaleCheckBox, 1, 6);
            _layout.Controls.Add(_saveLabel, 0, 7);
            _layout.Controls.Add(_pathTextBox, 1, 7);
            _layout.Controls.Add(_browseButton, 2, 7);
            _layout.Controls.Add(_buttonPanel, 0, 8);
            _layout.Dock = DockStyle.Fill;
            _layout.Location = new Point(0, 0);
            _layout.Name = "_layout";
            _layout.Padding = new Padding(4);
            _layout.RowCount = 9;
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.RowStyles.Add(new RowStyle());
            _layout.Size = new Size(438, 356);
            _layout.TabIndex = 0;
            // 
            // _formatLabel
            // 
            _formatLabel.AutoSize = true;
            _formatLabel.Dock = DockStyle.Fill;
            _formatLabel.Location = new Point(7, 4);
            _formatLabel.Name = "_formatLabel";
            _formatLabel.Size = new Size(75, 39);
            _formatLabel.TabIndex = 0;
            _formatLabel.Text = "Format:";
            _formatLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _formatCombo
            // 
            _layout.SetColumnSpan(_formatCombo, 2);
            _formatCombo.Dock = DockStyle.Fill;
            _formatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _formatCombo.Location = new Point(88, 7);
            _formatCombo.Name = "_formatCombo";
            _formatCombo.Size = new Size(343, 33);
            _formatCombo.TabIndex = 1;
            _formatCombo.SelectedIndexChanged += UpdateQualityVisibility;
            // 
            // _widthLabel
            // 
            _widthLabel.AutoSize = true;
            _widthLabel.Dock = DockStyle.Fill;
            _widthLabel.Location = new Point(7, 43);
            _widthLabel.Name = "_widthLabel";
            _widthLabel.Size = new Size(75, 37);
            _widthLabel.TabIndex = 2;
            _widthLabel.Text = "Width:";
            _widthLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _widthUpDown
            // 
            _layout.SetColumnSpan(_widthUpDown, 2);
            _widthUpDown.Dock = DockStyle.Fill;
            _widthUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            _widthUpDown.Location = new Point(88, 46);
            _widthUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            _widthUpDown.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            _widthUpDown.Name = "_widthUpDown";
            _widthUpDown.Size = new Size(343, 31);
            _widthUpDown.TabIndex = 3;
            _widthUpDown.Value = new decimal(new int[] { 800, 0, 0, 0 });
            // 
            // _heightLabel
            // 
            _heightLabel.AutoSize = true;
            _heightLabel.Dock = DockStyle.Fill;
            _heightLabel.Location = new Point(7, 80);
            _heightLabel.Name = "_heightLabel";
            _heightLabel.Size = new Size(75, 37);
            _heightLabel.TabIndex = 4;
            _heightLabel.Text = "Height:";
            _heightLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _heightUpDown
            // 
            _layout.SetColumnSpan(_heightUpDown, 2);
            _heightUpDown.Dock = DockStyle.Fill;
            _heightUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            _heightUpDown.Location = new Point(88, 83);
            _heightUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            _heightUpDown.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            _heightUpDown.Name = "_heightUpDown";
            _heightUpDown.Size = new Size(343, 31);
            _heightUpDown.TabIndex = 5;
            _heightUpDown.Value = new decimal(new int[] { 600, 0, 0, 0 });
            // 
            // _dpiLabel
            // 
            _dpiLabel.AutoSize = true;
            _dpiLabel.Dock = DockStyle.Fill;
            _dpiLabel.Location = new Point(7, 117);
            _dpiLabel.Name = "_dpiLabel";
            _dpiLabel.Size = new Size(75, 37);
            _dpiLabel.TabIndex = 6;
            _dpiLabel.Text = "DPI:";
            _dpiLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _dpiUpDown
            // 
            _layout.SetColumnSpan(_dpiUpDown, 2);
            _dpiUpDown.Dock = DockStyle.Fill;
            _dpiUpDown.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            _dpiUpDown.Location = new Point(88, 120);
            _dpiUpDown.Maximum = new decimal(new int[] { 1200, 0, 0, 0 });
            _dpiUpDown.Minimum = new decimal(new int[] { 72, 0, 0, 0 });
            _dpiUpDown.Name = "_dpiUpDown";
            _dpiUpDown.Size = new Size(343, 31);
            _dpiUpDown.TabIndex = 7;
            _dpiUpDown.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // 
            // _qualityLabel
            // 
            _qualityLabel.AutoSize = true;
            _qualityLabel.Dock = DockStyle.Fill;
            _qualityLabel.Location = new Point(7, 154);
            _qualityLabel.Name = "_qualityLabel";
            _qualityLabel.Size = new Size(75, 37);
            _qualityLabel.TabIndex = 8;
            _qualityLabel.Text = "Quality:";
            _qualityLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _qualityUpDown
            // 
            _layout.SetColumnSpan(_qualityUpDown, 2);
            _qualityUpDown.Dock = DockStyle.Fill;
            _qualityUpDown.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            _qualityUpDown.Location = new Point(88, 157);
            _qualityUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            _qualityUpDown.Name = "_qualityUpDown";
            _qualityUpDown.Size = new Size(343, 31);
            _qualityUpDown.TabIndex = 9;
            _qualityUpDown.Value = new decimal(new int[] { 90, 0, 0, 0 });
            // 
            // _titleLabel
            // 
            _titleLabel.AutoSize = true;
            _titleLabel.Dock = DockStyle.Fill;
            _titleLabel.Location = new Point(7, 191);
            _titleLabel.Name = "_titleLabel";
            _titleLabel.Size = new Size(75, 37);
            _titleLabel.TabIndex = 10;
            _titleLabel.Text = "Title:";
            _titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _titleTextBox
            // 
            _layout.SetColumnSpan(_titleTextBox, 2);
            _titleTextBox.Dock = DockStyle.Fill;
            _titleTextBox.Location = new Point(88, 194);
            _titleTextBox.Name = "_titleTextBox";
            _titleTextBox.Size = new Size(343, 31);
            _titleTextBox.TabIndex = 11;
            // 
            // _grayscaleCheckBox
            // 
            _layout.SetColumnSpan(_grayscaleCheckBox, 2);
            _grayscaleCheckBox.Dock = DockStyle.Fill;
            _grayscaleCheckBox.Location = new Point(88, 231);
            _grayscaleCheckBox.Name = "_grayscaleCheckBox";
            _grayscaleCheckBox.Size = new Size(343, 30);
            _grayscaleCheckBox.TabIndex = 12;
            _grayscaleCheckBox.Text = "Convert to grayscale";
            // 
            // _saveLabel
            // 
            _saveLabel.AutoSize = true;
            _saveLabel.Dock = DockStyle.Fill;
            _saveLabel.Location = new Point(7, 264);
            _saveLabel.Name = "_saveLabel";
            _saveLabel.Size = new Size(75, 37);
            _saveLabel.TabIndex = 13;
            _saveLabel.Text = "Save to:";
            _saveLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _pathTextBox
            // 
            _pathTextBox.Dock = DockStyle.Fill;
            _pathTextBox.Location = new Point(88, 267);
            _pathTextBox.Name = "_pathTextBox";
            _pathTextBox.Size = new Size(237, 31);
            _pathTextBox.TabIndex = 14;
            // 
            // _browseButton
            // 
            _browseButton.Dock = DockStyle.Fill;
            _browseButton.Location = new Point(331, 267);
            _browseButton.Name = "_browseButton";
            _browseButton.Size = new Size(100, 31);
            _browseButton.TabIndex = 15;
            _browseButton.Text = "Browse...";
            _browseButton.Click += BrowseClick;
            // 
            // _buttonPanel
            // 
            _buttonPanel.AutoSize = true;
            _layout.SetColumnSpan(_buttonPanel, 3);
            _buttonPanel.Controls.Add(_cancelButton);
            _buttonPanel.Controls.Add(_okButton);
            _buttonPanel.Dock = DockStyle.Bottom;
            _buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            _buttonPanel.Location = new Point(7, 304);
            _buttonPanel.Name = "_buttonPanel";
            _buttonPanel.Size = new Size(424, 46);
            _buttonPanel.TabIndex = 16;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(321, 3);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(100, 40);
            _cancelButton.TabIndex = 0;
            _cancelButton.Text = "Cancel";
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(215, 3);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(100, 40);
            _okButton.TabIndex = 1;
            _okButton.Text = "OK";
            _okButton.Click += OkClick;
            // 
            // GraphExportDialog
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(438, 356);
            Controls.Add(_layout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(460, 380);
            Name = "GraphExportDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export Graph";
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