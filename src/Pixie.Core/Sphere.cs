namespace Pixie.Core
{
    using System;

    public class Sphere : IShape
    {
        private Float4x4 transform = Float4x4.Identity;
        private Float4x4 inv = Float4x4.Identity;

        public Sphere()
        {
        }

        public Float4x4 Transform
        {
            get => this.transform;
            set 
            {
                this.inv = value.Inverse();
                this.transform = value;
            }
        }

        public Float4x4 Inverse => this.inv;

        public Float4 NormalAt(Float4 point)
        {
            var objectPoint = this.inv * point;
            var objectNormal = objectPoint - Float4.Zero;
            var worldNormal = this.inv.Transpose() * objectNormal;
            worldNormal.W = 0f; // hacky fix
            return worldNormal.Normalize();
        }
            

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