using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace modified_structure_analysis.Forms
{
    public partial class Viewport : UserControl
    {
        private Graphics _graphics;
        private Image _img;
        private Point _mouseDown;
        private int _startx = 0; // offset of image when mouse was pressed
        private int _starty = 0;
        private int _imgx = 0; // current offset of image
        private int _imgy = 0;

        private int _oldWidth;
        private int _oldHeight;

        private bool _mousepressed = false; // true as long as left mousebutton is pressed
        private bool _mouseOnPicture = false;
        private float _zoom = 1;

        public Image Image => _img;

        public event Action<float> Zooming;
        public event Action<int, int> Moving;

        public Viewport()
        {
            InitializeComponent();

            _graphics = CreateGraphics();
        }

        private void Viewport_Load(object sender, EventArgs e)
        {
            _oldWidth = pictureBox.Width;
            _oldHeight = pictureBox.Height;
        }

        public void UpdateImage(Image img)
        {
            _img = img;
            ResetImage(this, EventArgs.Empty);
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

            Zooming?.Invoke(_zoom);
            Moving?.Invoke(_imgx, _imgy);

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

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (!_mousepressed)
                {
                    _mousepressed = true;
                    _mouseDown = e.Location;
                    _startx = _imgx;
                    _starty = _imgy;

                    Cursor.Current = Cursors.Hand;
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _mousepressed = false;

            Cursor.Current = Cursors.Default;
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            _mouseOnPicture = true;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            _mouseOnPicture = false;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Point mousePosNow = e.Location;

                // the distance the mouse has been moved since mouse was pressed
                int deltaX = mousePosNow.X - _mouseDown.X;
                int deltaY = mousePosNow.Y - _mouseDown.Y;

                // calculate new offset of image based on the current zoom factor
                _imgx = (int)(_startx + (deltaX / _zoom));
                _imgy = (int)(_starty + (deltaY / _zoom));

                Moving?.Invoke(_imgx, _imgy);

                pictureBox.Refresh();
            }
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

                Zooming?.Invoke(_zoom);
                Moving?.Invoke(_imgx, _imgy);

                pictureBox.Refresh();  // calls imageBox_Paint
            }

            base.OnMouseWheel(e);
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

                Moving?.Invoke(_imgx, _imgy);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Viewport_Resize(object sender, EventArgs e)
        {
            if (_img == null)
                return;

            float oldZoom = _zoom;

            float oldFullZoom = Math.Min(
             ((float)_oldHeight / (float)_img.Height) * (_img.VerticalResolution / _graphics.DpiY),
             ((float)_oldWidth / (float)_img.Width) * (_img.HorizontalResolution / _graphics.DpiX)
            );

            float fullZoom = Math.Min(
             ((float)pictureBox.Height / (float)_img.Height) * (_img.VerticalResolution / _graphics.DpiY),
             ((float)pictureBox.Width / (float)_img.Width) * (_img.HorizontalResolution / _graphics.DpiX)
            );

            _zoom += fullZoom - oldFullZoom;

            _imgx += (int)(0.5f * (pictureBox.Width / _zoom - _oldWidth / oldZoom));
            _imgy += (int)(0.5f * (pictureBox.Height / _zoom - _oldHeight / oldZoom));

            _oldWidth = pictureBox.Width;
            _oldHeight = pictureBox.Height;

            pictureBox.Refresh();
        }

        public void OnLinkZoom(float zoom)
        {
            if (_img == null)
                return;

            _zoom = zoom;

            pictureBox.Refresh();
        }

        public void OnLinkMove(int x, int y)
        {
            if (_img == null)
                return;

            _imgx = x;
            _imgy = y;

            pictureBox.Refresh();
        }
    }
}
