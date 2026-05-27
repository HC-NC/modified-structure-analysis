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

            viewport1.Moving += viewport2.OnLinkMove;
            viewport1.Zooming += viewport2.OnLinkZoom;
            viewport1.CursorMoved += OnCursorMovedViewport1;

            viewport2.Moving += viewport1.OnLinkMove;
            viewport2.Zooming += viewport1.OnLinkZoom;
            viewport2.CursorMoved += OnCursorMovedViewport2;
        }

        private void TwoImageViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewport1.Moving -= viewport2.OnLinkMove;
            viewport1.Zooming -= viewport2.OnLinkZoom;
            viewport1.CursorMoved -= OnCursorMovedViewport1;

            viewport2.Moving -= viewport1.OnLinkMove;
            viewport2.Zooming -= viewport1.OnLinkZoom;
            viewport2.CursorMoved += OnCursorMovedViewport2;
        }

        private void OnCursorMovedViewport1(int x, int y)
        {
            OnCursorMoved(x, y, viewport2);
        }

        private void OnCursorMovedViewport2(int x, int y)
        {
            OnCursorMoved(x, y, viewport1);
        }

        private void OnCursorMoved(int x, int y, Viewport viewport)
        {
            if (x == -1 && y == -1) viewport.ClearExternalCursor();
            else viewport.UpdateExternalCursor(x, y);
        }
    }
}
