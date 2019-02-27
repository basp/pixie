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

            var inv = this.camera.TransformInv;

            var pixel = inv * Double4.Point(worldX, worldY, -1);
            var origin = inv * Double4.Point(0, 0, 0);
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
            // https://steveharveynz.wordpress.com/2012/12/21/ray-tracer-part-5-depth-of-field/
            var color = Color.Black;
            var primary = RayForPixel(x, y);
            //Console.WriteLine($"Origin: {primary.Origin}");
            //Console.WriteLine($"Direction: {primary.Direction}");
            var fp = primary.Position(this.focalDepth);
            for(var i = 0; i < n; i++)
            {
                var rx = 0.5 - rng.NextDouble();
                var ry = 0.5 - rng.NextDouble();
                var offsetv = Double4.Vector(rx, ry, 0) * this.na;
                var origin = primary.Origin + offsetv;
                var direction = (fp - origin).Normalize();
                var secondary = new Ray(origin, direction);
                color += this.world.ColorAt(secondary);
            }

            color *= this.oneOverN;
            return color;
        }
    }
}