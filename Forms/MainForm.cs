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
            int lineNumber = 1;

            while (!reader.EndOfStream)
            {
                lineNumber++;
                values = reader.ReadLine().Split('\t');

                for (int i = 0; i < fieldTypes.Count; i++)
                {
                    if (!float.TryParse(values[i], out float v))
                    {
                        MessageBox.Show($"Warning: Cannot parse value '{values[i]}' at line {lineNumber}, column {i + 1}. Skipping.", "Parse Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

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
