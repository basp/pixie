namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct BoundingBox
    {
        public static BoundingBox Infinity =>
            new BoundingBox(
                Double4.Point(
                    double.NegativeInfinity,
                    double.NegativeInfinity,
                    double.NegativeInfinity),
                Double4.Point(
                    double.PositiveInfinity,
                    double.PositiveInfinity,
                    double.PositiveInfinity));

        public static BoundingBox Empty =>
            new BoundingBox(
                Double4.Point(
                    double.PositiveInfinity,
                    double.PositiveInfinity,
                    double.PositiveInfinity),
                Double4.Point(
                    double.NegativeInfinity,
                    double.NegativeInfinity,
                    double.NegativeInfinity));


        public readonly Double4 Min;
        public readonly Double4 Max;

        public BoundingBox(Double4 min, Double4 max)
        {
            this.Min = min;
            this.Max = max;
        }

        public static BoundingBox operator +(BoundingBox box, Double4 p) =>
            BoundingBox.Add(box, p);

        public static BoundingBox operator +(BoundingBox a, BoundingBox b) =>
            BoundingBox.Add(a, b);

        public static BoundingBox operator *(BoundingBox b, Double4x4 m) =>
            BoundingBox.Multiply(b, m);

        public BoundingBox Add(Double4 point) =>
            BoundingBox.Add(this, point);

        public BoundingBox Add(BoundingBox b) =>
            BoundingBox.Add(this, b);

        public static BoundingBox Add(BoundingBox box, Double4 point)
        {
            var min = Double4.Point(
                Math.Min(box.Min.X, point.X),
                Math.Min(box.Min.Y, point.Y),
                Math.Min(box.Min.Z, point.Z));

            var max = Double4.Point(
                Math.Max(box.Max.X, point.X),
                Math.Max(box.Max.Y, point.Y),
                Math.Max(box.Max.Z, point.Z));

            return new BoundingBox(min, max);
        }

        public static BoundingBox Add(BoundingBox a, BoundingBox b)
        {
            a += b.Min;
            a += b.Max;
            return a;
        }

        public static BoundingBox Multiply(BoundingBox b, Double4x4 m)
        {
            var p1 = b.Min;
            var p2 = Double4.Point(b.Min.X, b.Min.Y, b.Max.Z);
            var p3 = Double4.Point(b.Min.X, b.Max.Y, b.Min.Z);
            var p4 = Double4.Point(b.Min.X, b.Max.Y, b.Max.Z);
            var p5 = Double4.Point(b.Max.X, b.Min.Y, b.Min.Z);
            var p6 = Double4.Point(b.Max.X, b.Min.Y, b.Max.Z);
            var p7 = Double4.Point(b.Max.X, b.Max.Y, b.Min.Z);
            var p8 = b.Max;

            var box2 = BoundingBox.Empty;

            foreach (var p in new[] { p1, p2, p3, p4, p5, p6, p7, p8 })
            {
                box2 += (m * p);
            }

            return box2;
        }

        public IEnumerable<Double4> Corners()
        {
            return new List<Double4>()
            {
                this.Min,
                Double4.Point(this.Min.X, this.Min.Y, this.Max.Z),
                Double4.Point(this.Min.X, this.Max.Y, this.Min.Z),
                Double4.Point(this.Min.X, this.Max.Y, this.Max.Z),
                Double4.Point(this.Max.X, this.Min.Y, this.Min.Z),
                Double4.Point(this.Max.X, this.Min.Y, this.Max.Z),
                Double4.Point(this.Max.X, this.Max.Y, this.Min.Z),
                this.Max,
            };
        }

        public bool Contains(Double4 p) =>
            this.Min.X <= p.X && p.X <= this.Max.X &&
            this.Min.Y <= p.Y && p.Y <= this.Max.Y &&
            this.Min.Z <= p.Z && p.Z <= this.Max.Z;

        public bool Contains(BoundingBox b) =>
            this.Contains(b.Min) &&
            this.Contains(b.Max);

        const double Epsilon = 0.00001;

        private static void CheckAxis(double origin, double direction, double tmin, double tmax, out double min, out double max)
        {
            var tminNum = (tmin - origin);
            var tmaxNum = (tmax - origin);

            if (Math.Abs(direction) >= Epsilon)
            {
                min = tminNum / direction;
                max = tmaxNum / direction;
            }
            else
            {
                min = tminNum * double.PositiveInfinity;
                max = tmaxNum * double.PositiveInfinity;
            }

            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
        }

        public bool Intersect(Ray ray)
        {
            CheckAxis(ray.Origin.X, ray.Direction.X, this.Min.X, this.Max.X, out var xtmin, out var xtmax);
            CheckAxis(ray.Origin.Y, ray.Direction.Y, this.Min.Y, this.Max.Y, out var ytmin, out var ytmax);
            CheckAxis(ray.Origin.Z, ray.Direction.Z, this.Min.Z, this.Max.Z, out var ztmin, out var ztmax);

            var tmin = Math.Max(xtmin, Math.Max(ytmin, ztmin));
            var tmax = Math.Min(xtmax, Math.Min(ytmax, ztmax));

            if (tmin > tmax)
            {
                return false;
            }

            return true;
        }
    }
}