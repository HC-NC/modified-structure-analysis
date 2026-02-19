using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace modified_structure_analysis
{
    public partial class TextTableColumnSelector : Form
    {
        private List<string> _hd;

        public TextTableColumnSelector(List<string> hd)
        {
            InitializeComponent();

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "NameColumn";
            nameColumn.HeaderText = "Name";

            dataGridView1.Columns.Add(nameColumn);

            DataGridViewComboBoxColumn typeColumn = new DataGridViewComboBoxColumn();
            typeColumn.Name = "TypeColumn";
            typeColumn.HeaderText = "Type";
            typeColumn.Items.AddRange(Enum.GetNames(typeof(FieldType)));

            dataGridView1.Columns.Add(typeColumn);

            _hd = hd;
        }

        public void FormLoad(object sender, EventArgs e)
        {
            int tmp = (int)FieldType.X;

            foreach (string s in _hd)
            {
                dataGridView1.Rows.Add(s, ((FieldType)tmp).ToString());

                if (tmp < (int)FieldType.Band)
                    tmp++;
            }
        }

        public List<FieldType> GetFieldTypes()
        {
            List<FieldType> fields = new List<FieldType>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                fields.Add(Enum.Parse<FieldType>(row.Cells[1].Value.ToString()));
            }

            return fields;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public enum FieldType
        {
            None = 0,
            X = 1,
            Y = 2,
            Band = 3
        }
    }
}
