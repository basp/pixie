namespace Pixie.Tests
{
    using System;

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
                Vector4.CreatePosition(-1, -1, -1),
                Vector4.CreatePosition(1, 1, 1));

        public override Vector4 LocalNormalAt(Vector4 point) =>
            Vector4.CreateDirection(point.X, point.Y, point.Z);
    }
}