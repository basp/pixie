namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class GroupTests
    {
        [Fact]
        public void CreateNewGroup()
        {
            var g = new Group();
            Assert.Equal(Double4x4.Identity, g.Transform);
            Assert.Empty(g);
        }

        [Fact]
        public void AddChildToGroup()
        {
            var g = new Group();
            var s = new TestShape();
            g.Add(s);
            Assert.NotEmpty(g);
            Assert.Contains(s, g);
            Assert.Equal(g, s.Parent);
        }

        [Fact]
        public void IntersectRayWithEmptyGroup()
        {
            var g = new Group();
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 0, 1));
            var xs = g.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void IntersectRayWithNonEmptyGroup()
        {
            var g = new Group();

            var s1 = new Sphere();

            var s2 = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 0, -3),
            };

            var s3 = new Sphere()
            {
                Transform =
                    Transform.Translate(5, 0, 0),
            };

            g.Add(s1);
            g.Add(s2);
            g.Add(s3);

            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var xs = g.LocalIntersect(r);
            Assert.Equal(4, xs.Count);
            Assert.Equal(s2, xs[0].Object);
            Assert.Equal(s2, xs[1].Object);
            Assert.Equal(s1, xs[2].Object);
            Assert.Equal(s1, xs[3].Object);
        }

        [Fact]
        public void IntersectingTransformedGroup()
        {
            var g = new Group();
            g.Transform = Transform.Scale(2, 2, 2);
            var sphere = new Sphere();
            sphere.Transform = Transform.Translate(5, 0, 0);
            g.Add(sphere);
            var r = new Ray(Double4.Point(10, 0, -10), Double4.Vector(0, 0, 1));
            var xs = g.Intersect(r);
            Assert.Equal(2, xs.Count);
        }

        [Fact]
        public void ConvertPointFromWorldToObjectSpace()
        {
            var g1 = new Group()
            {
                Transform = Transform.RotateY(Math.PI / 2),
            };

            var g2 = new Group()
            {
                Transform = Transform.Scale(2, 2, 2),
            };

            g1.Add(g2);

            var s = new Sphere()
            {
                Transform = Transform.Translate(5, 0, 0),
            };

            g2.Add(s);

            var p = s.WorldToObject(Double4.Point(-2, 0, -10));
            const double eps = 0.00001;
            var comparer = Double4.GetEqualityComparer(eps);
            Assert.Equal(Double4.Point(0, 0, -1), p, comparer);
        }

        [Fact]
        public void ConvertNormalFromObjectSpaceToWorldSpace()
        {
            var g1 = new Group()
            {
                Transform = Transform.RotateY(Math.PI / 2),
            };

            var g2 = new Group()
            {
                Transform = Transform.Scale(1, 2, 3),
            };

            g1.Add(g2);

            var s = new Sphere()
            {
                Transform = Transform.Translate(5, 0, 0),
            };

            g2.Add(s);

            var v = Double4.Vector(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3);
            var n = s.NormalToWorld(v);
            const double eps = 0.0001;
            var comparer = Double4.GetEqualityComparer(eps);
            var expected = Double4.Vector(0.2857, 0.4286, -0.8571);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void FindNormalOnChildObject()
        {
            var g1 = new Group()
            {
                Transform = Transform.RotateY(Math.PI / 2),
            };

            var g2 = new Group()
            {
                Transform = Transform.Scale(1, 2, 3),
            };

            g1.Add(g2);

            var s = new Sphere()
            {
                Transform = Transform.Translate(5, 0, 0),
            };

            g2.Add(s);

            var p = Double4.Point(1.7321, 1.1547, -5.5774);
            var n = s.NormalAt(p);
            const double eps = 0.0001;
            var comparer = Double4.GetEqualityComparer(eps);
            var expected = Double4.Vector(0.2857, 0.4286, -0.8571);
            Assert.Equal(expected, n, comparer);
        }

        [Fact]
        public void GroupHasBoundingBoxThatContainsItsChildren()
        {
            var s = new Sphere()
            {
                Transform =
                    Transform.Translate(2, 5, -3) *
                    Transform.Scale(2, 2, 2),
            };

            var c = new Cylinder()
            {
                Minimum = -2,
                Maximum = 2,
                Transform =
                    Transform.Translate(-4, -1, 4) *
                    Transform.Scale(0.5, 1, 0.5),
            };

            var shape = new Group();
            shape.Add(s);
            shape.Add(c);

            var box = shape.Bounds();
            Assert.Equal(
                Double4.Point(-4.5, -3, -5),
                box.Min);
            Assert.Equal(
                Double4.Point(4, 7, 4.5),
                box.Max);
        }

        [Fact]
        public void CsgHasBoundingBoxThatContainsItsChildren()
        {
            var left = new Sphere();
            var right = new Sphere()
            {
                Transform = Transform.Translate(2, 3, 4),
            };

            var shape = new Csg(Operation.Difference, left, right);
            var box = shape.Bounds();
            Assert.Equal(
                Double4.Point(-1, -1, -1),
                box.Min);
            Assert.Equal(
                Double4.Point(3, 4, 5),
                box.Max);
        }
    }
}