namespace Pixie.Core
{
    using System;
    using System.Threading;

    public class FocalBlurSampler : ISampler
    {
        private static Random rng = new Random();
        private readonly World world;
        private readonly Camera camera;
        private readonly double na;
        private readonly double focalDepth;
        private readonly int n;
        private readonly double oneOverN;

        public FocalBlurSampler(
            World world, 
            Camera camera, 
            double na = 0.03, 
            double focalDepth = 1,
            int n = 5)
        {
            this.world = world;
            this.camera = camera;
            this.na = na;         
            this.focalDepth = focalDepth;   
            this.n = n;
            this.oneOverN = 1.0 / n;
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

            var inv = this.camera.Transform.Inverse();

            var pixel = inv * Double4.Point(worldX, worldY, -this.focalDepth);
            var origin = inv * RandomPointOnAperture();
            var direction = (pixel - origin).Normalize();

            return new Ray(origin, direction);
        }

        private Double4 RandomPointOnAperture()
        {
            var x = this.na - (rng.NextDouble() * 2 * this.na);
            var y = this.na - (rng.NextDouble() * 2 * this.na);
            return Double4.Point(x, y, 0);
        }

        public Color Sample(int x, int y)
        {
            var color = Color.Black;
            for(var i = 0; i < n; i++)
            {
                Interlocked.Increment(ref Stats.PrimaryRays);
                var ray = this.RayForPixel(x, y);
                color += this.world.ColorAt(ray);
            }

            color *= this.oneOverN;
            return color;
        }
    }
}