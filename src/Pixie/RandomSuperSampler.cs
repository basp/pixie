// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Linsi;

    public class RandomSuperSampler : ISampler
    {
        private readonly Random rng = new Random();

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

        public Color Sample(int x, int y)
        {
            var color = Color.Black;
            var rays = this.Supersample(x, y).ToList();
            foreach (var ray in rays)
            {
                Interlocked.Increment(ref Stats.PrimaryRays);
                color += this.world.Trace(ray, 5);
            }

            color *= this.oneOverN;
            return color;
        }

        private IEnumerable<Ray> Supersample(int px, int py)
        {
            var inv = this.camera.TransformInv;
            var origin = inv * Vector4.CreatePosition(0, 0, 0);

            var pixelSize = this.camera.PixelSize;
            var halfWidth = this.camera.HalfWidth;
            var halfHeight = this.camera.HalfHeight;

            for (var i = 0; i < this.n; i++)
            {
                var xOffset = px + 0.5;
                var yOffset = py + 0.5;

                var rx = this.rng.NextDouble();
                var ry = this.rng.NextDouble();

                xOffset += 0.5 - rx;
                yOffset += 0.5 - ry;

                xOffset *= pixelSize;
                yOffset *= pixelSize;

                var worldX = halfWidth - xOffset;
                var worldY = halfHeight - yOffset;

                var pixel = inv * Vector4.CreatePosition(worldX, worldY, -1);
                var direction = (pixel - origin).Normalize();

                yield return new Ray(origin, direction);
            }
        }
    }
}
