namespace Linsi
{
    using System;

    internal class Point3EqualityComparer : ApproxEqualityComparer<Point3>
    {
        private const double id = 7;

        public Point3EqualityComparer(double epsilon = 0)
            : base(epsilon)
        {
        }

        public override bool Equals(Point3 a, Point3 b) =>
            this.ApproxEqual(a.X, b.X) &&
            this.ApproxEqual(a.Y, b.Y) &&
            this.ApproxEqual(a.Z, b.Z);

        public override int GetHashCode(Point3 p) =>
            HashCode.Combine(id, p.X, p.Y, p.Z);
    }
}