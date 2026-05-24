using modified_structure_analysis.Models;
using modified_structure_analysis.Services;

namespace modified_structure_analysis.Forms;

public partial class ExportClassificationDialog : Form
{
    public ClassificationExportOptions ExportOptions { get; private set; } = new();
    public StatsExportOptions StatsOptions { get; private set; } = new();
    public bool ExportStatsChecked { get; private set; }
    public bool UseHsvPalette { get; private set; }
    public bool UseGrayscalePalette { get; private set; }

    private readonly ComboBox _formatCombo;
    private readonly CheckBox _geoCheckBox;
    private readonly TextBox _projectionTextBox;
    private readonly TextBox _pathTextBox;
    private readonly ComboBox _paletteCombo;
    private readonly CheckBox _exportStatsCheckBox;
    private readonly ComboBox _statsFormatCombo;

    public ExportClassificationDialog(string? projectionWkt = null, List<Band>? bands = null)
    {
        Text = "Export Classification";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        MinimumSize = new Size(480, 300);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10),
            ColumnCount = 3,
            RowCount = 8,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        int row = 0;

        mainLayout.Controls.Add(new Label { Text = "Format:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _formatCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _formatCombo.Items.AddRange(new[] { "GeoTIFF", "PNG", "PNG + World file" });
        _formatCombo.SelectedIndex = 0;
        mainLayout.Controls.Add(_formatCombo, 1, row);
        mainLayout.SetColumnSpan(_formatCombo, 2);
        row++;

        _geoCheckBox = new CheckBox { Text = "Include georeferencing", Checked = true, Anchor = AnchorStyles.Left };
        mainLayout.Controls.Add(_geoCheckBox, 1, row);
        mainLayout.SetColumnSpan(_geoCheckBox, 2);
        row++;

        mainLayout.Controls.Add(new Label { Text = "Projection:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _projectionTextBox = new TextBox { ReadOnly = true, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _projectionTextBox.Text = projectionWkt ?? "(not available)";
        mainLayout.Controls.Add(_projectionTextBox, 1, row);
        mainLayout.SetColumnSpan(_projectionTextBox, 2);
        row++;

        mainLayout.Controls.Add(new Label { Text = "Palette:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _paletteCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _paletteCombo.Items.AddRange(new[] { "Current palette", "HSV palette", "Grayscale palette" });
        _paletteCombo.SelectedIndex = 0;
        mainLayout.Controls.Add(_paletteCombo, 1, row);
        mainLayout.SetColumnSpan(_paletteCombo, 2);
        row++;

        mainLayout.Controls.Add(new Label { Text = "Save to:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _pathTextBox = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        mainLayout.Controls.Add(_pathTextBox, 1, row);
        var browseButton = new Button { Text = "Browse...", Width = 80, Anchor = AnchorStyles.Right };
        browseButton.Click += BrowseClick;
        mainLayout.Controls.Add(browseButton, 2, row);
        row++;

        var statsGroup = new GroupBox { Text = "Statistics", Dock = DockStyle.Fill };
        var statsLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Padding = new Padding(5),
            AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

        _exportStatsCheckBox = new CheckBox { Text = "Export alongside classification", Checked = true, Anchor = AnchorStyles.Left };
        statsLayout.Controls.Add(_exportStatsCheckBox, 0, 0);
        statsLayout.SetColumnSpan(_exportStatsCheckBox, 2);

        statsLayout.Controls.Add(new Label { Text = "Format:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, 1);
        _statsFormatCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _statsFormatCombo.Items.AddRange(new[] { "CSV", "TXT", "JSON" });
        _statsFormatCombo.SelectedIndex = 0;
        statsLayout.Controls.Add(_statsFormatCombo, 1, 1);

        statsGroup.Controls.Add(statsLayout);
        mainLayout.Controls.Add(statsGroup, 0, row);
        mainLayout.SetColumnSpan(statsGroup, 3);
        row++;

        var buttonPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
        var okButton = new Button { Text = "Export", DialogResult = DialogResult.OK, Width = 100 };
        okButton.Click += OkClick;
        var cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 100 };
        buttonPanel.Controls.Add(cancelButton);
        buttonPanel.Controls.Add(okButton);
        mainLayout.Controls.Add(buttonPanel, 0, row);
        mainLayout.SetColumnSpan(buttonPanel, 3);

        Controls.Add(mainLayout);
    }

    private void BrowseClick(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        dialog.Filter = _formatCombo.SelectedIndex switch
        {
            0 => "GeoTIFF files|*.tif;*.tiff",
            1 => "PNG files|*.png",
            2 => "PNG files|*.png",
            _ => "All files|*.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK)
            _pathTextBox.Text = dialog.FileName;
    }

    private void OkClick(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_pathTextBox.Text))
        {
            MessageBox.Show("Please specify a file path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DialogResult = DialogResult.None;
            return;
        }

        string filePath = _pathTextBox.Text;

        ExportOptions = new ClassificationExportOptions
        {
            FilePath = filePath,
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

        UseHsvPalette = _paletteCombo.SelectedIndex == 1;
        UseGrayscalePalette = _paletteCombo.SelectedIndex == 2;
        ExportStatsChecked = _exportStatsCheckBox.Checked;

        if (ExportStatsChecked)
        {
            string statsPath = Path.ChangeExtension(filePath, null) + "_stats";
            statsPath += _statsFormatCombo.SelectedIndex switch
            {
                0 => ".csv",
                1 => ".txt",
                2 => ".json",
                _ => ".csv"
            };

            StatsOptions = new StatsExportOptions
            {
                FilePath = statsPath,
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
