namespace Linsi.Tests
{
    using Xunit;

    public class RayTests
    {
        [Fact]
        public void TestRayConstructor()
        {
            var origin = Vector4.CreatePosition(1, 2, 3);
            var direction = Vector4.CreateDirection(4, 5, 6);
            var r = new Ray(origin, direction);
            Assert.Equal(origin, r.Origin);
            Assert.Equal(direction, r.Direction);
        }

        [Fact]
        public void TestComputePointFromDistance()
        {
            var r = new Ray(Vector4.CreatePosition(2, 3, 4), Vector4.CreateDirection(1, 0, 0));
            Assert.Equal(Vector4.CreatePosition(2, 3, 4), r[0]);
            Assert.Equal(Vector4.CreatePosition(3, 3, 4), r[1]);
            Assert.Equal(Vector4.CreatePosition(1, 3, 4), r[-1]);
            Assert.Equal(Vector4.CreatePosition(4.5, 3, 4), r[2.5]);
        }

        [Fact]
        public void TestTranslateRay()
        {
            var r = new Ray(Vector4.CreatePosition(1, 2, 3), Vector4.CreateDirection(0, 1, 0));
            var t = Transform.Translate(3, 4, 5);
            var r2 = t * r;
            Assert.Equal(Vector4.CreatePosition(4, 6, 8), r2.Origin);
            Assert.Equal(Vector4.CreateDirection(0, 1, 0), r2.Direction);
        }

        [Fact]
        public void TestScaleRay()
        {
            var r = new Ray(Vector4.CreatePosition(1, 2, 3), Vector4.CreateDirection(0, 1, 0));
            var t = Transform.Scale(2, 3, 4);
            var r2 = t * r;
            Assert.Equal(Vector4.CreatePosition(2, 6, 12), r2.Origin);
            Assert.Equal(Vector4.CreateDirection(0, 3, 0), r2.Direction);
        }
    }
}