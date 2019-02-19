namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Camera
    {
        const bool supersample = true;

        private static readonly Random rng = new Random();
        private readonly int hsize;
        private readonly int vsize;
        private readonly double fov;
        private readonly double aspect;
        private readonly double halfWidth;
        private readonly double halfHeight;
        private readonly double pixelSize;

        public Camera(int hsize, int vsize, double fov)
        {
            this.hsize = hsize;
            this.vsize = vsize;
            this.fov = fov;

            var halfView = (double)Math.Tan(fov / 2);
            this.aspect = (double)hsize / vsize;
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

        public double FieldOfView => this.fov;

        public double PixelSize => this.pixelSize;

        public Double4x4 Transform { get; set; } = Double4x4.Identity;

        public IProgressMonitor ProgressMonitor { get; set; } =
            new ProgressMonitor();

        public IEnumerable<Ray> Supersample(int px, int py, int n = 8)
        {
            var inv = this.Transform.Inverse();
            var origin = inv * Double4.Point(0, 0, 0);

            for (var i = 0; i < n; i++)
            {
                var xOffset = (px + 0.5);
                var yOffset = (py + 0.5);

                // This causes RenderingWorldWithCamera test to 
                // fail due to the random offsets

                var rx = rng.NextDouble();
                var ry = rng.NextDouble();

                xOffset += (0.5 - rx);
                yOffset += (0.5 - ry);

                xOffset *= this.pixelSize;
                yOffset *= this.pixelSize;

                var worldX = this.halfWidth - xOffset;
                var worldY = this.halfHeight - yOffset;

                var pixel = inv * Double4.Point(worldX, worldY, -1);
                var direction = (pixel - origin).Normalize();

                yield return new Ray(origin, direction);
            }
        }

        public Ray RayForPixel(int px, int py)
        {
            var xOffset = (px + 0.5) * this.pixelSize;
            var yOffset = (py + 0.5) * this.pixelSize;

            var worldX = this.halfWidth - xOffset;
            var worldY = this.halfHeight - yOffset;

            var inv = this.Transform.Inverse();

            var pixel = inv * Double4.Point(worldX, worldY, -1);
            var origin = inv * Double4.Point(0, 0, 0);
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(World w)
        {
            Stats.Reset();
            var img = new Canvas(this.hsize, this.vsize);
            Parallel.For(0, this.vsize, y =>
            {
                this.ProgressMonitor.OnRowStarted(y);
                for (var x = 0; x < this.hsize - 1; x++)
                {
                    var color = Color.Black;
                    if (supersample)
                    {
                        var rays = this.Supersample(x, y).ToList();
                        foreach (var ray in rays)
                        {
                            color += w.ColorAt(ray, 5);
                        }

                        color *= (1.0 / rays.Count);
                    }
                    else
                    {
                        var ray = this.RayForPixel(x, y);
                        color = w.ColorAt(ray);
                    }

                    img[x, y] = color;
                }

                this.ProgressMonitor.OnRowFinished(y);
            });

            return img;
        }
    }
}