namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class PointLight : ILight, ILightSource
    {
        public PointLight(Double4 position, Color intensity)
        {
            this.Position = position;
            this.Intensity = intensity;
        }

        public Color Intensity { get; }

        public Double4 Position { get; }

        public override bool Equals(object obj)
        {
            var other = obj as PointLight;
            if(other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        public override int GetHashCode() =>
            this.Position.GetHashCode() * this.Intensity.GetHashCode();

        public bool Equals(PointLight other) =>
            this.Equals((ILight)other);

        public bool Equals(ILight other) =>
            this.Position.Equals(other.Position) &&
            this.Intensity.Equals(other.Intensity);

        public IEnumerable<ILight> GetLights() =>
            new[] { this };
    }
}