namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class ShapeTests
    {
        [Fact]
        public void DefaultTransformation()
        {
            var s = new TestShape();
            Assert.Equal(Matrix4x4.Identity, s.Transform);
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
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var s = new TestShape();
            s.Transform = Transform.Scale(2, 2, 2);
            var xs = s.Intersect(r);
            var savedRay = s.SavedRay.Value;
            Assert.Equal(Vector4.CreatePosition(0, 0, -2.5), savedRay.Origin);
            Assert.Equal(Vector4.CreateDirection(0, 0, 0.5), savedRay.Direction);
        }

        [Fact]
        public void IntersectTranslatedShapeWithRay()
        {
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var s = new TestShape();
            s.Transform = Transform.Translate(5, 0, 0);
            var xs = s.Intersect(r);
            var savedRay = s.SavedRay.Value;
            Assert.Equal(Vector4.CreatePosition(-5, 0, -5), savedRay.Origin);
            Assert.Equal(Vector4.CreateDirection(0, 0, 1), savedRay.Direction);
        }

        [Fact]
        public void ComputeNormalOnTranslatedShape()
        {
            var s = new TestShape()
            {
                Transform = Transform.Translate(0, 1, 0),
            };

            var n = s.NormalAt(Vector4.CreatePosition(0, 1.70711, -0.70711));
            var expected = Vector4.CreateDirection(0, 0.70711, -0.70711);
            const double eps = 0.00001;
            var comparer = Vector4.GetEqualityComparer(eps);
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

            var n = s.NormalAt(Vector4.CreatePosition(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
            var expected = Vector4.CreateDirection(0, 0.97014, -0.24254);
            const double eps = 0.00001;
            var comparer = Vector4.GetEqualityComparer(eps);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void ShapeHasParentAttribute()
        {
            var s = new TestShape();
            Assert.Null(s.Parent);
        }

        [Fact]
        public void TestShapeHasArbitraryBounds()
        {
            var s = new TestShape();
            var box = s.Bounds();
            Assert.Equal(
                Vector4.CreatePosition(-1, -1, -1),
                box.Min);
            Assert.Equal(
                Vector4.CreatePosition(1, 1, 1),
                box.Max);
        }

        [Fact]
        public void QueryShapeBoundingBoxInParentSpace()
        {
            var shape = new Sphere();
            shape.Transform = 
                Transform.Translate(1,-3,5) *
                Transform.Scale(0.5, 2, 4);
            
            var box = shape.ParentSpaceBounds();
            Assert.Equal(
                Vector4.CreatePosition(0.5,-5,1),
                box.Min);
            Assert.Equal(
                Vector4.CreatePosition(1.5,-1,9),
                box.Max);
        }
    }
}