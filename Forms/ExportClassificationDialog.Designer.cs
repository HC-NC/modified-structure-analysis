namespace modified_structure_analysis.Forms
{
    partial class ExportClassificationDialog
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
            _formatCombo = new ComboBox();
            _geoCheckBox = new CheckBox();
            _projectionTextBox = new TextBox();
            _pathTextBox = new TextBox();
            _paletteCombo = new ComboBox();
            _exportStatsCheckBox = new CheckBox();
            _statsFormatCombo = new ComboBox();
            _mainLayout = new TableLayoutPanel();
            _formatLabel = new Label();
            _projectionLabel = new Label();
            _paletteLabel = new Label();
            _saveLabel = new Label();
            _browseButton = new Button();
            _statsGroup = new GroupBox();
            _statsLayout = new TableLayoutPanel();
            _statsFormatLabel = new Label();
            _buttonPanel = new FlowLayoutPanel();
            _cancelButton = new Button();
            _okButton = new Button();
            _mainLayout.SuspendLayout();
            _statsGroup.SuspendLayout();
            _statsLayout.SuspendLayout();
            _buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _formatCombo
            // 
            _mainLayout.SetColumnSpan(_formatCombo, 2);
            _formatCombo.Dock = DockStyle.Fill;
            _formatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _formatCombo.Location = new Point(108, 7);
            _formatCombo.Name = "_formatCombo";
            _formatCombo.Size = new Size(343, 33);
            _formatCombo.TabIndex = 1;
            // 
            // _geoCheckBox
            // 
            _geoCheckBox.Checked = true;
            _geoCheckBox.CheckState = CheckState.Checked;
            _mainLayout.SetColumnSpan(_geoCheckBox, 2);
            _geoCheckBox.Dock = DockStyle.Fill;
            _geoCheckBox.Location = new Point(108, 46);
            _geoCheckBox.Name = "_geoCheckBox";
            _geoCheckBox.Size = new Size(343, 31);
            _geoCheckBox.TabIndex = 2;
            _geoCheckBox.Text = "Include georeferencing";
            // 
            // _projectionTextBox
            // 
            _mainLayout.SetColumnSpan(_projectionTextBox, 2);
            _projectionTextBox.Dock = DockStyle.Fill;
            _projectionTextBox.Location = new Point(108, 83);
            _projectionTextBox.Name = "_projectionTextBox";
            _projectionTextBox.ReadOnly = true;
            _projectionTextBox.Size = new Size(343, 31);
            _projectionTextBox.TabIndex = 4;
            // 
            // _pathTextBox
            // 
            _pathTextBox.Dock = DockStyle.Fill;
            _pathTextBox.Location = new Point(108, 159);
            _pathTextBox.Name = "_pathTextBox";
            _pathTextBox.Size = new Size(237, 31);
            _pathTextBox.TabIndex = 8;
            // 
            // _paletteCombo
            // 
            _mainLayout.SetColumnSpan(_paletteCombo, 2);
            _paletteCombo.Dock = DockStyle.Fill;
            _paletteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _paletteCombo.Location = new Point(108, 120);
            _paletteCombo.Name = "_paletteCombo";
            _paletteCombo.Size = new Size(343, 33);
            _paletteCombo.TabIndex = 6;
            // 
            // _exportStatsCheckBox
            // 
            _statsLayout.SetColumnSpan(_exportStatsCheckBox, 2);
            _exportStatsCheckBox.Dock = DockStyle.Fill;
            _exportStatsCheckBox.Location = new Point(3, 3);
            _exportStatsCheckBox.Name = "_exportStatsCheckBox";
            _exportStatsCheckBox.Size = new Size(432, 31);
            _exportStatsCheckBox.TabIndex = 0;
            _exportStatsCheckBox.Text = "Export alongside classification";
            // 
            // _statsFormatCombo
            // 
            _statsFormatCombo.Dock = DockStyle.Fill;
            _statsFormatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _statsFormatCombo.Location = new Point(82, 40);
            _statsFormatCombo.Name = "_statsFormatCombo";
            _statsFormatCombo.Size = new Size(353, 33);
            _statsFormatCombo.TabIndex = 2;
            // 
            // _mainLayout
            // 
            _mainLayout.AutoSize = true;
            _mainLayout.ColumnStyles.Add(new ColumnStyle());
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _mainLayout.ColumnStyles.Add(new ColumnStyle());
            _mainLayout.Controls.Add(_formatLabel, 0, 0);
            _mainLayout.Controls.Add(_formatCombo, 1, 0);
            _mainLayout.Controls.Add(_geoCheckBox, 1, 1);
            _mainLayout.Controls.Add(_projectionLabel, 0, 2);
            _mainLayout.Controls.Add(_projectionTextBox, 1, 2);
            _mainLayout.Controls.Add(_paletteLabel, 0, 3);
            _mainLayout.Controls.Add(_paletteCombo, 1, 3);
            _mainLayout.Controls.Add(_saveLabel, 0, 4);
            _mainLayout.Controls.Add(_pathTextBox, 1, 4);
            _mainLayout.Controls.Add(_browseButton, 2, 4);
            _mainLayout.Controls.Add(_statsGroup, 0, 5);
            _mainLayout.Controls.Add(_buttonPanel, 0, 6);
            _mainLayout.Dock = DockStyle.Fill;
            _mainLayout.Location = new Point(0, 0);
            _mainLayout.Name = "_mainLayout";
            _mainLayout.Padding = new Padding(4);
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.RowStyles.Add(new RowStyle());
            _mainLayout.Size = new Size(458, 360);
            _mainLayout.TabIndex = 0;
            // 
            // _formatLabel
            // 
            _formatLabel.AutoSize = true;
            _formatLabel.Dock = DockStyle.Fill;
            _formatLabel.Location = new Point(7, 4);
            _formatLabel.Name = "_formatLabel";
            _formatLabel.Size = new Size(95, 39);
            _formatLabel.TabIndex = 0;
            _formatLabel.Text = "Format:";
            _formatLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _projectionLabel
            // 
            _projectionLabel.AutoSize = true;
            _projectionLabel.Dock = DockStyle.Fill;
            _projectionLabel.Location = new Point(7, 80);
            _projectionLabel.Name = "_projectionLabel";
            _projectionLabel.Size = new Size(95, 37);
            _projectionLabel.TabIndex = 3;
            _projectionLabel.Text = "Projection:";
            _projectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _paletteLabel
            // 
            _paletteLabel.AutoSize = true;
            _paletteLabel.Dock = DockStyle.Fill;
            _paletteLabel.Location = new Point(7, 117);
            _paletteLabel.Name = "_paletteLabel";
            _paletteLabel.Size = new Size(95, 39);
            _paletteLabel.TabIndex = 5;
            _paletteLabel.Text = "Palette:";
            _paletteLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _saveLabel
            // 
            _saveLabel.AutoSize = true;
            _saveLabel.Dock = DockStyle.Fill;
            _saveLabel.Location = new Point(7, 156);
            _saveLabel.Name = "_saveLabel";
            _saveLabel.Size = new Size(95, 37);
            _saveLabel.TabIndex = 7;
            _saveLabel.Text = "Save to:";
            _saveLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _browseButton
            // 
            _browseButton.Dock = DockStyle.Fill;
            _browseButton.Location = new Point(351, 159);
            _browseButton.Name = "_browseButton";
            _browseButton.Size = new Size(100, 31);
            _browseButton.TabIndex = 9;
            _browseButton.Text = "Browse...";
            _browseButton.Click += BrowseClick;
            // 
            // _statsGroup
            // 
            _statsGroup.AutoSize = true;
            _mainLayout.SetColumnSpan(_statsGroup, 3);
            _statsGroup.Controls.Add(_statsLayout);
            _statsGroup.Dock = DockStyle.Fill;
            _statsGroup.Location = new Point(7, 196);
            _statsGroup.Name = "_statsGroup";
            _statsGroup.Size = new Size(444, 106);
            _statsGroup.TabIndex = 10;
            _statsGroup.TabStop = false;
            _statsGroup.Text = "Statistics";
            // 
            // _statsLayout
            // 
            _statsLayout.AutoSize = true;
            _statsLayout.ColumnStyles.Add(new ColumnStyle());
            _statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _statsLayout.Controls.Add(_exportStatsCheckBox, 0, 0);
            _statsLayout.Controls.Add(_statsFormatLabel, 0, 1);
            _statsLayout.Controls.Add(_statsFormatCombo, 1, 1);
            _statsLayout.Dock = DockStyle.Fill;
            _statsLayout.Location = new Point(3, 27);
            _statsLayout.Name = "_statsLayout";
            _statsLayout.RowStyles.Add(new RowStyle());
            _statsLayout.RowStyles.Add(new RowStyle());
            _statsLayout.Size = new Size(438, 76);
            _statsLayout.TabIndex = 0;
            // 
            // _statsFormatLabel
            // 
            _statsFormatLabel.AutoSize = true;
            _statsFormatLabel.Dock = DockStyle.Fill;
            _statsFormatLabel.Location = new Point(3, 37);
            _statsFormatLabel.Name = "_statsFormatLabel";
            _statsFormatLabel.Size = new Size(73, 39);
            _statsFormatLabel.TabIndex = 1;
            _statsFormatLabel.Text = "Format:";
            _statsFormatLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _buttonPanel
            // 
            _buttonPanel.AutoSize = true;
            _mainLayout.SetColumnSpan(_buttonPanel, 3);
            _buttonPanel.Controls.Add(_cancelButton);
            _buttonPanel.Controls.Add(_okButton);
            _buttonPanel.Dock = DockStyle.Bottom;
            _buttonPanel.FlowDirection = FlowDirection.RightToLeft;
            _buttonPanel.Location = new Point(7, 308);
            _buttonPanel.Name = "_buttonPanel";
            _buttonPanel.Size = new Size(444, 46);
            _buttonPanel.TabIndex = 11;
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.Location = new Point(341, 3);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(100, 40);
            _cancelButton.TabIndex = 0;
            _cancelButton.Text = "Cancel";
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            _okButton.Location = new Point(235, 3);
            _okButton.Name = "_okButton";
            _okButton.Size = new Size(100, 40);
            _okButton.TabIndex = 1;
            _okButton.Text = "Export";
            _okButton.Click += OkClick;
            // 
            // ExportClassificationDialog
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(458, 360);
            Controls.Add(_mainLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(480, 416);
            Name = "ExportClassificationDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Export Classification";
            TopMost = true;
            _mainLayout.ResumeLayout(false);
            _mainLayout.PerformLayout();
            _statsGroup.ResumeLayout(false);
            _statsGroup.PerformLayout();
            _statsLayout.ResumeLayout(false);
            _statsLayout.PerformLayout();
            _buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox _formatCombo;
        private CheckBox _geoCheckBox;
        private TextBox _projectionTextBox;
        private TextBox _pathTextBox;
        private ComboBox _paletteCombo;
        private CheckBox _exportStatsCheckBox;
        private ComboBox _statsFormatCombo;
        private TableLayoutPanel _mainLayout;
        private Label _formatLabel;
        private Label _projectionLabel;
        private Label _paletteLabel;
        private Label _saveLabel;
        private Label _statsFormatLabel;
        private Button _browseButton;
        private GroupBox _statsGroup;
        private TableLayoutPanel _statsLayout;
        private FlowLayoutPanel _buttonPanel;
        private Button _cancelButton;
        private Button _okButton;
    }
}