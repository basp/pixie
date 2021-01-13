namespace Linsi
{
    using System;

    public class Vector3Operations : IVectorOperations<Vector3>
    {
        public Vector3 Add(Vector3 u, Vector3 v) => u + v;

        public Vector3 Cross(Vector3 u, Vector3 v) => Vector3.Cross(u, v);

        public double Dot(Vector3 u, Vector3 v) => Vector3.Dot(u, v);

        public double Magnitude(Vector3 u) =>
            Math.Sqrt(this.MagnitudeSquared(u));

        public double MagnitudeSquared(Vector3 u) =>
            u.X * u.X + u.Y * u.Y + u.Z + u.Z;

        public Vector3 Normalize(Vector3 u) => u / this.Magnitude(u);

        public Vector3 Scale(Vector3 u, double a) => a * u;

        public Vector3 Subtract(Vector3 u, Vector3 v) => u - v;
    }
}