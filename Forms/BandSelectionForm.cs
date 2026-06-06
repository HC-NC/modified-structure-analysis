using System.Data;

using modified_structure_analysis.Models;

namespace modified_structure_analysis.Forms
{
    public partial class BandSelectionForm : Form
    {
        private List<Band> _bands;

        public List<Band> Result { get; private set; } = new List<Band>();

        public BandSelectionForm(List<Band> bands)
        {
            InitializeComponent();
            
            _bands = bands;
        }

        private void BandSelectionForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(_bands.ToArray());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Result.AddRange(listBox1.SelectedItems.Cast<Band>());

            Close();
        }
    }
}
