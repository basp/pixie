namespace Pixie.Tests
{
    using System;
    using Pixie.Core;

    class TestShape : Shape
    {
        public Nullable<Ray> SavedRay { get; set; }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            this.SavedRay = ray;
            return IntersectionList.Empty();
        }

        public override BoundingBox Bounds() =>
            new BoundingBox(
                Double4.Point(-1, -1, -1),
                Double4.Point(1, 1, 1));

        public override Double4 LocalNormalAt(Double4 point) =>
            Double4.Vector(point.X, point.Y, point.Z);
    }
}