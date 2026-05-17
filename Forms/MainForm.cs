using modified_structure_analysis;
using modified_structure_analysis.Properties;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OSGeo.GDAL;
using GdalBand = OSGeo.GDAL.Band;
using System.ComponentModel;
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

        private List<ClassificationRule> _classificationRules = new();

        private PlotModel _kdeModel;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "All|*.tif;*.tiff;*.img;*.csv;*.txt|GeoTIFF|*.tif;*.tiff|ERDAS|*.img|CSV|*.csv|Text file|*.txt";
            openFileDialog1.Multiselect = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _kdeModel = new PlotModel();
            _kdeModel.Axes.Add(new LinearAxis { Key = "X", Position = AxisPosition.Bottom, Title = "Normalized Value", Minimum = 0d, Maximum = 1d });
            _kdeModel.Axes.Add(new LinearAxis { Key = "Y", Position = AxisPosition.Left, Title = "Density", Minimum = 0d });
            _kdeModel.Legends.Add(new Legend { LegendPosition = LegendPosition.TopRight });
            kdePlotView.Model = _kdeModel;

            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 2)
            {
                var rule = _classificationRules[e.RowIndex];
                var editor = new RuleEditorForm(_bands, rule);
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    UpdateClassificationRulesGrid();
                }
            }
        }

        private void AddClassificationRule(object sender, EventArgs e)
        {
            var editor = new RuleEditorForm(_bands, null);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                _classificationRules.Add(editor.ResultRule);
                UpdateClassificationRulesGrid();
            }
        }

        private void DeleteClassificationRule(object sender, EventArgs e)
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
                _classificationRules.RemoveAt(selectedIndex);
                UpdateClassificationRulesGrid();
            }
        }

        private void MoveRuleUp(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;

            if (selectedIndex == 0)
                return;

            var rule = _classificationRules[selectedIndex];
            _classificationRules.RemoveAt(selectedIndex);
            _classificationRules.Insert(selectedIndex - 1, rule);

            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex - 1].Selected = true;
            UpdateClassificationRulesGrid();
            dataGridView1.Rows[selectedIndex - 1].Selected = true;
        }

        private void MoveRuleDown(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rule to move.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;

            if (selectedIndex >= _classificationRules.Count - 1)
                return;

            var rule = _classificationRules[selectedIndex];
            _classificationRules.RemoveAt(selectedIndex);
            _classificationRules.Insert(selectedIndex + 1, rule);

            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex + 1].Selected = true;
            UpdateClassificationRulesGrid();
            dataGridView1.Rows[selectedIndex + 1].Selected = true;
        }

        private void UpdateClassificationRulesGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var rule in _classificationRules)
            {
                int rowIndex = dataGridView1.Rows.Add();
                var row = dataGridView1.Rows[rowIndex];

                row.Cells[0].Value = CreateColorBitmap(rule.Color);
                row.Cells[1].Value = rule.GenerateName();
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

        private void BuildScatterPlot(object? sender, EventArgs e)
        {
            if (scatterXListBox.SelectedItem == null || scatterYListBox.SelectedItem == null)
                return;

            Band? bandX = scatterXListBox.SelectedItem as Band;
            Band? bandY = scatterYListBox.SelectedItem as Band;

            if (bandX == null || bandY == null)
                return;

            var model = new PlotModel();
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

        private void KdeSingle(object? sender, EventArgs e)
        {
            if (kdeBandsListBox!.SelectedItems.Count == 0)
                return;

            kdePlotView.Model = null;

            foreach (var item in kdeBandsListBox.SelectedItems)
            {
                Band band = item as Band;
                if (band == null) continue;

                var series = new FunctionSeries { Title = $"Single: {band.Name}", XAxisKey = "X", YAxisKey = "Y" };
                for (double x = 0; x <= 1; x += 0.01)
                {
                    series.Points.Add(new DataPoint(x, band.GetKernelDensityEstimate((float)x)));
                }

                _kdeModel!.Series.Add(series);
            }

            kdePlotView.Model = _kdeModel;
            PlotView_DoubleClick(kdePlotView, e);
        }

        private void KdeProduct(object? sender, EventArgs e)
        {
            if (kdeBandsListBox!.SelectedItems.Count < 2)
            {
                MessageBox.Show("Select at least 2 bands for Product", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            kdePlotView.Model = null;

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

            kdePlotView.Model = _kdeModel;
            PlotView_DoubleClick(kdePlotView, e);
        }

        private void KdeMultivariate(object? sender, EventArgs e)
        {
            if (kdeBandsListBox!.SelectedItems.Count < 2)
            {
                MessageBox.Show("Select at least 2 bands for Multivariate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            kdePlotView.Model = null;

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

            kdePlotView.Model = _kdeModel;
            PlotView_DoubleClick(kdePlotView, e);
        }

        private void ClearKdePlot(object? sender, EventArgs e)
        {
            if (_kdeModel == null) return;

            _kdeModel.Series.Clear();

            PlotView_DoubleClick(kdePlotView, e);
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
                { _redBand = band; redToolStripDropDownButton.Text = _redBand.ToString(); });
                greenToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _greenBand = band; greenToolStripDropDownButton.Text = _greenBand.ToString(); });
                blueToolStripDropDownButton.DropDownItems.Add(band.Name, null, (object? sender, EventArgs e) =>
                { _blueBand = band; blueToolStripDropDownButton.Text = _blueBand.ToString(); });

                scatterXListBox.Items.Add(band);
                scatterYListBox.Items.Add(band);
                kdeBandsListBox.Items.Add(band);

                correlationDataGridView.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = band.Name });
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
                _geoTransform = null;
                correlationDataGridView.Columns.Clear();

                string[] fileNames = openFileDialog1.FileNames;

                foreach (string fileName in fileNames)
                {
                    string ext = Path.GetExtension(fileName).ToLower();

                    switch (ext)
                    {
                        case ".txt":
                        case ".csv":
                            if (_bands.Count > 0)
                            {
                                MessageBox.Show("Cannot mix text files with image files. Please open them separately.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                _bands.Clear();
                                return;
                            }
                            if (ext == ".csv")
                                ReadCsvFile(fileName);
                            else
                                ReadTextFile(fileName, '\t');
                            break;
                        case ".tif":
                        case ".tiff":
                        case ".img":
                            if (_geoTransform != null && (_bands.Count > 0 && _bands[0].GeoTransform == null))
                            {
                                MessageBox.Show("Cannot mix text files with image files. Please open them separately.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                _bands.Clear();
                                return;
                            }
                            LoadGeoTiff(fileName);
                            break;
                    }
                }

                if (_bands.Count == 0)
                    return;

                UpdateBandsList();
                UpdateImage(sender, e);
                CalculateBandsStatistics();
                CalcCorrelation();
            }
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
                Gdal.AllRegister();

                using (Dataset ds = Gdal.Open(fileName, Access.GA_ReadOnly))
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
                        if (!IsGeoTransformEqual(_geoTransform, newGeoTransform))
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
                            string bandName = Path.GetFileNameWithoutExtension(fileName);
                            if (ds.RasterCount > 1)
                                bandName += $"_{i}";

                            string desc = gdalBand.GetDescription();
                            if (!string.IsNullOrWhiteSpace(desc))
                                bandName = desc;

                            double[] minmax = new double[2];
                            gdalBand.ComputeRasterMinMax(minmax, 0);

                            float[] values = new float[width * height];
                            gdalBand.ReadRaster(0, 0, width, height, values, width, height, 0, 0);

                            Band band = new Band(bandName);
                            band.SetDimensions(width, height);
                            band.SetGeoTransform(_geoTransform);
                            band.SetMinMax((float)minmax[0], (float)minmax[1]);
                            for (int idx = 0; idx < values.Length; idx++)
                            {
                                band.SetValueAt(idx, values[idx]);
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

        private bool IsGeoTransformEqual(GeoTransform gt1, GeoTransform gt2)
        {
            const double eps = 1e-6;
            return Math.Abs(gt1.OriginX - gt2.OriginX) < eps &&
                   Math.Abs(gt1.OriginY - gt2.OriginY) < eps &&
                   Math.Abs(gt1.PixelSizeX - gt2.PixelSizeX) < eps &&
                   Math.Abs(gt1.PixelSizeY - gt2.PixelSizeY) < eps;
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
                band.SetGeoTransform(new GeoTransform(minX, maxY, _cellSize, _cellSize));
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
                band.CalculateStatistics();
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

            if (band == null)
                return;

            int columnsCount = BrooksCarrutherDivisionRule(band.Count);
            float columnsWidth = (band.Maximum - band.Minimum) / columnsCount;

            int[] pointses = new int[columnsCount];
            int[] asseses = new int[columnsCount];

            for(int i = 0; i < band.Count; i++)
            {
                float x = band.GetValue(i);

                if (x == 0)
                    continue;

                for (int j = 0; j < columnsCount; j++)
                {
                    float min = j * columnsWidth + band.Minimum;

                    if (min <= x && x < min + columnsWidth)
                        pointses[j]++;

                    if (x <= min)
                        asseses[j]++;
                }
            }

            var histSeries = new HistogramSeries();
            var lineSeries = new LineSeries();

            for (int i = 0; i < columnsCount; i++)
            {
                float min = i * columnsWidth + band.Minimum;

                histSeries.Items.Add(new HistogramItem(min, min + columnsWidth, (float)pointses[i] / band.Count, pointses[i]));
                lineSeries.Points.Add(new DataPoint(min, (float)asseses[i] / band.Count));
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

            //ApplyPlotSettings(plot);
            histogramPlotView.Model = plot;
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
                        row.Cells.Add(new DataGridViewTextBoxCell() { Style = new DataGridViewCellStyle() { BackColor = Color.Red } });
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
            if (_classificationRules.Count == 0)
            {
                MessageBox.Show("No classification rules defined. Please add rules first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_bands.Count == 0)
            {
                MessageBox.Show("No bands loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            backgroundWorker.RunWorkerAsync(_classificationRules);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;
            List<ClassificationRule>? rules = e.Argument as List<ClassificationRule>;

            if (worker == null || rules == null || _bands.Count == 0)
                return;

            int totalPixels = _width * _height;
            int lastReportedProgress = -1;
            int reportInterval = Math.Max(1, totalPixels / 100);

            worker.ReportProgress(0, "Starting classification...");

            var engine = new ClassificationEngine(_bands, rules);

            var classificationResult = new ClassificationResult(_width, _height, rules);
            int currentPixel = 0;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int pixelIndex = y * _width + x;
                    int? classIndex = engine.EvaluatePixel(pixelIndex);

                    if (classIndex.HasValue)
                        classificationResult.SetClass(pixelIndex, classIndex.Value);

                    currentPixel++;

                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (currentPixel % reportInterval == 0 || currentPixel == totalPixels)
                    {
                        int progress = (currentPixel * 50) / totalPixels;
                        if (progress != lastReportedProgress)
                        {
                            lastReportedProgress = progress;
                            worker.ReportProgress(progress, $"Classifying: {currentPixel}/{totalPixels} pixels ({progress}%)");
                        }
                    }
                }
            }

            worker.ReportProgress(50, "Generating bitmap...");

            Bitmap bitmap = new Bitmap(_width, _height);
            Rectangle rect = new Rectangle(0, 0, _width, _height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * _height;
            byte[] rgbValues = new byte[bytes];

            int lastRenderProgress = 49;
            int renderReportInterval = Math.Max(1, _height / 55);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;
                    Color color = classificationResult.GetPixelColor(i);

                    int idx = (y * bmpData.Stride) + (x * 4);
                    rgbValues[idx] = color.B;
                    rgbValues[idx + 1] = color.G;
                    rgbValues[idx + 2] = color.R;
                    rgbValues[idx + 3] = color.A;
                }

                if (y % renderReportInterval == 0 || y == _height - 1)
                {
                    int progress = 50 + (y * 50 / _height);
                    if (progress != lastRenderProgress)
                    {
                        lastRenderProgress = progress;
                        worker.ReportProgress(progress, $"Rendering: {y}/{_height} rows ({progress}%)");
                    }
                }
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            worker.ReportProgress(100, "Complete");

            e.Result = (bitmap, classificationResult);
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
                if (e.Result is ValueTuple<Bitmap, ClassificationResult> result)
                {
                    viewport2.UpdateImage(result.Item1);

                    var stats = result.Item2.GetClassStatistics();
                    var statsText = "Classification Statistics:\n";
                    for (int i = 0; i < result.Item2.Rules.Count; i++)
                    {
                        string ruleName = result.Item2.Rules[i].GenerateName();
                        statsText += $"Class {i} ({ruleName}): {stats.GetValueOrDefault(i, 0)} pixels\n";
                    }
                    statsText += $"Undefined: {stats.GetValueOrDefault(-1, 0)} pixels";
                    MessageBox.Show(statsText, "Classification Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mainStatusLabel.Text = e.UserState?.ToString();
            mainProgressBar.Value = e.ProgressPercentage;
        }

        //public void ApplyPlotSettings(PlotModel model)
        //{
        //    foreach (var axis in model.Axes)
        //    {
        //        if (axis.Position == AxisPosition.Bottom)
        //            _plotSettings.XAxis.ApplyToAxis(axis);
        //        else if (axis.Position == AxisPosition.Left || axis.Position == AxisPosition.Right)
        //            _plotSettings.YAxis.ApplyToAxis(axis);
        //    }

        //    if (_plotSettings.Legend.IsVisible)
        //    {
        //        if (model.Legends.Count == 0)
        //            model.Legends.Add(new Legend());
        //        model.Legends[0].LegendPosition = _plotSettings.Legend.Position;
        //    }

        //    foreach (var axis in model.Axes)
        //    {
        //        axis.MajorGridlineStyle = _plotSettings.Grid.IsVisible ? LineStyle.Solid : LineStyle.None;
        //        axis.MinorGridlineStyle = _plotSettings.Grid.IsMinorGridVisible ? LineStyle.Dot : LineStyle.None;
        //    }
        //}

        //public PlotSettings GetPlotSettings() => _plotSettings;

        //public void SetPlotSettings(PlotSettings settings)
        //{
        //    _plotSettings = settings;
        //}

        //public void SetAxisMinMax(double? xMin, double? xMax, double? yMin, double? yMax)
        //{
        //    _plotSettings.XAxis.Minimum = xMin;
        //    _plotSettings.XAxis.Maximum = xMax;
        //    _plotSettings.YAxis.Minimum = yMin;
        //    _plotSettings.YAxis.Maximum = yMax;
        //}

        //public void SetAxisLogarithmic(bool xLog, bool yLog)
        //{
        //    _plotSettings.XAxis.IsLogarithmic = xLog;
        //    _plotSettings.YAxis.IsLogarithmic = yLog;
        //}

        //public void SetLegendVisible(bool visible)
        //{
        //    _plotSettings.Legend.IsVisible = visible;
        //}

        //public void SetLegendPosition(LegendPosition position)
        //{
        //    _plotSettings.Legend.Position = position;
        //}

        //public void SetGridVisible(bool visible)
        //{
        //    _plotSettings.Grid.IsVisible = visible;
        //}

        //public void RefreshAllPlots()
        //{
        //    if (histogramPlotView?.Model != null)
        //        ApplyPlotSettings(histogramPlotView.Model);
        //    if (scatterPlotView?.Model != null)
        //        ApplyPlotSettings(scatterPlotView.Model);
        //    if (profilePlotView?.Model != null)
        //        ApplyPlotSettings(profilePlotView.Model);
        //    if (kdePlotView?.Model != null)
        //        ApplyPlotSettings(kdePlotView.Model);
        //    if (kdePlotView?.Model != null)
        //        ApplyPlotSettings(kdePlotView.Model);
        //}

        //private void OpenPlotSettings(object? sender, EventArgs e)
        //{
        //    var form = new Form { Text = "Plot Settings", Width = 400, Height = 350, StartPosition = FormStartPosition.CenterParent };

        //    var chkLegend = new CheckBox { Text = "Show Legend", Left = 20, Top = 20, Width = 150, Checked = _plotSettings.Legend.IsVisible };
        //    chkLegend.CheckedChanged += (s, args) => { _plotSettings.Legend.IsVisible = chkLegend.Checked; };

        //    var chkGrid = new CheckBox { Text = "Show Grid", Left = 20, Top = 50, Width = 150, Checked = _plotSettings.Grid.IsVisible };
        //    chkGrid.CheckedChanged += (s, args) => { _plotSettings.Grid.IsVisible = chkGrid.Checked; };

        //    var lblXMin = new Label { Left = 20, Top = 90, Width = 80, Text = "X Min:" };
        //    var txtXMin = new TextBox { Left = 100, Top = 88, Width = 80 };
        //    if (_plotSettings.XAxis.Minimum.HasValue) txtXMin.Text = _plotSettings.XAxis.Minimum.Value.ToString();

        //    var lblXMax = new Label { Left = 190, Top = 90, Width = 80, Text = "X Max:" };
        //    var txtXMax = new TextBox { Left = 270, Top = 88, Width = 80 };
        //    if (_plotSettings.XAxis.Maximum.HasValue) txtXMax.Text = _plotSettings.XAxis.Maximum.Value.ToString();

        //    var lblYMin = new Label { Left = 20, Top = 130, Width = 80, Text = "Y Min:" };
        //    var txtYMin = new TextBox { Left = 100, Top = 128, Width = 80 };
        //    if (_plotSettings.YAxis.Minimum.HasValue) txtYMin.Text = _plotSettings.YAxis.Minimum.Value.ToString();

        //    var lblYMax = new Label { Left = 190, Top = 130, Width = 80, Text = "Y Max:" };
        //    var txtYMax = new TextBox { Left = 270, Top = 128, Width = 80 };
        //    if (_plotSettings.YAxis.Maximum.HasValue) txtYMax.Text = _plotSettings.YAxis.Maximum.Value.ToString();

        //    var cmbLegendPos = new ComboBox { Left = 100, Top = 170, Width = 150 };
        //    cmbLegendPos.Items.AddRange(new string[] { "Right Top", "Left Top", "Top Right", "Top Left" });
        //    cmbLegendPos.SelectedIndex = 0;
        //    var lblLegendPos = new Label { Left = 20, Top = 172, Width = 80, Text = "Legend:" };

        //    var btnApply = new Button { Text = "Apply", Left = 100, Top = 220, Width = 80 };
        //    btnApply.Click += (s, args) =>
        //    {
        //        if (double.TryParse(txtXMin.Text, out double xMin)) _plotSettings.XAxis.Minimum = xMin;
        //        if (double.TryParse(txtXMax.Text, out double xMax)) _plotSettings.XAxis.Maximum = xMax;
        //        if (double.TryParse(txtYMin.Text, out double yMin)) _plotSettings.YAxis.Minimum = yMin;
        //        if (double.TryParse(txtYMax.Text, out double yMax)) _plotSettings.YAxis.Maximum = yMax;

        //        _plotSettings.Legend.IsVisible = chkLegend.Checked;
        //        _plotSettings.Grid.IsVisible = chkGrid.Checked;

        //        RefreshAllPlots();
        //    };

        //    var btnClose = new Button { Text = "Close", Left = 190, Top = 220, Width = 80 };
        //    btnClose.Click += (s, args) => form.Close();

        //    form.Controls.AddRange(new Control[] { chkLegend, chkGrid, lblXMin, txtXMin, lblXMax, txtXMax, lblYMin, txtYMin, lblYMax, txtYMax, cmbLegendPos, lblLegendPos, btnApply, btnClose });
        //    form.ShowDialog();
        //}
    }
}
