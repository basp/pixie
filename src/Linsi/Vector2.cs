namespace Linsi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Vector of 2 <c>double</c> values.
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>
    {
        public static Vector2 Zero => new Vector2(0, 0);
        public readonly double X;
        public readonly double Y;

        public Vector2(double v)
        {
            this.X = v;
            this.Y = v;
        }

        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) =>
            new Vector2(
                a.X + b.X,
                a.Y + b.Y);

        public static Vector2 operator -(Vector2 a, Vector2 b) =>
            new Vector2(
                a.X - b.X,
                a.Y - b.Y);

        public static Vector2 operator -(Vector2 a) =>
            new Vector2(-a.X, -a.Y);

        public static Vector2 operator *(Vector2 a, double s) =>
            new Vector2(
                a.X * s,
                a.Y * s);

        public static Vector2 operator *(double s, Vector2 a) => a * s;

        public static Vector2 operator /(Vector2 a, double s) =>
            new Vector2(
                a.X / s,
                a.Y / s);

        public static double MagnitudeSquared(Vector2 a) =>
            a.X * a.X + a.Y * a.Y;

        public static double Magnitude(Vector2 a) =>
            Math.Sqrt(Vector2.MagnitudeSquared(a));

        public static Vector2 Normalize(Vector2 a)
        {
            var mag = Vector2.Magnitude(a);
            var oneOverMag = 1 / mag;
            return new Vector2(
                a.X * oneOverMag,
                a.Y * oneOverMag);
        }

        public static double Dot(Vector2 a, Vector2 b) =>
            a.X * b.X + a.Y * b.Y;

        public static Vector2 Reflect(Vector2 a, Vector2 n) =>
            a - n * 2 * Dot(a, n);

        public double Magnitude() => Vector2.Magnitude(this);

        public Vector2 Normalize() => Vector2.Normalize(this);

        public double Dot(Vector2 v) => Vector2.Dot(this, v);

        public Vector2 Reflect(Vector2 n) => Vector2.Reflect(this, n);

        public static IEqualityComparer<Vector2> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxVector2EqualityComparer(epsilon);

        public override string ToString() =>
            $"({this.X}, {this.Y})";

        public bool Equals(Vector2 other) =>
            this.X == other.X &&
            this.Y == other.Y;
    }

    internal class ApproxVector2EqualityComparer : ApproxEqualityComparer<Vector2>
    {
        public ApproxVector2EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector2 a, Vector2 b) =>
            ApproxEqual(a.X, b.X) &&
            ApproxEqual(a.Y, b.Y);

        public override int GetHashCode(Vector2 obj) =>
            HashCode.Combine(obj.X, obj.Y);
    }
}
