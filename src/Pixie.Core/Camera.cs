namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Instances of the <c>Camera</c> class are used to take pictures
    /// of the world.
    /// </summary>
    /// <remarks>
    /// Since there are many ways in how to capture a color, any advanced 
    /// camera will rely on an sampler to do the actual colorization.
    /// </remarks>
    public class Camera
    {
        private readonly int hsize;
        private readonly int vsize;
        private readonly double fov;
        private readonly double aspect;
        private readonly double halfWidth;
        private readonly double halfHeight;
        private readonly double pixelSize;
        private Matrix4x4 transform;
        private Matrix4x4 transformInv;

        /// <summary>
        /// Constructs a new <c>Camera</c> instance.
        /// </summary>
        /// <param name="hsize">The horizontal size of the canvas.</param>
        /// <param name="vsize">The vertical size of the canvas.</param>
        /// <param name="fov">The size of the field of view.</param>
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
            this.transform = Matrix4x4.Identity;
            this.transformInv = this.transform.Inverse();
        }

        public int HorizontalSize => this.hsize;

        public int VerticalSize => this.vsize;

        public double FieldOfView => this.fov;

        public double PixelSize => this.pixelSize;

        public double HalfWidth => this.halfWidth;

        public double HalfHeight => this.halfHeight;

        public Matrix4x4 Transform
        {
            get => this.transform;
            set
            {
                this.transform = value;
                this.transformInv = value.Inverse();
            }
        }

        public Matrix4x4 TransformInv => this.transformInv;

        public IProgressMonitor ProgressMonitor { get; set; } =
            new ProgressMonitor();

        /// <summary>
        /// Renders a world using the default shader. This shader
        /// will cast a single ray right through the center of each
        /// pixel.
        /// </summary>
        /// <remarks>
        /// The default shader is very fast but it causes really hard
        /// shadows and anti-aliasing artifacts. One way to compensate
        /// is to render at a larger-than-required resolution and use an
        /// external image manipulation program to scale it down to the
        /// desired resolution. An arguably better but more expensive way 
        /// is to use either the random supersampler or focal-blur sampler
        /// implementations.
        /// </remarks>
        /// <param name="w">The <c>World</c> instance to be rendered.</param>
        public Canvas Render(World w) =>
            Render(w, () => new DefaultSampler(w, this));

        /// <summary>
        /// Renders a world to a canvas. 
        /// </summary>
        /// <remarks>
        /// Since we have multiple lines being rendered in parallel it's 
        /// very importatnt to make sure all the samplers can operate 
        /// independently. This takes extra care when implementing a new
        /// sampler.
        /// </remarks>
        /// <param name="w">The <c>World</c> instance to be rendered.</param>
        /// <param name="samplerFactory">
        /// A factory function to provide sampler instances.
        /// </param>
        public Canvas Render(World w, Func<ISampler> samplerFactory)
        {
            Stats.Reset();
            this.ProgressMonitor.OnStarted();
            var img = new Canvas(this.hsize, this.vsize);
            Parallel.For(0, this.vsize, y =>
            {
                var sampler = samplerFactory();
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