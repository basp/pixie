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
            var fov = (float)Math.PI / 2;
            var c = new Camera(hsize, vsize, fov);
            Assert.Equal(hsize, c.HorizontalSize);
            Assert.Equal(vsize, c.VerticalSize);
            Assert.Equal(fov, c.FieldOfView);
            Assert.Equal(Float4x4.Identity, c.Transform);
        }

        [Fact]
        public void TestPixelSizeForHorizontalCanvas()
        {
            var c = new Camera(200, 125, (float)Math.PI / 2);
            Assert.Equal(0.01f, c.PixelSize);
        }

        [Fact]
        public void TestPixelSizeForVerticalCanvas()
        {
            var c = new Camera(125, 200, (float)Math.PI / 2);
            Assert.Equal(0.01f, c.PixelSize);
        }

        [Fact]
        public void TestCreateRayThroughCenterOfCanvas()
        {
            var c = new Camera(201, 101, (float)Math.PI / 2);
            var r = c.RayForPixel(100, 50);
            const float epsilon = 0.00001f;
            var comparer = Float4.GetEqualityComparer(epsilon);
            Assert.Equal(Float4.Point(0, 0, 0), r.Origin, comparer);
            Assert.Equal(Float4.Vector(0, 0, -1), r.Direction, comparer);
        }

        [Fact]
        public void TestCreateRayThroughCornerOfCanvas()
        {
            var c = new Camera(201, 101, (float)Math.PI / 2);
            var r = c.RayForPixel(0, 0);
            var expectedOrigin = Float4.Point(0, 0, 0);
            var expectedDirection = Float4.Vector(0.66519f, 0.33259f, -0.66851f);
            const float epsilon = 0.00001f;
            var comparer = Float4.GetEqualityComparer(epsilon);
            Assert.Equal(expectedOrigin, r.Origin, comparer);
            Assert.Equal(expectedDirection, r.Direction, comparer);
        }

        [Fact]
        public void TestCreateRayWhenCameraIsTransformed()
        {
            var c = new Camera(201, 101, (float)Math.PI / 2);
            c.Transform = 
                Transform.RotateY((float)Math.PI / 4) *
                Transform.Translate(0, -2, 5);

            var r = c.RayForPixel(100, 50);
            var expectedOrigin = Float4.Point(0, 2, -5);
            var expectedDirection = Float4.Vector(
                (float)Math.Sqrt(2) / 2,
                0,
                -(float)Math.Sqrt(2) / 2);

            const float epsilon = 0.00001f;
            var comparer = Float4.GetEqualityComparer(epsilon);
            Assert.Equal(expectedOrigin, r.Origin, comparer);
            Assert.Equal(expectedDirection, r.Direction, comparer);
        }

        [Fact]
        public void RenderingWorldWithCamera()
        {
            var w = new DefaultWorld();
            var from = Float4.Point(0, 0, -5);
            var to = Float4.Point(0, 0, 0);
            var up = Float4.Vector(0, 1, 0);
            var c = new Camera(11, 11, (float)Math.PI / 2)
            {
                Transform = Transform.View(from, to, up),
            };

            var img = c.Render(w);
            var actual = img[5, 5];
            var expected = new Color(0.38066f, 0.47583f, 0.2855f);
            const float epsilon = 0.0001f;
            var comparer = Color.GetEqualityComparer(epsilon);
            Assert.Equal(expected, actual, comparer);            
        }
    }
}