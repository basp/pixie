namespace Pixie
{
    using System;
    using System.Collections.Generic;

    public struct Normal3 : IEquatable<Normal3>
    {
        public readonly double X, Y, Z;

        public Normal3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Normal3 operator -(Normal3 n) =>
            new Normal3(-n.X, -n.Y, -n.Z);

        public static Normal3 operator +(Normal3 n, Normal3 m) =>
            new Normal3(n.X + m.X, n.Y + m.Y, n.Z + m.Z);

        public static Normal3 operator *(double a, Normal3 n) =>
            new Normal3(a * n.X, a * n.Y, a * n.Z);

        public static Normal3 operator *(Normal3 n, double a) => a * n;

        public static Vector3 operator +(Normal3 n, Vector3 u) =>
            new Vector3(n.X + u.X, n.Y + u.Y, n.Z + u.Z);

        public static Vector3 operator +(Vector3 u, Normal3 n) => n + u;

        public static double Dot(Normal3 n, Vector3 u) =>
            (n.X * u.X) +
            (n.Y * u.Y) +
            (n.Z * u.Z);

        public static double Dot(Vector3 u, Normal3 n) => Dot(n, u);

        public static implicit operator Vector3(Normal3 n) =>
            new Vector3(n.X, n.Y, n.Z);

        public static double MagnitudeSquared(Normal3 n) =>
            (n.X * n.X) +
            (n.Y * n.Y) +
            (n.Z * n.Z);

        public static double Magnitude(Normal3 n) =>
            Math.Sqrt(MagnitudeSquared(n));

        public static Normal3 Normalize(Normal3 n) => n * (1 / n.Magnitude());

        public static IEqualityComparer<Normal3> GetEqualityComparer(double epsilon = 0.0) =>
            new Normal3EqualityComparer(epsilon);

        public double Magnitude() => Magnitude(this);

        public Normal3 Normalize() => Normalize(this);

        public override string ToString() =>
            $"({this.X}, {this.Y}, {this.Z})";

        public bool Equals(Normal3 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z;
    }
}