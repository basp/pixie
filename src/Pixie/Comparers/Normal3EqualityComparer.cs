namespace Pixie
{
    using System;

    internal class Normal3EqualityComparer : ApproxEqualityComparer<Normal3>
    {
        private const double id = 13;

        public Normal3EqualityComparer(double epsilon = 0) : base(epsilon)
        {
        }

        public override bool Equals(Normal3 n, Normal3 m) =>
            this.ApproxEqual(n.X, m.X) &&
            this.ApproxEqual(n.Y, m.Y) &&
            this.ApproxEqual(n.Z, m.Z);

        public override int GetHashCode(Normal3 obj) =>
            HashCode.Combine(id, obj.X, obj.Y, obj.Z);
    }
}