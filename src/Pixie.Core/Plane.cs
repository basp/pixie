namespace Pixie.Core
{
    using System;

    public class Plane : Shape
    {
        const double Epsilon = 0.00001;

        public override Bounds3 Bounds()
        {
            var min = Double4.Point(double.NegativeInfinity, -Epsilon, double.NegativeInfinity);
            var max = Double4.Point(double.PositiveInfinity, Epsilon, double.PositiveInfinity);
            return new Bounds3(min, max);
        }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            if (Math.Abs(ray.Direction.Y) < Epsilon)
            {
                return IntersectionList.Empty();
            }

            var t = -ray.Origin.Y / ray.Direction.Y;
            var i = new Intersection(t, this);
            return IntersectionList.Create(i);
        }

        public override Double4 LocalNormalAt(Double4 point) =>
            Double4.Vector(0, 1, 0);
    }
}