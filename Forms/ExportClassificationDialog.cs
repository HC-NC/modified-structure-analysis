using modified_structure_analysis.Models;
using modified_structure_analysis.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace modified_structure_analysis.Forms
{
    public partial class ExportClassificationDialog : Form
    {
        private string? _projectionWkt;
        private List<Band>? _bands;

        public ClassificationExportOptions ExportOptions { get; private set; } = new();
        public StatsExportOptions StatsOptions { get; private set; } = new();
        public bool ExportStatsChecked { get; private set; }
        public bool UseHsvPalette { get; private set; }
        public bool UseGrayscalePalette { get; private set; }

        public ExportClassificationDialog(string? projectionWkt = null, List<Band>? bands = null)
        {
            _projectionWkt = projectionWkt;
            _bands = bands;

            InitializeComponent();

            _formatCombo.Items.AddRange(new[] { Resources.Format_GeoTiff, Resources.Format_Png, Resources.Format_PngWorld });
            _formatCombo.SelectedIndex = 0;
            _projectionTextBox.Text = _projectionWkt ?? $"({Resources.not_available})";
            _paletteCombo.Items.AddRange(new[] { Resources.Current_palette, Resources.HSV_palette, Resources.Grayscale_palette });
            _paletteCombo.SelectedIndex = 0;
            _statsFormatCombo.Items.AddRange(new[] { Resources.Format_Csv, Resources.Format_Txt, Resources.Format_Json });
            _statsFormatCombo.SelectedIndex = 0;
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
                MessageBox.Show(Resources.Msg_NeedFilePath, Resources.Msg_ValidationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ProjectionWkt = _projectionTextBox.Text != $"({Resources.not_available})" ? _projectionTextBox.Text : null
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
}
