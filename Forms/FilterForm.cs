using modified_structure_analysis.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace modified_structure_analysis.Forms
{
    public partial class FilterForm : Form
    {
        private readonly string[] _classNames;
        private readonly Color[]? _palette;
        private bool _isUpdating;

        public int[] SelectedClassIndices { get; private set; } = [];

        public FilterForm(string[] classNames, int[] initiallySelected, Color[]? palette = null)
        {
            InitializeComponent();

            FormClosing += FilterForm_FormClosing;
            Text = Resources.Filter_Title;
            dataGridView1.ReadOnly = false;
            CheckBoxColumn.ReadOnly = false;
            dataGridView1.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dataGridView1.CurrentCell?.ColumnIndex == 0)
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            _classNames = classNames;
            _palette = palette;
            PopulateGrid(initiallySelected);
        }

        private void PopulateGrid(int[] initiallySelected)
        {
            dataGridView1.Rows.Clear();

            dataGridView1.Rows.Add(true, Resources.Filter_SelectAll, null);

            for (int i = 0; i < _classNames.Length; i++)
            {
                Image? colorImage = null;
                if (_palette != null && i < _palette.Length)
                {
                    var bmp = new Bitmap(40, 20);
                    using (var g = Graphics.FromImage(bmp))
                        g.Clear(_palette[i]);
                    colorImage = bmp;
                }
                dataGridView1.Rows.Add(initiallySelected.Contains(i), _classNames[i], colorImage);
            }
        }

        private void DataGridView1_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0 || _isUpdating)
                return;

            _isUpdating = true;

            if (e.RowIndex == 0)
            {
                bool checked_ = dataGridView1.Rows[0].Cells[0].Value is true;
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].Value = checked_;
            }
            else
            {
                bool allChecked = true;
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value is not true)
                    {
                        allChecked = false;
                        break;
                    }
                }
                dataGridView1.Rows[0].Cells[0].Value = allChecked;
            }

            _isUpdating = false;
        }

        private void FilterForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                var selected = new List<int>();
                for (int i = 0; i < _classNames.Length; i++)
                {
                    int rowIdx = i + 1;
                    if (rowIdx < dataGridView1.Rows.Count && dataGridView1.Rows[rowIdx].Cells[0].Value is true)
                        selected.Add(i);
                }
                SelectedClassIndices = selected.ToArray();
            }
        }
    }
}
