namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A vector of 4 floating point values.
    /// </summary>
    public struct Double4 : IEquatable<Double4>
    {
        public static Double4 Zero =>
            new Double4(0, 0, 0, 0);

        public double X;
        public double Y;
        public double Z;
        public double W;

        public Double4(double v)
        {
            this.X = v;
            this.Y = v;
            this.Z = v;
            this.W = v;
        }

        public Double4(double x, double y, double z, double w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public bool IsPoint => this.W == 1.0;

        public bool IsVector => this.W == 0.0;

        /// <summary>
        /// Constructs a point vector with W component set to 1.
        /// </summary>
        public static Double4 Point(double x, double y, double z) =>
            new Double4(x, y, z, 1);

        /// <summary>
        /// Constructs a direction vector with W component set to 0.
        /// </summary>
        /// <remarks>
        /// Note that these direction vectors are unaffected by translations.
        /// </remarks>
        public static Double4 Vector(double x, double y, double z) =>
            new Double4(x, y, z, 0);

        public static Double4 operator +(Double4 a, Double4 b) =>
            new Double4(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z,
                a.W + b.W);

        public static Double4 operator -(Double4 a, Double4 b) =>
            new Double4(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z,
                a.W - b.W);

        public static Double4 operator -(Double4 a) =>
            new Double4(-a.X, -a.Y, -a.Z, -a.W);

        public static Double4 operator *(Double4 a, double s) =>
            new Double4(
                a.X * s,
                a.Y * s,
                a.Z * s,
                a.W * s);

        public static Double4 operator *(double s, Double4 a) => a * s;

        public static Double4 operator /(Double4 a, double s) =>
            new Double4(
                a.X / s,
                a.Y / s,
                a.Z / s,
                a.W / s);

        public static double MagnitudeSquared(Double4 a) =>
            a.X * a.X + a.Y * a.Y + a.Z * a.Z + a.W * a.W;

        public static double Magnitude(Double4 a) =>
            (double)Math.Sqrt(Double4.MagnitudeSquared(a));

        public static Double4 Normalize(Double4 a)
        {
            var mag = Double4.Magnitude(a);
            return new Double4(
                a.X / mag,
                a.Y / mag,
                a.Z / mag,
                a.W / mag);
        }

        public static double Dot(Double4 a, Double4 b) =>
            a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

        public static Double4 Cross(Double4 a, Double4 b) =>
            Double4.Vector(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X);

        public static Double4 Reflect(Double4 a, Double4 n) =>
            a - n * 2 * Dot(a, n);

        public double Magnitude() => Double4.Magnitude(this);

        public Double4 Normalize() => Double4.Normalize(this);

        public double Dot(Double4 v) => Double4.Dot(this, v);

        public Double4 Cross(Double4 v) => Double4.Cross(this, v);

        public Double4 Reflect(Double4 n) => Double4.Reflect(this, n);

        public static IEqualityComparer<Double4> GetEqualityComparer(double epsilon = 0.0) =>
            new ApproxDouble4EqualityComparer(epsilon);

        public override string ToString() =>
            $"({this.X}, {this.Y}, {this.Z}, {this.W})";

        public bool Equals(Double4 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z &&
            this.W == other.W;
    }

    internal class ApproxDouble4EqualityComparer : ApproxEqualityComparer<Double4>
    {
        public ApproxDouble4EqualityComparer(double epsilon = 0.0)
            : base(epsilon)
        {
        }

        public override bool Equals(Double4 a, Double4 b) =>
            ApproxEqual(a.X, b.X) &&
            ApproxEqual(a.Y, b.Y) &&
            ApproxEqual(a.Z, b.Z) &&
            ApproxEqual(a.W, b.W);

        public override int GetHashCode(Double4 obj) =>
            HashCode.Combine(obj.X, obj.Y, obj.Z, obj.W);
    }
}
