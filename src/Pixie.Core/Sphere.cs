namespace Pixie.Core
{
    using System;

    public class Sphere : Shape, IEquatable<Sphere>
    {
        public override Double4 LocalNormalAt(Double4 point) =>
            point - Double4.Zero;

        public override IntersectionList LocalIntersect(Ray ray)
        {
            var sphereToRay = ray.Origin - Double4.Point(0, 0, 0);

            var a = Double4.Dot(ray.Direction, ray.Direction);
            var b = 2 * Double4.Dot(ray.Direction, sphereToRay);
            var c = Double4.Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = b * b - 4 * a * c;

            // If the discriminant is negative, the ray misses
            // and there are no intersections.
            if (discriminant < 0)
            {
                return IntersectionList.Empty();
            }

            // Otherwise, we have two intersections which we can
            // solve for t using the quadratic formula.
            var t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            var xs = new[]
            {
                new Intersection(t1, this),
                new Intersection(t2, this),
            };

            return IntersectionList.Create(xs);
        }

        public bool Equals(Sphere other) =>
            this.Material.Equals(other.Material) &&
            this.transform.Equals(other.transform) &&
            this.inv.Equals(other.inv);

        public override BoundingBox Bounds()
        {
            var min = Double4.Point(-1, -1, -1);
            var max = Double4.Point(1, 1, 1);
            return new BoundingBox(min, max);
        }
    }
}