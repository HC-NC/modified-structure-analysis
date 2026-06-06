using System.ComponentModel;
using System.Data;
using System.Text;
using modified_structure_analysis.Models;
using modified_structure_analysis.Properties;
using modified_structure_analysis.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace modified_structure_analysis.Forms;

public partial class ClassAnalysisForm : Form
{
    private readonly ClassStatistics[] _classStats;
    private readonly List<Band> _bands;
    private readonly int _width;
    private readonly int _height;
    private readonly GeoTransform? _geoTransform;
    private readonly ClassificationResult? _classificationResult;
    private readonly int _totalPixels;
    private readonly ToolTip _toolTip;

    private const int MaxPixelsInGrid = 10_000;
    private const int KdeSteps = 101;
    private const int ScatterMaxPoints = 50_000;

    public ClassAnalysisForm(
        ClassStatistics[] classStats,
        List<Band> bands,
        int width,
        int height,
        GeoTransform? geoTransform,
        ClassificationResult? classificationResult,
        int initialClassIndex = 0)
    {
        _classStats = classStats;
        _bands = bands;
        _width = width;
        _height = height;
        _geoTransform = geoTransform;
        _classificationResult = classificationResult;
        _totalPixels = width * height;

        InitializeComponent();
        
        _toolTip = new ToolTip();
        _toolTip.SetToolTip(_buildScatterButton, Resources._buildScatterButton_ToolTip);
        _toolTip.SetToolTip(_kdeClearButton, Resources._kdeClearButton_ToolTip);
        _toolTip.SetToolTip(_kdeSingleButton, Resources._kdeSingleButton_ToolTip);
        _toolTip.SetToolTip(_kdeProductButton, Resources._kdeProductButton_ToolTip);
        _toolTip.SetToolTip(_kdeMultivariateButton, Resources._kdeMultivariateButton_ToolTip);

        RebuildPixelsGridColumns(0);

        PopulateClassList(initialClassIndex);
    }

    private void ClassAnalysisForm_Load(object? sender, EventArgs e)
    {
    }

    // ── Class list ─────────────────────────────────────────────

    private void PopulateClassList(int selectIndex)
    {
        _classesListBox.Items.Clear();
        for (int i = 0; i < _classStats.Length; i++)
        {
            double pct = _totalPixels > 0 ? _classStats[i].PixelCount * 100.0 / _totalPixels : 0;
            _classesListBox.Items.Add($"{Resources.Class} {i}  [{pct:F1}%]");
        }

        if (selectIndex >= 0 && selectIndex < _classesListBox.Items.Count)
            _classesListBox.SelectedIndex = selectIndex;
    }

