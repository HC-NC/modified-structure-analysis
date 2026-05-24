using modified_structure_analysis.Models;

namespace modified_structure_analysis.Forms;

public partial class GraphExportDialog : Form
{
    public GraphExportOptions Options { get; private set; } = new();
    public string FilePath { get; private set; } = "";

    private readonly ComboBox _formatCombo;
    private readonly NumericUpDown _widthUpDown;
    private readonly NumericUpDown _heightUpDown;
    private readonly NumericUpDown _dpiUpDown;
    private readonly NumericUpDown _qualityUpDown;
    private readonly Label _qualityLabel;
    private readonly TextBox _titleTextBox;
    private readonly CheckBox _grayscaleCheckBox;
    private readonly TextBox _pathTextBox;
    private readonly Button _browseButton;

    public GraphExportDialog()
    {
        Text = "Export Graph";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        MinimumSize = new Size(420, 350);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10),
            ColumnCount = 3,
            RowCount = 9,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        int row = 0;

        layout.Controls.Add(new Label { Text = "Format:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _formatCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        _formatCombo.Items.AddRange(new[] { "PNG", "JPEG", "SVG", "PDF" });
        _formatCombo.SelectedIndex = 0;
        _formatCombo.SelectedIndexChanged += (_, _) => UpdateQualityVisibility();
        layout.Controls.Add(_formatCombo, 1, row);
        layout.SetColumnSpan(_formatCombo, 2);
        row++;

        layout.Controls.Add(new Label { Text = "Width:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _widthUpDown = new NumericUpDown { Minimum = 100, Maximum = 10000, Value = 800, Increment = 100, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        layout.Controls.Add(_widthUpDown, 1, row);
        layout.SetColumnSpan(_widthUpDown, 2);
        row++;

        layout.Controls.Add(new Label { Text = "Height:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _heightUpDown = new NumericUpDown { Minimum = 100, Maximum = 10000, Value = 600, Increment = 100, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        layout.Controls.Add(_heightUpDown, 1, row);
        layout.SetColumnSpan(_heightUpDown, 2);
        row++;

        layout.Controls.Add(new Label { Text = "DPI:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _dpiUpDown = new NumericUpDown { Minimum = 72, Maximum = 1200, Value = 150, Increment = 10, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        layout.Controls.Add(_dpiUpDown, 1, row);
        layout.SetColumnSpan(_dpiUpDown, 2);
        row++;

        _qualityLabel = new Label { Text = "Quality:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left };
        layout.Controls.Add(_qualityLabel, 0, row);
        _qualityUpDown = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 90, Increment = 5, Anchor = AnchorStyles.Left | AnchorStyles.Right };
        layout.Controls.Add(_qualityUpDown, 1, row);
        layout.SetColumnSpan(_qualityUpDown, 2);
        row++;

        layout.Controls.Add(new Label { Text = "Title:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _titleTextBox = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        layout.Controls.Add(_titleTextBox, 1, row);
        layout.SetColumnSpan(_titleTextBox, 2);
        row++;

        _grayscaleCheckBox = new CheckBox { Text = "Convert to grayscale", Checked = false, Anchor = AnchorStyles.Left };
        layout.Controls.Add(_grayscaleCheckBox, 1, row);
        layout.SetColumnSpan(_grayscaleCheckBox, 2);
        row++;

        layout.Controls.Add(new Label { Text = "Save to:", TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left }, 0, row);
        _pathTextBox = new TextBox { Anchor = AnchorStyles.Left | AnchorStyles.Right };
        layout.Controls.Add(_pathTextBox, 1, row);
        _browseButton = new Button { Text = "Browse...", Width = 80, Anchor = AnchorStyles.Right };
        _browseButton.Click += BrowseClick;
        layout.Controls.Add(_browseButton, 2, row);
        row++;

        var buttonPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
        var okButton = new Button { Text = "Export", DialogResult = DialogResult.OK, Width = 100 };
        okButton.Click += OkClick;
        var cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 100 };
        buttonPanel.Controls.Add(cancelButton);
        buttonPanel.Controls.Add(okButton);
        layout.Controls.Add(buttonPanel, 0, row);
        layout.SetColumnSpan(buttonPanel, 3);

        Controls.Add(layout);
        UpdateQualityVisibility();
    }

    private void UpdateQualityVisibility()
    {
        bool isJpeg = _formatCombo.SelectedIndex == 1;
        _qualityLabel.Visible = isJpeg;
        _qualityUpDown.Visible = isJpeg;
    }

    private void BrowseClick(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog();
        dialog.Filter = _formatCombo.SelectedIndex switch
        {
            0 => "PNG files|*.png",
            1 => "JPEG files|*.jpg;*.jpeg",
            2 => "SVG files|*.svg",
            3 => "PDF files|*.pdf",
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

        FilePath = _pathTextBox.Text;
        Options = new GraphExportOptions
        {
            Format = _formatCombo.SelectedIndex switch
            {
                0 => GraphExportFormat.Png,
                1 => GraphExportFormat.Jpeg,
                2 => GraphExportFormat.Svg,
                3 => GraphExportFormat.Pdf,
                _ => GraphExportFormat.Png
            },
            Width = (int)_widthUpDown.Value,
            Height = (int)_heightUpDown.Value,
            Dpi = (int)_dpiUpDown.Value,
            JpegQuality = (int)_qualityUpDown.Value,
            Title = string.IsNullOrWhiteSpace(_titleTextBox.Text) ? null : _titleTextBox.Text,
            Grayscale = _grayscaleCheckBox.Checked
        };
    }
}
