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
using System.Globalization;

namespace modified_structure_analysis.Forms
{
    public partial class MainForm : Form
    {
        private List<Band> _bands = new List<Band>();

        private int _width;
        private int _height;

        private Band _redBand;
        private Band _greenBand;
        private Band _blueBand;

        private GeoTransform? _geoTransform;

        private List<ClassificationRule> _primaryClassificationRules = new();
        private List<ClassificationRule> _secondaryClassificationRules = new();

        private ClassificationEngine? _primaryClassificationEngine;
        private ClassificationResult? _primaryClassificationResult;
        private ClassificationResult? _secondaryClassificationResult;
        private ClassStatistics[]? _primaryClassificationClassStats;
        private HashSet<int>? _secondStageFilterIndices;
        private ClassStatistics[]? _secondaryClassificationClassStats;

        private PlotModel _kdeModel;

        public MainForm()
        {
            InitializeComponent();

            _openFileDialog.Filter = "All|*.tif;*.tiff;*.img;*.png;*.jpg;*.jpeg*.csv;*.txt|GeoTIFF|*.tif;*.tiff|ERDAS|*.png|PNG|*.jpg;*.jpeg|JPEG|*.img|CSV|*.csv|Text file|*.txt";
            _openFileDialog.Multiselect = true;

            _mainStatusLabel.Text = $"{Resources.Open_file} (Ctrl+O)";
            _mainProgressBar.Visible = false;

            _primaryClassificationDataGridView.CellToolTipTextNeeded += ClassificationGrid_CellToolTipTextNeeded;
            _secondaryClassificationDataGridView.CellToolTipTextNeeded += ClassificationGrid_CellToolTipTextNeeded;
            _bandPropertyGrid.PropertyValueChanged += BandPropertyGrid_PropertyValueChanged;

            WireViewportNavButtons();
        }

        private void WireViewportNavButtons()
        {
            WireNavGroup(_dataViewport,
                _dataUpToolStripButton, _dataDownToolStripButton,
                _dataLeftToolStripButton, _dataRightToolStripButton,
                _dataZoomInToolStripButton, _dataZoomOutToolStripButton);

            WireNavGroup(_primaryClassificationViewport,
                _primaryUpToolStripButton, _primaryDownToolStripButton,
                _primaryLeftToolStripButton, _primaryRightToolStripButton,
                _primaryZoomInToolStripButton, _primaryZoomOutToolStripButton);

            WireNavGroup(_secondaryClassificationViewport,
                _secondaryUpToolStripButton, _secondaryDownToolStripButton,
                _secondaryLeftToolStripButton, _secondaryRightToolStripButton,
                _secondaryZoomInToolStripButton, _secondaryZoomOutToolStripButton);
        }

        private static void WireNavGroup(Viewport vp, ToolStripButton up, ToolStripButton down,
            ToolStripButton left, ToolStripButton right, ToolStripButton zoomIn, ToolStripButton zoomOut)
        {
            const float pan = 20f;
            up.Click += (_, _) => vp.PanBy(0, pan);
            down.Click += (_, _) => vp.PanBy(0, -pan);
            left.Click += (_, _) => vp.PanBy(pan, 0);
            right.Click += (_, _) => vp.PanBy(-pan, 0);
            zoomIn.Click += (_, _) => vp.ZoomIn();
            zoomOut.Click += (_, _) => vp.ZoomOut();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var s = AppSettings.Instance;
            _kdeModel = new PlotModel();
            _kdeModel.Axes.Add(new LinearAxis { Key = "X", Position = AxisPosition.Bottom, Title = s.GraphShowAxisLabels ? Resources.Normalized_Value : null, Minimum = 0d, Maximum = 1d });
            _kdeModel.Axes.Add(new LinearAxis { Key = "Y", Position = AxisPosition.Left, Title = s.GraphShowAxisLabels ? Resources.Density : null, Minimum = 0d });
            if (s.GraphShowLegend)
                _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
            _kdePlotView.Model = _kdeModel;
        }

        private void UpdateUI(bool hasProcces)
        {
            _mainProgressBar.Visible = hasProcces;

            _openToolStripMenuItem.Enabled = !hasProcces;
            _exportGraphToolStripMenuItem.Enabled = !hasProcces;
            _exportClassificationToolStripMenuItem.Enabled = !hasProcces;

            _kdeSingleButton.Enabled = !hasProcces;
            _kdeProductButton.Enabled = !hasProcces;
            _kdeMultivariateButton.Enabled = !hasProcces;
            _kdeClearButton.Enabled = !hasProcces;

            _buildScatterButton.Enabled = !hasProcces;

            _primaryClassificationToolStrip.Enabled = !hasProcces;
            _primaryClassificationRuleToolStrip.Enabled = !hasProcces;
            _secondaryClassificationToolStrip.Enabled = !hasProcces;
            _secondaryClassificationRuleToolStrip.Enabled = !hasProcces;

            _primaryClassificationDataGridView.Enabled = !hasProcces;
            _secondaryClassificationDataGridView.Enabled = !hasProcces;

            _primaryClassificationDataGridView.Refresh();
            _secondaryClassificationDataGridView.Refresh();

            _ruleContextMenuStrip.Enabled = !hasProcces;
        }

        private void RuleDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView grid = sender.Equals(_primaryRuleDataGridView) ? _primaryRuleDataGridView : _secondaryRuleDataGridView;

            if (e.Button == MouseButtons.Right)
            {
                grid.Rows[e.RowIndex].Selected = true;

                int x = e.X;
                int y = e.Y;

                for (int i = 0; i < e.ColumnIndex; i++)
                    x += grid.Columns[i].Width;

                for (int i = 0; i < e.RowIndex; i++)
                    y += grid.Rows[e.RowIndex].Height;

                _ruleContextMenuStrip.Show(grid, x, y);
            }
        }

        private void RuleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            List<ClassificationRule> rules = sender.Equals(_primaryRuleDataGridView) ? _primaryClassificationRules : _secondaryClassificationRules;

