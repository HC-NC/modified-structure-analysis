using modified_structure_analysis.Properties;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace modified_structure_analysis
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

        private ComboBox scatterXComboBox;
        private ComboBox scatterYComboBox;
        private Button buildScatterButton;
        private PlotView scatterPlotView;
        private ComboBox profileBandComboBox;
        private ComboBox profileAxisComboBox;
        private NumericUpDown profilePositionNumeric;
        private Button buildProfileButton;
        private PlotView profilePlotView;
        private ListBox kdeBandsListBox;
        private Button kdeSingleButton;
        private Button kdeProductButton;
        private Button kdeMultivariateButton;
        private Button kdeClearButton;
        private PlotView kdePlotView;
        private PlotModel? _kdeModel;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text file|*.txt";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeExplorationUI();
        }

        private void InitializeExplorationUI()
        {
            var scatterPanel = new Panel { Dock = DockStyle.Top, Height = 45 };
            scatterXComboBox = new ComboBox { Left = 10, Top = 10, Width = 150 };
            scatterYComboBox = new ComboBox { Left = 170, Top = 10, Width = 150 };
            buildScatterButton = new Button { Left = 330, Top = 8, Width = 80, Text = "Build" };
            buildScatterButton.Click += BuildScatterPlot;
            scatterPanel.Controls.AddRange(new Control[] { scatterXComboBox, scatterYComboBox, buildScatterButton });

            var labelX = new Label { Left = 10, Top = 35, Width = 150, Text = "X Axis:" };
            var labelY = new Label { Left = 170, Top = 35, Width = 150, Text = "Y Axis:" };
            scatterPanel.Controls.AddRange(new Control[] { labelX, labelY });

            scatterPlotView = new PlotView { Dock = DockStyle.Fill };

            var scatterContainer = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical };
            scatterContainer.Panel1.Controls.Add(scatterPanel);
            scatterContainer.Panel2.Controls.Add(scatterPlotView);
            scatterContainer.SplitterDistance = 45;

            var profilePanel = new Panel { Dock = DockStyle.Top, Height = 45 };
            profileBandComboBox = new ComboBox { Left = 10, Top = 10, Width = 120 };
            profileAxisComboBox = new ComboBox { Left = 140, Top = 10, Width = 100 };
            profileAxisComboBox.Items.AddRange(new string[] { "Horizontal", "Vertical" });
            profileAxisComboBox.SelectedIndex = 0;
            profilePositionNumeric = new NumericUpDown { Left = 250, Top = 10, Width = 80, Maximum = 10000 };
            profilePositionNumeric.Value = 50;
            buildProfileButton = new Button { Left = 340, Top = 8, Width = 80, Text = "Profile" };
            buildProfileButton.Click += BuildProfilePlot;
            profilePanel.Controls.AddRange(new Control[] { profileBandComboBox, profileAxisComboBox, profilePositionNumeric, buildProfileButton });

            var labelBand = new Label { Left = 10, Top = 35, Width = 120, Text = "Band:" };
            var labelAxis = new Label { Left = 140, Top = 35, Width = 100, Text = "Axis:" };
            var labelPos = new Label { Left = 250, Top = 35, Width = 80, Text = "Position:" };
            profilePanel.Controls.AddRange(new Control[] { labelBand, labelAxis, labelPos });

            profilePlotView = new PlotView { Dock = DockStyle.Fill };

            var profileContainer = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical };
            profileContainer.Panel1.Controls.Add(profilePanel);
            profileContainer.Panel2.Controls.Add(profilePlotView);
            profileContainer.SplitterDistance = 45;

            var kdePanel = new Panel { Dock = DockStyle.Top, Height = 70 };
            kdeBandsListBox = new ListBox { Left = 10, Top = 10, Width = 200, Height = 50, SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended };
            kdeSingleButton = new Button { Left = 220, Top = 8, Width = 80, Text = "Single" };
            kdeSingleButton.Click += KdeSingle;
            kdeProductButton = new Button { Left = 310, Top = 8, Width = 80, Text = "Product" };
            kdeProductButton.Click += KdeProduct;
            kdeMultivariateButton = new Button { Left = 400, Top = 8, Width = 100, Text = "Multivar" };
            kdeMultivariateButton.Click += KdeMultivariate;
            kdeClearButton = new Button { Left = 510, Top = 8, Width = 70, Text = "Clear" };
            kdeClearButton.Click += ClearKdePlot;
            kdePanel.Controls.AddRange(new Control[] { kdeBandsListBox, kdeSingleButton, kdeProductButton, kdeMultivariateButton, kdeClearButton });

            var labelKdeBands = new Label { Left = 10, Top = 35, Width = 200, Text = "Bands (multi-select):" };
            kdePanel.Controls.Add(labelKdeBands);

            kdePlotView = new PlotView { Dock = DockStyle.Fill };
            kdePlotView.DoubleClick += PlotView_DoubleClick;

            kdePlotView = new PlotView { Dock = DockStyle.Fill };

            var kdeContainer = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical };
            kdeContainer.Panel1.Controls.Add(kdePanel);
            kdeContainer.Panel2.Controls.Add(kdePlotView);
            kdeContainer.SplitterDistance = 70;

            _kdeModel = new PlotModel { Title = "KDE Comparison" };
            _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Normalized Value" });
            _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Density" });
            _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
            kdePlotView.Model = _kdeModel;

            var explorationPanel = new TabControl { Dock = DockStyle.Fill };
            var scatterTab = new TabPage { Text = "Scatter Plot" };
            var profileTab = new TabPage { Text = "Profile" };
            var kdeTab = new TabPage { Text = "KDE" };
            scatterTab.Controls.Add(scatterContainer);
            profileTab.Controls.Add(profileContainer);
            kdeTab.Controls.Add(kdeContainer);
            explorationPanel.TabPages.Add(scatterTab);
            explorationPanel.TabPages.Add(profileTab);
            explorationPanel.TabPages.Add(kdeTab);

            var existingControls = explorationTabPage.Controls.OfType<Control>().ToList();
            foreach (var ctrl in existingControls)
            {
                explorationTabPage.Controls.Remove(ctrl);
            }

            explorationTabPage.Controls.Add(explorationPanel);
        }

        private void BuildScatterPlot(object? sender, EventArgs e)
        {
            if (scatterXComboBox.SelectedItem == null || scatterYComboBox.SelectedItem == null)
                return;

            Band? bandX = scatterXComboBox.SelectedItem as Band;
            Band? bandY = scatterYComboBox.SelectedItem as Band;

            if (bandX == null || bandY == null)
                return;

            var model = new PlotModel { Title = $"Scatter: {bandX.Name} vs {bandY.Name}" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = bandX.Name });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = bandY.Name });

            var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 2 };

            for (int i = 0; i < _width * _height; i++)
            {
                float vx = bandX.GetNormalizedValue(i);
                float vy = bandY.GetNormalizedValue(i);
                if (vx > 0 && vy > 0)
                {
                    scatterSeries.Points.Add(new ScatterPoint(vx, vy));
                }
            }

            model.Series.Add(scatterSeries);
            scatterPlotView.Model = model;
        }

        private void BuildProfilePlot(object? sender, EventArgs e)
        {
            if (_bands.Count == 0 || profileBandComboBox.SelectedItem == null)
                return;

            Band band = profileBandComboBox.SelectedItem as Band;
            if (band == null) return;

            var model = new PlotModel { Title = $"Brightness Profile - {band.Name}" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Pixel" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Value" });

            var lineSeries = new LineSeries();

            bool isHorizontal = profileAxisComboBox.SelectedIndex == 0;
            int position = (int)profilePositionNumeric.Value;

            if (isHorizontal)
            {
                position = Math.Min(position, _height - 1);
                for (int x = 0; x < _width; x++)
                {
                    int idx = position * _width + x;
                    lineSeries.Points.Add(new DataPoint(x, band.GetValue(idx)));
                }
            }
            else
            {
                position = Math.Min(position, _width - 1);
                for (int y = 0; y < _height; y++)
                {
                    int idx = y * _width + position;
                    lineSeries.Points.Add(new DataPoint(y, band.GetValue(idx)));
                }
            }

            model.Series.Add(lineSeries);
            profilePlotView.Model = model;
        }

        private void KdeSingle(object? sender, EventArgs e)
        {
            if (_kdeModel == null)
            {
                _kdeModel = new PlotModel { Title = "KDE Comparison" };
                _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Normalized Value" });
                _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Density" });
                _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
                kdePlotView.Model = _kdeModel;
            }

            if (kdeBandsListBox!.SelectedItems.Count == 0)
                return;

            foreach (var item in kdeBandsListBox.SelectedItems)
            {
                Band band = item as Band;
                if (band == null) continue;

                var series = new FunctionSeries { Title = $"Single: {band.Name}" };
                for (double x = 0; x <= 1; x += 0.01)
                {
                    series.Points.Add(new DataPoint(x, band.GetKernelDensityEstimate((float)x)));
                }
                _kdeModel!.Series.Add(series);
            }
            kdePlotView.Refresh();
        }

        private void KdeProduct(object? sender, EventArgs e)
        {
            if (_kdeModel == null)
            {
                _kdeModel = new PlotModel { Title = "KDE Comparison" };
                _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Normalized Value" });
                _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Density" });
                _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
                kdePlotView.Model = _kdeModel;
            }

            if (kdeBandsListBox!.SelectedItems.Count < 2)
            {
                MessageBox.Show("Select at least 2 bands for Product", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedBands = new List<Band>();
            foreach (var item in kdeBandsListBox.SelectedItems)
            {
                Band band = item as Band;
                if (band != null) selectedBands.Add(band);
            }

            var series = new FunctionSeries { Title = $"Product: {string.Join("×", selectedBands.Select(b => b.Name))}" };
            for (double x = 0; x <= 1; x += 0.01)
            {
                double product = 1;
                foreach (var band in selectedBands)
                {
                    product *= band.GetKernelDensityEstimate((float)x);
                }
                series.Points.Add(new DataPoint(x, product));
            }
            _kdeModel!.Series.Add(series);
            kdePlotView.Refresh();
        }

        private void KdeMultivariate(object? sender, EventArgs e)
        {
            if (_kdeModel == null)
            {
                _kdeModel = new PlotModel { Title = "KDE Comparison" };
                _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Normalized Value" });
                _kdeModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Density" });
                _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
                kdePlotView.Model = _kdeModel;
            }

            if (kdeBandsListBox!.SelectedItems.Count < 2)
            {
                MessageBox.Show("Select at least 2 bands for Multivariate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedBands = new List<Band>();
            foreach (var item in kdeBandsListBox.SelectedItems)
            {
                Band band = item as Band;
                if (band != null) selectedBands.Add(band);
            }

            int n = _width * _height;

            double productBandwidths = 1;
            foreach (var b in selectedBands)
            {
                productBandwidths *= b.NormalizeKernelC;
            }
            double normalization = n * productBandwidths;

            var series = new FunctionSeries { Title = $"Multivariate: {string.Join(",", selectedBands.Select(b => b.Name))}" };
            for (double x = 0; x <= 1; x += 0.01)
            {
                double density = 0;

                for (int pixel = 0; pixel < n; pixel++)
                {
                    double kernelProduct = 1;
                    foreach (var band in selectedBands)
                    {
                        double v = band.GetNormalizedValue(pixel);
                        if (v <= 0) { kernelProduct = 0; break; }
                        kernelProduct *= KernelFunctions.GetKernel(band.KernelType, (x - v) / band.NormalizeKernelC);
                    }
                    density += kernelProduct;
                }

                density /= normalization;
                series.Points.Add(new DataPoint(x, density));
            }
            _kdeModel!.Series.Add(series);
            kdePlotView.Refresh();
        }

        private void ClearKdePlot(object? sender, EventArgs e)
        {
            if (_kdeModel == null) return;
            _kdeModel.Series.Clear();
            _kdeModel.Title = "KDE Comparison";
            kdePlotView.Refresh();
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

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            if (scatterXComboBox != null) scatterXComboBox.Items.Clear();
            if (scatterYComboBox != null) scatterYComboBox.Items.Clear();
            if (profileBandComboBox != null) profileBandComboBox.Items.Clear();
            if (kdeBandsListBox != null) kdeBandsListBox.Items.Clear();

            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;

            if (_bands.Count == 0)
                return;
            else if (_bands.Count >= 3)
            {
                _redBand = _bands[0];
                _greenBand = _bands[1];
                _blueBand = _bands[2];
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
                { _redBand = band; redToolStripDropDownButton.Text = _redBand.ToString(); });
                greenToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _greenBand = band; greenToolStripDropDownButton.Text = _greenBand.ToString(); });
                blueToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _blueBand = band; blueToolStripDropDownButton.Text = _blueBand.ToString(); });

                if (scatterXComboBox != null) scatterXComboBox.Items.Add(band);
                if (scatterYComboBox != null) scatterYComboBox.Items.Add(band);
                if (profileBandComboBox != null) profileBandComboBox.Items.Add(band);
                if (kdeBandsListBox != null) kdeBandsListBox.Items.Add(band);

                correlationDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = band.Name });

                comboBox1.Items.Add(band);
                comboBox2.Items.Add(band);
                comboBox3.Items.Add(band);
            }

            bandListBox.SelectedIndex = 0;
        }

        private void UpdateImage(object sender, EventArgs e)
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
                    rgbValues[idx + 3] = 255;
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            viewport1.UpdateImage(bitmap);
        }

        private void CalculateBandsStatistics()
        {
            foreach (Band band in _bands)
            {
                band.CalculateStatistics();
                band.Normalize();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _bands.Clear();

                correlationDataGridView.Columns.Clear();

                switch (Path.GetExtension(openFileDialog1.FileName))
                {
                    case ".txt":
                        ReadTextFile();
                        break;
                }

                if (_bands.Count == 0)
                    return;

                UpdateBandsList();
                UpdateImage(sender, e);
                CalculateBandsStatistics();
                CalcCorrelation();
            }
        }

        private void ReadTextFile()
        {
            StreamReader reader = new StreamReader(openFileDialog1.FileName);

            List<string> hd = new List<string>();

            foreach (string s in reader.ReadLine().Split('\t'))
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
                values = reader.ReadLine().Split('\t');

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
                int gridY = (int)Math.Floor((point.y - minY) / _cellSize);

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
                        band.SetPixelValue(cell.Key.x, cell.Key.y, val);
                    }
                }
            }

            foreach (Band band in _bands)
            {
                band.CalculateStatistics();
            }

            _geoTransform = new GeoTransform(minX, maxY, _cellSize, -_cellSize);
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

            if (band == null)
                return;

            int columnsCount = BrooksCarrutherDivisionRule(band.Count);
            float columnsWidth = (band.Maximum - band.Minimum) / columnsCount;

            var histSeries = new HistogramSeries();
            var lineSeries = new LineSeries();

            int pointsCount;
            float assesCount;

            float min;
            float x;

            for (int i = 0; i < columnsCount; i++)
            {
                pointsCount = 0;
                assesCount = 0;

                min = i * columnsWidth + band.Minimum;

                for (int j = 0; j < band.Count; j++)
                {
                    x = band.GetValue(j);

                    if (min <= x && x < min + columnsWidth)
                        pointsCount++;

                    if (x <= min)
                        assesCount++;
                }

                histSeries.Items.Add(new HistogramItem(min, min + columnsWidth, (float)pointsCount / band.Count, pointsCount));
                lineSeries.Points.Add(new DataPoint(min, assesCount / band.Count));
            }

            PlotModel plot = new PlotModel();

            plot.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Minimum = band.Minimum, Maximum = band.Maximum });
            plot.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Minimum = 0, Key = "axesY1" });
            plot.Axes.Add(new LinearAxis() { Position = AxisPosition.Right, Minimum = 0, Maximum = 1d, Key = "axesY2" });

            histSeries.YAxisKey = "axesY1";
            lineSeries.YAxisKey = "axesY2";

            lineSeries.Color = OxyColor.FromRgb(255, 0, 0);

            plot.Series.Add(histSeries);
            plot.Series.Add(lineSeries);

            histogramPlotView.Model = plot;
        }

        private void BuildNormalizedHistograms()
        {
            PlotModel plot = new PlotModel();
            plot.Legends.Add(new Legend());

            int pointsCount = 100;

            foreach (Band band in _bands)
            {
                var series = new FunctionSeries();
                series.Title = band.Name;

                float x;

                for (int i = 0; i < pointsCount; i++)
                {
                    x = (float)i / pointsCount;
                    series.Points.Add(new DataPoint(x, band.GetKernelDensityEstimate(x)));
                }

                plot.Series.Add(series);
            }

            plotView1.Model = plot;
        }

        private void CalcCorrelation()
        {
            DataGridViewRow row;

            Band bandX;
            Band bandY;

            for (int bandXI = 0; bandXI < _bands.Count; bandXI++)
            {
                row = new DataGridViewRow();

                bandX = _bands[bandXI];

                for (int bandYI = 0; bandYI < _bands.Count; bandYI++)
                {
                    if (bandYI == bandXI)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Style = new DataGridViewCellStyle() { BackColor = Color.Black } });
                        continue;
                    }

                    if (bandYI < bandXI)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = correlationDataGridView.Rows[bandYI].Cells[bandXI].Value });
                        continue;
                    }

                    bandY = _bands[bandYI];

                    if (bandX.Count != bandY.Count)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell() { Value = "Error" });
                        continue;
                    }

                    float tmp = 0;

                    for (int i = 0; i < bandX.Count; i++)
                    {
                        tmp += F(bandX, i) * F(bandY, i);
                    }

                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = tmp / (bandX.Count - 1) });
                }

                int rowID = correlationDataGridView.Rows.Add(row);
                correlationDataGridView.Rows[rowID].HeaderCell.Value = bandX.Name;
            }

            float F(Band band, int i)
            {
                return (band.GetValue(i) - band.Mean) / band.Sigma;
            }
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == histogramTabPage)
                BuildHistogram();

            if (e.TabPage == explorationTabPage)
                BuildNormalizedHistograms();
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
            Band band1 = comboBox1.SelectedItem as Band;
            Band band2 = comboBox2.SelectedItem as Band;
            Band band3 = comboBox3.SelectedItem as Band;

            if (band1 == null || band2 == null || band3 == null || band1 == band2 || band1 == band3 || band2 == band3)
            {
                MessageBox.Show("Error!!!");
                return;
            }

            backgroundWorker.RunWorkerAsync(new ClassificationParams(band1, band2, band3));
        }

        private class ClassificationParams
        {
            public Band Band1 { get; }
            public Band Band2 { get; }
            public Band Band3 { get; }

            public ClassificationParams(Band b1, Band b2, Band b3)
            {
                Band1 = b1;
                Band2 = b2;
                Band3 = b3;
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            ClassificationParams? p = e.Argument as ClassificationParams;

            if (worker == null || p == null)
                return;

            Bitmap bitmap = new Bitmap(_width, _height);
            Rectangle rect = new Rectangle(0, 0, _width, _height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * _height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;

                    float band1v = p.Band1.GetNormalizedValue(i);
                    float band2v = p.Band2.GetNormalizedValue(i);
                    float band3v = p.Band3.GetNormalizedValue(i);

                    double band1p = 0;
                    double band2p = 0;
                    double band3p = 0;

                    for (int j = 0; j < p.Band1.Count; j++)
                    {
                        if (i == j)
                            continue;

                        band1p += KernelFunctions.GetKernel(p.Band1.KernelType, (band1v - p.Band1.GetNormalizedValue(j)) / p.Band1.NormalizeKernelC);
                        band2p += KernelFunctions.GetKernel(p.Band2.KernelType, (band2v - p.Band2.GetNormalizedValue(j)) / p.Band2.NormalizeKernelC);
                        band3p += KernelFunctions.GetKernel(p.Band3.KernelType, (band3v - p.Band3.GetNormalizedValue(j)) / p.Band3.NormalizeKernelC);
                    }

                    band1p /= p.Band1.Count * p.Band1.NormalizeKernelC;
                    band2p /= p.Band2.Count * p.Band2.NormalizeKernelC;
                    band3p /= p.Band3.Count * p.Band3.NormalizeKernelC;

                    int idx = (y * bmpData.Stride) + (x * 4);

                    if (band1p > band2p && band1p > band3p)
                    {
                        rgbValues[idx] = 0;
                        rgbValues[idx + 1] = 0;
                        rgbValues[idx + 2] = 255;
                    }
                    else if (band2p > band1p && band2p > band3p)
                    {
                        rgbValues[idx] = 0;
                        rgbValues[idx + 1] = 255;
                        rgbValues[idx + 2] = 0;
                    }
                    else if (band3p > band1p && band3p > band2p)
                    {
                        rgbValues[idx] = 255;
                        rgbValues[idx + 1] = 0;
                        rgbValues[idx + 2] = 0;
                    }
                    else
                    {
                        rgbValues[idx] = 0;
                        rgbValues[idx + 1] = 0;
                        rgbValues[idx + 2] = 0;
                    }
                    rgbValues[idx + 3] = 255;
                }

                worker.ReportProgress((int)((y + 1) * 100.0 / _height), $"Classification: {y + 1}/{_height}");
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            e.Result = bitmap;
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
                viewport2.UpdateImage(e.Result as Bitmap);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mainStatusLabel.Text = e.UserState?.ToString();
            mainProgressBar.Value = e.ProgressPercentage;
        }
    }
}
