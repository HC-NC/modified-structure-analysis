using modified_structure_analysis.Models;
using modified_structure_analysis.Engine;
using modified_structure_analysis.Services;
using modified_structure_analysis.Config;
using modified_structure_analysis.Forms;
using modified_structure_analysis.Properties;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GdalBand = OSGeo.GDAL.Band;

namespace modified_structure_analysis.Forms
{
    public partial class MainForm : Form
    {
        private List<Band> _bands = new List<Band>();

        private double _cellSize = 30.0;

        private int _width;
        private int _height;

        private Band _redBand;
        private Band _greenBand;
        private Band _blueBand;

        private GeoTransform? _geoTransform;

        private List<ClassificationRule> _firstStageRules = new();
        private List<ClassificationRule> _secondStageRules = new();

        private ClassificationEngine? _firstStageEngine;
        private ClassificationResult? _firstStageResult;
        private ClassStatistics[]? _firstStageClassStats;
        private ClassificationResult? _lastClassificationResult;
        private DataGridView? _paletteGridView;

        private PlotModel _kdeModel;
        private readonly BackgroundWorker _statsWorker;
        private readonly BackgroundWorker _kdeWorker;
        private readonly BackgroundWorker _scatterWorker;
        private readonly BackgroundWorker _loadWorker;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "All|*.tif;*.tiff;*.img;*.csv;*.txt|GeoTIFF|*.tif;*.tiff|ERDAS|*.img|CSV|*.csv|Text file|*.txt";
            openFileDialog1.Multiselect = true;

            _loadWorker = new BackgroundWorker { WorkerReportsProgress = true };
            _loadWorker.DoWork += LoadWorker_DoWork;
            _loadWorker.RunWorkerCompleted += LoadWorker_RunWorkerCompleted;
            _loadWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            _statsWorker = new BackgroundWorker { WorkerReportsProgress = true };
            _statsWorker.DoWork += StatsWorker_DoWork;
            _statsWorker.RunWorkerCompleted += StatsWorker_RunWorkerCompleted;
            _statsWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            _kdeWorker = new BackgroundWorker { WorkerReportsProgress = true };
            _kdeWorker.DoWork += KdeWorker_DoWork;
            _kdeWorker.RunWorkerCompleted += KdeWorker_RunWorkerCompleted;
            _kdeWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            _scatterWorker = new BackgroundWorker { WorkerReportsProgress = true };
            _scatterWorker.DoWork += ScatterWorker_DoWork;
            _scatterWorker.RunWorkerCompleted += ScatterWorker_RunWorkerCompleted;
            _scatterWorker.ProgressChanged += backgroundWorker_ProgressChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _kdeModel = new PlotModel();
            _kdeModel.Axes.Add(new LinearAxis { Key = "X", Position = AxisPosition.Bottom, Title = "Normalized Value", Minimum = 0d, Maximum = 1d });
            _kdeModel.Axes.Add(new LinearAxis { Key = "Y", Position = AxisPosition.Left, Title = "Density", Minimum = 0d });
            _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
            kdePlotView.Model = _kdeModel;
        }

        private void ruleDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ruleDataGridView.Rows[e.RowIndex].Selected = true;

                int x = e.X;
                int y = e.Y;

                for (int i = 0; i < e.ColumnIndex; i++)
                    x += ruleDataGridView.Columns[i].Width;

                for (int i = 0; i < e.RowIndex; i++)
                    y += ruleDataGridView.Rows[e.RowIndex].Height;

