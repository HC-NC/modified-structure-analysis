namespace modified_structure_analysis
{
    public partial class MainForm : Form
    {
        private List<Band> _bands;

        private int _resolution = 30;
        private int _xMin = int.MaxValue;
        private int _yMin = int.MaxValue;
        private int _xMax = int.MinValue;
        private int _yMax = int.MinValue;

        private int _width;
        private int _height;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text file|*.txt";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateBandsList()
        {
            foreach (Band band in _bands)
                bandListBox.Items.Add(band);

            bandListBox.SelectedIndex = 0;
        }

        private void openToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _bands = new List<Band>();

                switch (Path.GetExtension(openFileDialog1.FileName))
                {
                    case ".txt":
                        ReadTextFile();
                        break;
                }

                UpdateBandsList();
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
        }
    }
}
