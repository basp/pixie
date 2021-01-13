namespace Linsi
{
    using System;

    public class Vector4Operations : IVectorOperations<Vector4>
    {
        public Vector4 Add(Vector4 u, Vector4 v) => u + v;

        public double Dot(Vector4 u, Vector4 v) => Vector4.Dot(u, v);

        public double Magnitude(Vector4 u) =>
            Math.Sqrt(this.MagnitudeSquared(u));

        public double MagnitudeSquared(Vector4 u) =>
            u.X * u.X +
            u.Y * u.Y +
            u.Z * u.Z +
            u.W * u.W;

        public Vector4 Normalize(Vector4 u) => u / this.Magnitude(u);

        public Vector4 Scale(Vector4 u, double a) => a * u;

        public Vector4 Subtract(Vector4 u, Vector4 v) => u - v;
    }
}