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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportClassificationDialog));
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
            // _geoCheckBox
            // 
            resources.ApplyResources(_geoCheckBox, "_geoCheckBox");
            _mainLayout.SetColumnSpan(_geoCheckBox, 2);
            _geoCheckBox.Name = "_geoCheckBox";
            // 
            // _projectionTextBox
            // 
            _mainLayout.SetColumnSpan(_projectionTextBox, 2);
            resources.ApplyResources(_projectionTextBox, "_projectionTextBox");
            _projectionTextBox.Name = "_projectionTextBox";
            _projectionTextBox.ReadOnly = true;
            // 
            // _pathTextBox
            // 
            resources.ApplyResources(_pathTextBox, "_pathTextBox");
            _pathTextBox.Name = "_pathTextBox";
            // 
            // _paletteCombo
            // 
            _mainLayout.SetColumnSpan(_paletteCombo, 2);
            resources.ApplyResources(_paletteCombo, "_paletteCombo");
            _paletteCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _paletteCombo.Name = "_paletteCombo";
            // 
            // _exportStatsCheckBox
            // 
            _statsLayout.SetColumnSpan(_exportStatsCheckBox, 2);
            resources.ApplyResources(_exportStatsCheckBox, "_exportStatsCheckBox");
            _exportStatsCheckBox.Name = "_exportStatsCheckBox";
            // 
            // _statsFormatCombo
            // 
            resources.ApplyResources(_statsFormatCombo, "_statsFormatCombo");
            _statsFormatCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _statsFormatCombo.Name = "_statsFormatCombo";
            // 
            // _mainLayout
            // 
            resources.ApplyResources(_mainLayout, "_mainLayout");
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
            _mainLayout.Name = "_mainLayout";
            // 
            // _formatLabel
            // 
            resources.ApplyResources(_formatLabel, "_formatLabel");
            _formatLabel.Name = "_formatLabel";
            // 
            // _projectionLabel
            // 
            resources.ApplyResources(_projectionLabel, "_projectionLabel");
            _projectionLabel.Name = "_projectionLabel";
            // 
            // _paletteLabel
            // 
            resources.ApplyResources(_paletteLabel, "_paletteLabel");
            _paletteLabel.Name = "_paletteLabel";
            // 
            // _saveLabel
            // 
            resources.ApplyResources(_saveLabel, "_saveLabel");
            _saveLabel.Name = "_saveLabel";
            // 
            // _browseButton
            // 
            resources.ApplyResources(_browseButton, "_browseButton");
            _browseButton.Name = "_browseButton";
            _browseButton.Click += BrowseClick;
            // 
            // _statsGroup
            // 
            resources.ApplyResources(_statsGroup, "_statsGroup");
            _mainLayout.SetColumnSpan(_statsGroup, 3);
            _statsGroup.Controls.Add(_statsLayout);
            _statsGroup.Name = "_statsGroup";
            _statsGroup.TabStop = false;
            // 
            // _statsLayout
            // 
            resources.ApplyResources(_statsLayout, "_statsLayout");
            _statsLayout.Controls.Add(_exportStatsCheckBox, 0, 0);
            _statsLayout.Controls.Add(_statsFormatLabel, 0, 1);
            _statsLayout.Controls.Add(_statsFormatCombo, 1, 1);
            _statsLayout.Name = "_statsLayout";
            // 
            // _statsFormatLabel
            // 
            resources.ApplyResources(_statsFormatLabel, "_statsFormatLabel");
            _statsFormatLabel.Name = "_statsFormatLabel";
            // 
            // _buttonPanel
            // 
            resources.ApplyResources(_buttonPanel, "_buttonPanel");
            _mainLayout.SetColumnSpan(_buttonPanel, 3);
            _buttonPanel.Controls.Add(_cancelButton);
            _buttonPanel.Controls.Add(_okButton);
            _buttonPanel.Name = "_buttonPanel";
            // 
            // _cancelButton
            // 
            _cancelButton.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(_cancelButton, "_cancelButton");
            _cancelButton.Name = "_cancelButton";
            // 
            // _okButton
            // 
            _okButton.DialogResult = DialogResult.OK;
            resources.ApplyResources(_okButton, "_okButton");
            _okButton.Name = "_okButton";
            _okButton.Click += OkClick;
            // 
            // ExportClassificationDialog
            // 
            AcceptButton = _okButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _cancelButton;
            Controls.Add(_mainLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportClassificationDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
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