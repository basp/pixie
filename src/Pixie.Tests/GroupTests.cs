namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linsi;

    public class GroupTests
    {
        [Fact]
        public void CreateNewGroup()
        {
            var g = new Group();
            Assert.Equal(Matrix4x4.Identity, g.Transform);
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
            var r = new Ray(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 0, 1));
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

            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
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
            var r = new Ray(Vector4.CreatePosition(10, 0, -10), Vector4.CreateDirection(0, 0, 1));
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

            var p = s.WorldToObject(Vector4.CreatePosition(-2, 0, -10));
            const double eps = 0.00001;
            var comparer = Vector4.GetEqualityComparer(eps);
            Assert.Equal(Vector4.CreatePosition(0, 0, -1), p, comparer);
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

            var v = Vector4.CreateDirection(Math.Sqrt(3) / 3, Math.Sqrt(3) / 3, Math.Sqrt(3) / 3);
            var n = s.NormalToWorld(v);
            const double eps = 0.0001;
            var comparer = Vector4.GetEqualityComparer(eps);
            var expected = Vector4.CreateDirection(0.2857, 0.4286, -0.8571);
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

            var p = Vector4.CreatePosition(1.7321, 1.1547, -5.5774);
            var n = s.GetNormal(p);
            const double eps = 0.0001;
            var comparer = Vector4.GetEqualityComparer(eps);
            var expected = Vector4.CreateDirection(0.2857, 0.4286, -0.8571);
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

            var box = shape.GetBounds();
            Assert.Equal(
                Vector4.CreatePosition(-4.5, -3, -5),
                box.Min);
            Assert.Equal(
                Vector4.CreatePosition(4, 7, 4.5),
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
            var box = shape.GetBounds();
            Assert.Equal(
                Vector4.CreatePosition(-1, -1, -1),
                box.Min);
            Assert.Equal(
                Vector4.CreatePosition(3, 4, 5),
                box.Max);
        }

        [Fact]
        public void IntersectGroupDoesNotIntersectChildrenIfBoxIsMissed()
        {
            var child = new TestShape();
            var shape = new Group();
            shape.Add(child);
            var r = new Ray(
                Vector4.CreatePosition(0, 0, -5),
                Vector4.CreateDirection(0, 1, 0));
            var xs = shape.Intersect(r);
            Assert.Null(child.SavedRay);
        }

        [Fact]
        public void IntersectGroupTestsChildrenIfBoxHit()
        {
            var child = new TestShape();
            var shape = new Group();
            shape.Add(child);
            var r = new Ray(
                Vector4.CreatePosition(0, 0, -5),
                Vector4.CreateDirection(0, 0, 1));
            var xs = shape.Intersect(r);
            Assert.NotNull(child.SavedRay);
        }

        [Fact]
        public void PartitionGroupChildren()
        {
            var s1 = new Sphere()
            {
                Transform = Transform.Translate(-2, 0, 0),
            };

            var s2 = new Sphere()
            {
                Transform = Transform.Translate(2, 0, 0),
            };

            var s3 = new Sphere();

            var g = new Group();
            g.Add(s1);
            g.Add(s2);
            g.Add(s3);
            g.Partition(out var left, out var right);

            Assert.Single(g);
            Assert.Contains(s3, g);
            Assert.Single(left);
            Assert.Contains(s1, left);
            Assert.Single(right);
            Assert.Contains(s2, right);
        }

        [Fact]
        public void CreatingSubGroupFromListOfChildren()
        {
            var s1 = new Sphere();
            var s2 = new Sphere();
            var g = new Group();
            g.Subgroup(s1, s2);

            Assert.Single(g);
            Assert.Contains(s1, (Group)g[0]);
            Assert.Contains(s2, (Group)g[0]);
        }

        [Fact]
        public void SubdivideGroupPartitionsItsChildren()
        {
            var s1 = new Sphere()
            {
                Transform = Transform.Translate(-2, -2, 0),
            };

            var s2 = new Sphere()
            {
                Transform = Transform.Translate(-2, 2, 0),
            };

            var s3 = new Sphere()
            {
                Transform = Transform.Scale(4, 4, 4),
            };

            var g = new Group();
            g.Add(s1);
            g.Add(s2);
            g.Add(s3);

            g.Divide(1);

            var subgroup = (Group)g[1];

            Assert.Equal(s3, g[0]);
            Assert.Contains(s1, (Group)subgroup[0]);
            Assert.Contains(s2, (Group)subgroup[1]);
        }

        [Fact]
        public void SubdivideGroupWithTooFewChildren()
        {
            var s1 = new Sphere()
            {
                Transform = Transform.Translate(-2, 0, 0),
            };

            var s2 = new Sphere()
            {
                Transform = Transform.Translate(2, 1, 0),
            };

            var s3 = new Sphere()
            {
                Transform = Transform.Translate(2, -1, 0),
            };

            var subgroup = new Group();
            subgroup.Add(s1);
            subgroup.Add(s2);
            subgroup.Add(s3);

            var s4 = new Sphere();
            var g = new Group();
            g.Add(subgroup);
            g.Add(s4);

            g.Divide(3);

            Assert.Equal(subgroup, g[0]);
            Assert.Equal(s4, g[1]);
            Assert.Equal(2, subgroup.Count);
            Assert.Contains(s1, (Group)subgroup[0]);
            Assert.Contains(s2, (Group)subgroup[1]);
            Assert.Contains(s3, (Group)subgroup[1]);
        }
    }
}