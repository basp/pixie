namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class PointLight : ILight
    {
        public PointLight(Double4 position, Color intensity)
        {
            this.Position = position;
            this.Intensity = intensity;
        }

        public Color Intensity { get; }

        public Double4 Position { get; }

        public int Samples => 1;

        public override bool Equals(object obj)
        {
            var other = obj as PointLight;
            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        public override int GetHashCode() =>
            this.Position.GetHashCode() * this.Intensity.GetHashCode();

        public bool Equals(PointLight other) =>
            this.Intensity.Equals(other.Intensity) &&
            this.Position.Equals(other.Position);

        public bool Equals(ILight obj)
        {
            var other = obj as PointLight;
            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        public double IntensityAt(Double4 point, World w)
        {
            var shadow = w.IsShadowed(this.Position, point);
            return shadow ? 0.0 : 1.0;
        }

        public Double4 PointOnLight(double u, double v) => this.Position;

        public IEnumerable<Double4> Sample() => new[] { this.Position };
    }
}