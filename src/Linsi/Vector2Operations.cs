namespace Linsi
{
    using System;

    public class Vector2Operations : IVectorOperations<Vector2>
    {
        public Vector2 Add(Vector2 u, Vector2 v) => u + v;

        public double Dot(Vector2 u, Vector2 v) => Vector2.Dot(u, v);

        public double Magnitude(Vector2 u) =>
            Math.Sqrt(this.MagnitudeSquared(u));

        public double MagnitudeSquared(Vector2 u) => u.X * u.X + u.Y * u.Y;

        public Vector2 Normalize(Vector2 u) => u / this.Magnitude(u);

        public Vector2 Scale(Vector2 u, double a) => a * u;

        public Vector2 Subtract(Vector2 u, Vector2 v) => u - v;
    }
}
