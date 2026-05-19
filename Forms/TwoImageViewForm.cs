using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace modified_structure_analysis.Forms
{
    public partial class TwoImageViewForm : Form
    {
        private Image _left;
        private Image _right;

        public TwoImageViewForm(Image left, Image right)
        {
            InitializeComponent();

            _left = left;
            _right = right;
        }

        private void TwoImageViewForm_Load(object sender, EventArgs e)
        {
            viewport1.UpdateImage(_left);
            viewport2.UpdateImage(_right);
        }
    }
}
