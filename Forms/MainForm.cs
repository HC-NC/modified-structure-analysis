using modified_structure_analysis.Properties;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;

namespace modified_structure_analysis
{
    public partial class MainForm : Form
    {
        private List<Band> _bands = new List<Band>();

        private int _resolution = 30;
        private int _xMin;
        private int _yMin;
        private int _xMax;
        private int _yMax;

        private int _width;
        private int _height;

        private Band _redBand;
        private Band _greenBand;
        private Band _blueBand;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text file|*.txt";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //mainStatusLabel.Visible = false;
            //mainProgressBar.Visible = false;
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

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;

                    byte r = (byte)(_redBand.GetNormalizedValue(i) * 255);
                    byte g = (byte)(_greenBand.GetNormalizedValue(i) * 255);
                    byte b = (byte)(_blueBand.GetNormalizedValue(i) * 255);

                    Color color = Color.FromArgb(r, g, b);

                    bitmap.SetPixel(x, y, color);
                }
            }

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

                _xMin = int.MaxValue;
                _yMin = int.MaxValue;
                _xMax = int.MinValue;
                _yMax = int.MinValue;

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

            List<TextTableColumnSelector.FieldType> fieldTypes = columnSelector.GetFieldTypes();

            string[] values;
            float v;

            while (!reader.EndOfStream)
            {
                values = reader.ReadLine().Split('\t');

                for (int i = 0; i < fieldTypes.Count; i++)
                {
                    v = float.Parse(values[i]);

                    switch (fieldTypes[i])
                    {
                        case TextTableColumnSelector.FieldType.X:
                            _xMin = Math.Min(_xMin, (int)v);
                            _xMax = Math.Max(_xMax, (int)v);
                            break;
                        case TextTableColumnSelector.FieldType.Y:
                            _yMin = Math.Min(_yMin, (int)v);
                            _yMax = Math.Max(_yMax, (int)v);
                            break;
                        case TextTableColumnSelector.FieldType.Band:
                            Band? band = GetBand(hd[i], true);

                            if (band == null)
                                break;

                            band.AddValue(v);
                            break;
                        case TextTableColumnSelector.FieldType.None:
                        default:
                            continue;
                    }
                }
            }

            reader.Close();

            _width = (_xMax - _xMin) / _resolution;
            _height = (_yMax - _yMin) / _resolution;
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

        private double GetKernelGrade(Band band, double v)
        {
            double result = 0;

            for (int i = 0; i < band.Count; i++)
                result += GetEpanechnikovKernel((v - band.GetNormalizedValue(i)) / band.NormalizeKernelC);

            return result / (band.Count * band.NormalizeKernelC);
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
                    series.Points.Add(new DataPoint(x, GetKernelGrade(band, x)));
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

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker? worker = sender as BackgroundWorker;

            if (worker == null)
                return;

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mainStatusLabel.Text = e.UserState?.ToString();
            mainProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                string msg = String.Format("Result = {0}", e.Result);
                MessageBox.Show(msg);
            }
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

        private double GetUniformKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 0.5 : 0;
        }

        private double GetTriangularKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 1 - Math.Abs(u) : 0;
        }

        private double GetEpanechnikovKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 3d / 4d * (1 - Math.Pow(u, 2)) : 0;
        }

        private double GetQuarticKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 15d / 16d * Math.Pow(1 - Math.Pow(u, 2), 2) : 0;
        }

        private double GetTriweightKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 35d / 32d * Math.Pow(1 - Math.Pow(u, 2), 3) : 0;
        }

        private double GetTricubeKernel(double u)
        {
            return Math.Abs(u) <= 1 ? 70d / 81d * Math.Pow(1 - Math.Pow(Math.Abs(u), 3), 3) : 0;
        }

        private double GetGaussianKernel(double u)
        {
            return 1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-Math.Pow(u, 2) / 2);
        }

        private double GetCosineKernel(double u)
        {
            return Math.Abs(u) <= 1 ? Math.PI / 4 * Math.Cos(Math.PI / 2 * u) : 0;
        }

        private double GetLogisticKernel(double u)
        {
            return 1 / (Math.Exp(u) + 2 + Math.Exp(-u));
        }

        private double GetSigmoidFunctionKernel(double u)
        {
            return 2 / Math.PI * 1 / (Math.Exp(u) + Math.Exp(-u));
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

            Bitmap bitmap = new Bitmap(_width, _height);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;

                    float band1v = band1.GetNormalizedValue(i);
                    float band2v = band2.GetNormalizedValue(i);
                    float band3v = band3.GetNormalizedValue(i);

                    double band1p = 0;
                    double band2p = 0;
                    double band3p = 0;

                    //double band12p = 0;

                    for (int j = 0; j < band1.Count; j++)
                    {
                        if (i == j)
                            continue;

                        band1p += GetEpanechnikovKernel((band1v - band1.GetNormalizedValue(j)) / band1.NormalizeKernelC);
                        band2p += GetEpanechnikovKernel((band2v - band2.GetNormalizedValue(j)) / band2.NormalizeKernelC);
                        band3p += GetEpanechnikovKernel((band3v - band3.GetNormalizedValue(j)) / band3.NormalizeKernelC);

                        //band12p += GetEpanechnikovKernel((band1v - band1.GetNormalizedValue(j)) / band1.NormalizeKernelC) * GetEpanechnikovKernel((band2v - band2.GetNormalizedValue(j)) / band2.NormalizeKernelC);
                    }

                    band1p /= band1.Count * band1.NormalizeKernelC;
                    band2p /= band2.Count * band2.NormalizeKernelC;
                    band3p /= band3.Count * band3.NormalizeKernelC;

                    //band12p /= band1.Count * band1.NormalizeKernelC * band2.NormalizeKernelC;

                    if (band1p > band2p && band1p > band3p)
                        bitmap.SetPixel(x, y, Color.Red);
                    else if (band2p > band1p && band2p > band3p)
                        bitmap.SetPixel(x, y, Color.Green);
                    else if (band3p > band1p && band3p > band2p)
                        bitmap.SetPixel(x, y, Color.Blue);
                    else
                        bitmap.SetPixel(x, y, Color.Black);
                }

                Debug.WriteLine($"Classification prog: {y}/{_height}");
            }

            viewport2.UpdateImage(bitmap);
        }
    }
}
