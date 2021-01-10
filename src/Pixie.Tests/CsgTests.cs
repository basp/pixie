namespace Pixie.Tests
{
    using Xunit;

    public class CsgTests
    {
        [Fact]
        public void CreateCsg()
        {
            var s1 = new Sphere();
            var s2 = new Cube();
            var csg = new Csg(Operation.Union, s1, s2);
            Assert.Equal(s1, csg.Left);
            Assert.Equal(s2, csg.Right);
            Assert.Equal(Operation.Union, csg.Operation);
        }

        [Theory]

        [InlineData(Operation.Union, true, true, true, false)]
        [InlineData(Operation.Union, true, true, false, true)]
        [InlineData(Operation.Union, true, false, true, false)]
        [InlineData(Operation.Union, true, false, false, true)]
        [InlineData(Operation.Union, false, true, true, false)]
        [InlineData(Operation.Union, false, true, false, false)]
        [InlineData(Operation.Union, false, false, true, true)]
        [InlineData(Operation.Union, false, false, false, true)]

        [InlineData(Operation.Intersect, true, true, true, true)]
        [InlineData(Operation.Intersect, true, true, false, false)]
        [InlineData(Operation.Intersect, true, false, true, true)]
        [InlineData(Operation.Intersect, true, false, false, false)]
        [InlineData(Operation.Intersect, false, true, true, true)]
        [InlineData(Operation.Intersect, false, true, false, true)]
        [InlineData(Operation.Intersect, false, false, true, false)]
        [InlineData(Operation.Intersect, false, false, false, false)]

        [InlineData(Operation.Difference, true, true, true, false)]
        [InlineData(Operation.Difference, true, true, false, true)]
        [InlineData(Operation.Difference, true, false, true, false)]
        [InlineData(Operation.Difference, true, false, false, true)]
        [InlineData(Operation.Difference, false, true, true, true)]
        [InlineData(Operation.Difference, false, true, false, true)]
        [InlineData(Operation.Difference, false, false, true, false)]
        [InlineData(Operation.Difference, false, false, false, false)]
        public void EvaluatingRuleForCsgOperation(
            Operation op,
            bool lhit,
            bool inl,
            bool inr,
            bool expected)
        {
            var actual = Csg.IntersectionAllowed(
                op,
                lhit,
                inl,
                inr);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Operation.Union, 0, 3)]
        [InlineData(Operation.Intersect, 1, 2)]
        [InlineData(Operation.Difference, 0, 1)]
        public void FilterListOfIntersections(
            Operation op,
            int x0,
            int x1)
        {
            var s1 = new Sphere();
            var s2 = new Cube();
            var csg = new Csg(op, s1, s2);
            var xs = IntersectionList.Create(
                new Intersection(1, s1),
                new Intersection(2, s2),
                new Intersection(3, s1),
                new Intersection(4, s2));

            var result = csg.FilterIntersections(xs);
            Assert.Equal(2, result.Count);
            Assert.Equal(xs[x0], result[0]);
            Assert.Equal(xs[x1], result[1]);
        }

        [Fact]
        public void RayMissesCsgObject()
        {
            var c = new Csg(Operation.Union, new Sphere(), new Cube());
            var r = new Ray(Vector4.CreatePosition(0, 2, -5), Vector4.CreateDirection(0, 0, 1));
            var xs = c.LocalIntersect(r);
            Assert.Empty(xs);
        }

        [Fact]
        public void RayHitsCsgObject()
        {
            var s1 = new Sphere();
            var s2 = new Sphere()
            {
                Transform =
                    Transform.Translate(0, 0, 0.5),
            };

            var c = new Csg(Operation.Union, s1, s2);
            var r = new Ray(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var xs = c.LocalIntersect(r);
            Assert.Equal(2, xs.Count);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(s1, xs[0].Object);
            Assert.Equal(6.5, xs[1].T);
            Assert.Equal(s2, xs[1].Object);
        }

        [Fact]
        public void IntersectCsgDoesNotTestChildrenIfBoxMissed()
        {
            var left = new TestShape();
            var right = new TestShape();
            var shape = new Csg(Operation.Difference, left, right);
            var r = new Ray(
                Vector4.CreatePosition(0, 0, -5),
                Vector4.CreateDirection(0, 1, 0));
            var xs = shape.Intersect(r);
            Assert.Null(left.SavedRay);
            Assert.Null(right.SavedRay);
        }

        [Fact]
        public void IntersectCsgTestsChildrenIfBoxHit()
        {
            var left = new TestShape();
            var right = new TestShape();
            var shape = new Csg(Operation.Difference, left, right);
            var r = new Ray(
                Vector4.CreatePosition(0, 0, -5),
                Vector4.CreateDirection(0, 0, 1));
            var xs = shape.Intersect(r);
            Assert.NotNull(left.SavedRay);
            Assert.NotNull(right.SavedRay);
        }

        [Fact]
        public void SubdividingCsgShapeSubdividesItsChildren()
        {
            var s1 = new Sphere()
            {
                Transform = Transform.Translate(-1.5, 0, 0),
            };

            var s2 = new Sphere()
            {
                Transform = Transform.Translate(1.5, 0, 0),
            };

            var left = new Group();
            left.Add(s1);
            left.Add(s2);

            var s3 = new Sphere()
            {
                Transform = Transform.Translate(0, 0, -1.5),
            };

            var s4 = new Sphere()
            {
                Transform = Transform.Translate(0, 0, 1.5),
            };

            var right = new Group();
            right.Add(s3);
            right.Add(s4);

            var shape = new Csg(Operation.Difference, left, right);
            shape.Divide(1);

            Assert.Contains(s1, (Group)left[0]);
            Assert.Contains(s2, (Group)left[1]);
            Assert.Contains(s3, (Group)right[0]);
            Assert.Contains(s4, (Group)right[1]);
        }
    }
}