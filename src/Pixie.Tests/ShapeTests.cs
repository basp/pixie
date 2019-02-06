namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    class TestShape : Shape
    {
        public override IntersectionList LocalIntersect(Ray ray)
        {
            throw new System.NotImplementedException();
        }

        public override Float4 NormalAt(Float4 point)
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
            Assert.Equal(Float4x4.Identity, s.Transform);
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
    }
}