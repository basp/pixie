namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class Camera
    {
        private readonly int hsize;
        private readonly int vsize;
        private readonly double fov;
        private readonly double aspect;
        private readonly double halfWidth;
        private readonly double halfHeight;
        private readonly double pixelSize;
        private Double4x4 transform;
        private Double4x4 transformInv;

        public Camera(int hsize, int vsize, double fov)
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
            this.transform = Double4x4.Identity;
        }

        public int HorizontalSize => this.hsize;

        public int VerticalSize => this.vsize;

        public double FieldOfView => this.fov;

        public double PixelSize => this.pixelSize;

        public double HalfWidth => this.halfWidth;

        public double HalfHeight => this.halfHeight;

        public Double4x4 Transform 
        {
            get => this.transform;
            set
            {
                this.transform = value;
                this.transformInv = value.Inverse();
            }
        }

        public Double4x4 TransformInv => this.transformInv;

        public IProgressMonitor ProgressMonitor { get; set; } =
            new ProgressMonitor();

        public Canvas Render(World w) =>
            Render(w, new DefaultSampler(w, this));

        public Canvas Render(World w, ISampler sampler)
        {
            Stats.Reset();
            this.ProgressMonitor.OnStarted();
            var img = new Canvas(this.hsize, this.vsize);
            Parallel.For(0, this.vsize, y =>
            {
                this.ProgressMonitor.OnRowStarted(y);
                for (var x = 0; x < this.hsize; x++)
                {
                    img[x, y] = sampler.Sample(x, y);
                }

                this.ProgressMonitor.OnRowFinished(y);
            });

            this.ProgressMonitor.OnFinished();
            return img;
        }
    }
}