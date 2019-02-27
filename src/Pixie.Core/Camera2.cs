namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class Camera2
    {
        private readonly int hsize;
        private readonly int vsize;
        private readonly double fov;
        private readonly double aspect;
        private readonly double halfWidth;
        private readonly double halfHeight;
        private readonly double pixelSize;
        static readonly Random rng = new Random();

        private static Double4 RandomInUnitDisk()
        {
            Double4 v;
            do
            {
                v = 2.0 * Double4.Vector(
                    rng.NextDouble(),
                    rng.NextDouble(),
                    0.0) - Double4.Vector(1, 1, 0);
            }
            while (v.Dot(v) >= 1.0); // force vector in disk
            return v;
        }

        public Camera2(int hsize, int vsize, double fov)
        {
            this.hsize = hsize;
            this.vsize = vsize;
            this.fov = fov;

            var halfView = Math.Tan(fov / 2);
            this.aspect = (double)hsize / vsize;
            if (aspect >= 1)
            {
                this.halfWidth = halfView;
                this.halfHeight = halfView / aspect;
            }
            else
            {
                this.halfWidth = halfView * aspect;
                this.halfHeight = halfView;
            }

            this.pixelSize = (this.halfWidth * 2) / this.hsize;
        }

        public int HorizontalSize => this.hsize;

        public int VerticalSize => this.vsize;

        public double FieldOfView => this.fov;

        public double PixelSize => this.pixelSize;

        public double HalfWidth => this.halfWidth;

        public double HalfHeight => this.halfHeight;

        public Double4x4 Transform { get; set; } = Double4x4.Identity;

        public IProgressMonitor ProgressMonitor { get; set; } =
            new ProgressMonitor();

        public Ray RayForPixel(int px, int py)
        {
            var pixelSize = this.PixelSize;

            var halfWidth = this.HalfWidth;
            var halfHeight = this.HalfHeight;

            var xOffset = (px + 0.5) * pixelSize;
            var yOffset = (py + 0.5) * pixelSize;

            var worldX = halfWidth - xOffset;
            var worldY = halfHeight - yOffset;

            var inv = this.Transform.Inverse();

            var pixel = inv * Double4.Point(worldX, worldY, -1);
            var origin = inv * Double4.Point(0, 0, 0);
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        public Canvas Render(World w)
        {
            Stats.Reset();
            this.ProgressMonitor.OnStarted();
            var img = new Canvas(this.hsize, this.vsize);
            
            const double focalDistance = 3.605;
            const double aperture = 0.04;
            
            for (var y = 0; y < this.vsize; y++)
            {
                this.ProgressMonitor.OnRowStarted(y);
                for (var x = 0; x < this.hsize; x++)
                {
                    var primaryRay = this.RayForPixel(x, y);
                    var focalPoint = primaryRay.Position(focalDistance);
                    var col = Color.Black;
                    const int n = 100;
                    for (var i = 0; i < n; i++)
                    {
                        // Get a vector with a random displacement 
                        // on a disk with radius 1.0 and use it to 
                        // offset the origin.
                        var offset = RandomInUnitDisk() * aperture;
                        var origin = primaryRay.Origin + offset;

                        // Create a new ray offset from the original ray
                        // and pointing at the focal point.
                        var secondaryRay = new Ray(
                            origin,
                            (focalPoint - origin).Normalize());
                        
                        col += w.ColorAt(secondaryRay);
                    }


                    img[x, y] = (1.0 / n) * col;
                }

                this.ProgressMonitor.OnRowFinished(y);
            };

            // Parallel.For(0, this.vsize, y =>
            // {
            //     this.ProgressMonitor.OnRowStarted(y);
            //     for (var x = 0; x < this.hsize; x++)
            //     {
            //         var ray = this.RayForPixel(x, y);
            //         img[x, y] = w.ColorAt(ray);
            //     }

            //     this.ProgressMonitor.OnRowFinished(y);
            // });



            this.ProgressMonitor.OnFinished();
            return img;
        }
    }
}