                ruleContextMenuStrip.Show((Control)sender, x, y);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 1)
            {
                var rule = _secondStageRules[e.RowIndex];

                EditClassificationRule(rule);
            }
        }

        private void AddClassificationRule(object sender, EventArgs e)
        {
            var editor = new RuleEditorForm(_bands, null, isSecondStage: true, classStats: _firstStageClassStats);
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                _secondStageRules.Add(editor.Rule);
                UpdateClassificationRulesGrid();
                ruleDataGridView.Rows[ruleDataGridView.Rows.GetLastRow(DataGridViewElementStates.None)].Selected = true;
            }
        }

        private void EditClassificationRule(ClassificationRule rule)
        {
            var editor = new RuleEditorForm(_bands, rule, isSecondStage: true, classStats: _firstStageClassStats);

            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateClassificationRulesGrid();
                ruleDataGridView.Rows[_secondStageRules.IndexOf(rule)].Selected = true;
            }
        }

        private void EditClassificationRule(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = ruleDataGridView.SelectedRows[0].Index;

            var rule = _secondStageRules[selectedIndex];

            EditClassificationRule(rule);
        }

        private void DeleteClassificationRule(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = ruleDataGridView.SelectedRows[0].Index;

            var result = MessageBox.Show(
                $"Are you sure you want to delete rule #{selectedIndex + 1}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _secondStageRules.RemoveAt(selectedIndex);
                UpdateClassificationRulesGrid();

                if (_secondStageRules.Count == 0)
                    return;

                if (selectedIndex >= ruleDataGridView.Rows.Count)
                    selectedIndex = ruleDataGridView.Rows.GetLastRow(DataGridViewElementStates.None);

                ruleDataGridView.Rows[selectedIndex].Selected = true;
            }
        }

        private void CloneClassificationRule(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = ruleDataGridView.SelectedRows[0].Index;

            var rule = _secondStageRules[selectedIndex];

            _secondStageRules.Insert(selectedIndex + 1, (ClassificationRule)rule.Clone());

            UpdateClassificationRulesGrid();

            ruleDataGridView.Rows[selectedIndex + 1].Selected = true;
        }

        private void MoveRuleUp(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = ruleDataGridView.SelectedRows[0].Index;

            if (selectedIndex == 0)
                return;

            var rule = _secondStageRules[selectedIndex];
            _secondStageRules.RemoveAt(selectedIndex);
            _secondStageRules.Insert(selectedIndex - 1, rule);

            UpdateClassificationRulesGrid();

            ruleDataGridView.Rows[selectedIndex - 1].Selected = true;
        }

        private void MoveRuleDown(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = ruleDataGridView.SelectedRows[0].Index;

            if (selectedIndex >= _secondStageRules.Count - 1)
                return;

            var rule = _secondStageRules[selectedIndex];
            _secondStageRules.RemoveAt(selectedIndex);
            _secondStageRules.Insert(selectedIndex + 1, rule);

            UpdateClassificationRulesGrid();

            ruleDataGridView.Rows[selectedIndex + 1].Selected = true;
        }

        private void ChangeRuleColor(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = ruleDataGridView.SelectedRows[0].Index;

            var rule = _secondStageRules[selectedIndex];

            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = rule.Color;
                if (colorDialog.ShowDialog(this) == DialogResult.OK)
                {
                    rule.Color = colorDialog.Color;
                }
            }

            UpdateClassificationRulesGrid();

            ruleDataGridView.Rows[selectedIndex].Selected = true;
        }

        private void FirstStageAddRule_Click(object? sender, EventArgs e)
        {
            ClassificationMode mode = ClassificationModeToolStripComboBox.SelectedItem?.ToString() == "DirectCheck"
                ? ClassificationMode.DirectCheck
                : ClassificationMode.RulePerClass;

            if (mode == ClassificationMode.DirectCheck)
            {
                MessageBox.Show("In DirectCheck mode, use 'Auto' to generate rules from bands.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var editor = new RuleEditorForm(_bands, null);
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                _firstStageRules.Add(editor.Rule);
                UpdateFirstStageRulesGrid();
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Selected = true;
            }
        }

        private void FirstStageDeleteRule_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;

            var result = MessageBox.Show(
                $"Are you sure you want to delete rule #{selectedIndex + 1}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _firstStageRules.RemoveAt(selectedIndex);
                UpdateFirstStageRulesGrid();

                if (_firstStageRules.Count == 0)
                    return;

                if (selectedIndex >= dataGridView1.Rows.Count)
                    selectedIndex = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None);

                dataGridView1.Rows[selectedIndex].Selected = true;
            }
        }

        private void FirstStageMoveUp_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;

            if (selectedIndex == 0)
                return;

            var rule = _firstStageRules[selectedIndex];
            _firstStageRules.RemoveAt(selectedIndex);
            _firstStageRules.Insert(selectedIndex - 1, rule);

            UpdateFirstStageRulesGrid();
            dataGridView1.Rows[selectedIndex - 1].Selected = true;
        }

        private void FirstStageMoveDown_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;

            if (selectedIndex >= _firstStageRules.Count - 1)
                return;

            var rule = _firstStageRules[selectedIndex];
            _firstStageRules.RemoveAt(selectedIndex);
            _firstStageRules.Insert(selectedIndex + 1, rule);

            UpdateFirstStageRulesGrid();
            dataGridView1.Rows[selectedIndex + 1].Selected = true;
        }

        private void UpdateClassificationRulesGrid()
        {
            ruleDataGridView.Rows.Clear();
            foreach (var rule in _secondStageRules)
            {
                int rowIndex = ruleDataGridView.Rows.Add();
                var row = ruleDataGridView.Rows[rowIndex];

                row.Cells[0].Value = rule.GenerateName();
            }
        }

        private Bitmap CreateColorBitmap(Color color)
        {
            Bitmap bmp = new Bitmap(20, 20);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(color);
                g.DrawRectangle(Pens.Black, 0, 0, 19, 19);
            }
            return bmp;
        }

        private void PopulatePaletteTab()
        {
            tabPage9.Controls.Clear();

            var result = _lastClassificationResult;
            if (result?.Palette == null) return;

            _paletteGridView?.Dispose();
            _paletteGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ColumnHeadersVisible = true,
                ReadOnly = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var colorCol = new DataGridViewImageColumn
            {
                HeaderText = "Color",
                Width = 40,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = true
            };
            var nameCol = new DataGridViewTextBoxColumn
            {
                HeaderText = "Class",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            var countCol = new DataGridViewTextBoxColumn
            {
                HeaderText = "Pixels",
                ReadOnly = true,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            var editCol = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Change",
                UseColumnTextForButtonValue = true,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };

            _paletteGridView.Columns.Add(colorCol);
            _paletteGridView.Columns.Add(nameCol);
            _paletteGridView.Columns.Add(countCol);
            _paletteGridView.Columns.Add(editCol);

            var stats = result.GetClassStatistics();
            int totalPixels = result.Width * result.Height;

            for (int i = 0; i < result.Palette.Length; i++)
            {
                int rowIdx = _paletteGridView.Rows.Add();
                var row = _paletteGridView.Rows[rowIdx];
                row.Cells[0].Value = CreateColorBitmap(result.Palette[i]);
                row.Cells[1].Value = $"Class {i}";
                row.Cells[2].Value = stats.GetValueOrDefault(i, 0);
            }

            int undefCount = stats.GetValueOrDefault(-1, 0);
            if (undefCount > 0)
            {
                int rowIdx = _paletteGridView.Rows.Add();
                var row = _paletteGridView.Rows[rowIdx];
                row.Cells[0].Value = CreateColorBitmap(Color.Transparent);
                row.Cells[1].Value = "Undefined";
                row.Cells[2].Value = undefCount;
            }

            _paletteGridView.CellClick += PaletteGrid_CellClick;

            tabPage9.Controls.Add(_paletteGridView);
        }

        private void PopulateSecondAnalysisTab()
        {
            tabPage5.Controls.Clear();

            var result = _lastClassificationResult;
            if (result?.Palette == null) return;

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ColumnHeadersVisible = true,
                ReadOnly = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            grid.Columns.Add(new DataGridViewImageColumn
            {
                HeaderText = "Color",
                Width = 40,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = true
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Class",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Pixels",
                ReadOnly = true,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });
            grid.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Change",
                UseColumnTextForButtonValue = true,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            var stats = result.GetClassStatistics();
            for (int i = 0; i < result.Palette.Length; i++)
            {
                int rowIdx = grid.Rows.Add();
                var row = grid.Rows[rowIdx];
                row.Cells[0].Value = CreateColorBitmap(result.Palette[i]);
                row.Cells[1].Value = $"Class {i}";
                row.Cells[2].Value = stats.GetValueOrDefault(i, 0);
            }

            int undefCount = stats.GetValueOrDefault(-1, 0);
            if (undefCount > 0)
            {
                int rowIdx = grid.Rows.Add();
                var row = grid.Rows[rowIdx];
                row.Cells[0].Value = CreateColorBitmap(Color.Transparent);
                row.Cells[1].Value = "Undefined";
                row.Cells[2].Value = undefCount;
            }

            grid.CellClick += (s, args) =>
            {
                if (args.RowIndex < 0 || args.ColumnIndex != 3) return;
                if (_lastClassificationResult?.Palette == null) return;
                int rowIdx = args.RowIndex;
                if (rowIdx >= _lastClassificationResult.Palette.Length) return; // Undefined row

                using var dialog = new ColorDialog();
                dialog.Color = _lastClassificationResult.Palette[rowIdx];
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _lastClassificationResult.Palette[rowIdx] = dialog.Color;
                    grid.Rows[rowIdx].Cells[0].Value = CreateColorBitmap(dialog.Color);
                    var bitmap = ResultRenderer.ToBitmap(_lastClassificationResult);
                    viewport2.UpdateImage(bitmap);
                }
            };

            tabPage5.Controls.Add(grid);
        }

        private void PaletteGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3) return;
            if (_firstStageResult?.Palette == null) return;
            if (e.RowIndex >= _firstStageResult.Palette.Length) return;

            using var dialog = new ColorDialog();
            dialog.Color = _firstStageResult.Palette[e.RowIndex];
            dialog.FullOpen = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _firstStageResult.Palette[e.RowIndex] = dialog.Color;
                if (_paletteGridView != null && e.RowIndex < _paletteGridView.Rows.Count)
                    _paletteGridView.Rows[e.RowIndex].Cells[0].Value = CreateColorBitmap(dialog.Color);
                var bitmap = ResultRenderer.ToBitmap(_firstStageResult);
                viewport3.UpdateImage(bitmap);
            }
        }

        private void BuildScatterPlot(object? sender, EventArgs e)
        {
            if (scatterXListBox.SelectedItem == null || scatterYListBox.SelectedItem == null)
                return;

            Band? bandX = scatterXListBox.SelectedItem as Band;
            Band? bandY = scatterYListBox.SelectedItem as Band;
            if (bandX == null || bandY == null) return;

            if (_scatterWorker.IsBusy) return;
            _scatterWorker.RunWorkerAsync((bandX, bandY));
        }

        private void KdeSingle(object? sender, EventArgs e)
        {
            if (kdeBandsListBox!.SelectedItems.Count == 0) return;
            if (_kdeWorker.IsBusy) return;

            var bands = kdeBandsListBox.SelectedItems.Cast<Band>().ToList();
            _kdeWorker.RunWorkerAsync(("single", bands, (object?)null));
        }

        private void KdeProduct(object? sender, EventArgs e)
        {
            if (kdeBandsListBox!.SelectedItems.Count < 2) return;
            if (_kdeWorker.IsBusy) return;

            var bands = kdeBandsListBox.SelectedItems.Cast<Band>().ToList();
            _kdeWorker.RunWorkerAsync(("product", bands, (object?)null));
        }

        private void KdeMultivariate(object? sender, EventArgs e)
        {
            if (kdeBandsListBox!.SelectedItems.Count < 2) return;
            if (_kdeWorker.IsBusy) return;

            var bands = kdeBandsListBox.SelectedItems.Cast<Band>().ToList();
            _kdeWorker.RunWorkerAsync(("multivariate", bands, (object?)null));
        }

        private void ClearKdePlot(object? sender, EventArgs e)
        {
            if (_kdeModel == null) return;

            _kdeModel.Series.Clear();
            PlotView_DoubleClick(kdePlotView, e);
        }

        // ── Background Workers ──────────────────────────────────────────

        private const int KdeMaxSamples = 500_000;
        private const int ScatterMaxPoints = 50_000;

        private void StatsWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                var worker = sender as BackgroundWorker;
                var bands = _bands.ToList();
                int total = bands.Count;
                for (int i = 0; i < total; i++)
                {
                    BandStatisticsComputer.Compute(bands[i]);
                    int pct = (i + 1) * 100 / total;
                    worker?.ReportProgress(pct, $"Statistics: band {i + 1}/{total}");
                }

                float[][]? corrData = null;
                if (total > 1)
                {
                    worker?.ReportProgress(0, "Computing correlation...");
                    corrData = CalcCorrelationData(bands);
                }

                e.Result = corrData;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"StatsWorker error: {ex.Message}", ex);
            }
        }

        private void StatsWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Stats error: {e.Error.Message}");
                return;
            }

            foreach (Band band in _bands)
                band.ClearRawData();

            if (e.Result is float[][] corrData)
            {
                UpdateCorrelationGrid(corrData);
            }

            mainStatusLabel.Text = "Ready";
        }

        private void ScatterWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            var (bandX, bandY) = ((Band, Band))e.Argument!;
            int totalPixels = bandX.OriginalWidth * bandX.OriginalHeight;
            int targetSamples = ScatterMaxPoints * 2;
            int step = Math.Max(1, totalPixels / targetSamples);

            var rng = new Random(42);
            var points = new List<(double x, double y)>(ScatterMaxPoints);

            for (int i = 0; i < totalPixels; i += step)
            {
                if (!float.IsNaN(bandX.GetPixelValue(i)) && !float.IsNaN(bandY.GetPixelValue(i)))
                {
                    float vx = bandX.GetNormalizedValue(i);
                    float vy = bandY.GetNormalizedValue(i);
                    points.Add((vx, vy));
                }
            }

            while (points.Count > ScatterMaxPoints)
            {
                int idx = rng.Next(points.Count);
                points.RemoveAt(idx);
            }

            e.Result = points;
        }

        private void ScatterWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Scatter error: {e.Error.Message}");
                return;
            }

            var rawPoints = (List<(double x, double y)>)e.Result!;
            var series = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 2 };
            foreach (var (x, y) in rawPoints)
                series.Points.Add(new ScatterPoint(x, y));

            var model = new PlotModel();
            var bandX = scatterXListBox.SelectedItem as Band;
            var bandY = scatterYListBox.SelectedItem as Band;
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = bandX?.Name ?? "X" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = bandY?.Name ?? "Y" });
            model.Series.Add(series);

            scatterPlotView.Model = model;
            mainStatusLabel.Text = $"Scatter: {rawPoints.Count} points";
        }

        private void KdeWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            var (mode, bands, _) = ((string, List<Band>, object?))e.Argument!;
            var worker = sender as BackgroundWorker;
            int totalPixels = bands[0].OriginalWidth * bands[0].OriginalHeight;
            int step = Math.Max(1, totalPixels / KdeMaxSamples);

            if (mode == "single")
            {
                var results = new List<(Band band, List<DataPoint> points)>();
                for (int bi = 0; bi < bands.Count; bi++)
                {
                    var band = bands[bi];
                    var points = new List<DataPoint>(101);
                    int barCount = 101;
                    for (int xi = 0; xi < barCount; xi++)
                    {
                        double x = xi / 100.0;
                        double bandDensity = 0;
                        int validCount = 0;

                        for (int i = 0; i < totalPixels; i += step)
                        {
                            float pv = band.GetNormalizedValue(i);
                            if (!float.IsNaN(band.GetPixelValue(i)))
                            {
                                bandDensity += KernelFunctions.GetKernel(band.KernelType, (x - pv) / band.NormalizeKernelC);
                                validCount++;
                            }
                        }

                        bandDensity /= validCount * band.NormalizeKernelC;
                        points.Add(new DataPoint(x, bandDensity));
                        worker?.ReportProgress((bi * barCount + xi) * 100 / (bands.Count * barCount), $"KDE: band {bi + 1}/{bands.Count}");
                    }
                    results.Add((band, points));
                }
                e.Result = (mode, "", results);
                return;
            }

            var allPoints = new List<DataPoint>(101);

            if (mode == "multivariate")
            {
                string multiTitle = $"Multivariate: {string.Join(",", bands.Select(b => b.Name))}";
                double productBandwidths = 1;
                foreach (var b in bands)
                    productBandwidths *= b.NormalizeKernelC;
                double normalization = (totalPixels / step) * productBandwidths;

                for (double x = 0; x <= 1; x += 0.01)
                {
                    double density = 0;

                    for (int pixel = 0; pixel < totalPixels; pixel += step)
                    {
                        double kernelProduct = 1;
                        foreach (var band in bands)
                        {
                            double v = band.GetNormalizedValue(pixel);
                            if (v <= 0) { kernelProduct = 0; break; }
                            kernelProduct *= KernelFunctions.GetKernel(band.KernelType, (x - v) / band.NormalizeKernelC);
                        }
                        density += kernelProduct;
                    }

                    density /= normalization;
                    allPoints.Add(new DataPoint(x, density));

                    int pct = (int)(x * 100);
                    worker?.ReportProgress(pct, $"Multivariate KDE: {pct}%");
                }

                e.Result = (mode, multiTitle, new List<(Band? band, List<DataPoint> points)> { (null, allPoints) });
            }
            else
            {
                bool isProduct = mode == "product";
                string prodTitle = isProduct
                    ? $"Product: {string.Join("×", bands.Select(b => b.Name))}"
                    : "";
                int barCount = 101;
                for (int xi = 0; xi < barCount; xi++)
                {
                    double x = xi / 100.0;
                    double result = isProduct ? 1.0 : 0.0;

                    foreach (var band in bands)
                    {
                        double bandDensity = 0;
                        int validCount = 0;

                        for (int i = 0; i < totalPixels; i += step)
                        {
                            float pv = band.GetNormalizedValue(i);
                            if (!float.IsNaN(band.GetPixelValue(i)))
                            {
                                bandDensity += KernelFunctions.GetKernel(band.KernelType, (x - pv) / band.NormalizeKernelC);
                                validCount++;
                            }
                        }

                        bandDensity /= validCount * band.NormalizeKernelC;

                        if (isProduct)
                            result *= bandDensity;
                        else
                            result += bandDensity;
                    }

                    allPoints.Add(new DataPoint(x, result));
                    worker?.ReportProgress(xi * 100 / barCount, $"KDE: {xi}/{barCount}");
                }

                e.Result = (mode, prodTitle, new List<(Band? band, List<DataPoint> points)> { (null, allPoints) });
            }
        }

        private void KdeWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"KDE error: {e.Error.Message}");
                return;
            }

            kdePlotView.Model = null;

            var (mode, title, results) = ((string, string, List<(Band? band, List<DataPoint> points)>))e.Result!;

            foreach (var (band, points) in results)
            {
                var series = new FunctionSeries
                {
                    Title = !string.IsNullOrEmpty(title) ? title : band?.Name ?? "KDE"
                };
                series.Points.AddRange(points);
                _kdeModel!.Series.Add(series);
            }

            kdePlotView.Model = _kdeModel;
            PlotView_DoubleClick(kdePlotView, e);
            mainStatusLabel.Text = "KDE ready";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateBandsList()
        {
            bandListBox.Items.Clear();
            redToolStripDropDownButton.DropDownItems.Clear();
            greenToolStripDropDownButton.DropDownItems.Clear();
            blueToolStripDropDownButton.DropDownItems.Clear();

            scatterXListBox.Items.Clear();
            scatterYListBox.Items.Clear();
            kdeBandsListBox.Items.Clear();

            if (_bands.Count == 0)
                return;
            else if (_bands.Count >= 3)
            {
                _redBand = _bands[2];
                _greenBand = _bands[1];
                _blueBand = _bands[0];
            }
            else
            {
                _redBand = _bands[0];
                _greenBand = _bands[0];
                _blueBand = _bands[0];
            }

            redToolStripDropDownButton.Text = _redBand.ToString();
            greenToolStripDropDownButton.Text = _greenBand.ToString();
            blueToolStripDropDownButton.Text = _blueBand.ToString();

            foreach (Band band in _bands)
            {
                bandListBox.Items.Add(band);

                redToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _redBand = band; redToolStripDropDownButton.Text = _redBand.ToString(); UpdateImage(sender, e); });
                greenToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _greenBand = band; greenToolStripDropDownButton.Text = _greenBand.ToString(); UpdateImage(sender, e); });
                blueToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _blueBand = band; blueToolStripDropDownButton.Text = _blueBand.ToString(); UpdateImage(sender, e); });

                scatterXListBox.Items.Add(band);
                scatterYListBox.Items.Add(band);
                kdeBandsListBox.Items.Add(band);

                correlationDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = band.Name });
            }

            bandListBox.SelectedIndex = 0;
        }

        private void UpdateImage(object? sender, EventArgs e)
        {
            if (_width == 0 || _height == 0)
                return;

            Bitmap bitmap = new Bitmap(_width, _height);
            Rectangle rect = new Rectangle(0, 0, _width, _height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * _height;
            byte[] rgbValues = new byte[bytes];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;

                    byte r = (byte)(_redBand.GetNormalizedValue(i) * 255);
                    byte g = (byte)(_greenBand.GetNormalizedValue(i) * 255);
                    byte b = (byte)(_blueBand.GetNormalizedValue(i) * 255);

                    int idx = (y * bmpData.Stride) + (x * 4);
                    rgbValues[idx] = b;
                    rgbValues[idx + 1] = g;
                    rgbValues[idx + 2] = r;

                    if (r == 0 && g == 0 && b == 0)
                        rgbValues[idx + 3] = 0;
                    else
                        rgbValues[idx + 3] = 255;
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            viewport1.UpdateImage(bitmap);
        }

        private void ClearFirstStageCache()
        {
            _firstStageEngine?.ClearCache();
            _firstStageEngine = null;
            _firstStageResult = null;
            _firstStageClassStats = null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            ClearFirstStageCache();

            string[] fileNames = openFileDialog1.FileNames;

            // Determine content type
            bool hasText = false, hasImage = false;
            foreach (string fn in fileNames)
            {
                string ext = Path.GetExtension(fn).ToLower();
                if (ext == ".csv" || ext == ".txt") hasText = true;
                else if (ext == ".tif" || ext == ".tiff" || ext == ".img") hasImage = true;
            }

            if (hasText && hasImage)
            {
                MessageBox.Show("Cannot mix text files with image files. Please open them separately.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hasText)
            {
                // Text files are small enough to load on UI thread
                foreach (Band b in _bands) b.UnloadPixelData();
                _bands.Clear();
                _geoTransform = null;
                correlationDataGridView.Columns.Clear();

                foreach (string fileName in fileNames)
                {
                    string ext = Path.GetExtension(fileName).ToLower();
                    if (ext == ".csv")
                        ReadCsvFile(fileName);
                    else
                        ReadTextFile(fileName, '\t');

                    if (_bands.Count == 0) break;
                }

                if (_bands.Count == 0) return;
                UpdateBandsList();
                UpdateImage(sender, e);
                if (!_statsWorker.IsBusy) _statsWorker.RunWorkerAsync();
                return;
            }

            foreach (Band b in _bands) b.UnloadPixelData();
            _bands.Clear();
            _geoTransform = null;
            correlationDataGridView.Columns.Clear();

            if (_loadWorker.IsBusy) return;
            _loadWorker.RunWorkerAsync(new FileLoadInfo(fileNames, '\t'));
        }

        private record FileLoadInfo(string[] FileNames, char CsvDelimiter);
        private record FileLoadResult(List<Band> Bands, GeoTransform? GeoTransform, int Width, int Height, List<string> Messages);

        private void LoadWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            var info = (FileLoadInfo)e.Argument!;
            var worker = (BackgroundWorker)sender!;
            var bands = new List<Band>();
            GeoTransform? geoTransform = null;
            int width = 0, height = 0;
            var messages = new List<string>();

            int fileIndex = 0;
            foreach (string fileName in info.FileNames)
            {
                string ext = Path.GetExtension(fileName).ToLower();
                worker.ReportProgress(fileIndex * 100 / info.FileNames.Length, $"Loading {Path.GetFileName(fileName)}...");

                if (ext == ".tif" || ext == ".tiff" || ext == ".img")
                {
                    var (newBands, newGeo, newWidth, newHeight) = LoadGeoTiffCore(fileName, geoTransform, bands, messages);
                    bands.AddRange(newBands);
                    if (newGeo != null)
                    {
                        geoTransform ??= newGeo;
                        width = newWidth;
                        height = newHeight;
                    }
                }
                else if (ext == ".csv" || ext == ".txt")
                {
                    if (geoTransform != null)
                    {
                        messages.Add($"Cannot mix text files with image files. Skipping '{fileName}'.");
                        continue;
                    }
                    char delim = ext == ".csv" ? info.CsvDelimiter : '\t';
                    var (newBands, newGeo, newWidth, newHeight) = ReadTextFileCore(fileName, delim, messages);
                    bands.AddRange(newBands);
                    geoTransform = newGeo;
                    width = newWidth;
                    height = newHeight;
                }

                fileIndex++;
            }

            e.Result = new FileLoadResult(bands, geoTransform, width, height, messages);
        }

        private void LoadWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Error loading files: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = (FileLoadResult)e.Result!;

            foreach (string msg in result.Messages)
                MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result.Bands.Count == 0)
                return;

            _bands = result.Bands;
            _geoTransform = result.GeoTransform;
            _width = result.Width;
            _height = result.Height;

            UpdateBandsList();
            UpdateImage(this, EventArgs.Empty);

            if (!_statsWorker.IsBusy)
                _statsWorker.RunWorkerAsync();
        }

        private (List<Band> bands, GeoTransform? geo, int width, int height) LoadGeoTiffCore(string fileName, GeoTransform? existingGeo, List<Band> existingBands, List<string> messages)
        {
            var bands = new List<Band>();
            GeoTransform? geo = null;
            int width = 0, height = 0;

            try
            {
                using (OSGeo.GDAL.Dataset ds = OSGeo.GDAL.Gdal.Open(fileName, OSGeo.GDAL.Access.GA_ReadOnly))
                {
                    if (ds == null)
                    {
                        messages.Add($"Error: Cannot open GeoTIFF file: {fileName}");
                        return (bands, null, 0, 0);
                    }

                    width = ds.RasterXSize;
                    height = ds.RasterYSize;

                    double[] geoTransformArr = new double[6];
                    ds.GetGeoTransform(geoTransformArr);
                    var newGeoTransform = GeoTransform.FromGdalArray(geoTransformArr);
                    newGeoTransform.ProjectionWkt = ds.GetProjection();
                    newGeoTransform.ProjectionName = ds.GetProjectionRef();

                    if (existingGeo != null)
                    {
                        if (!existingGeo.Equals(newGeoTransform))
                        {
                            messages.Add($"Error: GeoTransform mismatch in '{Path.GetFileName(fileName)}'!");
                            return (bands, null, 0, 0);
                        }

                        if (!string.IsNullOrEmpty(existingGeo.ProjectionWkt) && !string.IsNullOrEmpty(newGeoTransform.ProjectionWkt))
                        {
                            if (existingGeo.ProjectionWkt != newGeoTransform.ProjectionWkt)
                            {
                                messages.Add($"Error: Projection mismatch in '{Path.GetFileName(fileName)}'!");
                                return (bands, null, 0, 0);
                            }
                        }
                    }

                    geo = newGeoTransform;

                    if (existingGeo == null && existingBands.Count > 0 && existingBands[0].GeoTransform == null)
                    {
                        messages.Add("Cannot mix text files with image files.");
                        return (bands, null, 0, 0);
                    }

                    for (int i = 1; i <= ds.RasterCount; i++)
                    {
                        using (GdalBand gdalBand = ds.GetRasterBand(i))
                        {
                            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                            string bandName = ExtractShortLandsatName(fileNameWithoutExt);
                            string desc;

                            if (ds.RasterCount == 1)
                                desc = ExtractDescription(fileName) ?? gdalBand.GetDescription();
                            else
                                desc = gdalBand.GetDescription();

                            if (!string.IsNullOrWhiteSpace(desc))
                                bandName += $"_{desc}";
                            else if (ds.RasterCount > 1)
                                bandName += $"_{i}";

                            gdalBand.ComputeStatistics(false, out double min, out double max, out double mean, out double stdev, null, null);

                            double gdalNoDataValue = 0;
                            int hasNoData = 0;
                            gdalBand.GetNoDataValue(out gdalNoDataValue, out hasNoData);

                            Band band = new Band(bandName);
                            band.SetDimensions(width, height);
                            band.SetGeoTransform(geo);
                            band.SetStats((float)min, (float)max, (float)mean, (float)stdev);
                            band.SetSource(fileName, i, gdalNoDataValue, hasNoData != 0);

                            bands.Add(band);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                messages.Add($"Error loading GeoTIFF '{fileName}': {ex.Message}");
            }

            return (bands, geo, width, height);
        }

        private (List<Band> bands, GeoTransform? geo, int width, int height) ReadTextFileCore(string fileName, char delimiter, List<string> messages)
        {
            var bands = new List<Band>();
            GeoTransform? geo = null;
            int width = 0, height = 0;

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    if (reader.EndOfStream)
                    {
                        messages.Add("Error: Empty text file.");
                        return (bands, null, 0, 0);
                    }

                    string? headerLine = reader.ReadLine();
                    if (headerLine == null)
                    {
                        messages.Add("Error: Empty text file.");
                        return (bands, null, 0, 0);
                    }
                    string[] hd = headerLine.Split(delimiter);

                    // Determine column types
                    int xIndex = -1, yIndex = -1;
                    List<int> bandIndices = new List<int>();
                    for (int i = 0; i < hd.Length; i++)
                    {
                        string lower = hd[i].ToLower();
                        if (lower == "x" || lower == "longitude") xIndex = i;
                        else if (lower == "y" || lower == "latitude") yIndex = i;
                        else if (lower != "") bandIndices.Add(i);
                    }

                    if (xIndex < 0 || yIndex < 0 || bandIndices.Count == 0)
                    {
                        messages.Add("Error: X, Y and at least one Band column are required in text file.");
                        return (bands, null, 0, 0);
                    }

                    // Read all data
                    string[] values;
                    int lineNumber = 1;
                    double minX = double.MaxValue, maxX = double.MinValue;
                    double minY = double.MaxValue, maxY = double.MinValue;
                    List<(double x, double y, Dictionary<string, float> bandValues)> rawData = new();

                    while (!reader.EndOfStream)
                    {
                        lineNumber++;
                        values = reader.ReadLine()?.Split(delimiter) ?? [];
                        if (values.Length <= Math.Max(xIndex, Math.Max(yIndex, bandIndices.Max())))
                            continue;

                        if (!double.TryParse(values[xIndex], out double x) || !double.TryParse(values[yIndex], out double y))
                            continue;

                        minX = Math.Min(minX, x);
                        maxX = Math.Max(maxX, x);
                        minY = Math.Min(minY, y);
                        maxY = Math.Max(maxY, y);

                        Dictionary<string, float> bandValues = new();
                        foreach (int bandIdx in bandIndices)
                        {
                            if (float.TryParse(values[bandIdx], out float v))
                                bandValues[hd[bandIdx]] = v;
                        }

                        if (bandValues.Count > 0)
                            rawData.Add((x, y, bandValues));
                    }

                    if (rawData.Count == 0)
                    {
                        messages.Add("No valid data found in text file.");
                        return (bands, null, 0, 0);
                    }

                    foreach (int bandIdx in bandIndices)
                        bands.Add(new Band(hd[bandIdx]));

                    double cellSize = Math.Min((maxX - minX) / rawData.Count, (maxY - minY) / rawData.Count);
                    if (cellSize <= 0) cellSize = 1.0;

                    width = Math.Max(1, (int)((maxX - minX) / cellSize));
                    height = Math.Max(1, (int)((maxY - minY) / cellSize));

                    foreach (Band band in bands)
                    {
                        band.SetDimensions(width, height);
                        band.SetGeoTransform(new GeoTransform(minX, maxY, cellSize, -cellSize));
                    }

                    // Grid the data
                    foreach (var point in rawData)
                    {
                        int gridX = (int)Math.Floor((point.x - minX) / cellSize);
                        int gridY = (int)Math.Floor((maxY - point.y) / cellSize);
                        gridX = Math.Clamp(gridX, 0, width - 1);
                        gridY = Math.Clamp(gridY, 0, height - 1);
                        int flatIndex = gridY * width + gridX;

                        foreach (Band band in bands)
                        {
                            if (point.bandValues.TryGetValue(band.Name, out float val))
                                band.SetValueAt(flatIndex, val);
                        }
                    }

                    geo = new GeoTransform(minX, maxY, cellSize, cellSize);
                }
            }
            catch (Exception ex)
            {
                messages.Add($"Error reading text file '{fileName}': {ex.Message}");
            }

            return (bands, geo, width, height);
        }

        private void ReadCsvFile(string fileName)
        {
            var delimSelector = new DelimiterSelector();
            if (delimSelector.ShowDialog() != DialogResult.OK)
                return;

            char delimiter = delimSelector.SelectedDelimiter switch
            {
                DelimiterType.Comma => ',',
                DelimiterType.Semicolon => ';',
                _ => '\t'
            };

            ReadTextFile(fileName, delimiter);
        }

        private void LoadGeoTiff(string fileName)
        {
            try
            {
                using (OSGeo.GDAL.Dataset ds = OSGeo.GDAL.Gdal.Open(fileName, OSGeo.GDAL.Access.GA_ReadOnly))
                {
                    if (ds == null)
                    {
                        MessageBox.Show($"Error: Cannot open GeoTIFF file: {fileName}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int width = ds.RasterXSize;
                    int height = ds.RasterYSize;

                    double[] geoTransform = new double[6];
                    ds.GetGeoTransform(geoTransform);

                    GeoTransform newGeoTransform = GeoTransform.FromGdalArray(geoTransform);
                    newGeoTransform.ProjectionWkt = ds.GetProjection();
                    newGeoTransform.ProjectionName = ds.GetProjectionRef();

                    if (_geoTransform != null)
                    {
                        if (!_geoTransform.Equals(newGeoTransform))
                        {
                            MessageBox.Show($"Error: GeoTransform mismatch!\n\nFile: {Path.GetFileName(fileName)}\nExpected: Origin=({_geoTransform.OriginX}, {_geoTransform.OriginY}), PixelSize=({_geoTransform.PixelSizeX}, {_geoTransform.PixelSizeY})\nGot: Origin=({newGeoTransform.OriginX}, {newGeoTransform.OriginY}), PixelSize=({newGeoTransform.PixelSizeX}, {newGeoTransform.PixelSizeY})",
                                "GeoTransform Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!string.IsNullOrEmpty(_geoTransform.ProjectionWkt) && !string.IsNullOrEmpty(newGeoTransform.ProjectionWkt))
                        {
                            if (_geoTransform.ProjectionWkt != newGeoTransform.ProjectionWkt)
                            {
                                MessageBox.Show($"Error: Projection mismatch!\n\nFile: {Path.GetFileName(fileName)}\nExpected: {_geoTransform.ProjectionName}\nGot: {newGeoTransform.ProjectionName}",
                                    "Projection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        _geoTransform = newGeoTransform;
                        _width = width;
                        _height = height;
                        _cellSize = Math.Abs(_geoTransform.PixelSizeX);
                    }

                    for (int i = 1; i <= ds.RasterCount; i++)
                    {
                        using (GdalBand gdalBand = ds.GetRasterBand(i))
                        {
                            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

                            string bandName = ExtractShortLandsatName(fileNameWithoutExt);
                            string desc;

                            if (ds.RasterCount == 1)
                                desc = ExtractDescription(fileName) ?? gdalBand.GetDescription();
                            else
                                desc = gdalBand.GetDescription();

                            if (!string.IsNullOrWhiteSpace(desc))
                                bandName += $"_{desc}";
                            else if (ds.RasterCount > 1)
                                bandName += $"_{i}";

                            gdalBand.ComputeStatistics(false, out double min, out double max, out double mean, out double stdev, null, null);

                            float[] values = new float[width * height];
                            gdalBand.ReadRaster(0, 0, width, height, values, width, height, 0, 0);

                            double gdalNoDataValue = 0;
                            int hasNoData = 0;
                            gdalBand.GetNoDataValue(out gdalNoDataValue, out hasNoData);
                            bool hasNoDataValue = hasNoData != 0;

                            Band band = new Band(bandName);
                            band.SetDimensions(width, height);
                            band.SetGeoTransform(_geoTransform);
                            band.SetStats((float)min, (float)max, (float)mean, (float)stdev);
                            for (int idx = 0; idx < values.Length; idx++)
                            {
                                float v = values[idx];
                                if (hasNoDataValue && (v == (float)gdalNoDataValue || double.IsNaN(v)))
                                    v = float.NaN;
                                band.SetValueAt(idx, v);
                            }
                            _bands.Add(band);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading GeoTIFF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExtractShortLandsatName(string fileName)
        {
            var match = Regex.Match(fileName, @"(LC\d\d)?[_]?(?:\w{4})?[_](\d{6})?[_](\d{8})", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return string.Join('_', (match.Groups[1].Value ?? ""), (match.Groups[2].Value ?? ""), (match.Groups[3].Value ?? ""));
            }

            return fileName;
        }

        private string? ExtractDescription(string fileName)
        {
            // Попытка определить описание по имени файла (Landsat band naming)
            var bandNumMatch = Regex.Match(fileName, @"[Bb](?:and)?[_]?(\d+)", RegexOptions.IgnoreCase);
            if (bandNumMatch.Success)
            {
                int bandNum = int.Parse(bandNumMatch.Groups[1].Value);
                return bandNum switch
                {
                    1 => "Costal",
                    2 => "Blue",
                    3 => "Green",
                    4 => "Red",
                    5 => "NIR",
                    6 => "SWIR_1",
                    7 => "SWIR_2",
                    8 => "Panchromatic",
                    9 => "Cirrus",
                    10 => "ThermalInfrared_1",
                    11 => "ThermalInfrared_2",
                    _ => bandNum.ToString(),
                };
            }
            return null;
        }

        private void ReadTextFile(string fileName, char delimiter = '\t')
        {
            StreamReader reader = new StreamReader(fileName);

            List<string> hd = new List<string>();

            foreach (string s in reader.ReadLine().Split(delimiter))
            {
                hd.Add(s);
            }

            TextTableColumnSelector columnSelector = new TextTableColumnSelector(hd);

            if (columnSelector.ShowDialog() == DialogResult.Cancel)
                return;

            _cellSize = columnSelector.GetResolution();

            List<TextTableColumnSelector.FieldType> fieldTypes = columnSelector.GetFieldTypes();

            int xIndex = fieldTypes.IndexOf(TextTableColumnSelector.FieldType.X);
            int yIndex = fieldTypes.IndexOf(TextTableColumnSelector.FieldType.Y);

            List<int> bandIndices = new List<int>();
            for (int i = 0; i < fieldTypes.Count; i++)
            {
                if (fieldTypes[i] == TextTableColumnSelector.FieldType.Band)
                    bandIndices.Add(i);
            }

            string[] values;
            int lineNumber = 1;

            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            List<(double x, double y, Dictionary<string, float> bands)> rawData = new();

            while (!reader.EndOfStream)
            {
                lineNumber++;
                values = reader.ReadLine().Split(delimiter);

                if (xIndex < 0 || yIndex < 0 || bandIndices.Count == 0)
                {
                    MessageBox.Show("Error: X, Y and at least one Band column are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(values[xIndex], out double x) || !double.TryParse(values[yIndex], out double y))
                {
                    MessageBox.Show($"Warning: Cannot parse coordinates at line {lineNumber}. Skipping.", "Parse Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                minX = Math.Min(minX, x);
                maxX = Math.Max(maxX, x);
                minY = Math.Min(minY, y);
                maxY = Math.Max(maxY, y);

                Dictionary<string, float> bandValues = new();

                foreach (int bandIdx in bandIndices)
                {
                    if (!float.TryParse(values[bandIdx], out float v))
                    {
                        MessageBox.Show($"Warning: Cannot parse value '{values[bandIdx]}' at line {lineNumber}, column {bandIdx + 1}. Skipping.", "Parse Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    bandValues[hd[bandIdx]] = v;
                }

                rawData.Add((x, y, bandValues));
            }

            reader.Close();

            if (rawData.Count == 0)
            {
                MessageBox.Show("No valid data found in file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (int bandIdx in bandIndices)
            {
                Band band = new Band(hd[bandIdx]);
                _bands.Add(band);
            }

            double dataRangeX = maxX - minX;
            double dataRangeY = maxY - minY;

            if (dataRangeX == 0 || dataRangeY == 0)
            {
                MessageBox.Show("Error: Data has zero range in X or Y direction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _width = Math.Max(1, (int)(dataRangeX / _cellSize));
            _height = Math.Max(1, (int)(dataRangeY / _cellSize));

            Dictionary<(int x, int y), Dictionary<string, float>> gridData = new();

            foreach (var point in rawData)
            {
                int gridX = (int)Math.Floor((point.x - minX) / _cellSize);
                int gridY = (int)Math.Floor((maxY - point.y) / _cellSize);

                gridX = Math.Clamp(gridX, 0, _width - 1);
                gridY = Math.Clamp(gridY, 0, _height - 1);

                if (!gridData.ContainsKey((gridX, gridY)))
                    gridData[(gridX, gridY)] = new Dictionary<string, float>();

                foreach (var kvp in point.bands)
                {
                    if (gridData[(gridX, gridY)].ContainsKey(kvp.Key))
                        gridData[(gridX, gridY)][kvp.Key] = (gridData[(gridX, gridY)][kvp.Key] + kvp.Value) / 2;
                    else
                        gridData[(gridX, gridY)][kvp.Key] = kvp.Value;
                }
            }

            foreach (Band band in _bands)
            {
                band.SetDimensions(_width, _height);
                band.SetGeoTransform(new GeoTransform(minX, maxY, _cellSize, -_cellSize));
            }

            foreach (var cell in gridData)
            {
                int flatIndex = cell.Key.y * _width + cell.Key.x;

                foreach (Band band in _bands)
                {
                    if (cell.Value.TryGetValue(band.Name, out float val))
                    {
                        band.SetValueAt(flatIndex, val);
                    }
                }
            }

            foreach (Band band in _bands)
            {
                BandStatisticsComputer.Compute(band);
            }

            _geoTransform = new GeoTransform(minX, maxY, _cellSize, _cellSize);
        }

        private Band? GetBand(string name, bool createIsNull)
        {
            foreach (Band band in _bands)
            {
                if (band.Name == name)
                    return band;
            }

            if (createIsNull)
            {
                Band band = new Band(name);
                _bands.Add(band);
                return band;
            }

            return null;
        }

        private void bandListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Band? band = bandListBox.SelectedItem as Band;

            if (band == null)
                return;

            bandPropertyGrid.SelectedObject = band;

            BuildHistogram();
        }

        private void BuildHistogram()
        {
            Band? band = bandListBox.SelectedItem as Band;
            if (band == null || band.OriginalWidth * band.OriginalHeight == 0)
                return;

            int columnsCount = BrooksCarrutherDivisionRule(band.Count);
            float minVal = band.Minimum;
            float maxVal = band.Maximum;
            float range = maxVal - minVal;
            if (range <= 0 || columnsCount <= 0)
                return;

            float columnsWidth = range / columnsCount;

            mainStatusLabel.Text = "Building histogram...";

            Task.Run(() =>
            {
                int[] pointses = new int[columnsCount];
                int[] asseses = new int[columnsCount];
                int totalPixels = band.OriginalWidth * band.OriginalHeight;

                for (int i = 0; i < totalPixels; i++)
                {
                    float x = band.GetPixelValue(i);
                    if (float.IsNaN(x)) continue;

                    int binIndex = (int)((x - minVal) / columnsWidth);
                    if (binIndex >= 0 && binIndex < columnsCount)
                        pointses[binIndex]++;
                }

                int cumulative = 0;
                for (int j = 0; j < columnsCount; j++)
                {
                    cumulative += pointses[j];
                    asseses[j] = cumulative;
                }

                BeginInvoke(() =>
                {
                    var histSeries = new HistogramSeries();
                    var lineSeries = new LineSeries();

                    for (int i = 0; i < columnsCount; i++)
                    {
                        float binMin = i * columnsWidth + minVal;
                        histSeries.Items.Add(new HistogramItem(binMin, binMin + columnsWidth, (float)pointses[i] / band.Count, pointses[i]));
                        lineSeries.Points.Add(new DataPoint(binMin, (float)asseses[i] / band.Count));
                    }

                    var plot = new PlotModel();
                    plot.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = minVal, Maximum = maxVal });
                    plot.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Key = "axesY1" });
                    plot.Axes.Add(new LinearAxis { Position = AxisPosition.Right, Minimum = 0, Maximum = 1d, Key = "axesY2" });

                    histSeries.YAxisKey = "axesY1";
                    lineSeries.YAxisKey = "axesY2";
                    lineSeries.Color = OxyColor.FromRgb(255, 0, 0);

                    plot.Series.Add(histSeries);
                    plot.Series.Add(lineSeries);

                    histogramPlotView.Model = plot;
                    mainStatusLabel.Text = "Ready";
                });
            });
        }

        private float[][]? CalcCorrelationData(List<Band> bands)
        {
            int n = bands.Count;
            var data = new float[n][];

            for (int bandXI = 0; bandXI < n; bandXI++)
            {
                data[bandXI] = new float[n];
                Band bandX = bands[bandXI];
                int totalPixels = bandX.OriginalWidth * bandX.OriginalHeight;

                for (int bandYI = 0; bandYI < n; bandYI++)
                {
                    if (bandYI < bandXI)
                    {
                        data[bandXI][bandYI] = data[bandYI][bandXI];
                        continue;
                    }

                    if (bandYI == bandXI)
                    {
                        data[bandXI][bandYI] = float.NaN;
                        continue;
                    }

                    Band bandY = bands[bandYI];

                    double sum = 0;
                    int validPairs = 0;
                    for (int i = 0; i < totalPixels; i++)
                    {
                        float vx = bandX.GetPixelValue(i);
                        float vy = bandY.GetPixelValue(i);
                        if (!float.IsNaN(vx) && !float.IsNaN(vy))
                        {
                            sum += F(vx, bandX.Mean, bandX.StDev) * F(vy, bandY.Mean, bandY.StDev);
                            validPairs++;
                        }
                    }

                    data[bandXI][bandYI] = validPairs > 1 ? (float)(sum / (validPairs - 1)) : float.NaN;
                }
            }

            return data;

            static float F(float v, float mean, float sigma)
            {
                return sigma != 0 ? (v - mean) / sigma : 0;
            }
        }

        private void UpdateCorrelationGrid(float[][] data)
        {
            correlationDataGridView.Rows.Clear();
            correlationDataGridView.Columns.Clear();

            for (int i = 0; i < data.Length; i++)
                correlationDataGridView.Columns.Add($"col{i}", _bands[i].Name);

            for (int bandXI = 0; bandXI < data.Length; bandXI++)
            {
                var row = new DataGridViewRow();
                Band bandX = _bands[bandXI];

                for (int bandYI = 0; bandYI < data.Length; bandYI++)
                {
                    if (float.IsNaN(data[bandXI][bandYI]))
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell
                        {
                            Style = bandXI == bandYI
                                ? new DataGridViewCellStyle { BackColor = Color.Black }
                                : new DataGridViewCellStyle { BackColor = Color.Red }
                        });
                    }
                    else
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell { Value = data[bandXI][bandYI] });
                    }
                }

                int rowId = correlationDataGridView.Rows.Add(row);
                correlationDataGridView.Rows[rowId].HeaderCell.Value = bandX.Name;
            }
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == histogramTabPage)
                BuildHistogram();
        }

        private void PlotView_DoubleClick(object sender, EventArgs e)
        {
            PlotView plotView = (PlotView)sender;

            plotView.Model.ResetAllAxes();
            plotView.Refresh();
        }

        private int StargesDivisionRule(int v)
        {
            return (int)(Math.Log(v) / Math.Log(2) + 1);
        }

        private int BrooksCarrutherDivisionRule(int v)
        {
            return (int)(5 * Math.Log(v));
        }

        private int HeinholdHeideDivisionRule(int v)
        {
            return (int)(Math.Sqrt(v));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_secondStageRules.Count == 0)
            {
                MessageBox.Show("No classification rules defined for Second stage. Please add rules first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_bands.Count == 0)
            {
                MessageBox.Show("No bands loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_firstStageResult == null || _firstStageEngine == null || _firstStageClassStats == null)
            {
                MessageBox.Show("Please run First stage classification (DirectCheck) first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            backgroundWorker.RunWorkerAsync((_secondStageRules, ClassificationMode.RulePerClass, viewport2, true));
        }

        private void FirstStageClassify_Click(object? sender, EventArgs e)
        {
            if (_firstStageRules.Count == 0)
            {
                MessageBox.Show("No classification rules defined for First stage. Please generate rules first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_bands.Count == 0)
            {
                MessageBox.Show("No bands loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClassificationMode mode = ClassificationModeToolStripComboBox.SelectedItem?.ToString() == "DirectCheck"
                ? ClassificationMode.DirectCheck
                : ClassificationMode.RulePerClass;

            backgroundWorker.RunWorkerAsync((_firstStageRules, mode, viewport3, false));
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;

            if (worker == null || _bands.Count == 0)
                return;

            if (e.Argument is not (List<ClassificationRule> rules, ClassificationMode mode, Viewport _, bool isSecondStage))
                return;

            if (rules.Count == 0)
                return;

            RunClassificationWork(worker, rules, mode, isSecondStage, e);
        }

        private void RunClassificationWork(BackgroundWorker worker, List<ClassificationRule> rules, ClassificationMode mode, bool isSecondStage, DoWorkEventArgs e)
        {
            int totalPixels = _width * _height;
            DateTime startTime = DateTime.Now;
            string stageName = isSecondStage ? "Second stage" : "Classification";
            worker.ReportProgress(0, $"Starting {stageName}...");

            var engine = new ClassificationEngine(_bands, rules);
            engine.Mode = isSecondStage ? ClassificationMode.RulePerClass : mode;

            int firstStageClassCount = 0;
            Color[]? firstStagePalette = null;
            int[]? firstStageClassIndices = null;

            if (isSecondStage)
            {
                if (_firstStageEngine?.ZScoreCache != null)
                    engine.UseZScoreCache(
                        _firstStageEngine.ZScoreCache,
                        _firstStageEngine.CachedBandCount,
                        _firstStageEngine.CachedPixelCount);

                if (_firstStageResult != null && _firstStageClassStats != null)
                    engine.SetFirstStageContext(_firstStageResult.ClassIndices, _firstStageClassStats);

                firstStageClassCount = _firstStageResult?.Palette?.Length
                    ?? _firstStageResult?.Rules?.Count
                    ?? 0;
                firstStagePalette = _firstStageResult?.Palette;
                firstStageClassIndices = _firstStageResult?.ClassIndices;
            }
            else if (mode == ClassificationMode.DirectCheck)
            {
                engine.EnsureZScoreCache(_bands.Count, totalPixels);
            }

            int secondStageRuleCount = isSecondStage ? rules.Count : 0;
            int totalClassCount = isSecondStage
                ? firstStageClassCount * secondStageRuleCount
                : mode == ClassificationMode.DirectCheck
                    ? 1 << rules.Count
                    : rules.Count;

            Color[] palette = isSecondStage
                ? (firstStagePalette != null
                    ? PaletteGenerator.GenerateSecondStage(firstStagePalette, secondStageRuleCount)
                    : PaletteGenerator.GenerateHSV(totalClassCount))
                : PaletteGenerator.GenerateHSV(totalClassCount);

            var classificationResult = new ClassificationResult(_width, _height, palette);

            int processedPixels = 0;

            Parallel.For(0, totalPixels, () => 0, (pixelIndex, loopState, localCount) =>
            {
                if (firstStageClassIndices != null)
                {
                    int firstClass = firstStageClassIndices[pixelIndex];
                    if (firstClass < 0 || firstClass >= firstStageClassCount)
                        return localCount;
                }

                int? classIndex = engine.EvaluatePixel(pixelIndex);

                if (classIndex.HasValue)
                {
                    int finalClass = isSecondStage
                        ? firstStageClassIndices![pixelIndex] * secondStageRuleCount + classIndex.Value
                        : classIndex.Value;
                    classificationResult.SetClass(pixelIndex, finalClass);
                }

                return localCount + 1;
            },
            localCount =>
            {
                int finalCount = Interlocked.Add(ref processedPixels, localCount);
                int progress = totalPixels > 0 ? (int)((long)finalCount * 99 / totalPixels) : 0;

                TimeSpan elapsed = DateTime.Now - startTime;
                double pixelsPerMs = finalCount / Math.Max(1.0, elapsed.TotalMilliseconds);
                int remainingPixels = totalPixels - finalCount;
                double remainingMsDouble = remainingPixels / Math.Max(0.01, pixelsPerMs);
                TimeSpan remaining = TimeSpan.FromMilliseconds(remainingMsDouble);
                string remainingStr = remaining.TotalSeconds < 60
                    ? $"~{remaining.TotalSeconds:F0}s"
                    : $"~{remaining.TotalMinutes:F1}m";

                worker.ReportProgress(progress, $"{stageName}: {finalCount}/{totalPixels} ({progress}%) ETA: {remainingStr}");
            });

            worker.ReportProgress(99, "Rendering bitmap...");

            Bitmap bitmap = RenderClassificationBitmap(classificationResult);

            worker.ReportProgress(100, "Complete");

            if (isSecondStage)
            {
                e.Result = (bitmap, classificationResult, ClassificationMode.RulePerClass, true);
            }
            else
            {
                _firstStageEngine = engine;
                _firstStageResult = classificationResult;

                if (engine.ZScoreCache != null)
                {
                    _firstStageClassStats = ClassStatistics.ComputeFromResult(
                        classificationResult, _bands, engine.ZScoreCache,
                        engine.CachedPixelCount, _width, _height);
                }

                e.Result = (bitmap, classificationResult, mode, false);
            }
        }

        private Bitmap RenderClassificationBitmap(ClassificationResult classificationResult)
        {
            Bitmap bitmap = new Bitmap(_width, _height);
            Rectangle rect = new Rectangle(0, 0, _width, _height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * _height;
            byte[] rgbValues = new byte[bytes];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;
                    Color color = ResultRenderer.GetPixelColor(classificationResult, i);

                    int idx = (y * bmpData.Stride) + (x * 4);
                    rgbValues[idx] = color.B;
                    rgbValues[idx + 1] = color.G;
                    rgbValues[idx + 2] = color.R;
                    rgbValues[idx + 3] = color.A;
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            return bitmap;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                MessageBox.Show($"An error occurred: {e.Error.Message}");
            }
            else
            {
                if (e.Result is (Bitmap bitmap, ClassificationResult classificationResult, ClassificationMode mode, bool isSecondStage))
                {
                    _lastClassificationResult = classificationResult;

                    if (isSecondStage)
                    {
                        viewport2.UpdateImage(bitmap);
                    }
                    else
                    {
                        viewport3.UpdateImage(bitmap);
                    }

                    if (isSecondStage)
                        PopulateSecondAnalysisTab();
                    else
                        PopulatePaletteTab();

                    var stats = classificationResult.GetClassStatistics();
                    string summary = $"Classification complete — {stats.GetValueOrDefault(-1, 0)} undefined pixels";
                    mainStatusLabel.Text = summary;
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            mainStatusLabel.Text = e.UserState?.ToString();
            mainProgressBar.Value = e.ProgressPercentage;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (ruleDataGridView.SelectedRows.Count == 0)
                conditionsRichTextBox.Text = "No rule select";
            else
                conditionsRichTextBox.Text = GetRulePreview(_secondStageRules[ruleDataGridView.Rows.IndexOf(ruleDataGridView.SelectedRows[0])]);
        }

        private string GetRulePreview(ClassificationRule rule)
        {
            if (rule.Conditions.Count == 0)
                return "No conditions";

            var parts = rule.Conditions.Select(c => new ConditionDisplayItem(c, _bands).Display);
            return string.Join('\n', parts);
        }

        private void compareToolStripButton_Click(object sender, EventArgs e)
        {
            TwoImageViewForm twoImageView = new TwoImageViewForm(viewport1.Image, tabControl2.SelectedIndex == 0 ? viewport3.Image : viewport2.Image);

            twoImageView.ShowDialog(this);
        }

        private void FirstGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                richTextBox1.Text = "No rule select";
            else
                richTextBox1.Text = GetRulePreview(_firstStageRules[dataGridView1.Rows.IndexOf(dataGridView1.SelectedRows[0])]);
        }

        private void FirstGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 1)
            {
                if (e.RowIndex < _firstStageRules.Count)
                {
                    var rule = _firstStageRules[e.RowIndex];
                    var editor = new RuleEditorForm(_bands, rule);
                    if (editor.ShowDialog(this) == DialogResult.OK)
                        UpdateFirstStageRulesGrid();
                }
            }
        }

        private void FirstGrid_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                int x = e.X;
                int y = e.Y;
                for (int i = 0; i < e.ColumnIndex; i++)
                    x += dataGridView1.Columns[i].Width;
                for (int i = 0; i < e.RowIndex; i++)
                    y += dataGridView1.Rows[e.RowIndex].Height;
                ruleContextMenuStrip.Show(dataGridView1, x, y);
            }
        }

        private void ChangeFirstRuleColor(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _firstStageRules.Count) return;

            var rule = _firstStageRules[rowIndex];
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = rule.Color;
                if (colorDialog.ShowDialog(this) == DialogResult.OK)
                {
                    rule.Color = colorDialog.Color;
                }
            }

            UpdateFirstStageRulesGrid();
            if (rowIndex < dataGridView1.Rows.Count)
                dataGridView1.Rows[rowIndex].Selected = true;
        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            BandSelectionForm selectionForm = new BandSelectionForm(_bands);

            if (selectionForm.ShowDialog(this) == DialogResult.OK)
            {
                List<Band> selectedBands = selectionForm.Result;
                if (selectedBands.Count == 0)
                    return;

                _firstStageRules.Clear();

                foreach (Band band in selectedBands)
                {
                    int bandIndex = _bands.IndexOf(band);
                    if (bandIndex < 0) continue;

                    var rule = new ClassificationRule
                    {
                        Name = $"z({band.Name}) >= 0",
                        Color = Color.Gray,
                        IsEnabled = true
                    };

                    var condition = new Condition
                    {
                        LeftDensityType = DensityType.ChannelZScore,
                        LeftSingleBandIndex = bandIndex,
                        Operator = ComparisonOperator.GreaterOrEqual,
                        RightSide = new CompareTarget { ConstantValue = 0 }
                    };

                    rule.Conditions.Add(condition);
                    _firstStageRules.Add(rule);
                }

                ClassificationModeToolStripComboBox.SelectedItem = "DirectCheck";
                UpdateFirstStageRulesGrid();
            }
        }

        private void UpdateFirstStageRulesGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var rule in _firstStageRules)
            {
                int rowIndex = dataGridView1.Rows.Add();
                var row = dataGridView1.Rows[rowIndex];
                row.Cells[0].Value = rule.GenerateName();
            }
        }

        public void ExportActivePlot(object? sender, EventArgs e)
        {
            PlotView? target = null;

            if (mainTabControl.SelectedTab == dataTabPage)
            {
                if (dataTabControl.SelectedTab == histogramTabPage)
                    target = histogramPlotView;
            }
            else if (mainTabControl.SelectedTab == explorationTabPage)
            {
                if (explorationTabControl.SelectedTab == tabPage2)
                    target = kdePlotView;
                else if (explorationTabControl.SelectedTab == tabPage3)
                    target = scatterPlotView;
            }

            if (target == null || target.Model == null)
            {
                MessageBox.Show("No plot available in the active tab.", "Export Graph",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new GraphExportDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            PlotExportService.Export(target.Model, dlg.FilePath, dlg.Options);
        }

        public void ExportKdePlot(object? sender, EventArgs e)
        {
            ExportPlotView(kdePlotView, null);
        }

        public void ExportScatterPlot(object? sender, EventArgs e)
        {
            ExportPlotView(scatterPlotView, null);
        }

        public void ExportHistogramPlot(object? sender, EventArgs e)
        {
            ExportPlotView(histogramPlotView, null);
        }

        private void ExportPlotView(PlotView plotView, string? filePath)
        {
            var model = plotView.Model;
            if (model == null) return;

            using var dlg = new GraphExportDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            PlotExportService.Export(model, dlg.FilePath, dlg.Options);
        }

        public void ExportClassification(object sender, EventArgs e)
        {
            if (_lastClassificationResult == null)
            {
                MessageBox.Show("No classification result available. Run classification first.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new ExportClassificationDialog(
                projectionWkt: _bands?.FirstOrDefault()?.GeoTransform?.ProjectionWkt,
                bands: _bands);
            if (dlg.ShowDialog() != DialogResult.OK) return;

            var result = _lastClassificationResult;

            if (dlg.UseHsvPalette && result.Palette != null)
            {
                result = new ClassificationResult(result.Width, result.Height,
                    PaletteGenerator.GenerateHSV(result.Palette.Length));
                Array.Copy(_lastClassificationResult.ClassIndices, result.ClassIndices, result.ClassIndices.Length);
            }
            else if (dlg.UseGrayscalePalette && result.Palette != null)
            {
                var grayPalette = new Color[result.Palette.Length];
                for (int i = 0; i < grayPalette.Length; i++)
                {
                    byte shade = grayPalette.Length > 1
                        ? (byte)(i * 255 / (grayPalette.Length - 1))
                        : (byte)128;
                    grayPalette[i] = Color.FromArgb(shade, shade, shade);
                }
                result = new ClassificationResult(result.Width, result.Height, grayPalette);
                Array.Copy(_lastClassificationResult.ClassIndices, result.ClassIndices, result.ClassIndices.Length);
            }

            ClassificationExporter.Export(result, dlg.ExportOptions, _bands);
            if (dlg.ExportStatsChecked)
                ClassificationExporter.ExportStats(result, dlg.StatsOptions);

            MessageBox.Show("Export completed successfully.", "Export",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string? GetExportFilePath(string filter)
        {
            using var dlg = new SaveFileDialog { Filter = filter };
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : null;
        }

        private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (sender is DataGridView dataGridView)
                dataGridView.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }
    }
}
