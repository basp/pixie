namespace Linsi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Vector of 3 <c>double</c> values.
    /// </summary>
    public struct Vector3
        : IEquatable<Vector3>
    {
        public static Vector3 Zero => new Vector3(0, 0, 0);
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Vector3(double v)
        {
            this.X = v;
            this.Y = v;
            this.Z = v;
        }

        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new Vector3(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);

        public static Vector3 operator -(Vector3 a, Vector3 b) =>
            new Vector3(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z);

        public static Vector3 operator -(Vector3 a) =>
            new Vector3(-a.X, -a.Y, -a.Z);

        public static Vector3 operator *(Vector3 a, double s) =>
            new Vector3(
                a.X * s,
                a.Y * s,
                a.Z * s);

        public static Vector3 operator *(double s, Vector3 a) => a * s;

        public static Vector3 operator /(Vector3 a, double s) =>
            new Vector3(
                a.X / s,
                a.Y / s,
                a.Z / s);

        public static double MagnitudeSquared(Vector3 a) =>
            a.X * a.X + a.Y * a.Y + a.Z * a.Z;

        public static double Magnitude(Vector3 a) =>
            (double)Math.Sqrt(Vector3.MagnitudeSquared(a));

        public static Vector3 Normalize(Vector3 a)
        {
            var mag = Vector3.Magnitude(a);
            return new Vector3(
                a.X / mag,
                a.Y / mag,
                a.Z / mag);
        }

        public static double Dot(Vector3 a, Vector3 b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static Vector3 Cross(Vector3 a, Vector3 b) =>
            new Vector3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);

        public static Vector3 Reflect(Vector3 a, Vector3 n) =>
            a - n * 2 * Dot(a, n);

        public double Magnitude() => Vector3.Magnitude(this);

        public Vector3 Normalize() => Vector3.Normalize(this);

        public double Dot(Vector3 v) => Vector3.Dot(this, v);

        public Vector3 Cross(Vector3 v) => Vector3.Cross(this, v);

        public Vector3 Reflect(Vector3 n) => Vector3.Reflect(this, n);

        public static IEqualityComparer<Vector3> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxVector3EqualityComparer(epsilon);

        public override string ToString() =>
            $"({this.X}, {this.Y}, {this.Z})";

        public bool Equals(Vector3 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z;
    }

    internal class ApproxVector3EqualityComparer 
        : ApproxEqualityComparer<Vector3>
    {
        public ApproxVector3EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector3 a, Vector3 b) =>
            ApproxEqual(a.X, b.X) &&
            ApproxEqual(a.Y, b.Y) &&
            ApproxEqual(a.Z, b.Z);

        public override int GetHashCode(Vector3 obj) =>
            HashCode.Combine(obj.X, obj.Y, obj.Z);
    }
}
