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

        public static Vector3 operator +(Vector3 u, Vector3 v) =>
            new Vector3(
                u.X + v.X,
                u.Y + v.Y,
                u.Z + v.Z);

        public static Vector3 operator -(Vector3 u, Vector3 v) =>
            new Vector3(
                u.X - v.X,
                u.Y - v.Y,
                u.Z - v.Z);

        public static Vector3 operator *(Vector3 u, double a) =>
            new Vector3(
                u.X * a,
                u.Y * a,
                u.Z * a);

        public static Vector3 operator *(double a, Vector3 u) => u * a;

        public static Vector3 operator /(Vector3 u, double a) => u * (1 / a);

        public static Vector3 operator -(Vector3 u) =>
            new Vector3(-u.X, -u.Y, -u.Z);

        public static double MagnitudeSquared(Vector3 u) =>
            (u.X * u.X) +
            (u.Y * u.Y) +
            (u.Z * u.Z);

        public static double Magnitude(Vector3 u) =>
            Math.Sqrt(Vector3.MagnitudeSquared(u));

        public static Vector3 Normalize(Vector3 u) => u * u.Magnitude();

        public static double Dot(Vector3 u, Vector3 v) =>
            (u.X * v.X) +
            (u.Y * v.Y) +
            (u.Z * v.Z);

        public static Vector3 Cross(Vector3 u, Vector3 v) =>
            new Vector3(
                (u.Y * v.Z) - (u.Z * v.Y),
                (u.Z * v.X) - (u.X * v.Z),
                (u.X * v.Y) - (u.Y * v.X));

        public static Vector3 Reflect(Vector3 u, Vector3 v) =>
            u - (v * 2 * Dot(u, v));

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
