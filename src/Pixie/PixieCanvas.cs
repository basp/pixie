namespace Pixie
{
    using System;
    using System.Drawing;

    public class PixieCanvas : IDisposable
    {
        readonly Graphics gfx;

        readonly Bitmap bmp;

        readonly int size;

        readonly int sizeOverTwo;

        bool disposed = false;

        public PixieCanvas(int size)
        {
            this.bmp = new Bitmap(size, size);
            this.gfx = Graphics.FromImage(this.bmp);
            this.size = this.bmp.Width;
            this.sizeOverTwo = this.size / 2;
        }

        public Bitmap Bitmap => this.bmp;

        public void DrawString(Color color, float x, float y, string s)
        {
            x = ScaleTranslateX(x);
            y = ScaleTranslateY(y);

            var font = new Font(new FontFamily("Hack"), 12, FontStyle.Italic);
            var brush = new SolidBrush(color.ToSystemDrawingColor());
            this.gfx.DrawString(s, font, brush, x, y);
        }

        public void FillPie(Color color, float x, float y, float r, float startAngle, float sweepAngle)
        {
            var brush = new SolidBrush(color.ToSystemDrawingColor());
            this.FillPie(brush, x, y, r, startAngle, sweepAngle);
        }

        public void FillPie(Brush brush, float x, float y, float r, float startAngle, float sweepAngle)
        {
            x = this.ScaleTranslateX(x);
            y = this.ScaleTranslateY(y);
            r = this.Scale(r);

            var x1 = x - r;
            var y1 = y - r;
            var w = 2 * r;
            var h = 2 * r;

            gfx.FillPie(brush, x1, y1, w, h, startAngle, sweepAngle);
        }

        public void DrawArc(Color color, float x, float y, float r, float startAngle, float sweepAngle)
        {
            var pen = new Pen(color);
            this.DrawArc(pen, x, y, r, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, float x, float y, float r, float startAngle, float sweepAngle)
        {
            x = this.ScaleTranslateX(x);
            y = this.ScaleTranslateY(y);
            r = this.Scale(r);

            var x1 = x - r;
            var y1 = y - r;
            var w = 2 * r;
            var h = 2 * r;

            gfx.DrawArc(pen.ToSystemDrawingPen(), x1, y1, w, h, startAngle, sweepAngle);
        }

        public void DrawLine(Color color, float x1, float y1, float x2, float y2)
        {
            var pen = new Pen(color);
            this.DrawLine(pen, x1, y1, x2, y2);
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            x1 = this.ScaleTranslateX(x1);
            y1 = this.ScaleTranslateY(y1);
            x2 = this.ScaleTranslateX(x2);
            y2 = this.ScaleTranslateY(y2);

            gfx.DrawLine(pen.ToSystemDrawingPen(), x1, y1, x2, y2);
        }

        public void FillCircle(Color color, float x, float y, float r)
        {
            var brush = new SolidBrush(color.ToSystemDrawingColor());
            this.FillCircle(brush, x, y, r);
        }

        public void FillCircle(Brush brush, float x, float y, float r)
        {
            x = this.ScaleTranslateX(x);
            y = this.ScaleTranslateY(y);
            r = this.Scale(r);

            var x1 = x - r;
            var y1 = y - r;
            var w = 2 * r;
            var h = 2 * r;

            var rect = new RectangleF(x1, y1, w, h);
            gfx.FillEllipse(brush, rect);
        }

        public void DrawCircle(Color color, float x, float y, float r)
        {
            var pen = new Pen(color);
            this.DrawCircle(pen, x, y, r);
        }

        public void DrawCircle(Pen pen, float x, float y, float r)
        {
            x = this.ScaleTranslateX(x);
            y = this.ScaleTranslateY(y);
            r = this.Scale(r);

            var x1 = x - r;
            var y1 = y - r;

            var w = 2 * r;
            var h = 2 * r;

            var rect = new RectangleF(x1, y1, w, h);
            gfx.DrawEllipse(pen.ToSystemDrawingPen(), rect);
        }

        public void Clear(Color color)
        {
            var brush = new SolidBrush(color.ToSystemDrawingColor());
            var rect = new Rectangle(0, 0, this.bmp.Width, this.bmp.Height);
            this.gfx.FillRectangle(brush, rect);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public float Scale(float v) => v * this.sizeOverTwo;

        public float ScaleTranslateX(float x) => x * this.sizeOverTwo + this.sizeOverTwo;

        public float ScaleTranslateY(float y) => -y * this.sizeOverTwo + this.sizeOverTwo;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.gfx.Dispose();
                this.bmp.Dispose();
            }

            this.disposed = true;
        }
    }
}