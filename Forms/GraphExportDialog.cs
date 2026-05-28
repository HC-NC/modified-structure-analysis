using modified_structure_analysis.Models;
using modified_structure_analysis.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;

namespace modified_structure_analysis.Forms
{
    public partial class GraphExportDialog : Form
    {
        public GraphExportOptions Options { get; private set; } = new();
        public string FilePath { get; private set; } = "";

        public GraphExportDialog()
        {
            InitializeComponent();

            _formatCombo.Items.AddRange(new[] { Resources.Format_Png, Resources.Format_Jpeg, Resources.Format_Svg, Resources.Format_Pdf });
            _formatCombo.SelectedIndex = 0;
        }

        private void UpdateQualityVisibility(object? sender, EventArgs e)
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
                MessageBox.Show(Resources.Msg_NeedFilePath, Resources.Msg_ValidationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
}
