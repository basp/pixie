namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class RandomSuperSampler : ISampler
    {
        private static readonly Random rng = new Random();
        private readonly World world;
        private readonly Camera camera;
        private readonly int n;
        private readonly double oneOverN;

        public RandomSuperSampler(World world, Camera camera, int n = 4)
        {
            this.world = world;
            this.camera = camera;
            this.n = n;
            this.oneOverN = 1.0 / n;
        }

        public IEnumerable<Ray> Supersample(int px, int py)
        {
            var inv = this.camera.Transform.Inverse();
            var origin = inv * Double4.Point(0, 0, 0);

            var pixelSize = this.camera.PixelSize;
            var halfWidth = this.camera.HalfWidth;
            var halfHeight = this.camera.HalfHeight;

            for (var i = 0; i < this.n; i++)
            {
                var xOffset = (px + 0.5);
                var yOffset = (py + 0.5);

                var rx = rng.NextDouble();
                var ry = rng.NextDouble();

                xOffset += (0.5 - rx);
                yOffset += (0.5 - ry);

                xOffset *= pixelSize;
                yOffset *= pixelSize;

                var worldX = halfWidth - xOffset;
                var worldY = halfHeight - yOffset;

                var pixel = inv * Double4.Point(worldX, worldY, -1);
                var direction = (pixel - origin).Normalize();

                yield return new Ray(origin, direction);
            }
        }

        public Color Sample(int x, int y)
        {
            var color = Color.Black;
            var rays = this.Supersample(x, y).ToList();
            foreach (var ray in rays)
            {
                Interlocked.Increment(ref Camera.Stats.PrimaryRays);
                color += this.world.ColorAt(ray, 5);
            }

            // color *= (1.0 / this.n);
            color *= this.oneOverN;
            return color;
        }
    }
}