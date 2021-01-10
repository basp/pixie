namespace Pixie
{
    using System;

    public class Triangle : Shape
    {
        private const double Epsilon = 0.00001;

        public Triangle(Vector4 p1, Vector4 p2, Vector4 p3)
        {
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
            this.E1 = p2 - p1;
            this.E2 = p3 - p1;
            this.Normal = this.E2.Cross(this.E1).Normalize();
        }

        public Vector4 P1 { get; }

        public Vector4 P2 { get; }

        public Vector4 P3 { get; }

        public Vector4 E1 { get; }

        public Vector4 E2 { get; }

        public Vector4 Normal { get; }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            var dirCrossE2 = ray.Direction.Cross(this.E2);
            var det = this.E1.Dot(dirCrossE2);

            if (Math.Abs(det) < Epsilon)
            {
                return IntersectionList.Empty();
            }

            var f = 1.0 / det;
            var p1ToOrigin = ray.Origin - this.P1;
            var u = f * p1ToOrigin.Dot(dirCrossE2);
            if (u < 0 || u > 1)
            {
                return IntersectionList.Empty();
            }

            var originCrossE1 = p1ToOrigin.Cross(this.E1);
            var v = f * ray.Direction.Dot(originCrossE1);

            if (v < 0 || (u + v) > 1)
            {
                return IntersectionList.Empty();
            }

            var t = f * this.E2.Dot(originCrossE1);
            return IntersectionList.Create(
                new Intersection(t, this));
        }

        public override Vector4 LocalNormalAt(Vector4 point) =>
            this.Normal;

        public override BoundingBox Bounds()
        {
            var box = BoundingBox.Empty;
            box += this.P1;
            box += this.P2;
            box += this.P3;
            return box;
        }
    }
}