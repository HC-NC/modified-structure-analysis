using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace modified_structure_analysis.Forms
{
    public partial class DelimiterSelector : Form
    {
        public DelimiterType SelectedDelimiter { get; private set; } = DelimiterType.Tab;

        public DelimiterSelector()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbTab.Checked) SelectedDelimiter = DelimiterType.Tab;
            else if (rbComma.Checked) SelectedDelimiter = DelimiterType.Comma;
            else if (rbSemicolon.Checked) SelectedDelimiter = DelimiterType.Semicolon;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public enum DelimiterType
    {
        Tab,
        Comma,
        Semicolon
    }
}
