namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Vector of 4 <c>double</c> values.
    /// </summary>
    public struct Vector4 : IEquatable<Vector4>
    {
        public static Vector4 Zero =>
            new Vector4(0, 0, 0, 0);

        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly double W;

        public Vector4(double v)
        {
            this.X = v;
            this.Y = v;
            this.Z = v;
            this.W = v;
        }

        public Vector4(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public bool IsPosition => this.W == 1.0;

        public bool IsDirection => this.W == 0.0;

        /// <summary>
        /// Constructs a point vector.
        /// </summary>
        /// <param name="x">Value for the <c>X</c> component.</param>
        /// <param name="y">Value for the <c>Y</c> component.</param>
        /// <param name="z">Value for the <c>Z</c> component.</param>
        public static Vector4 CreatePosition(double x, double y, double z) =>
            new Vector4(x, y, z, 1);

        /// <summary>
        /// Constructs a direction vector.
        /// </summary>
        /// <remarks>
        /// Note that direction vectors are unaffected by translations.
        /// </remarks>
        /// <param name="x">Value for the <c>X</c> component.</param>
        /// <param name="y">Value for the <c>Y</c> component.</param>
        /// <param name="z">Value for the <c>Z</c> component.</param>
        public static Vector4 CreateDirection(double x, double y, double z) =>
            new Vector4(x, y, z, 0);

        public static Vector4 operator +(Vector4 a, Vector4 b) =>
            new Vector4(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z,
                a.W + b.W);

        public static Vector4 operator -(Vector4 a, Vector4 b) =>
            new Vector4(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z,
                a.W - b.W);

        public static Vector4 operator -(Vector4 a) =>
            new Vector4(-a.X, -a.Y, -a.Z, -a.W);

        public static Vector4 operator *(Vector4 a, double s) =>
            new Vector4(
                a.X * s,
                a.Y * s,
                a.Z * s,
                a.W * s);

        public static Vector4 operator *(double s, Vector4 a) => a * s;

        public static Vector4 operator /(Vector4 a, double s) =>
            new Vector4(
                a.X / s,
                a.Y / s,
                a.Z / s,
                a.W / s);

        public static double MagnitudeSquared(Vector4 a) =>
            a.X * a.X + a.Y * a.Y + a.Z * a.Z + a.W * a.W;

        public static double Magnitude(Vector4 a) =>
            (double)Math.Sqrt(Vector4.MagnitudeSquared(a));

        public static Vector4 Normalize(Vector4 a)
        {
            var mag = Vector4.Magnitude(a);
            return new Vector4(
                a.X / mag,
                a.Y / mag,
                a.Z / mag,
                a.W / mag);
        }

        public static double Dot(Vector4 a, Vector4 b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

        public static Vector4 Cross(Vector4 a, Vector4 b) =>
            Vector4.CreateDirection(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);

        public static Vector4 Reflect(Vector4 a, Vector4 n) =>
            a - n * 2 * Dot(a, n);

        public double Magnitude() => Vector4.Magnitude(this);

        public Vector4 Normalize() => Vector4.Normalize(this);

        public double Dot(Vector4 v) => Vector4.Dot(this, v);

        public Vector4 Cross(Vector4 v) => Vector4.Cross(this, v);

        public Vector4 Reflect(Vector4 n) => Vector4.Reflect(this, n);

        public static IEqualityComparer<Vector4> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxVector4EqualityComparer(epsilon);

        public override string ToString() =>
            $"({this.X}, {this.Y}, {this.Z}, {this.W})";

        public bool Equals(Vector4 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z &&
            this.W == other.W;
    }

    internal class ApproxVector4EqualityComparer : ApproxEqualityComparer<Vector4>
    {
        public ApproxVector4EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Vector4 a, Vector4 b) =>
            ApproxEqual(a.X, b.X) &&
            ApproxEqual(a.Y, b.Y) &&
            ApproxEqual(a.Z, b.Z) &&
            ApproxEqual(a.W, b.W);

        public override int GetHashCode(Vector4 obj) =>
            HashCode.Combine(obj.X, obj.Y, obj.Z, obj.W);
    }
}
