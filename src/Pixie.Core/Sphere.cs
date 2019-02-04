namespace Pixie.Core
{
    using System;

    public class Sphere : IShape
    {
        public Sphere()
        {
            this.Transform = Float4x4.Identity;
        }

        public Float4x4 Transform { get; set; }

        public IntersectionList Intersect(Ray ray)
        {
            ray = this.Transform.Inverse() * ray;

            var sphereToRay = ray.Origin - Float4.Point(0, 0, 0);

            var a = Float4.Dot(ray.Direction, ray.Direction);
            var b = 2 * Float4.Dot(ray.Direction, sphereToRay);
            var c = Float4.Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            // If the discriminant is negative, the ray misses
            // and there are no intersections.
            if (discriminant < 0)
            {
                return IntersectionList.Empty();
            }

            // Otherwise, we have two intersections which we can
            // solve for t using the quadratic formula.
            var t1 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);
            var xs = new[]
            {
                new Intersection(t1, this),
                new Intersection(t2, this),
            };

            return IntersectionList.Create(xs);
        }
    }
}