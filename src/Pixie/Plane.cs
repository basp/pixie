namespace Pixie
{
    using System;

    public class Plane : Shape
    {
        const double Epsilon = 0.00001;

        public override BoundingBox GetBounds()
        {
            var min = Vector4.CreatePosition(double.NegativeInfinity, 0, double.NegativeInfinity);
            var max = Vector4.CreatePosition(double.PositiveInfinity, 0, double.PositiveInfinity);
            return new BoundingBox(min, max);
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

        public override Vector4 GetLocalNormal(Vector4 point) =>
            Vector4.CreateDirection(0, 1, 0);
    }
}