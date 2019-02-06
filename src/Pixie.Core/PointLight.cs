namespace Pixie.Core
{
    using System;

    public class PointLight : IEquatable<PointLight>
    {
        public readonly Double4 Position;
        public readonly Color Intensity;

        public PointLight(Double4 position, Color intensity)
        {
            this.Position = position;
            this.Intensity = intensity;
        }

        public bool Equals(PointLight other) =>
            this.Position.Equals(other.Position) &&
            this.Intensity.Equals(other.Intensity);
    }
}