namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class Cylinder : Shape
    {
        private const double Epsilon = 0.0001;

        public double Minimum { get; set; } = double.NegativeInfinity;

        public double Maximum { get; set; } = double.PositiveInfinity;

        public bool IsClosed { get; set; } = false;

        private bool CheckCap(Ray ray, double t)
        {
            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;
            return (x * x + z * z) <= 1;
        }

        private void IntersectCaps(Ray ray, List<Intersection> xs)
        {
            if (!this.IsClosed)
            {
                return;
            }

            if (Math.Abs(ray.Direction.Y) < Epsilon)
            {
                return;
            }

            double t;

            t = (this.Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (this.CheckCap(ray, t))
            {
                xs.Add(new Intersection(t, this));
            }

            t = (this.Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (this.CheckCap(ray, t))
            {
                xs.Add(new Intersection(t, this));
            }
        }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            var a = Math.Pow(ray.Direction.X, 2) + Math.Pow(ray.Direction.Z, 2);

            // ray is parallel to the y-axis
            if (Math.Abs(a) < Epsilon)
            {
                var caps = new List<Intersection>();
                this.IntersectCaps(ray, caps);
                return IntersectionList.Create(caps.ToArray());
            }

            var b =
                2 * ray.Origin.X * ray.Direction.X +
                2 * ray.Origin.Z * ray.Direction.Z;

            var c = Math.Pow(ray.Origin.X, 2) + Math.Pow(ray.Origin.Z, 2) - 1;

            var disc = b * b - 4 * a * c;

            if (disc < 0)
            {
                return IntersectionList.Empty();
            }

            var t0 = (-b - Math.Sqrt(disc)) / (2 * a);
            var t1 = (-b + Math.Sqrt(disc)) / (2 * a);

            if (t0 > t1)
            {
                var tmp = t0;
                t0 = t1;
                t1 = tmp;
            }

            var xs = new List<Intersection>();

            var y0 = ray.Origin.Y + t0 * ray.Direction.Y;
            if (this.Minimum < y0 && y0 < this.Maximum)
            {
                xs.Add(new Intersection(t0, this));
            }

            var y1 = ray.Origin.Y + t1 * ray.Direction.Y;
            if (this.Minimum < y1 && y1 < this.Maximum)
            {
                xs.Add(new Intersection(t1, this));
            }

            this.IntersectCaps(ray, xs);

            return IntersectionList.Create(xs.ToArray());
        }

        public override Double4 LocalNormalAt(Double4 point)
        {
            var dist = point.X * point.X + point.Z * point.Z;
            if (dist < 1 && point.Y >= this.Maximum - Epsilon)
            {
                return Double4.Vector(0, 1, 0);
            }

            if (dist < 1 && point.Y <= this.Minimum + Epsilon)
            {
                return Double4.Vector(0, -1, 0);
            }

            return Double4.Vector(point.X, 0, point.Z);
        }

        public override Bounds3 Bounds()
        {
            var min = Double4.Point(-1, this.Minimum, -1);
            var max = Double4.Point(1, this.Maximum, 1);
            return new Bounds3(min, max);
        }
    }
}