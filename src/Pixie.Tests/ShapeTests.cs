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

        public override Double4 NormalAt(Double4 point)
        {
            throw new System.NotImplementedException();
        }
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
    }
}