// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Linsi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Vector of 3 <c>double</c> values.
    /// </summary>
    public struct Vector3 : IEquatable<Vector3>
    {
        public readonly double X, Y, Z;

        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(double a) : this(a, a, a)
        {
        }

        public static Vector3 Zero => new Vector3(0, 0, 0);

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

        public static Vector3 operator *(Vector3 a, double s) =>
            new Vector3(
                a.X * s,
                a.Y * s,
                a.Z * s);

        public static Vector3 operator *(double s, Vector3 a) => a * s;

        public static Vector3 operator /(Vector3 a, double s) => a * (1 / s);

        public static Vector3 operator -(Vector3 a) =>
            new Vector3(-a.X, -a.Y, -a.Z);

        public static double MagnitudeSquared(Vector3 a) =>
            (a.X * a.X) +
            (a.Y * a.Y) +
            (a.Z * a.Z);

        public static double Magnitude(Vector3 a) =>
            Math.Sqrt(Vector3.MagnitudeSquared(a));

        public static Vector3 Normalize(Vector3 a) => a * a.Magnitude();

        public static double Dot(Vector3 a, Vector3 b) =>
            (a.X * b.X) +
            (a.Y * b.Y) +
            (a.Z * b.Z);

        public static Vector3 Cross(Vector3 a, Vector3 b) =>
            new Vector3(
                (a.Y * b.Z) - (a.Z * b.Y),
                (a.Z * b.X) - (a.X * b.Z),
                (a.X * b.Y) - (a.Y * b.X));

        public static Vector3 Reflect(Vector3 a, Vector3 n) =>
            a - (n * 2 * Dot(a, n));

        public static IEqualityComparer<Vector3> GetEqualityComparer(double epsilon = 0) =>
            new Vector3EqualityComparer(epsilon);

        public double Magnitude() => Vector3.Magnitude(this);

        public Vector3 Normalize() => Vector3.Normalize(this);

        public double Dot(Vector3 v) => Vector3.Dot(this, v);

        public Vector3 Cross(Vector3 v) => Vector3.Cross(this, v);

        public Vector3 Reflect(Vector3 n) => Vector3.Reflect(this, n);

        public override string ToString() =>
            $"({this.X}, {this.Y}, {this.Z})";

        public bool Equals(Vector3 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z;
    }
}
