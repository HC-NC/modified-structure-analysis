using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using modified_structure_analysis.Config;

namespace modified_structure_analysis.Forms
{
    public partial class Viewport : UserControl
    {
        private Graphics _graphics;
        private Image _img;
        private Point _mouseDown;
        private float _startx = 0;
        private float _starty = 0;
        private float _imgx = 0;
        private float _imgy = 0;

        private int _oldWidth;
        private int _oldHeight;

        private bool _mousepressed = false;
        private bool _mouseOnPicture = false;
        private float _zoom = 1;

        private PointF? _externalCursorPos = null;

        public Image Image => _img;

        public float Zoom => _zoom;
        public float OffsetX => _imgx;
        public float OffsetY => _imgy;

        public event Action<float> Zooming;
        public event Action<float, float> Moving;
        public event Action<float, float> CursorMoved;

        public Viewport()
        {
            InitializeComponent();

            _graphics = CreateGraphics();
        }

        public void ZoomIn()
        {
            if (_img == null) return;
            float oldZoom = _zoom;
            float newZoom = _zoom * 1.1f;
            float cx = pictureBox.Width * 0.5f;
            float cy = pictureBox.Height * 0.5f;
            _imgx += cx * (1f / newZoom - 1f / oldZoom);
            _imgy += cy * (1f / newZoom - 1f / oldZoom);
            _zoom = newZoom;
            Zooming?.Invoke(_zoom);
            Moving?.Invoke(_imgx, _imgy);
            pictureBox.Refresh();
        }

        public void ZoomOut()
        {
            if (_img == null) return;
            float oldZoom = _zoom;
            float newZoom = _zoom * 0.9f;
            float cx = pictureBox.Width * 0.5f;
            float cy = pictureBox.Height * 0.5f;
            _imgx += cx * (1f / newZoom - 1f / oldZoom);
            _imgy += cy * (1f / newZoom - 1f / oldZoom);
            _zoom = newZoom;
            Zooming?.Invoke(_zoom);
            Moving?.Invoke(_imgx, _imgy);
            pictureBox.Refresh();
        }

        public void ZoomToExtent()
        {
            if (_img == null) return;
            ResetImage(this, EventArgs.Empty);
        }

        public void SetZoom(float zoom)
        {
            if (_img == null) return;
            _zoom = Math.Max(0.01f, zoom);
            Zooming?.Invoke(_zoom);
            pictureBox.Refresh();
        }

        public void PanBy(float dx, float dy)
        {
            _imgx += dx / _zoom;
            _imgy += dy / _zoom;
            Moving?.Invoke(_imgx, _imgy);
            pictureBox.Refresh();
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

            _imgx = pictureBox.Width * 0.5f / _zoom - _img.Width * 0.5f;
            _imgy = pictureBox.Height * 0.5f / _zoom - _img.Height * 0.5f;

            Zooming?.Invoke(_zoom);
            Moving?.Invoke(_imgx, _imgy);

            pictureBox.Refresh();
        }

        private static InterpolationMode GetInterpolationMode(ViewportInterpolation mode)
        {
            return mode switch
            {
                ViewportInterpolation.NearestNeighbor => InterpolationMode.NearestNeighbor,
                ViewportInterpolation.Bilinear => InterpolationMode.Bilinear,
                ViewportInterpolation.Bicubic => InterpolationMode.Bicubic,
                ViewportInterpolation.HighQualityBilinear => InterpolationMode.HighQualityBilinear,
                ViewportInterpolation.HighQualityBicubic => InterpolationMode.HighQualityBicubic,
                _ => InterpolationMode.NearestNeighbor
            };
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_img == null)
                return;

            e.Graphics.InterpolationMode = GetInterpolationMode(AppSettings.Instance.Interpolation);
            e.Graphics.ScaleTransform(_zoom, _zoom);
            e.Graphics.DrawImage(_img, _imgx, _imgy);

            if (_externalCursorPos.HasValue)
            {
                float drawX = _externalCursorPos.Value.X + _imgx;
                float drawY = _externalCursorPos.Value.Y + _imgy;

                float crosshairRadius = 2.5f;

                var oldSmoothing = e.Graphics.SmoothingMode;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen blackPen = new Pen(Color.Black, 1.5f / _zoom))
                {
                    float dotRadius = crosshairRadius / _zoom;
                    e.Graphics.DrawEllipse(blackPen, drawX - dotRadius, drawY - dotRadius, dotRadius * 2, dotRadius * 2);
                    e.Graphics.FillEllipse(Brushes.White, drawX - dotRadius, drawY - dotRadius, dotRadius * 2, dotRadius * 2);
                }

                e.Graphics.SmoothingMode = oldSmoothing;
            }
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

            CursorMoved?.Invoke(-1, -1);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Point mousePosNow = e.Location;

                // the distance the mouse has been moved since mouse was pressed
                int deltaX = mousePosNow.X - _mouseDown.X;
                int deltaY = mousePosNow.Y - _mouseDown.Y;

                _imgx = _startx + deltaX / _zoom;
                _imgy = _starty + deltaY / _zoom;

                Moving?.Invoke(_imgx, _imgy);

                pictureBox.Refresh();
            }

            if (_img != null)
            {
                float imgCursorX = e.X / _zoom - _imgx;
                float imgCursorY = e.Y / _zoom - _imgy;

                if (imgCursorX >= 0 && imgCursorX <= _img.Width && imgCursorY >= 0 && imgCursorY <= _img.Height)
                    CursorMoved?.Invoke(imgCursorX, imgCursorY);
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

                int x = mousePosNow.X - pBoxLocation.X;
                int y = mousePosNow.Y - pBoxLocation.Y;

                float oldimagex = x / oldzoom;
                float oldimagey = y / oldzoom;

                float newimagex = x / _zoom;
                float newimagey = y / _zoom;

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

                    case Keys.Add:
                    case Keys.Oemplus when (keyData & Keys.Modifiers) is Keys.None or Keys.Control:
                        ZoomIn();
                        break;

                    case Keys.Subtract:
                    case Keys.OemMinus when (keyData & Keys.Modifiers) is Keys.None or Keys.Control:
                        ZoomOut();
                        break;
                }

                Moving?.Invoke(_imgx, _imgy);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Viewport_Resize(object sender, EventArgs e)
        {
            if (_img == null || _oldWidth <= 0 || _oldHeight <= 0)
            {
                _oldWidth = pictureBox.Width;
                _oldHeight = pictureBox.Height;
                return;
            }

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

        public void UpdateExternalCursor(float imageX, float imageY)
        {
            _externalCursorPos = new PointF(imageX, imageY);
            pictureBox.Refresh();
        }

        public void ClearExternalCursor()
        {
            if (_externalCursorPos != null)
            {
                _externalCursorPos = null;
                pictureBox.Refresh();
            }
        }

        public void OnLinkZoom(float zoom)
        {
            if (_img == null) return;
            _zoom = zoom;
            pictureBox.Refresh();
        }

        public void OnLinkMove(float x, float y)
        {
            if (_img == null) return;
            _imgx = x;
            _imgy = y;
            pictureBox.Refresh();
        }
    }
}
