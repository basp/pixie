namespace Pixie.Core
{
    using System;

    public class Cube : Shape
    {
        private const double Epsilon = 0.0001;

        private static double Max(double a, double b, double c) =>
            Math.Max(a, Math.Max(b, c));

        private static double Min(double a, double b, double c) =>
            Math.Min(a, Math.Min(b, c));

        private static void CheckAxis(double origin, double direction, out double min, out double max)
        {
            var tminNum = -1 - origin;
            var tmaxNum = 1 - origin;

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

        public override IntersectionList LocalIntersect(Ray ray)
        {
            CheckAxis(ray.Origin.X, ray.Direction.X, out var xtmin, out var xtmax);
            CheckAxis(ray.Origin.Y, ray.Direction.Y, out var ytmin, out var ytmax);
            CheckAxis(ray.Origin.Z, ray.Direction.Z, out var ztmin, out var ztmax);

            var tmin = Max(xtmin, ytmin, ztmin);
            var tmax = Min(xtmax, ytmax, ztmax);

            if (tmin > tmax)
            {
                return IntersectionList.Empty();
            }

            return IntersectionList.Create(
                new Intersection(tmin, this),
                new Intersection(tmax, this));
        }

        public override Vector4 LocalNormalAt(Vector4 point)
        {
            var maxc = Max(
                Math.Abs(point.X),
                Math.Abs(point.Y),
                Math.Abs(point.Z));

            if (maxc == Math.Abs(point.X))
            {
                return Vector4.CreateDirection(point.X, 0, 0);
            }
            else if (maxc == Math.Abs(point.Y))
            {
                return Vector4.CreateDirection(0, point.Y, 0);
            }
            else
            {
                return Vector4.CreateDirection(0, 0, point.Z);
            }
        }

        public override BoundingBox Bounds()
        {
            var min = Vector4.CreatePosition(-1, -1, -1);
            var max = Vector4.CreatePosition(1, 1, 1);
            return new BoundingBox(min, max);
        }
    }
}