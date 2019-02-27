namespace Pixie.Core
{
    using System;
    using System.Threading;

    public class FocalBlurSampler : ISampler
    {
        private readonly Random rng = new Random();
        private readonly World world;
        private readonly Camera camera;
        private readonly double focalDistance;
        private readonly double aperture;
        private readonly int n;

        public FocalBlurSampler(
            World world,
            Camera camera,
            // You probably want to set focal distance to norm(at-from).
            double focalDistance = 1.0,
            // Aperture size works best at smaller values such as 0.1
            double aperture = 1.0,
            int n = 8)
        {
            this.world = world;
            this.camera = camera;
            this.focalDistance = focalDistance;
            this.aperture = aperture;
            this.n = n;
        }

        public Ray RayForPixel(int px, int py)
        {
            var pixelSize = this.camera.PixelSize;

            var halfWidth = this.camera.HalfWidth;
            var halfHeight = this.camera.HalfHeight;

            var xOffset = (px + 0.5) * pixelSize;
            var yOffset = (py + 0.5) * pixelSize;

            var worldX = halfWidth - xOffset;
            var worldY = halfHeight - yOffset;

            var inv = this.camera.TransformInv;

            var pixel = inv * Double4.Point(worldX, worldY, -1);
            var origin = inv * Double4.Point(0, 0, 0);
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        // It was definitely not a good idea to have this static
        // and multiple threads trying to mess around with the
        // random number generator. So now every thread gets its 
        // own sampler and every sampler gets its own rng.
        private Double4 RandomInUnitDisk()
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

        public Color Sample(int x, int y)
        {
            var primaryRay = this.RayForPixel(x, y);
            var focalPoint = primaryRay.Position(focalDistance);
            var col = Color.Black;
            for (var i = 0; i < this.n; i++)
            {
                // Get a vector with a random displacement 
                // on a disk with radius 1.0 and use it to 
                // offset the origin.
                var offset = RandomInUnitDisk() * aperture;

                // Create a new ray offset from the original ray
                // and pointing at the focal point.
                var origin = primaryRay.Origin + offset;
                var direction = (focalPoint - origin).Normalize();
                var secondaryRay = new Ray(origin, direction);

                // We probably should count these "secondary" rays
                // as primary rays for stats purposes; this is consistent
                // with RandomSuperSampler behavior.
                Interlocked.Increment(ref Stats.PrimaryRays);
                col += this.world.ColorAt(secondaryRay);
            }

            return (1.0 / this.n) * col;
        }
    }
}