            if (e.ColumnIndex == 1 && e.RowIndex < rules.Count)
            {
                EditClassificationRule(rules, e.RowIndex, (DataGridView)sender);
            }
        }

        private void UpdateClassificationRulesGrid(DataGridView grid, List<ClassificationRule> rules)
        {
            grid.Rows.Clear();
            foreach (var rule in rules)
            {
                int rowIndex = grid.Rows.Add();
                var row = grid.Rows[rowIndex];

                row.Cells[0].Value = rule.GenerateName();
            }
        }

        private void AddClassificationRule(object sender, EventArgs e)
        {
            DataGridView grid = _primaryRuleDataGridView;
            List<ClassificationRule> rules = _primaryClassificationRules;
            bool isSecondStage = false;

            if (_classificationTabControl.SelectedTab == _secondaryClassificationTabPage)
            {
                grid = _secondaryRuleDataGridView;
                rules = _secondaryClassificationRules;
                isSecondStage = true;
            }

            var editor = new RuleEditorForm(_bands, null, isSecondStage: isSecondStage, classStats: _primaryClassificationClassStats);
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                rules.Add(editor.Rule);
                UpdateClassificationRulesGrid(grid, rules);
                grid.Rows[grid.Rows.GetLastRow(DataGridViewElementStates.None)].Selected = true;
            }
        }

        private void EditClassificationRule(List<ClassificationRule> rules, int ruleIndex, DataGridView grid)
        {
            var editor = new RuleEditorForm(_bands, rules[ruleIndex], grid.Equals(_secondaryRuleDataGridView), classStats: _primaryClassificationClassStats);

            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                UpdateClassificationRulesGrid(grid, rules);
                grid.Rows[ruleIndex].Selected = true;
            }
        }

        private void EditClassificationRule(object sender, EventArgs e)
        {
            DataGridView grid = _primaryRuleDataGridView;
            List<ClassificationRule> rules = _primaryClassificationRules;

            if (_classificationTabControl.SelectedTab == _secondaryClassificationTabPage)
            {
                grid = _secondaryRuleDataGridView;
                rules = _secondaryClassificationRules;
            }

            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.Msg_SelectRuleToEdit, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = grid.SelectedRows[0].Index;

            EditClassificationRule(rules, selectedIndex, grid);
        }

        private void DeleteClassificationRule(object sender, EventArgs e)
        {
            DataGridView grid = _primaryRuleDataGridView;
            List<ClassificationRule> rules = _primaryClassificationRules;

            if (_classificationTabControl.SelectedTab == _secondaryClassificationTabPage)
            {
                grid = _secondaryRuleDataGridView;
                rules = _secondaryClassificationRules;
            }

            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.Msg_SelectRuleToDelete, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = grid.SelectedRows[0].Index;

            var result = MessageBox.Show(
                string.Format(Resources.Msg_ConfirmDeleteRule, selectedIndex + 1),
                Resources.Msg_ConfirmDelete,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                rules.RemoveAt(selectedIndex);
                UpdateClassificationRulesGrid(grid, rules);

                if (rules.Count == 0)
                    return;

                if (selectedIndex >= grid.Rows.Count)
                    selectedIndex = grid.Rows.GetLastRow(DataGridViewElementStates.None);

                grid.Rows[selectedIndex].Selected = true;
            }
        }

        private void CloneClassificationRule(object sender, EventArgs e)
        {
            DataGridView grid = _primaryRuleDataGridView;
            List<ClassificationRule> rules = _primaryClassificationRules;

            if (_classificationTabControl.SelectedTab == _secondaryClassificationTabPage)
            {
                grid = _secondaryRuleDataGridView;
                rules = _secondaryClassificationRules;
            }

            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.Msg_SelectRuleToClone, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = grid.SelectedRows[0].Index;

            var rule = rules[selectedIndex];

            rules.Insert(selectedIndex + 1, (ClassificationRule)rule.Clone());

            UpdateClassificationRulesGrid(grid, rules);

            grid.Rows[selectedIndex + 1].Selected = true;
        }

        private void MoveRuleUp(object sender, EventArgs e)
        {
            DataGridView grid = _primaryRuleDataGridView;
            List<ClassificationRule> rules = _primaryClassificationRules;

            if (_classificationTabControl.SelectedTab == _secondaryClassificationTabPage)
            {
                grid = _secondaryRuleDataGridView;
                rules = _secondaryClassificationRules;
            }

            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.Msg_SelectRuleToMove, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = grid.SelectedRows[0].Index;

            if (selectedIndex == 0)
                return;

            var rule = rules[selectedIndex];
            rules.RemoveAt(selectedIndex);
            rules.Insert(selectedIndex - 1, rule);

            UpdateClassificationRulesGrid(grid, rules);

            grid.Rows[selectedIndex - 1].Selected = true;
        }

        private void MoveRuleDown(object sender, EventArgs e)
        {
            DataGridView grid = _primaryRuleDataGridView;
            List<ClassificationRule> rules = _primaryClassificationRules;

            if (_classificationTabControl.SelectedTab == _secondaryClassificationTabPage)
            {
                grid = _secondaryRuleDataGridView;
                rules = _secondaryClassificationRules;
            }

            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.Msg_SelectRuleToMove, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = grid.SelectedRows[0].Index;

            if (selectedIndex >= rules.Count - 1)
                return;

            var rule = rules[selectedIndex];
            rules.RemoveAt(selectedIndex);
            rules.Insert(selectedIndex + 1, rule);

            UpdateClassificationRulesGrid(grid, rules);

            grid.Rows[selectedIndex + 1].Selected = true;
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

        private void PopelateTableTab(DataGridView grid)
        {
            var result = grid.Equals(_primaryClassificationDataGridView) ? _primaryClassificationResult : _secondaryClassificationResult;
            if (result?.Palette == null) return;

            grid.Rows.Clear();

            var stats = result.GetClassStatistics();
            int totalPixels = result.Width * result.Height;

            for (int i = 0; i < result.Palette.Length; i++)
            {
                int rowIdx = grid.Rows.Add();
                var row = grid.Rows[rowIdx];
                row.Cells[0].Value = CreateColorBitmap(result.Palette[i]);
                row.Cells[1].Value = string.Format(Resources.Label_ClassFormat, i);
                row.Cells[2].Value = stats.GetValueOrDefault(i, 0);
            }

            int undefCount = stats.GetValueOrDefault(-1, 0);
            if (undefCount > 0)
            {
                int rowIdx = grid.Rows.Add();
                var row = grid.Rows[rowIdx];
                row.Cells[0].Value = CreateColorBitmap(Color.Transparent);
                row.Cells[1].Value = Resources.Label_Undefined;
                row.Cells[2].Value = undefCount;
            }
        }

        private void ClassificationGrid_CellToolTipTextNeeded(object? sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
                e.ToolTipText = Resources.Tooltip_ViewClassAnalysis;
        }

        private void ClassificationGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridView? grid = sender as DataGridView;

            if (grid == null) return;

            var result = grid.Equals(_primaryClassificationDataGridView) ? _primaryClassificationResult : _secondaryClassificationResult;

            if (result?.Palette == null) return;

            if (e.RowIndex >= result.Palette.Length) return;

            if (e.ColumnIndex == 0)
            {
                using var dialog = new ColorDialog();
                dialog.Color = result.Palette[e.RowIndex];
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    result.Palette[e.RowIndex] = dialog.Color;

                    if (e.RowIndex < grid.Rows.Count)
                        grid.Rows[e.RowIndex].Cells[0].Value = CreateColorBitmap(dialog.Color);

                    var bitmap = ResultRenderer.ToBitmap(result);
                    (grid.Equals(_primaryClassificationDataGridView) ? _primaryClassificationViewport : _secondaryClassificationViewport).UpdateImage(bitmap);
                }
            }

            if (e.ColumnIndex == 3)
            {
                var stats = grid.Equals(_primaryClassificationDataGridView)
                    ? _primaryClassificationClassStats
                    : _secondaryClassificationClassStats;

                if (stats == null)
                {
                    MessageBox.Show(Resources.Msg_ClassStatsNotAvailable, Resources.Msg_Analysis, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var form = new ClassAnalysisForm(
                    stats, _bands, _width, _height,
                    _bands.Count > 0 ? _bands[0].GeoTransform : null,
                    result, e.RowIndex);
                form.Show();
            }
        }

        private void BuildScatterPlot(object? sender, EventArgs e)
        {
            if (_scatterXListBox.SelectedItem == null || _scatterYListBox.SelectedItem == null)
                return;

            Band? bandX = _scatterXListBox.SelectedItem as Band;
            Band? bandY = _scatterYListBox.SelectedItem as Band;
            if (bandX == null || bandY == null) return;

            if (_scatterWorker.IsBusy) return;

            UpdateUI(true);

            _scatterWorker.RunWorkerAsync((bandX, bandY));
        }

        private void KdeSingle(object? sender, EventArgs e)
        {
            if (_kdeBandsListBox!.SelectedItems.Count == 0) return;
            if (_kdeWorker.IsBusy) return;

            UpdateUI(true);

            var bands = _kdeBandsListBox.SelectedItems.Cast<Band>().ToList();
            _kdeWorker.RunWorkerAsync(("single", bands, (object?)null));
        }

        private void KdeProduct(object? sender, EventArgs e)
        {
            if (_kdeBandsListBox!.SelectedItems.Count < 2) return;
            if (_kdeWorker.IsBusy) return;

            UpdateUI(true);

            var bands = _kdeBandsListBox.SelectedItems.Cast<Band>().ToList();

            _kdeWorker.RunWorkerAsync(("product", bands, (object?)null));
        }

        private void KdeMultivariate(object? sender, EventArgs e)
        {
            if (_kdeBandsListBox!.SelectedItems.Count < 2) return;
            if (_kdeWorker.IsBusy) return;

            UpdateUI(true);

            var bands = _kdeBandsListBox.SelectedItems.Cast<Band>().ToList();

            _kdeWorker.RunWorkerAsync(("multivariate", bands, (object?)null));
        }

        private void ClearKdePlot(object? sender, EventArgs e)
        {
            if (_kdeModel == null) return;

            _kdeModel.Series.Clear();
            PlotView_DoubleClick(_kdePlotView, e);
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
                    worker?.ReportProgress(pct, $"{Resources.Statistics}: {Resources.band} {i + 1}/{total}");
                }

                float[][]? corrData = null;
                if (total > 1)
                {
                    worker?.ReportProgress(0, Resources.Progress_Correlation);
                    corrData = CalcCorrelationData(bands);
                }

                e.Result = corrData;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(Resources.Error_Stats, ex.Message), ex);
            }
        }

        private void StatsWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(string.Format(Resources.Error_Stats, e.Error.Message));
                UpdateUI(false);
                return;
            }

            foreach (Band band in _bands)
                band.ClearRawData();

            if (e.Result is float[][] corrData)
            {
                UpdateCorrelationGrid(corrData);
            }

            _mainStatusLabel.Text = Resources.Status_Ready;
            UpdateUI(false);
        }

        private void ScatterWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;

            var (bandX, bandY) = ((Band, Band))e.Argument!;
            int totalPixels = bandX.OriginalWidth * bandX.OriginalHeight;
            int targetSamples = ScatterMaxPoints * 2;
            int step = Math.Max(1, totalPixels / targetSamples);

            var rng = new Random(42);
            var points = new List<(double x, double y)>(ScatterMaxPoints);

            int lastPct = 0;
            worker?.ReportProgress(0, Resources.Progress_Scatter);

            for (int i = 0; i < totalPixels; i += step)
            {
                if (!float.IsNaN(bandX.GetValue(i)) && !float.IsNaN(bandY.GetValue(i)))
                {
                    float vx = bandX.GetValue(i);
                    float vy = bandY.GetValue(i);
                    points.Add((vx, vy));
                }

                int pct = (int)Math.Clamp((long)i * 100 / totalPixels, 0, 100);

                if (pct != lastPct)
                {
                    worker?.ReportProgress(pct, string.Format(Resources.Progress_ScatterPercent, pct));
                    lastPct = pct;
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
                MessageBox.Show(string.Format(Resources.Error_Scatter, e.Error.Message));
                UpdateUI(false);
                return;
            }

            var rawPoints = (List<(double x, double y)>)e.Result!;
            var series = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 2 };
            foreach (var (x, y) in rawPoints)
                series.Points.Add(new ScatterPoint(x, y));

            var model = new PlotModel();
            var bandX = _scatterXListBox.SelectedItem as Band;
            var bandY = _scatterYListBox.SelectedItem as Band;
            var showLabels = AppSettings.Instance.GraphShowAxisLabels;
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = showLabels ? bandX?.Name ?? "X" : null });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = showLabels ? bandY?.Name ?? "Y" : null });
            model.Series.Add(series);

            _scatterPlotView.Model = model;
            _mainStatusLabel.Text = string.Format(Resources.Status_ScatterComplete, rawPoints.Count);
            UpdateUI(false);
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
                            if (!float.IsNaN(band.GetValue(i)))
                            {
                                bandDensity += KernelFunctions.GetKernel(band.KernelType, (x - pv) / band.NormalizeKernelC);
                                validCount++;
                            }
                        }

                        bandDensity /= validCount * band.NormalizeKernelC;
                        points.Add(new DataPoint(x, bandDensity));
                        worker?.ReportProgress((bi * barCount + xi) * 100 / (bands.Count * barCount), string.Format(Resources.Progress_Kde, bi + 1, bands.Count));
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
                    worker?.ReportProgress(pct, string.Format(Resources.Progress_KdeMultivariate, pct));
                }

                e.Result = (mode, multiTitle, new List<(Band? band, List<DataPoint> points)> { (null, allPoints) });
            }
            else
            {
                bool isProduct = mode == "product";
                string prodTitle = isProduct
                    ? $"Product: {string.Join("×", bands.Select(b => b.Name))}"
                    : "";

                for (double x = 0; x < 1; x += 0.01)
                {
                    double result = isProduct ? 1.0 : 0.0;

                    foreach (var band in bands)
                    {
                        double bandDensity = 0;
                        int validCount = 0;

                        for (int i = 0; i < totalPixels; i += step)
                        {
                            float pv = band.GetNormalizedValue(i);
                            if (!float.IsNaN(band.GetValue(i)))
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

                    int pct = (int)(x * 100);
                    worker?.ReportProgress(pct, $"KDE: {pct}%");
                }

                e.Result = (mode, prodTitle, new List<(Band? band, List<DataPoint> points)> { (null, allPoints) });
            }
        }

        private void KdeWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(string.Format(Resources.Error_Kde, e.Error.Message));
                UpdateUI(false);
                return;
            }

            _kdePlotView.Model = null;

            var (mode, title, results) = ((string, string, List<(Band? band, List<DataPoint> points)>))e.Result!;

            foreach (var (band, points) in results)
            {
                var series = new FunctionSeries
                {
                    Title = !string.IsNullOrEmpty(title) ? title : band?.Name ?? Resources.Series_Kde
                };
                series.Points.AddRange(points);
                _kdeModel!.Series.Add(series);
            }

            _kdePlotView.Model = _kdeModel;
            PlotView_DoubleClick(_kdePlotView, e);
            _mainStatusLabel.Text = Resources.Status_KdeReady;
            UpdateUI(false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SettingsToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var oldMethod = AppSettings.Instance.BandwidthMethod;
            using var form = new Forms.SettingsForm();
            form.ShowDialog(this);
            ApplyKdeSettings();

            if (_bands.Count > 0 && AppSettings.Instance.BandwidthMethod != oldMethod && !_statsWorker.IsBusy)
            {
                _mainStatusLabel.Text = Resources.Status_RecalculatingBandwidth;
                UpdateUI(true);
                _statsWorker.RunWorkerAsync();
            }
        }

        private void BandPropertyGrid_PropertyValueChanged(object? sender, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem?.PropertyDescriptor?.Name != nameof(Band.KernelType))
                return;
            if (AppSettings.Instance.BandwidthMethod != BandwidthMethod.LeaveOneOutLikelihood)
                return;

            var band = _bandPropertyGrid.SelectedObject as Band;
            if (band == null) return;

            _mainStatusLabel.Text = string.Format(Resources.Status_RecalculatingBandwidthFor, band.Name);
            Task.Run(() =>
            {
                BandStatisticsComputer.Compute(band);
                BeginInvoke(() =>
                {
                    _mainStatusLabel.Text = string.Format(Resources.Status_BandwidthRecalculated, band.Name);
                });
            });
        }

        private void ApplyKdeSettings()
        {
            if (_kdeModel == null) return;
            var s = AppSettings.Instance;

            foreach (var axis in _kdeModel.Axes)
            {
                if (axis.Key == "X")
                    axis.Title = s.GraphShowAxisLabels ? Resources.Normalized_Value : null;
                else if (axis.Key == "Y")
                    axis.Title = s.GraphShowAxisLabels ? Resources.Density : null;
            }

            bool hasLegend = _kdeModel.Legends.Count > 0;
            if (s.GraphShowLegend && !hasLegend)
                _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
            else if (!s.GraphShowLegend && hasLegend)
                _kdeModel.Legends.Clear();

            _kdePlotView.InvalidatePlot(true);
        }

        private void UpdateBandsList()
        {
            _bandListBox.Items.Clear();
            _redToolStripDropDownButton.DropDownItems.Clear();
            _greenToolStripDropDownButton.DropDownItems.Clear();
            _blueToolStripDropDownButton.DropDownItems.Clear();

            _scatterXListBox.Items.Clear();
            _scatterYListBox.Items.Clear();
            _kdeBandsListBox.Items.Clear();

            if (_bands.Count == 0)
                return;

            var s = AppSettings.Instance;
            int Clamp(int idx) => Math.Clamp(idx, 0, _bands.Count - 1);
            _redBand = _bands[Clamp(s.DefaultRedBand)];
            _greenBand = _bands[Clamp(s.DefaultGreenBand)];
            _blueBand = _bands[Clamp(s.DefaultBlueBand)];

            _redToolStripDropDownButton.Text = _redBand.ToString();
            _greenToolStripDropDownButton.Text = _greenBand.ToString();
            _blueToolStripDropDownButton.Text = _blueBand.ToString();

            foreach (Band band in _bands)
            {
                _bandListBox.Items.Add(band);

                _redToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _redBand = band; _redToolStripDropDownButton.Text = _redBand.ToString(); UpdateImage(sender, e); });
                _greenToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _greenBand = band; _greenToolStripDropDownButton.Text = _greenBand.ToString(); UpdateImage(sender, e); });
                _blueToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _blueBand = band; _blueToolStripDropDownButton.Text = _blueBand.ToString(); UpdateImage(sender, e); });

                _scatterXListBox.Items.Add(band);
                _scatterYListBox.Items.Add(band);
                _kdeBandsListBox.Items.Add(band);

                _correlationDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = band.Name });
            }

            _bandListBox.SelectedIndex = 0;
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

            _dataViewport.UpdateImage(bitmap);
        }

        private void ClearFirstStageCache()
        {
            _primaryClassificationEngine?.ClearCache();
            _primaryClassificationEngine = null;
            _primaryClassificationResult = null;
            _primaryClassificationClassStats = null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            ClearFirstStageCache();

            string[] fileNames = _openFileDialog.FileNames;

            // Determine content type
            bool hasText = false, hasImage = false;
            foreach (string fn in fileNames)
            {
                string ext = Path.GetExtension(fn).ToLower();
                if (ext == ".csv" || ext == ".txt") hasText = true;
                else if (ext == ".tif" || ext == ".tiff" || ext == ".img" || ext == ".png" || ext == ".jpg" || ext == ".jpeg") hasImage = true;
            }

            if (hasText && hasImage)
            {
                MessageBox.Show(Resources.Msg_MixedFiles, Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hasText)
            {
                // Text files are small enough to load on UI thread
                foreach (Band b in _bands) b.UnloadPixelData();
                _bands.Clear();
                _geoTransform = null;
                _correlationDataGridView.Columns.Clear();

                foreach (string fileName in fileNames)
                {
                    string ext = Path.GetExtension(fileName).ToLower();
                    _mainStatusLabel.Text = $"Opening \"{Path.GetFileName(fileName)}\"...";
                    if (ext == ".csv")
                        ReadCsvFile(fileName);
                    else
                        ReadTextFile(fileName, '\t');

                    if (_bands.Count == 0) break;
                }

                if (_bands.Count == 0) return;
                UpdateBandsList();
                UpdateImage(sender, e);
                if (!_statsWorker.IsBusy)
                {
                    _mainStatusLabel.Text = Resources.Status_ComputingStats;
                    UpdateUI(true);
                    _statsWorker.RunWorkerAsync();
                }
                return;
            }

            foreach (Band b in _bands) b.UnloadPixelData();
            _bands.Clear();
            _geoTransform = null;
            _correlationDataGridView.Columns.Clear();

            if (_loadWorker.IsBusy) return;
            UpdateUI(true);
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
                worker.ReportProgress(fileIndex * 100 / info.FileNames.Length, $"{Resources.Loading} {Path.GetFileName(fileName)}...");

                if (ext == ".tif" || ext == ".tiff" || ext == ".img" || ext == ".png" || ext == ".jpg" || ext == ".jpeg")
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
                MessageBox.Show($"Error loading files: {e.Error.Message}", Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateUI(false);
                return;
            }

            var result = (FileLoadResult)e.Result!;

            foreach (string msg in result.Messages)
                MessageBox.Show(msg, Resources.Msg_Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            else
                UpdateUI(false);
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

                        if (!TryParseDouble(values[xIndex], out double x) || !TryParseDouble(values[yIndex], out double y))
                            continue;

                        minX = Math.Min(minX, x);
                        maxX = Math.Max(maxX, x);
                        minY = Math.Min(minY, y);
                        maxY = Math.Max(maxY, y);

                        Dictionary<string, float> bandValues = new();
                        foreach (int bandIdx in bandIndices)
                        {
                            if (TryParseFloat(values[bandIdx], out float v))
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

            double cellSize = columnSelector.GetResolution();

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
                    MessageBox.Show("Error: X, Y and at least one Band column are required.", Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!TryParseDouble(values[xIndex], out double x) || !TryParseDouble(values[yIndex], out double y))
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
                    if (!TryParseFloat(values[bandIdx], out float v))
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
                MessageBox.Show(Resources.Msg_NoValidData, Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error: Data has zero range in X or Y direction.", Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _width = Math.Max(1, (int)(dataRangeX / cellSize));
            _height = Math.Max(1, (int)(dataRangeY / cellSize));

            Dictionary<(int x, int y), Dictionary<string, float>> gridData = new();

            foreach (var point in rawData)
            {
                int gridX = (int)Math.Floor((point.x - minX) / cellSize);
                int gridY = (int)Math.Floor((maxY - point.y) / cellSize);

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

            _geoTransform = new GeoTransform(maxX, maxY, cellSize, -cellSize);

            foreach (Band band in _bands)
            {
                band.SetDimensions(_width, _height);
                band.SetGeoTransform(_geoTransform);
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

            // Background worker computes stats at line 968
        }

        private void bandListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Band? band = _bandListBox.SelectedItem as Band;

            if (band == null)
                return;

            _bandPropertyGrid.SelectedObject = band;

            BuildHistogram();
        }

        private void BuildHistogram()
        {
            Band? band = _bandListBox.SelectedItem as Band;
            if (band == null || band.OriginalWidth * band.OriginalHeight == 0)
                return;

            int columnsCount = Config.AppSettings.Instance.GetHistogramBinCount(band.Count);
            float minVal = band.Minimum;
            float maxVal = band.Maximum;
            float range = maxVal - minVal;
            if (range <= 0 || columnsCount <= 0)
                return;

            float columnsWidth = range / columnsCount;

            Task.Run(() =>
            {
                int[] pointses = new int[columnsCount];
                int[] asseses = new int[columnsCount];
                int totalPixels = band.OriginalWidth * band.OriginalHeight;

                for (int i = 0; i < totalPixels; i++)
                {
                    float x = band.GetValue(i);
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
                    var s = AppSettings.Instance;
                    var plot = new PlotModel();

                    plot.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Bottom,
                        Minimum = minVal,
                        Maximum = maxVal,
                        Title = s.GraphShowAxisLabels ? Resources.Axis_HistogramValue : null
                    });
                    plot.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Left,
                        Minimum = 0,
                        Key = "axesY1",
                        Title = s.GraphShowAxisLabels ? Resources.Axis_HistogramFrequency : null
                    });
                    plot.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Right,
                        Minimum = 0,
                        Maximum = 1d,
                        Key = "axesY2"
                    });

                    if (s.GraphShowLegend)
                        plot.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });

                    var barColor = System.Drawing.ColorTranslator.FromHtml(s.HistogramBarColor);
                    var histSeries = new HistogramSeries
                    {
                        YAxisKey = "axesY1",
                        FillColor = OxyColor.FromArgb(barColor.A, barColor.R, barColor.G, barColor.B)
                    };
                    for (int i = 0; i < columnsCount; i++)
                    {
                        float binMin = i * columnsWidth + minVal;
                        histSeries.Items.Add(new HistogramItem(binMin, binMin + columnsWidth, (float)pointses[i] / band.Count, pointses[i]));
                    }
                    plot.Series.Add(histSeries);

                    if (s.HistogramShowCumulative)
                    {
                        var lineColor = System.Drawing.ColorTranslator.FromHtml(s.HistogramLineColor);
                        var lineSeries = new LineSeries
                        {
                            YAxisKey = "axesY2",
                            Color = OxyColor.FromArgb(lineColor.A, lineColor.R, lineColor.G, lineColor.B),
                            Title = Resources.Series_Cumulative
                        };
                        for (int i = 0; i < columnsCount; i++)
                        {
                            float binMin = i * columnsWidth + minVal;
                            lineSeries.Points.Add(new DataPoint(binMin, (float)asseses[i] / band.Count));
                        }
                        plot.Series.Add(lineSeries);
                    }

                    _histogramPlotView.Model = plot;
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
                        float vx = bandX.GetValue(i);
                        float vy = bandY.GetValue(i);
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
            _correlationDataGridView.Rows.Clear();
            _correlationDataGridView.Columns.Clear();

            for (int i = 0; i < data.Length; i++)
                _correlationDataGridView.Columns.Add($"col{i}", _bands[i].Name);

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

                int rowId = _correlationDataGridView.Rows.Add(row);
                _correlationDataGridView.Rows[rowId].HeaderCell.Value = bandX.Name;
            }
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == _dataHistogramTabPage)
                BuildHistogram();
        }

        private void PlotView_DoubleClick(object sender, EventArgs e)
        {
            if (sender is OxyPlot.WindowsForms.PlotView pv && pv.Model != null)
            {
                foreach (var axis in pv.Model.Axes)
                    axis.Reset();
                pv.InvalidatePlot(true);
            }
        }

        private void Classify_Click(object sender, EventArgs e)
        {
            if (_classifyWorker.IsBusy)
            {
                MessageBox.Show(Resources.Msg_ClassInProgress, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_bands.Count == 0)
            {
                MessageBox.Show(Resources.Msg_NoBandsLoaded, Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isSecondary = _classificationTabControl.SelectedTab == _secondaryClassificationTabPage;

            if (isSecondary && (_primaryClassificationResult == null || _primaryClassificationEngine == null || _primaryClassificationClassStats == null))
            {
                MessageBox.Show(Resources.Msg_NeedPrimaryClass, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<ClassificationRule> rules = isSecondary ? _secondaryClassificationRules : _primaryClassificationRules;

            if (rules.Count == 0)
            {
                MessageBox.Show(Resources.Msg_NoRules, Resources.Msg_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Viewport viewport = isSecondary ? _secondaryClassificationViewport : _primaryClassificationViewport;
            ClassificationMode mode = isSecondary ? ClassificationMode.RulePerClass : (ClassificationMode)_primaryClassificationModeToolStripComboBox.SelectedIndex;

            UpdateUI(true);
            _classificationAbortButton.Visible = true;
            _classifyWorker.RunWorkerAsync((rules, mode, viewport, isSecondary));
        }

        private void AbortClassification_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.Msg_ClassInterrupt, Resources.Msg_Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                _classifyWorker.CancelAsync();
        }

        private void ClassifyWorker_DoWork(object sender, DoWorkEventArgs e)
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
            string stageName = isSecondStage ? Resources.Label_SecondStage : Resources.Label_Classification;
            worker.ReportProgress(0, string.Format(Resources.Progress_Starting, stageName));

            var engine = new ClassificationEngine(_bands, rules);
            engine.Mode = isSecondStage ? ClassificationMode.RulePerClass : mode;

            int firstStageClassCount = 0;
            Color[]? firstStagePalette = null;
            int[]? firstStageClassIndices = null;

            if (isSecondStage)
            {
                if (_primaryClassificationEngine?.ZScoreCache != null)
                    engine.UseZScoreCache(
                        _primaryClassificationEngine.ZScoreCache,
                        _primaryClassificationEngine.CachedBandCount,
                        _primaryClassificationEngine.CachedPixelCount);

                if (_primaryClassificationResult != null && _primaryClassificationClassStats != null)
                    engine.SetFirstStageContext(_primaryClassificationResult.ClassIndices, _primaryClassificationClassStats);

                firstStageClassCount = _primaryClassificationResult?.Palette?.Length
                    ?? _primaryClassificationResult?.Rules?.Count
                    ?? 0;
                firstStagePalette = _primaryClassificationResult?.Palette;
                firstStageClassIndices = _primaryClassificationResult?.ClassIndices;
            }
            else if (mode == ClassificationMode.DirectCheck)
            {
                engine.EnsureZScoreCache(_bands.Count, totalPixels);
            }

            int secondStageRuleCount = isSecondStage ? rules.Count : 0;

            int[]? firstToCompact = null;
            int compactFirstStageCount = firstStageClassCount;
            if (isSecondStage && _secondStageFilterIndices != null && firstStageClassIndices != null)
            {
                var filtered = firstStageClassIndices
                    .Where(c => c >= 0 && c < firstStageClassCount)
                    .Distinct()
                    .Where(c => _secondStageFilterIndices.Contains(c))
                    .OrderBy(c => c)
                    .ToArray();
                compactFirstStageCount = filtered.Length;
                firstToCompact = new int[firstStageClassCount];
                Array.Fill(firstToCompact, -1);
                for (int i = 0; i < filtered.Length; i++)
                    firstToCompact[filtered[i]] = i;
            }

            int totalClassCount = isSecondStage
                ? compactFirstStageCount * secondStageRuleCount
                : mode == ClassificationMode.DirectCheck
                    ? 1 << rules.Count
                    : rules.Count;

            Color[] palette;
            if (isSecondStage && firstToCompact != null && firstStagePalette != null)
            {
                var fullPalette = PaletteGenerator.GenerateSecondStage(firstStagePalette, secondStageRuleCount);
                palette = new Color[totalClassCount];
                int dest = 0;
                for (int fsc = 0; fsc < firstStageClassCount; fsc++)
                {
                    int compact = firstToCompact[fsc];
                    if (compact < 0) continue;
                    for (int r = 0; r < secondStageRuleCount; r++)
                        palette[dest++] = fullPalette[fsc * secondStageRuleCount + r];
                }
            }
            else
            {
                palette = isSecondStage
                    ? (firstStagePalette != null
                        ? PaletteGenerator.GenerateSecondStage(firstStagePalette, secondStageRuleCount)
                        : PaletteGenerator.GenerateHSV(totalClassCount))
                    : PaletteGenerator.GenerateHSV(totalClassCount);
            }

            var classificationResult = new ClassificationResult(_width, _height, palette);

            int processedPixels = 0;

            Parallel.For(0, totalPixels, () => 0, (pixelIndex, loopState, localCount) =>
            {
                int? classIndex = null;
                if (firstStageClassIndices != null)
                {
                    int firstClass = firstStageClassIndices[pixelIndex];
                    if (firstClass < 0 || firstClass >= firstStageClassCount)
                        return localCount;
                    if (firstToCompact != null)
                    {
                        int compact = firstToCompact[firstClass];
                        if (compact < 0) return localCount;
                        int? secondClass = engine.EvaluatePixel(pixelIndex);
                        if (secondClass.HasValue)
                            classIndex = compact * secondStageRuleCount + secondClass.Value;
                    }
                    else
                    {
                        int? secondClass = engine.EvaluatePixel(pixelIndex);
                        if (secondClass.HasValue)
                            classIndex = firstClass * secondStageRuleCount + secondClass.Value;
                    }
                }
                else
                {
                    int? rawClass = engine.EvaluatePixel(pixelIndex);
                    if (rawClass.HasValue)
                        classIndex = rawClass.Value;
                }

                if (classIndex.HasValue)
                    classificationResult.SetClass(pixelIndex, classIndex.Value);

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
                string remainingStr = remaining.ToString(@"hh\:mm\:ss");
                string elapsedStr = elapsed.ToString(@"hh\:mm\:ss");

                worker.ReportProgress(progress, $"{stageName}: {finalCount}/{totalPixels} ({progress}%) ETA: {remainingStr} ({elapsedStr})");
            });

            Bitmap bitmap = RenderClassificationBitmap(classificationResult);

            worker.ReportProgress(99, Resources.Progress_ComputeClassStats);

            if (isSecondStage)
            {
                _secondaryClassificationResult = classificationResult;

                if (engine.ZScoreCache != null)
                {
                    _secondaryClassificationClassStats = ClassStatistics.ComputeFromResult(
                        classificationResult, _bands, engine.ZScoreCache,
                        engine.CachedPixelCount, _width, _height);
                }

                e.Result = (bitmap, classificationResult, ClassificationMode.RulePerClass, true);
            }
            else
            {
                _primaryClassificationEngine = engine;
                _primaryClassificationResult = classificationResult;

                if (engine.ZScoreCache != null)
                {
                    _primaryClassificationClassStats = ClassStatistics.ComputeFromResult(
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

        private void ClassifyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(Resources.Msg_Canceled);
                _mainStatusLabel.Text = Resources.Status_ClassCanceled;
            }
            else if (e.Error != null)
            {
                MessageBox.Show(string.Format(Resources.Error_Generic, e.Error.Message));
                _mainStatusLabel.Text = "Error classification";
            }
            else
            {
                if (e.Result is (Bitmap bitmap, ClassificationResult classificationResult, ClassificationMode mode, bool isSecondStage))
                {
                    if (isSecondStage)
                    {
                        _secondaryClassificationViewport.UpdateImage(bitmap);
                        PopelateTableTab(_secondaryClassificationDataGridView);
                    }
                    else
                    {
                        _primaryClassificationViewport.UpdateImage(bitmap);
                        PopelateTableTab(_primaryClassificationDataGridView);
                    }

                    var stats = classificationResult.GetClassStatistics();
                    string summary = string.Format(Resources.Status_ClassComplete, stats.GetValueOrDefault(-1, 0));
                    _mainStatusLabel.Text = summary;
                }
            }

            _classificationAbortButton.Visible = false;
            UpdateUI(false);
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            _mainStatusLabel.Text = e.UserState?.ToString();
            _mainProgressBar.Value = e.ProgressPercentage;
        }

        private void RuleDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView? grid = sender as DataGridView;

            if (grid == null) return;

            RichTextBox richText = _primaryClassificationRichTextBox;
            List<ClassificationRule> rules = _primaryClassificationRules;

            if (grid.Equals(_secondaryRuleDataGridView))
            {
                richText = _secondaryClassificationRichTextBox;
                rules = _secondaryClassificationRules;
            }

            if (grid.SelectedRows.Count == 0)
                richText.Text = Resources.Label_NoRuleSelect;
            else
                richText.Text = GetRulePreview(rules[grid.Rows.IndexOf(grid.SelectedRows[0])]);
        }

        private string GetRulePreview(ClassificationRule rule)
        {
            if (rule.Conditions.Count == 0)
                return Resources.Label_NoConditions;

            var parts = rule.Conditions.Select(c => new ConditionDisplayItem(c, _bands).Display);
            return string.Join('\n', parts);
        }

        private void CompareToolStripButton_Click(object sender, EventArgs e)
        {
            Viewport viewport = _classificationTabControl.SelectedTab == _primaryClassificationTabPage ? _primaryClassificationViewport : _secondaryClassificationViewport;

            TwoImageViewForm twoImageView = new TwoImageViewForm(_dataViewport.Image, viewport.Image);

            twoImageView.ShowDialog(this);
        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            BandSelectionForm selectionForm = new BandSelectionForm(_bands);

            if (selectionForm.ShowDialog(this) == DialogResult.OK)
            {
                List<Band> selectedBands = selectionForm.Result;
                if (selectedBands.Count == 0)
                    return;

                _primaryClassificationRules.Clear();

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
                    _primaryClassificationRules.Add(rule);
                }

                _primaryClassificationModeToolStripComboBox.SelectedIndex = (int)ClassificationMode.DirectCheck;
                UpdateClassificationRulesGrid(_primaryRuleDataGridView, _primaryClassificationRules);
            }
        }

        public void ExportActivePlot(object? sender, EventArgs e)
        {
            PlotView? target = null;

            if (_mainTabControl.SelectedTab == _dataTabPage)
            {
                if (_dataTabControl.SelectedTab == _dataHistogramTabPage)
                    target = _histogramPlotView;
            }
            else if (_mainTabControl.SelectedTab == _explorationTabPage)
            {
                if (_explorationTabControl.SelectedTab == _kdeTabPage)
                    target = _kdePlotView;
                else if (_explorationTabControl.SelectedTab == _scatterTabPage)
                    target = _scatterPlotView;
            }

            if (target == null || target.Model == null)
            {
                MessageBox.Show(Resources.Msg_NoPlot, Resources.Msg_ExportGraph,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new GraphExportDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            PlotExportService.Export(target.Model, dlg.FilePath, dlg.Options);
        }

        public void ExportKdePlot(object? sender, EventArgs e)
        {
            ExportPlotView(_kdePlotView, null);
        }

        public void ExportScatterPlot(object? sender, EventArgs e)
        {
            ExportPlotView(_scatterPlotView, null);
        }

        public void ExportHistogramPlot(object? sender, EventArgs e)
        {
            ExportPlotView(_histogramPlotView, null);
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
            ClassificationResult? classificationResult = _classificationTabControl.SelectedTab == _primaryClassificationTabPage ? _primaryClassificationResult : _secondaryClassificationResult;

            if (classificationResult == null)
            {
                MessageBox.Show(Resources.Msg_NoClassResult,
                    Resources.Msg_Export, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new ExportClassificationDialog(
                projectionWkt: _bands?.FirstOrDefault()?.GeoTransform?.ProjectionWkt,
                bands: _bands);
            if (dlg.ShowDialog() != DialogResult.OK) return;

            var result = classificationResult;

            if (dlg.UseHsvPalette && result.Palette != null)
            {
                result = new ClassificationResult(result.Width, result.Height,
                    PaletteGenerator.GenerateHSV(result.Palette.Length));
                Array.Copy(classificationResult.ClassIndices, result.ClassIndices, result.ClassIndices.Length);
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
                Array.Copy(classificationResult.ClassIndices, result.ClassIndices, result.ClassIndices.Length);
            }

            ClassificationExporter.Export(result, dlg.ExportOptions, _bands);
            if (dlg.ExportStatsChecked)
                ClassificationExporter.ExportStats(result, dlg.StatsOptions);

            MessageBox.Show(Resources.Msg_ExportComplete, Resources.Msg_Export,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (sender is DataGridView dataGridView)
                dataGridView.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private static bool TryParseDouble(string s, out double result)
        {
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return true;
            return double.TryParse(s, NumberStyles.Float, new CultureInfo("ru-RU"), out result);
        }

        private static bool TryParseFloat(string s, out float result)
        {
            if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return true;
            return float.TryParse(s, NumberStyles.Float, new CultureInfo("ru-RU"), out result);
        }

        private void filterToolStripButton_Click(object sender, EventArgs e)
        {
            if (_primaryClassificationResult == null)
            {
                MessageBox.Show(Resources.Filter_NeedPrimaryFirst, Resources.Msg_Warning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classCount = _primaryClassificationResult.Palette?.Length ?? 0;
            string[] classNames;
            if (classCount > 0 && _primaryClassificationRules.Count == classCount)
                classNames = _primaryClassificationRules.Select(r => r.Name).ToArray();
            else
                classNames = Enumerable.Range(0, classCount).Select(i => string.Format(Resources.Label_ClassFormat, i)).ToArray();

            int[] initiallySelected = _secondStageFilterIndices != null
                ? _secondStageFilterIndices.ToArray()
                : Enumerable.Range(0, classNames.Length).ToArray();

            var filterForm = new FilterForm(classNames, initiallySelected, _primaryClassificationResult.Palette);
            if (filterForm.ShowDialog(this) == DialogResult.OK)
            {
                _secondStageFilterIndices = filterForm.SelectedClassIndices.Length == classNames.Length
                    ? null
                    : new HashSet<int>(filterForm.SelectedClassIndices);
            }
        }
    }
}
