// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System.Collections.Generic;
    using Linie;

    public class AreaLight : ILight
    {
        public AreaLight(
            Vector4 corner,
            Vector4 fullUvec,
            int usteps,
            Vector4 fullVvec,
            int vsteps,
            Color intensity)
        {
            this.Corner = corner;
            this.Uvec = fullUvec / usteps;
            this.Usteps = usteps;
            this.Vvec = fullVvec / vsteps;
            this.Vsteps = vsteps;
            this.Intensity = intensity;
            this.Position = corner + 0.5 * (fullUvec + fullVvec);
        }

        public Vector4 Corner { get; set; }

        public Vector4 Uvec { get; set; }

        public int Usteps { get; set; }

        public Vector4 Vvec { get; set; }

        public int Vsteps { get; set; }

        public int Samples => Usteps * Vsteps;

        public Color Intensity { get; set; }

        public Vector4 Position { get; }

        public ISequence Jitter { get; set; } = new Sequence(0.5);

        public bool Equals(ILight other)
        {
            throw new System.NotImplementedException();
        }

        public double GetIntensity(Vector4 point, World w)
        {
            var total = 0.0;
            for (var v = 0; v < this.Vsteps; v++)
            {
                for (var u = 0; u < this.Usteps; u++)
                {
                    var lightPos = this.GetPoint(u, v);
                    if (!w.IsShadowed(lightPos, point))
                    {
                        total = total + 1.0;
                    }
                }
            }

            return total / this.Samples;
        }

        public IEnumerable<Vector4> Sample()
        {
            for (var v = 0; v < this.Vsteps; v++)
            {
                for (var u = 0; u < this.Usteps; u++)
                {
                    yield return this.GetPoint(u, v);
                }
            }
        }

        public Vector4 GetPoint(double u, double v) =>
            this.Corner +
                this.Uvec * (u + this.Jitter.Next()) +
                this.Vvec * (v + this.Jitter.Next());
    }
}
