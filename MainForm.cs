namespace modified_structure_analysis
{
    public partial class MainForm : Form
    {
        private List<Band> _bands;

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text file|*.txt";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(openFileDialog1.FileName))
                {
                    case ".txt":
                        ReadTextFile();
                        break;
                }
            }
        }

        private void ReadTextFile()
        {
            StreamReader reader = new StreamReader(openFileDialog1.FileName);

            List<string> hd = new List<string>();

            foreach (string v in reader.ReadLine().Split('\t'))
            {
                dataGridView1.Columns.Add(v, v);
                hd.Add(v);
            }

            TextTableColumnSelector columnSelector = new TextTableColumnSelector(hd);

            if (columnSelector.ShowDialog() == DialogResult.Cancel)
                return;

            List<TextTableColumnSelector.FieldType> fieldTypes = columnSelector.GetFieldTypes();

            while (!reader.EndOfStream)
            {
                dataGridView1.Rows.Add(reader.ReadLine().Split('\t'));
            }

            reader.Close();
        }
    }
}
