// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Vector of 2 <c>double</c> values.
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>
    {
        public readonly double X, Y;

        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(double a) : this(a, a)
        {
        }

        public static Vector2 Zero => new Vector2(0, 0);

        public static Vector2 operator +(Vector2 u, Vector2 v) =>
            new Vector2(u.X + v.X, u.Y + v.Y);

        public static Vector2 operator -(Vector2 u, Vector2 v) =>
            new Vector2(u.X - v.X, u.Y - v.Y);

        public static Vector2 operator *(double a, Vector2 u) => u * a;

        public static Vector2 operator *(Vector2 u, double a) =>
            new Vector2(u.X * a, u.Y * a);

        public static Vector2 operator /(Vector2 u, double a) => u * (1 / a);

        public static Vector2 operator -(Vector2 a) => new Vector2(-a.X, -a.Y);

        public static explicit operator Point2(Vector2 u) =>
            new Point2(u.X, u.Y);

        public static double MagnitudeSquared(Vector2 a) =>
            (a.X * a.X) +
            (a.Y * a.Y);

        public static double Magnitude(Vector2 a) =>
            Math.Sqrt(Vector2.MagnitudeSquared(a));

        public static Vector2 Normalize(Vector2 a) => a / a.Magnitude();

        public static double Dot(Vector2 a, Vector2 b) =>
            (a.X * b.X) +
            (a.Y * b.Y);

        public static Vector2 Reflect(Vector2 a, Vector2 n) =>
            a - (n * 2 * Dot(a, n));

        public static IEqualityComparer<Vector2> GetEqualityComparer(
            double epsilon = 0.0) =>
            new Vector2EqualityComparer(epsilon);

        public double Magnitude() => Vector2.Magnitude(this);

        public Vector2 Normalize() => Vector2.Normalize(this);

        public double Dot(Vector2 v) => Vector2.Dot(this, v);

        public Vector2 Reflect(Vector2 n) => Vector2.Reflect(this, n);

        public override string ToString() => $"({this.X}, {this.Y})";

        public bool Equals(Vector2 other) =>
            this.X == other.X &&
            this.Y == other.Y;
    }
}
