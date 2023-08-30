namespace Pixie
{
    using System;
    using System.Collections.Generic;

    public struct Point3 : IEquatable<Point3>
    {
        public readonly double X, Y, Z;

        public Point3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point3(double a) : this(a, a, a)
        {
        }

        public static Point3 operator +(Point3 a, Vector3 u) =>
            new Point3(a.X + u.X, a.Y + u.Y, a.Z + u.Z);

        public static Point3 operator -(Point3 a, Vector3 u) =>
            new Point3(a.X - u.X, a.Y - u.Y, a.Z - u.Z);

        public static Vector3 operator -(Point3 a, Point3 b) =>
            new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Point3 operator *(double c, Point3 a) =>
            new Point3(c * a.X, c * a.Y, c * a.Z);

        public static Point3 operator *(Point3 a, double c) => c * a;

        public static IEqualityComparer<Point3> GetEqualityComparer(double epsilon = 0) =>
            new Point3EqualityComparer(epsilon);

        public override string ToString() => $"({this.X}, {this.Y}, {this.Z})";

        public bool Equals(Point3 other) =>
            this.X == other.X &&
            this.Y == other.Y &&
            this.Z == other.Z;
    }
}