// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using System;
    using Linsi;

    public class Sphere : Shape, IEquatable<Sphere>
    {
        public override Vector4 GetLocalNormal(Vector4 point) =>
            point - Vector4.Zero;

        public override IntersectionList LocalIntersect(Ray ray)
        {
            var sphereToRay = ray.Origin - Vector4.CreatePosition(0, 0, 0);

            var a = Vector4.Dot(ray.Direction, ray.Direction);
            var b = 2 * Vector4.Dot(ray.Direction, sphereToRay);
            var c = Vector4.Dot(sphereToRay, sphereToRay) - 1;

            var discriminant = (b * b) - (4 * a * c);

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

        public override BoundingBox GetBounds()
        {
            var min = Vector4.CreatePosition(-1, -1, -1);
            var max = Vector4.CreatePosition(1, 1, 1);
            return new BoundingBox(min, max);
        }
    }
}
