namespace Pixie.Core
{
    using System;

    public class Camera
    {
        private readonly int hsize;
        private readonly int vsize;
        private readonly float fov;
        private readonly float aspect;
        private readonly float halfWidth;
        private readonly float halfHeight;
        private readonly float pixelSize;

        public Camera(int hsize, int vsize, float fov)
        {
            this.hsize = hsize;
            this.vsize = vsize;
            this.fov = fov;

            var halfView = (float)Math.Tan(fov / 2);
            this.aspect = (float)hsize / vsize;
            if (hsize >= vsize)
            {
                this.halfWidth = halfView;
                this.halfHeight = halfView / aspect;
            }
            else
            {
                this.halfWidth = halfView * aspect;
                this.halfHeight = halfView;
            }

            this.pixelSize = this.halfWidth * 2 / this.hsize;
        }

        public int HorizontalSize => this.hsize;

        public int VerticalSize => this.vsize;

        public float FieldOfView => this.fov;

        public float PixelSize => this.pixelSize;

        public Float4x4 Transform { get; set; } = Float4x4.Identity;

        public IProgressMonitor ProgressMonitor { get; set; } =
            new ProgressMonitor();

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (px + 0.5f) * this.pixelSize;
            var yOffset = (py + 0.5f) * this.pixelSize;

            var worldX = this.halfWidth - xOffset;
            var worldY = this.halfHeight - yOffset;

            var inv = this.Transform.Inverse();

            var pixel = inv * Float4.Point(worldX, worldY, -1);
            var origin = inv * Float4.Point(0, 0, 0);
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(World w)
        {
            this.ProgressMonitor.OnStarted();
            var img = new Canvas(this.hsize, this.vsize);
            for (var y = 0; y < this.vsize - 1; y++)
            {
                this.ProgressMonitor.OnRowStarted(y);
                for (var x = 0; x < this.hsize - 1; x++)
                {
                    this.ProgressMonitor.OnPixelStarted(y, x);
                    var ray = this.RayForPixel(x, y);
                    var color = w.ColorAt(ray);
                    img[x, y] = color;
                    this.ProgressMonitor.OnPixelFinished(y, x);
                }

                this.ProgressMonitor.OnRowFinished(y);
            }

            return img;
        }
    }
}