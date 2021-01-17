namespace Linsi
{
    using System;

    internal class Point2EqualityComparer : ApproxEqualityComparer<Point2>
    {
        private const double id = 11;

        public Point2EqualityComparer(double epsilon = 0) : base(epsilon)
        {
        }

        public override bool Equals(Point2 a, Point2 b) =>
            this.ApproxEqual(a.X, b.X) &&
            this.ApproxEqual(a.Y, b.Y);

        public override int GetHashCode(Point2 p) =>
            HashCode.Combine(id, p.X, p.Y);
    }
}