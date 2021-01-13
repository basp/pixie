namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linsi;

    public class DefaultSamplerTests
    {
        [Fact]
        public void TestCreateRayThroughCenterOfCanvas()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            var s = new DefaultSampler(new World(), c);
            var r = s.RayForPixel(100, 50);
            const double epsilon = 0.00001;
            var comparer = Vector4.GetEqualityComparer(epsilon);
            Assert.Equal(Vector4.CreatePosition(0, 0, 0), r.Origin, comparer);
            Assert.Equal(Vector4.CreateDirection(0, 0, -1), r.Direction, comparer);
        }

        [Fact]
        public void TestCreateRayThroughCornerOfCanvas()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            var s = new DefaultSampler(new World(), c);
            var r = s.RayForPixel(0, 0);
            var expectedOrigin = Vector4.CreatePosition(0, 0, 0);
            var expectedDirection = Vector4.CreateDirection(0.66519, 0.33259, -0.66851);
            const double epsilon = 0.00001;
            var comparer = Vector4.GetEqualityComparer(epsilon);
            Assert.Equal(expectedOrigin, r.Origin, comparer);
            Assert.Equal(expectedDirection, r.Direction, comparer);
        }

        [Fact]
        public void TestCreateRayWhenCameraIsTransformed()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            c.Transform = 
                Transform.RotateY(Math.PI / 4) *
                Transform.Translate(0, -2, 5);

            var s = new DefaultSampler(new World(), c);
            var r = s.RayForPixel(100, 50);
            var expectedOrigin = Vector4.CreatePosition(0, 2, -5);
            var expectedDirection = Vector4.CreateDirection(
                Math.Sqrt(2) / 2,
                0,
                -Math.Sqrt(2) / 2);

            const double epsilon = 0.00001;
            var comparer = Vector4.GetEqualityComparer(epsilon);
            Assert.Equal(expectedOrigin, r.Origin, comparer);
            Assert.Equal(expectedDirection, r.Direction, comparer);
        }
    }
}