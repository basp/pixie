namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Pixie.Core;

    public class CameraTests
    {
        [Fact]
        public void TestCameraConstructor()
        {
            var hsize = 160;
            var vsize = 120;
            var fov = Math.PI / 2;
            var c = new Camera(hsize, vsize, fov);
            Assert.Equal(hsize, c.HorizontalSize);
            Assert.Equal(vsize, c.VerticalSize);
            Assert.Equal(fov, c.FieldOfView);
            Assert.Equal(Double4x4.Identity, c.Transform);
        }

        [Fact]
        public void TestPixelSizeForHorizontalCanvas()
        {
            var c = new Camera(200, 125, Math.PI / 2);
            const int prec = 10;
            Assert.Equal(0.01, c.PixelSize, prec);
        }

        [Fact]
        public void TestPixelSizeForVerticalCanvas()
        {
            var c = new Camera(125, 200, Math.PI / 2);
            const int prec = 10;
            Assert.Equal(0.01, c.PixelSize, prec);
        }

        [Fact]
        public void TestCreateRayThroughCenterOfCanvas()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            var r = c.RayForPixel(100, 50);
            const double epsilon = 0.00001;
            var comparer = Double4.GetEqualityComparer(epsilon);
            Assert.Equal(Double4.Point(0, 0, 0), r.Origin, comparer);
            Assert.Equal(Double4.Vector(0, 0, -1), r.Direction, comparer);
        }

        [Fact]
        public void TestCreateRayThroughCornerOfCanvas()
        {
            var c = new Camera(201, 101, Math.PI / 2);
            var r = c.RayForPixel(0, 0);
            var expectedOrigin = Double4.Point(0, 0, 0);
            var expectedDirection = Double4.Vector(0.66519, 0.33259, -0.66851);
            const double epsilon = 0.00001;
            var comparer = Double4.GetEqualityComparer(epsilon);
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

            var r = c.RayForPixel(100, 50);
            var expectedOrigin = Double4.Point(0, 2, -5);
            var expectedDirection = Double4.Vector(
                Math.Sqrt(2) / 2,
                0,
                -Math.Sqrt(2) / 2);

            const double epsilon = 0.00001;
            var comparer = Double4.GetEqualityComparer(epsilon);
            Assert.Equal(expectedOrigin, r.Origin, comparer);
            Assert.Equal(expectedDirection, r.Direction, comparer);
        }

        [Fact]
        public void RenderingWorldWithCamera()
        {
            var w = new DefaultWorld();
            var from = Double4.Point(0, 0, -5);
            var to = Double4.Point(0, 0, 0);
            var up = Double4.Vector(0, 1, 0);
            var c = new Camera(11, 11, Math.PI / 2)
            {
                Transform = Transform.View(from, to, up),
            };

            var img = c.Render(w);
            var actual = img[5, 5];
            var expected = new Color(0.38066, 0.47583, 0.2855);
            const double epsilon = 0.0001;
            var comparer = Color.GetEqualityComparer(epsilon);
            Assert.Equal(expected, actual, comparer);            
        }
    }
}