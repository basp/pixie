namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linsi;

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
            Assert.Equal(Matrix4x4.Identity, c.Transform);
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
        public void RenderingWorldWithCamera()
        {
            var w = new DefaultWorld();
            var from = Vector4.CreatePosition(0, 0, -5);
            var to = Vector4.CreatePosition(0, 0, 0);
            var up = Vector4.CreateDirection(0, 1, 0);
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