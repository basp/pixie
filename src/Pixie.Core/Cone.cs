namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public class Cone : Shape
    {
        private const double Epsilon = 0.00001;

        public double Minimum { get; set; } = double.NegativeInfinity;

        public double Maximum { get; set; } = double.PositiveInfinity;

        public bool IsClosed { get; set; } = false;

        private bool CheckCap(Ray ray, double t, double r)
        {
            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;
            return (x * x + z * z) <= (r * r);
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
            if (this.CheckCap(ray, t, this.Minimum))
            {
                xs.Add(new Intersection(t, this));
            }

            t = (this.Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (this.CheckCap(ray, t, this.Maximum))
            {
                xs.Add(new Intersection(t, this));
            }
        }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            List<Intersection> xs;

            var o = ray.Origin;
            var d = ray.Direction;

            var a = (d.X * d.X) - (d.Y * d.Y) + (d.Z * d.Z);
            var b = (2 * o.X * d.X) - (2 * o.Y * d.Y) + (2 * o.Z * d.Z);
            var c = (o.X * o.X) - (o.Y * o.Y) + (o.Z * o.Z);

            if (a == 0)
            {
                if (b == 0)
                {
                    return IntersectionList.Empty();
                }

                var t = -c / (2 * b);
                xs = new List<Intersection>()
                {
                    new Intersection(t, this),
                };

                this.IntersectCaps(ray, xs);
                return IntersectionList.Create(xs.ToArray());
            }

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

            xs = new List<Intersection>();

            var y0 = o.Y + t0 * d.Y;
            if (this.Minimum < y0 && y0 < this.Maximum)
            {
                xs.Add(new Intersection(t0, this));
            }

            var y1 = o.Y + t1 * d.Y;
            if (this.Minimum < y1 && y1 < this.Maximum)
            {
                xs.Add(new Intersection(t1, this));
            }

            this.IntersectCaps(ray, xs);
            return IntersectionList.Create(xs.ToArray());
        }

        public override Vector4 LocalNormalAt(Vector4 point)
        {
            var dist = point.X * point.X + point.Z * point.Z;
            if (dist < 1 && point.Y >= this.Maximum - Epsilon)
            {
                return Vector4.CreateDirection(0, 1, 0);
            }

            if (dist < 1 && point.Y <= this.Minimum + Epsilon)
            {
                return Vector4.CreateDirection(0, -1, 0);
            }

            var y = Math.Sqrt(dist);
            if (point.Y > 0)
            {
                y = -y;
            }

            return Vector4.CreateDirection(point.X, y, point.Z);
        }

        public override BoundingBox Bounds()
        {
            var r = Math.Max(
                Math.Abs(this.Minimum),
                Math.Abs(this.Maximum));

            var min = Vector4.CreatePosition(-r, this.Minimum, -r);
            var max = Vector4.CreatePosition(r, this.Maximum, r);
            return new BoundingBox(min, max);
        }
    }
}