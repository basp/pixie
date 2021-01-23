namespace Pixie.Tests
{
    using System;
    using Linie;

    class TestShape : Shape
    {
        public Nullable<Ray4> SavedRay4 { get; set; }

        public override IntersectionList LocalIntersect(Ray4 ray)
        {
            this.SavedRay4 = ray;
            return IntersectionList.Empty;
        }

        public override BoundingBox GetBounds() =>
            new BoundingBox(
                Vector4.CreatePosition(-1, -1, -1),
                Vector4.CreatePosition(1, 1, 1));

        public override Vector4 GetLocalNormal(Vector4 point) =>
            Vector4.CreateDirection(point.X, point.Y, point.Z);
    }
}