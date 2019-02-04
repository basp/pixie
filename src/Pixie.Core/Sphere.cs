namespace Pixie.Core
{
    using System;

    public class Sphere : IShape
    {
        public Intersection[] Intersect(Ray ray)
        {
            var sphereToRay = ray.Origin - Float4.Point(0, 0, 0);

            var a = Float4.Dot(ray.Direction, ray.Direction);
            var b = 2 * Float4.Dot(ray.Direction, sphereToRay);
            var c = Float4.Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            // If the discriminant is negative, the ray misses
            // and there are no intersections.
            if (discriminant < 0)
            {
                return new Intersection[0];
            }

            // Otherwise, we have two intersections which we can
            // solve for t using the quadratic formula.
            var t1 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);
            return new[]
            {
                new Intersection(t1, this),
                new Intersection(t2, this),
            };
        }
    }
}