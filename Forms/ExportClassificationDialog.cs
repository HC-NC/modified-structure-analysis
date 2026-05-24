using modified_structure_analysis.Models;

namespace modified_structure_analysis.Forms;

public partial class ExportClassificationDialog : Form
{
    public ClassificationExportOptions ExportOptions { get; private set; } = new();
    public StatsExportOptions StatsOptions { get; private set; } = new();
    public bool ExportStatsChecked { get; private set; }

    private readonly ComboBox _formatCombo;
    private readonly CheckBox _geoCheckBox;
    private readonly TextBox _projectionTextBox;
    private readonly Button _browseButton;
    private readonly TextBox _pathTextBox;
    private readonly Label _projectionLabel;
    private readonly GroupBox _statsGroupBox;
    private readonly CheckBox _exportStatsCheckBox;
    private readonly ComboBox _statsFormatCombo;
    private readonly Button _statsBrowseButton;
    private readonly TextBox _statsPathTextBox;
    private readonly Button _okButton;
    private readonly Button _cancelButton;

    public ExportClassificationDialog(string? projectionWkt = null, List<Band>? bands = null)
    {
        Text = "Export Classification";
        Size = new Size(520, 380);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10),
            ColumnCount = 3,
            RowCount = 7
        };
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        int row = 0;

        // Format selection
        mainLayout.Controls.Add(new Label { Text = "Format:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _formatCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _formatCombo.Items.AddRange(new[] { "GeoTIFF", "PNG", "PNG + World file" });
        _formatCombo.SelectedIndex = 0;
        mainLayout.Controls.Add(_formatCombo, 1, row);
        mainLayout.SetColumnSpan(_formatCombo, 2);
        row++;

        // Include geo transform
        _geoCheckBox = new CheckBox { Text = "Include georeferencing", Checked = true, Anchor = AnchorStyles.Left };
        mainLayout.Controls.Add(_geoCheckBox, 1, row);
        mainLayout.SetColumnSpan(_geoCheckBox, 2);
        row++;

        // Projection WKT
        _projectionLabel = new Label { Text = "Projection:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left };
        mainLayout.Controls.Add(_projectionLabel, 0, row);
        _projectionTextBox = new TextBox { ReadOnly = true, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _projectionTextBox.Text = projectionWkt ?? "(not available)";
        mainLayout.Controls.Add(_projectionTextBox, 1, row);
        mainLayout.SetColumnSpan(_projectionTextBox, 2);
        row++;

        // File path
        mainLayout.Controls.Add(new Label { Text = "Save to:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _pathTextBox = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        mainLayout.Controls.Add(_pathTextBox, 1, row);
        _browseButton = new Button { Text = "Browse...", Width = 80, Anchor = AnchorStyles.Right };
        _browseButton.Click += BrowseClick;
        mainLayout.Controls.Add(_browseButton, 2, row);
        row++;

        // Stats group
        _statsGroupBox = new GroupBox { Text = "Export Statistics", Dock = DockStyle.Fill, Height = 100 };
        var statsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 3,
            Padding = new Padding(5)
        };

        _exportStatsCheckBox = new CheckBox { Text = "Export class statistics", Checked = false, Anchor = AnchorStyles.Left };
        statsLayout.Controls.Add(_exportStatsCheckBox, 0, 0);
        statsLayout.SetColumnSpan(_exportStatsCheckBox, 3);

        statsLayout.Controls.Add(new Label { Text = "Format:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, 1);
        _statsFormatCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _statsFormatCombo.Items.AddRange(new[] { "CSV", "TXT", "JSON" });
        _statsFormatCombo.SelectedIndex = 0;
        statsLayout.Controls.Add(_statsFormatCombo, 1, 1);
        statsLayout.SetColumnSpan(_statsFormatCombo, 2);

        statsLayout.Controls.Add(new Label { Text = "Save to:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, 2);
        _statsPathTextBox = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        statsLayout.Controls.Add(_statsPathTextBox, 1, 2);
        _statsBrowseButton = new Button { Text = "Browse...", Width = 80, Anchor = AnchorStyles.Right };
        _statsBrowseButton.Click += StatsBrowseClick;
        statsLayout.Controls.Add(_statsBrowseButton, 2, 2);

        _statsGroupBox.Controls.Add(statsLayout);
        mainLayout.Controls.Add(_statsGroupBox, 0, row);
        mainLayout.SetColumnSpan(_statsGroupBox, 3);
        row++;

        // Buttons
        var buttonPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
        _okButton = new Button { Text = "Export", DialogResult = DialogResult.OK, Width = 100 };
        _okButton.Click += OkClick;
        _cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 100 };

        buttonPanel.Controls.Add(_cancelButton);
        buttonPanel.Controls.Add(_okButton);
        mainLayout.Controls.Add(buttonPanel, 0, row);
        mainLayout.SetColumnSpan(buttonPanel, 3);

        Controls.Add(mainLayout);
    }

    private void BrowseClick(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        dialog.Filter = (_formatCombo.SelectedIndex) switch
        {
            0 => "GeoTIFF files|*.tif;*.tiff",
            1 => "PNG files|*.png",
            2 => "PNG files|*.png",
            _ => "All files|*.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK)
            _pathTextBox.Text = dialog.FileName;
    }

    private void StatsBrowseClick(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        dialog.Filter = (_statsFormatCombo.SelectedIndex) switch
        {
            0 => "CSV files|*.csv",
            1 => "Text files|*.txt",
            2 => "JSON files|*.json",
            _ => "All files|*.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK)
            _statsPathTextBox.Text = dialog.FileName;
    }

    private void OkClick(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_pathTextBox.Text))
        {
            MessageBox.Show("Please specify a file path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.None;
            return;
        }

        ExportOptions = new ClassificationExportOptions
        {
            FilePath = _pathTextBox.Text,
            Format = _formatCombo.SelectedIndex switch
            {
                0 => ClassificationExportFormat.GeoTiff,
                1 => ClassificationExportFormat.Png,
                2 => ClassificationExportFormat.PngWithWorldFile,
                _ => ClassificationExportFormat.GeoTiff
            },
            IncludeGeoTransform = _geoCheckBox.Checked,
            ProjectionWkt = _projectionTextBox.Text != "(not available)" ? _projectionTextBox.Text : null
        };

        ExportStatsChecked = _exportStatsCheckBox.Checked;

        if (ExportStatsChecked)
        {
            if (string.IsNullOrWhiteSpace(_statsPathTextBox.Text))
            {
                MessageBox.Show("Please specify a statistics file path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            StatsOptions = new StatsExportOptions
            {
                FilePath = _statsPathTextBox.Text,
                Format = _statsFormatCombo.SelectedIndex switch
                {
                    0 => StatsExportFormat.Csv,
                    1 => StatsExportFormat.Txt,
                    2 => StatsExportFormat.Json,
                    _ => StatsExportFormat.Csv
                }
            };
        }
    }
}
