using modified_structure_analysis.Properties;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing.Drawing2D;

namespace modified_structure_analysis
{
    public partial class MainForm : Form
    {
        private List<Band> _bands;

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

        private Graphics _graphics;
        private Image _img;
        private Point _mouseDown;
        private int _startx = 0; // offset of image when mouse was pressed
        private int _starty = 0;
        private int _imgx = 0; // current offset of image
        private int _imgy = 0;

        private bool _mousepressed = false; // true as long as left mousebutton is pressed
        private bool _mouseOnPicture = false;
        private float _zoom = 1;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text file|*.txt";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //mainStatusLabel.Visible = false;
            //mainProgressBar.Visible = false;

            _graphics = toolStripContainer1.ContentPanel.CreateGraphics();
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
            }

            bandListBox.SelectedIndex = 0;
        }

        private void UpdateImage(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(_width, _height);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int i = y * _width + x;

                    byte r = (byte)(_redBand.Normalize(_redBand.Values[i]) * 255);
                    byte g = (byte)(_greenBand.Normalize(_greenBand.Values[i]) * 255);
                    byte b = (byte)(_blueBand.Normalize(_blueBand.Values[i]) * 255);

                    Color color = Color.FromArgb(r, g, b);

                    bitmap.SetPixel(x, y, color);
                }
            }

            _img = bitmap;
            ResetImage(sender, e);
        }

        private void CalculateBandsStatistics()
        {
            foreach (Band band in _bands)
                band.CalculateStatistics();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _bands = new List<Band>();

                _xMin = int.MaxValue;
                _yMin = int.MaxValue;
                _xMax = int.MinValue;
                _yMax = int.MinValue;

                switch (Path.GetExtension(openFileDialog1.FileName))
                {
                    case ".txt":
                        ReadTextFile();
                        break;
                }

                UpdateBandsList();
                UpdateImage(sender, e);
                CalculateBandsStatistics();
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

            int columnsCount = (int)MathF.Sqrt(band.Count);
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

        private void histogramPlotView_DoubleClick(object sender, EventArgs e)
        {
            histogramPlotView.Model.ResetAllAxes();
            histogramPlotView.Refresh();
        }

        private void dataTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == histogramTabPage)
                BuildHistogram();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_mouseOnPicture)
            {
                float oldzoom = _zoom;

                if (e.Delta > 0)
                {
                    _zoom *= 1.1F;
                }
                else if (e.Delta < 0)
                {
                    _zoom *= 0.9F;
                }

                MouseEventArgs mouse = e as MouseEventArgs;
                Point mousePosNow = mouse.Location;

                Point pBoxLocation = PointToClient(pictureBox.Parent.PointToScreen(pictureBox.Location));

                // Where location of the mouse in the pictureframe
                int x = mousePosNow.X - pBoxLocation.X;
                int y = mousePosNow.Y - pBoxLocation.Y;

                // Where in the IMAGE is it now
                int oldimagex = (int)(x / oldzoom);
                int oldimagey = (int)(y / oldzoom);

                // Where in the IMAGE will it be when the new zoom i made
                int newimagex = (int)(x / _zoom);
                int newimagey = (int)(y / _zoom);

                // Where to move image to keep focus on one point
                _imgx = newimagex - oldimagex + _imgx;
                _imgy = newimagey - oldimagey + _imgy;

                pictureBox.Refresh();  // calls imageBox_Paint
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (pictureBox.Focused && ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)))
            {
                switch (keyData)
                {
                    case Keys.Right:
                        _imgx -= (int)(pictureBox.Width * 0.1F / _zoom);
                        pictureBox.Refresh();
                        break;

                    case Keys.Left:
                        _imgx += (int)(pictureBox.Width * 0.1F / _zoom);
                        pictureBox.Refresh();
                        break;

                    case Keys.Down:
                        _imgy -= (int)(pictureBox.Height * 0.1F / _zoom);
                        pictureBox.Refresh();
                        break;

                    case Keys.Up:
                        _imgy += (int)(pictureBox.Height * 0.1F / _zoom);
                        pictureBox.Refresh();
                        break;

                    case Keys.PageDown:
                        _imgy -= (int)(pictureBox.Height * 0.90F / _zoom);
                        pictureBox.Refresh();
                        break;

                    case Keys.PageUp:
                        _imgy += (int)(pictureBox.Height * 0.90F / _zoom);
                        pictureBox.Refresh();
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ResetImage(object sender, EventArgs e)
        {
            if (_img == null)
                return;

            // Fit whole image
            _zoom = Math.Min(
             ((float)pictureBox.Height / (float)_img.Height) * (_img.VerticalResolution / _graphics.DpiY),
             ((float)pictureBox.Width / (float)_img.Width) * (_img.HorizontalResolution / _graphics.DpiX)
            );

            // Fit width
            //_zoom = ((float)pictureBox.Width / (float)_img.Width) *
            //(_img.HorizontalResolution / _graphics.DpiX);

            _imgx = (int)(pictureBox.Width * 0.5f / _zoom - _img.Width * 0.5f);
            _imgy = (int)(pictureBox.Height * 0.5f / _zoom - _img.Height * 0.5f);

            pictureBox.Refresh();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_img == null)
                return;

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.ScaleTransform(_zoom, _zoom);
            e.Graphics.DrawImage(_img, _imgx, _imgy);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            pictureBox.Focus();
        }

        private void pictureBox_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Left)
            {
                if (!_mousepressed)
                {
                    _mousepressed = true;
                    _mouseDown = mouse.Location;
                    _startx = _imgx;
                    _starty = _imgy;
                }
            }
        }

        private void pictureBox_MouseUp(object sender, EventArgs e)
        {
            _mousepressed = false;
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            _mouseOnPicture = true;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            _mouseOnPicture = false;
        }

        private void pictureBox_MouseMove(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Left)
            {
                Point mousePosNow = mouse.Location;

                // the distance the mouse has been moved since mouse was pressed
                int deltaX = mousePosNow.X - _mouseDown.X;
                int deltaY = mousePosNow.Y - _mouseDown.Y;

                // calculate new offset of image based on the current zoom factor
                _imgx = (int)(_startx + (deltaX / _zoom));
                _imgy = (int)(_starty + (deltaY / _zoom));

                pictureBox.Refresh();
            }
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
    }
}