    private void ClassesListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_classesListBox.SelectedItem == null) return;
        int classIdx = _classesListBox.SelectedIndex;
        var stats = _classStats[classIdx];

        int totalClassPixels = stats.PixelCount;
        double pct = _totalPixels > 0 ? totalClassPixels * 100.0 / _totalPixels : 0;
        _summaryToolStripStatusLabel.Text = $"{Resources.Class} {classIdx} — {totalClassPixels:N0} px ({pct:F1}%)";

        var bandSel = SaveListSelection(_bandsListBox);
        var kdeSel = SaveListSelection(_kdeListBox);
        var sXSel = SaveListSelection(_scatterXListBox);
        var sYSel = SaveListSelection(_scatterYListBox);

        PopulateBandList(classIdx, bandSel);
        PopulateKdeList(classIdx, kdeSel);
        PopulateScatterLists(classIdx, sXSel, sYSel);
        PopulatePixelsGrid(classIdx);
    }

    private static List<string> SaveListSelection(ListBox lb)
    {
        return lb.SelectedItems.Cast<string>().ToList();
    }

    private static void RestoreListSelection(ListBox lb, List<string> selection)
    {
        for (int i = 0; i < lb.Items.Count; i++)
            if (selection.Contains(lb.Items[i].ToString()))
                lb.SetSelected(i, true);
    }

    // ── Band list + PropertyGrid ───────────────────────────────

    private void PopulateBandList(int classIdx, List<string>? restore = null)
    {
        _bandsListBox.Items.Clear();
        for (int b = 0; b < _bands.Count; b++)
            _bandsListBox.Items.Add(_bands[b].Name);

        if (restore != null && restore.Count > 0)
            RestoreListSelection(_bandsListBox, restore);

        if (_bandsListBox.SelectedIndex < 0 && _bandsListBox.Items.Count > 0)
            _bandsListBox.SelectedIndex = 0;
    }

    private void BandsListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_classesListBox.SelectedItem == null || _bandsListBox.SelectedItem == null) return;

        int classIdx = _classesListBox.SelectedIndex;
        int bandIdx = _bandsListBox.SelectedIndex;
        var bandStats = _classStats[classIdx].Bands?[bandIdx];

        if (bandStats != null)
        {
            bool showZ = bandStats.HasValidZStats;
            _propertyGrid.SelectedObject = ClassBandStatsViewModel.Create(bandStats, showZ);
        }
    }

    // ── Pixels grid (capped, batched) ─────────────────────────

    private void RebuildPixelsGridColumns(int classIdx)
    {
        var stats = _classStats[classIdx];

        dataGridView1.SuspendLayout();
        dataGridView1.Columns.Clear();

        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = _indexColumn.HeaderText, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = _colColumn.HeaderText, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = _rowColumn.HeaderText, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        if (_geoTransform != null)
        {
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = _mapXColumn.HeaderText, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = _mapYColumn.HeaderText, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        for (int b = 0; b < _bands.Count; b++)
        {
            string name = _bands[b].Name;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = $"V_{name}", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            if (stats.Bands?[b]?.HasValidZStats == true)
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = $"Z_{name}", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        dataGridView1.ResumeLayout();
    }

    private void PopulatePixelsGrid(int classIdx)
    {
        var stats = _classStats[classIdx];
        var pixelIndices = stats.PixelIndices;
        int total = pixelIndices?.Length ?? 0;
        bool hasGeo = _geoTransform != null;

        _summaryToolStripStatusLabel.Text = $"{Resources.Loading} {Resources.pixels}...";

        if (total == 0)
        {
            dataGridView1.Rows.Clear();
            return;
        }

        RebuildPixelsGridColumns(classIdx);

        int colGeo = hasGeo ? 2 : 0;
        int colOffset = 5 + colGeo;

        int take = Math.Min(total, MaxPixelsInGrid);
        int colCount = dataGridView1.Columns.Count;

        var rows = new List<DataGridViewRow>(take);

        for (int i = 0; i < take; i++)
        {
            int px = pixelIndices![i];
            int col = px % _width;
            int row = px / _width;

            var cells = new DataGridViewCell[colCount];
            cells[0] = new DataGridViewTextBoxCell { Value = px };
            cells[1] = new DataGridViewTextBoxCell { Value = col };
            cells[2] = new DataGridViewTextBoxCell { Value = row };

            int ci = 3;
            if (hasGeo)
            {
                var (wx, wy) = _geoTransform!.PixelToWorld(col, row);
                cells[ci++] = new DataGridViewTextBoxCell { Value = wx };
                cells[ci++] = new DataGridViewTextBoxCell { Value = wy };
            }

            for (int b = 0; b < _bands.Count; b++)
            {
                cells[ci++] = new DataGridViewTextBoxCell { Value = _bands[b].GetValue(px) };

                if (stats.Bands?[b]?.HasValidZStats == true)
                    cells[ci++] = new DataGridViewTextBoxCell { Value = _bands[b].GetZScore(px) };
            }

            var dgvRow = new DataGridViewRow();
            dgvRow.Cells.AddRange(cells);
            rows.Add(dgvRow);
        }

        dataGridView1.SuspendLayout();
        dataGridView1.Rows.Clear();

        if (rows.Count > 0)
            dataGridView1.Rows.AddRange([.. rows]);

        dataGridView1.ResumeLayout();

        string suffix = total > take ? string.Format(Resources.ShowingSuffix, take.ToString("N0"), total.ToString("N0")) : "";
        _summaryToolStripStatusLabel.Text = $"{Resources.Class} {classIdx} — {total:N0} px {suffix}";
    }

    // ── KDE list ───────────────────────────────────────────────

    private void PopulateKdeList(int _classIdx, List<string>? restore = null)
    {
        _kdeListBox.Items.Clear();

        for (int b = 0; b < _bands.Count; b++)
            _kdeListBox.Items.Add(_bands[b].Name);

        if (restore != null)
            RestoreListSelection(_kdeListBox, restore);
    }

    private void KdeSingleButton_Click(object? sender, EventArgs e)
    {
        if (_kdeListBox.SelectedItems.Count == 0) return;
        if (_backgroundWorker.IsBusy) return;

        var selected = _kdeListBox.SelectedItems.Cast<string>().ToList();
        ToggleKdeUi(false);
        _backgroundWorker.RunWorkerAsync(("single", selected, _classesListBox.SelectedIndex));
    }

    private void KdeProductButton_Click(object? sender, EventArgs e)
    {
        if (_kdeListBox.SelectedItems.Count < 2) return;
        if (_backgroundWorker.IsBusy) return;

        var selected = _kdeListBox.SelectedItems.Cast<string>().ToList();
        ToggleKdeUi(false);
        _backgroundWorker.RunWorkerAsync(("product", selected, _classesListBox.SelectedIndex));
    }

    private void KdeMultivariateButton_Click(object? sender, EventArgs e)
    {
        if (_kdeListBox.SelectedItems.Count < 2) return;
        if (_backgroundWorker.IsBusy) return;

        var selected = _kdeListBox.SelectedItems.Cast<string>().ToList();
        ToggleKdeUi(false);
        _backgroundWorker.RunWorkerAsync(("multivariate", selected, _classesListBox.SelectedIndex));
    }

    private void KdeClearButton_Click(object? sender, EventArgs e)
    {
        _kdePlotView.Model = null;
    }

    private void ToggleKdeUi(bool enabled)
    {
        _kdeSingleButton.Visible = enabled;
        _kdeProductButton.Visible = enabled;
        _kdeMultivariateButton.Visible = enabled;
        _kdeClearButton.Visible = enabled;
        _kdeProgressBar.Visible = !enabled;
    }

    // ── Scatter plot ───────────────────────────────────────────

    private void PopulateScatterLists(int classIdx, List<string>? restoreX = null, List<string>? restoreY = null)
    {
        _scatterXListBox.Items.Clear();
        _scatterYListBox.Items.Clear();

        for (int b = 0; b < _bands.Count; b++)
        {
            _scatterXListBox.Items.Add(_bands[b].Name);
            _scatterYListBox.Items.Add(_bands[b].Name);
        }

        if (restoreX != null) RestoreListSelection(_scatterXListBox, restoreX);
        if (restoreY != null) RestoreListSelection(_scatterYListBox, restoreY);
    }

    private void BuildScatterButton_Click(object? sender, EventArgs e)
    {
        if (_scatterXListBox.SelectedItem == null || _scatterYListBox.SelectedItem == null) return;
        if (_scatterWorker.IsBusy) return;

        var xItem = _scatterXListBox.SelectedItem.ToString()!;
        var yItem = _scatterYListBox.SelectedItem.ToString()!;
        int classIdx = _classesListBox.SelectedIndex;

        ToggleScatterUi(false);
        _scatterWorker.RunWorkerAsync((xItem, yItem, classIdx));
    }

    private void ToggleScatterUi(bool enabled)
    {
        _buildScatterButton.Visible = enabled;
        _scatterProgressBar.Visible = !enabled;
    }

    private (int bandIndex, bool isZ) ParseBandEntry(string item, ClassStatistics? stats)
    {
        if (item.StartsWith("z(") && item.EndsWith(")"))
        {
            string name = item[2..^1];
            int bi = _bands.FindIndex(b => b.Name == name);
            if (bi >= 0 && stats?.Bands?[bi]?.HasValidZStats == true)
                return (bi, true);
        }
        else
        {
            int bi = _bands.FindIndex(b => b.Name == item);
            if (bi >= 0) return (bi, false);
        }
        return (-1, false);
    }

    private void ScatterWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        var (xItem, yItem, classIdx) = ((string, string, int))e.Argument!;
        var worker = (BackgroundWorker)sender!;
        var stats = _classStats[classIdx];
        var pixelIndices = stats.PixelIndices;
        if (pixelIndices == null || pixelIndices.Length == 0) return;

        var (xBi, xIsZ) = ParseBandEntry(xItem, stats);
        var (yBi, yIsZ) = ParseBandEntry(yItem, stats);
        if (xBi < 0 || yBi < 0) return;

        var bandX = _bands[xBi];
        var bandY = _bands[yBi];

        Func<int, float> getX = xIsZ
            ? px => bandX.GetZScore(px)
            : px => bandX.GetValue(px);

        Func<int, float> getY = yIsZ
            ? px => bandY.GetZScore(px)
            : px => bandY.GetValue(px);

        int total = pixelIndices.Length;
        int step = Math.Max(1, total / (ScatterMaxPoints * 2));

        var rng = new Random(42);
        var points = new List<(double x, double y)>(ScatterMaxPoints);

        for (int i = 0; i < total; i += step)
        {
            int px = pixelIndices[i];
            float vx = getX(px);
            float vy = getY(px);
            if (!float.IsNaN(vx) && !float.IsNaN(vy))
                points.Add((vx, vy));
        }

        while (points.Count > ScatterMaxPoints)
        {
            int idx = rng.Next(points.Count);
            points.RemoveAt(idx);
        }

        e.Result = (points, xItem, yItem);
    }

    private void ScatterWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        ToggleScatterUi(true);

        if (e.Error != null)
        {
            MessageBox.Show(string.Format(Resources.Error_Scatter, e.Error.Message), Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var (rawPoints, xTitle, yTitle) = ((List<(double x, double y)>, string, string))e.Result!;

        var series = new ScatterSeries
        {
            MarkerType = MarkerType.Circle,
            MarkerSize = 2
        };
        foreach (var (x, y) in rawPoints)
            series.Points.Add(new ScatterPoint(x, y));

        var model = new PlotModel();
        var showLabels = Config.AppSettings.Instance.GraphShowAxisLabels;
        model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = showLabels ? xTitle : null });
        model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = showLabels ? yTitle : null });
        model.Series.Add(series);

        _scatterPlotView.Model = model;
    }

    private void ScatterWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        _scatterProgressBar.Value = e.ProgressPercentage;
    }

    // ── KDE worker ─────────────────────────────────────────────

    private void KdeWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        var args = (ValueTuple<string, List<string>, int>)e.Argument!;
        string mode = args.Item1;
        var selectedItems = args.Item2;
        int classIdx = args.Item3;

        var worker = (BackgroundWorker)sender!;
        var stats = _classStats[classIdx];
        var pixelIndices = stats.PixelIndices;
        if (pixelIndices == null || pixelIndices.Length == 0) return;

        var entries = ParseKdeEntries(selectedItems, stats);
        if (entries.Count == 0) return;

        if (mode == "single")
            ComputeSingleKde(worker, e, entries, pixelIndices, stats, classIdx);
        else
            ComputeCombinedKde(worker, e, mode, entries, pixelIndices, stats, classIdx);
    }

    private List<(int bandIndex, bool isZ)> ParseKdeEntries(List<string> items, ClassStatistics stats)
    {
        var result = new List<(int bandIndex, bool isZ)>();
        foreach (string item in items)
        {
            if (item.StartsWith("z(") && item.EndsWith(")"))
            {
                string name = item[2..^1];
                int bi = _bands.FindIndex(b => b.Name == name);
                if (bi >= 0 && stats.Bands?[bi]?.HasValidZStats == true)
                    result.Add((bi, true));
            }
            else
            {
                int bi = _bands.FindIndex(b => b.Name == item);
                if (bi >= 0) result.Add((bi, false));
            }
        }
        return result;
    }

    private void ComputeSingleKde(BackgroundWorker worker, DoWorkEventArgs e,
        List<(int bi, bool isZ)> entries, int[] pixelIndices, ClassStatistics stats, int classIdx)
    {
        var results = new List<(string title, List<DataPoint> points)>();
        int totalSteps = entries.Count * KdeSteps;
        int done = 0;

        foreach (var (bi, isZ) in entries)
        {
            var band = _bands[bi];
            var bs = stats.Bands![bi];

            float min, max, kernelC;
            Func<int, float> getValue;

            if (isZ)
            {
                min = 0; max = 1; kernelC = bs.ZNormalizeKernelC;
                float zr = bs.ZMax - bs.ZMin;
                getValue = px =>
                {
                    float z = band.GetZScore(px);
                    return !float.IsNaN(z) && zr > 0 ? (z - bs.ZMin) / zr : float.NaN;
                };
            }
            else
            {
                min = 0; max = 1; kernelC = bs.NormalizeKernelC;
                float cr = bs.Maximum - bs.Minimum;
                getValue = px =>
                {
                    float v = band.GetValue(px);
                    return !float.IsNaN(v) && cr > 0 ? (v - bs.Minimum) / cr : float.NaN;
                };
            }

            float range = max - min;
            if (range <= 0 || kernelC <= 0) continue;

            var pts = new List<DataPoint>(KdeSteps);
            string shortTitle = isZ ? $"z({band.Name})" : band.Name;
            string title = $"[{Resources.Class} {classIdx}] {shortTitle}";

            for (int xi = 0; xi < KdeSteps; xi++)
            {
                double x = min + range * xi / (KdeSteps - 1);
                double density = 0;
                int valid = 0;

                foreach (int px in pixelIndices)
                {
                    float pv = getValue(px);
                    if (!float.IsNaN(pv))
                    {
                        density += KernelFunctions.GetKernel(band.KernelType, (x - pv) / kernelC);
                        valid++;
                    }
                }

                density /= valid * kernelC;
                pts.Add(new DataPoint(x, density));
                done++;
                worker.ReportProgress(done * 100 / totalSteps);
            }

            results.Add((title, pts));
        }

        e.Result = ("single", "", results);
    }

    private void ComputeCombinedKde(BackgroundWorker worker, DoWorkEventArgs e,
        string mode, List<(int bi, bool isZ)> entries, int[] pixelIndices, ClassStatistics stats, int classIdx)
    {
        bool isProduct = mode == "product";

        if (mode == "multivariate")
        {
            double productBandwidths = 1;
            var bandsData = entries.Select(entry =>
            {
                var band = _bands[entry.bi];
                var bs = stats.Bands![entry.bi];
                float h = entry.isZ ? bs.ZNormalizeKernelC : bs.NormalizeKernelC;
                productBandwidths *= h;
                return (band, h, getValue: MakeGetValue(entry, stats));
            }).ToArray();

            double norm = pixelIndices.Length * productBandwidths;
            var allPts = new List<DataPoint>(KdeSteps);

            for (int xi = 0; xi < KdeSteps; xi++)
            {
                double xVal = 1.0 * xi / (KdeSteps - 1);
                double sumProduct = 0;

                foreach (int px in pixelIndices)
                {
                    double pixelProduct = 1;
                    bool valid = true;

                    foreach (var (band, h, getVal) in bandsData)
                    {
                        float v = getVal(px);
                        if (float.IsNaN(v)) { valid = false; break; }
                        pixelProduct *= KernelFunctions.GetKernel(band.KernelType, (xVal - v) / h);
                    }

                    if (valid)
                        sumProduct += pixelProduct;
                }

                allPts.Add(new DataPoint(xVal, sumProduct / norm));
                worker.ReportProgress(xi * 100 / KdeSteps);
            }

            string detail = string.Join("+",
                entries.Select(e => e.isZ ? $"z({_bands[e.bi].Name})" : _bands[e.bi].Name));
            string mTitle = $"[{Resources.Class} {classIdx}] {Resources.Multivar}: {detail}";
            e.Result = ("multivariate", mTitle,
                new List<(string, List<DataPoint>)> { (mTitle, allPts) });
        }
        else
        {
            var allPts = new List<DataPoint>(KdeSteps);

            for (int xi = 0; xi < KdeSteps; xi++)
            {
                double xVal = 1.0 * xi / (KdeSteps - 1);
                double result = isProduct ? 1.0 : 0.0;

                foreach (var (bi, isZ) in entries)
                {
                    var band = _bands[bi];
                    var bs = stats.Bands![bi];
                    float kernelC = isZ ? bs.ZNormalizeKernelC : bs.NormalizeKernelC;
                    var getValue = MakeGetValue((bi, isZ), stats);

                    double bandDensity = 0;
                    int valid = 0;

                    foreach (int px in pixelIndices)
                    {
                        float pv = getValue(px);
                        if (!float.IsNaN(pv))
                        {
                            bandDensity += KernelFunctions.GetKernel(band.KernelType, (xVal - pv) / kernelC);
                            valid++;
                        }
                    }

                    bandDensity /= valid * kernelC;

                    if (isProduct)
                        result *= bandDensity;
                    else
                        result += bandDensity;
                }

                if (!isProduct && entries.Count > 0)
                    result /= entries.Count;

                allPts.Add(new DataPoint(xVal, result));
                worker.ReportProgress(xi * 100 / KdeSteps);
            }

            string detail = string.Join(isProduct ? "×" : "+",
                entries.Select(e => e.isZ ? $"z({_bands[e.bi].Name})" : _bands[e.bi].Name));
            string title = isProduct ? $"[{Resources.Class} {classIdx}] {Resources.Product}: {detail}" : "";
            e.Result = (mode, title, new List<(string, List<DataPoint>)> { (title, allPts) });
        }
    }

    private Func<int, float> MakeGetValue((int bi, bool isZ) entry, ClassStatistics stats)
    {
        var band = _bands[entry.bi];
        if (entry.isZ)
        {
            float zr = stats.Bands![entry.bi].ZMax - stats.Bands[entry.bi].ZMin;
            float zMin = stats.Bands[entry.bi].ZMin;
            return px =>
            {
                float z = band.GetZScore(px);
                return !float.IsNaN(z) && zr > 0 ? (z - zMin) / zr : float.NaN;
            };
        }
        else
        {
            var bs = stats.Bands![entry.bi];
            float range = bs.Maximum - bs.Minimum;
            float minVal = bs.Minimum;
            return px =>
            {
                float v = band.GetValue(px);
                return !float.IsNaN(v) && range > 0 ? (v - minVal) / range : float.NaN;
            };
        }
    }

    private void KdeWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        _kdeProgressBar.Value = e.ProgressPercentage;
    }

    private void KdeWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        ToggleKdeUi(true);

        if (e.Error != null)
        {
            MessageBox.Show(string.Format(Resources.Error_Kde, e.Error.Message), Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (e.Result is not (string mode, string title, List<(string seriesTitle, List<DataPoint> points)> results))
            return;

        var model = _kdePlotView.Model;
        if (model == null)
        {
            model = new PlotModel();
            var gs = Config.AppSettings.Instance;
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = gs.GraphShowAxisLabels ? Resources.Normalized_Value : null });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = gs.GraphShowAxisLabels ? Resources.Density : null, Minimum = 0 });
            if (gs.GraphShowLegend)
                model.Legends.Add(new Legend
                {
                    LegendPosition = LegendPosition.TopRight,
                    LegendPlacement = LegendPlacement.Inside
                });
            _kdePlotView.Model = model;
        }

        foreach (var (seriesTitle, pts) in results)
        {
            var series = new LineSeries
            {
                Title = !string.IsNullOrEmpty(seriesTitle) ? seriesTitle : Resources.Series_Kde
            };
            series.Points.AddRange(pts);
            model.Series.Add(series);
        }

        _kdePlotView.InvalidatePlot(true);
    }

    // ── Plot export ───────────────────────────────────────────

    private void ExportActivePlot_Click(object? sender, EventArgs e)
    {
        OxyPlot.WindowsForms.PlotView? target = null;

        if (tabControl1.SelectedTab == _kdeTabPage)
            target = _kdePlotView;
        else if (tabControl1.SelectedTab == _scatterTabPage)
            target = _scatterPlotView;

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

    // ── Reset axes ────────────────────────────────────────────

    private void ResetPlotAxes(object? sender, EventArgs e)
    {
        if (sender is OxyPlot.WindowsForms.PlotView pv && pv.Model != null)
        {
            foreach (var axis in pv.Model.Axes)
                axis.Reset();
            pv.InvalidatePlot(true);
        }
    }

    // ── Export ─────────────────────────────────────────────────

    private void ExportPixelsTable_Click(object? sender, EventArgs e)
    {
        if (dataGridView1.Rows.Count == 0) return;

        _saveFileDialog.Filter = "CSV files (*.csv)|*.csv|TXT files (*.txt)|*.txt";
        _saveFileDialog.DefaultExt = "csv";
        _saveFileDialog.FileName = $"pixels_class{_classesListBox.SelectedIndex}.csv";

        if (_saveFileDialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            char delim = Path.GetExtension(_saveFileDialog.FileName)?.ToLower() == ".csv" ? ',' : '\t';
            var sb = new StringBuilder();
            var ci = System.Globalization.CultureInfo.InvariantCulture;

            for (int c = 0; c < dataGridView1.Columns.Count; c++)
            {
                if (c > 0) sb.Append(delim);
                sb.Append(dataGridView1.Columns[c].HeaderText);
            }
            sb.AppendLine();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int c = 0; c < dataGridView1.Columns.Count; c++)
                {
                    if (c > 0) sb.Append(delim);
                    var val = row.Cells[c].Value;
                    if (val is float f)
                        sb.Append(f.ToString(ci));
                    else if (val is double d)
                        sb.Append(d.ToString(ci));
                    else
                        sb.Append(val ?? "");
                }
                sb.AppendLine();
            }

            File.WriteAllText(_saveFileDialog.FileName, sb.ToString());
            MessageBox.Show(Resources.Msg_PixelExportComplete, Resources.Msg_Export, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Resources.Error_Export, ex.Message), Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ExportToolStripButton_Click(object? sender, EventArgs e)
    {
        if (_classStats.Length == 0) return;

        _saveFileDialog.Filter = "CSV files (*.csv)|*.csv|TXT files (*.txt)|*.txt";
        _saveFileDialog.DefaultExt = "csv";
        _saveFileDialog.FileName = "class_statistics.csv";

        if (_saveFileDialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            char delim = Path.GetExtension(_saveFileDialog.FileName)?.ToLower() == ".csv" ? ',' : '\t';
            var sb = new StringBuilder();
            var ci = System.Globalization.CultureInfo.InvariantCulture;

            sb.Append(Resources.Class); sb.Append(delim);
            sb.Append("Band"); sb.Append(delim);
            sb.Append("Count"); sb.Append(delim);
            sb.Append("Sum"); sb.Append(delim);
            sb.Append("Min"); sb.Append(delim);
            sb.Append("Max"); sb.Append(delim);
            sb.Append("Mean"); sb.Append(delim);
            sb.Append("Sigma"); sb.Append(delim);
            sb.Append("Variance"); sb.Append(delim);
            sb.Append("Skewness"); sb.Append(delim);
            sb.Append("Kurtosis"); sb.Append(delim);
            sb.Append("KernelC"); sb.Append(delim);
            sb.Append("NormKernelC"); sb.Append(delim);
            sb.Append("ZMin"); sb.Append(delim);
            sb.Append("ZMax"); sb.Append(delim);
            sb.Append("ZMean"); sb.Append(delim);
            sb.Append("ZSigma"); sb.Append(delim);
            sb.Append("ZKernelC"); sb.Append(delim);
            sb.Append("ZNormKernelC");
            sb.AppendLine();

            for (int c = 0; c < _classStats.Length; c++)
            {
                var cs = _classStats[c];
                for (int b = 0; b < _bands.Count; b++)
                {
                    if (cs.Bands == null || b >= cs.Bands.Length) continue;
                    var bs = cs.Bands[b];

                    sb.Append(c); sb.Append(delim);
                    sb.Append(_bands[b].Name); sb.Append(delim);
                    AppendFloat(sb, bs.Count, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Sum, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Minimum, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Maximum, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Mean, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Sigma, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Variance, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Skewness, ci); sb.Append(delim);
                    AppendFloat(sb, bs.Kurtosis, ci); sb.Append(delim);
                    AppendFloat(sb, bs.KernelC, ci); sb.Append(delim);
                    AppendFloat(sb, bs.NormalizeKernelC, ci); sb.Append(delim);

                    if (bs.HasValidZStats)
                    {
                        AppendFloat(sb, bs.ZMin, ci); sb.Append(delim);
                        AppendFloat(sb, bs.ZMax, ci); sb.Append(delim);
                        AppendFloat(sb, bs.ZMean, ci); sb.Append(delim);
                        AppendFloat(sb, bs.ZSigma, ci); sb.Append(delim);
                        AppendFloat(sb, bs.ZKernelC, ci); sb.Append(delim);
                        AppendFloat(sb, bs.ZNormalizeKernelC, ci);
                    }
                    else
                    {
                        sb.Append(delim); sb.Append(delim); sb.Append(delim);
                        sb.Append(delim); sb.Append(delim); sb.Append("");
                    }

                    sb.AppendLine();
                }
            }

            File.WriteAllText(_saveFileDialog.FileName, sb.ToString());
            MessageBox.Show(Resources.Msg_StatsExportComplete, Resources.Msg_Export, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Resources.Error_Export, ex.Message), Resources.Msg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void AppendFloat(StringBuilder sb, float value, System.Globalization.CultureInfo ci)
    {
        sb.Append(value.ToString(ci));
    }
}

// ── View model for PropertyGrid ────────────────────────────────

file class ClassBandStatsViewModel
{
    public static object Create(ClassBandStatistics stats, bool showZ)
    {
        if (showZ)
            return new ClassBandStatsWithZ(stats);
        return new ClassBandStatsNoZ(stats);
    }
}

[TypeConverter(typeof(ExpandableObjectConverter))]
file class ClassBandStatsNoZ
{
    protected readonly ClassBandStatistics S;

    public ClassBandStatsNoZ(ClassBandStatistics s) => S = s;

    [Category("Raw"), DisplayName("Count")]
    public int Count => S.Count;
    [Category("Raw"), DisplayName("Sum")]
    public float Sum => S.Sum;
    [Category("Raw"), DisplayName("Minimum")]
    public float Minimum => S.Minimum;
    [Category("Raw"), DisplayName("Maximum")]
    public float Maximum => S.Maximum;
    [Category("Raw"), DisplayName("Mean")]
    public float Mean => S.Mean;
    [Category("Raw"), DisplayName("Sigma (σ)")]
    public float Sigma => S.Sigma;
    [Category("Raw"), DisplayName("Variance (σ²)")]
    public float Variance => S.Variance;
    [Category("Raw"), DisplayName("Skewness")]
    public float Skewness => S.Skewness;
    [Category("Raw"), DisplayName("Kurtosis")]
    public float Kurtosis => S.Kurtosis;
    [Category("Raw"), DisplayName("KernelC (bandwidth)")]
    public float KernelC => S.KernelC;
    [Category("Normalized"), DisplayName("Normalize KernelC")]
    public float NormalizeKernelC => S.NormalizeKernelC;
}

[TypeConverter(typeof(ExpandableObjectConverter))]
file class ClassBandStatsWithZ : ClassBandStatsNoZ
{
    public ClassBandStatsWithZ(ClassBandStatistics s) : base(s) { }

    [Category("Z-Score"), DisplayName("Z Minimum")]
    public float ZMin => S.ZMin;
    [Category("Z-Score"), DisplayName("Z Maximum")]
    public float ZMax => S.ZMax;
    [Category("Z-Score"), DisplayName("Z Mean")]
    public float ZMean => S.ZMean;
    [Category("Z-Score"), DisplayName("Z Sigma")]
    public float ZSigma => S.ZSigma;
    [Category("Z-Score"), DisplayName("Z KernelC (bandwidth)")]
    public float ZKernelC => S.ZKernelC;
    [Category("Normalized"), DisplayName("Z Normalize KernelC")]
    public float ZNormalizeKernelC => S.ZNormalizeKernelC;
}
