namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    class TestShape : Shape
    {
        public Ray SavedRay { get; set; }

        public override IntersectionList LocalIntersect(Ray ray)
        {
            this.SavedRay = ray;
            return IntersectionList.Empty();
        }

        public override Double4 LocalNormalAt(Double4 point) =>
            Double4.Vector(point.X, point.Y, point.Z);
    }

    public class ShapeTests
    {
        [Fact]
        public void DefaultTransformation()
        {
            var s = new TestShape();
            Assert.Equal(Double4x4.Identity, s.Transform);
        }

        [Fact]
        public void AssignTransformation()
        {
            var s = new TestShape()
            {
                Transform = Transform.Translate(2, 3, 4),
            };

            var expected = Transform.Translate(2, 3, 4);
            Assert.Equal(expected, s.Transform);
        }

        [Fact]
        public void DefaultMaterial()
        {
            var s = new TestShape();
            var expected = new Material();
            Assert.Equal(expected, s.Material);
        }

        [Fact]
        public void AssigningMaterial()
        {
            var m = new Material
            {
                Ambient = 1,
            };

            var s = new TestShape()
            {
                Material = m,
            };

            Assert.Equal(m, s.Material);
        }

        [Fact]
        public void IntersectScaledShapeWithRay()
        {
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var s = new TestShape();
            s.Transform = Transform.Scale(2, 2, 2);
            var xs = s.Intersect(r);
            Assert.Equal(Double4.Point(0, 0, -2.5), s.SavedRay.Origin);
            Assert.Equal(Double4.Vector(0, 0, 0.5), s.SavedRay.Direction);
        }

        [Fact]
        public void IntersectTranslatedShapeWithRay()
        {
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var s = new TestShape();
            s.Transform = Transform.Translate(5, 0, 0);
            var xs = s.Intersect(r);
            Assert.Equal(Double4.Point(-5, 0, -5), s.SavedRay.Origin);
            Assert.Equal(Double4.Vector(0, 0, 1), s.SavedRay.Direction);
        }

        [Fact]
        public void ComputeNormalOnTranslatedShape()
        {
            var s = new TestShape()
            {
                Transform = Transform.Translate(0, 1, 0),
            };

            var n = s.NormalAt(Double4.Point(0, 1.70711, -0.70711));
            var expected = Double4.Vector(0, 0.70711, -0.70711);
            const double eps = 0.00001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void ComputeNormalOnTransformedShape()
        {
            var s = new TestShape()
            {
                Transform = 
                    Transform.Scale(1, 0.5, 1) *
                    Transform.RotateZ(Math.PI / 5),
            };

            var n = s.NormalAt(Double4.Point(0, Math.Sqrt(2)/2, -Math.Sqrt(2)/2));
            var expected = Double4.Vector(0, 0.97014, -0.24254);
            const double eps = 0.00001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }
    }
}