namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using Linsi;

    public class PointLight : ILight
    {
        public PointLight(Vector4 position, Color intensity)
        {
            this.Position = position;
            this.Intensity = intensity;
        }

        public Color Intensity { get; }

        public Vector4 Position { get; }

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

        public double GetIntensity(Vector4 point, World w)
        {
            var shadow = w.IsShadowed(this.Position, point);
            return shadow ? 0.0 : 1.0;
        }

        public Vector4 GetPoint(double u, double v) => 
            this.Position;

        public IEnumerable<Vector4> Sample() => 
            new[] { this.Position };
    }
}