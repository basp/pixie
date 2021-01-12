namespace Pixie.Tests
{
    using Xunit;

    public class LightTests
    {
        [Fact]
        public void TestPointLightHasPositionAndIntensity()
        {
            var intensity = new Color(1, 1, 1);
            var position = Vector4.CreatePosition(0, 0, 0);
            var light = new PointLight(position, intensity);
            Assert.Equal(position, light.Position);
            Assert.Equal(intensity, light.Intensity);
        }

        [Theory]
        [InlineData(0, 1.0001, 0, 1.0)]
        [InlineData(-1.0001, 0, 0, 1.0)]
        [InlineData(0, 0, -1.0001, 1.0)]
        [InlineData(0, 0, 1.0001, 0.0)]
        [InlineData(1.0001, 0, 0, 0.0)]
        [InlineData(0, -1.0001, 0, 0.0)]
        [InlineData(0, 0, 0, 0.0)]
        public void PointLightsEvaluateLightIntensityAtGivenPoint(
            double px,
            double py,
            double pz,
            double result)
        {
            var w = new DefaultWorld();
            var light = w.Lights[0];
            var pt = Vector4.CreatePosition(px, py, pz);
            var intensity = light.GetIntensity(pt, w);
            Assert.Equal(result, intensity);
        }

        [Fact]
        public void CreatingAnAreaLight()
        {
            var corner = Vector4.CreatePosition(0, 0, 0);
            var v1 = Vector4.CreateDirection(2, 0, 0);
            var v2 = Vector4.CreateDirection(0, 0, 1);
            var light = new AreaLight(
                corner,
                v1,
                4,
                v2,
                2,
                Color.White);

            Assert.Equal(corner, light.Corner);
            Assert.Equal(Vector4.CreateDirection(0.5, 0, 0), light.Uvec);
            Assert.Equal(4, light.Usteps);
            Assert.Equal(Vector4.CreateDirection(0, 0, 0.5), light.Vvec);
            Assert.Equal(2, light.Vsteps);
            Assert.Equal(8, light.Samples);
            Assert.Equal(Vector4.CreatePosition(1, 0, 0.5), light.Position);
        }

        [Theory]
        [InlineData(0, 0, 0.25, 0, 0.25)]
        [InlineData(1, 0, 0.75, 0, 0.25)]
        [InlineData(0, 1, 0.25, 0, 0.75)]
        [InlineData(2, 0, 1.25, 0, 0.25)]
        [InlineData(3, 1, 1.75, 0, 0.75)]
        public void FindSinglePointOnAnAreaLight(
            double u,
            double v,
            double rx,
            double ry,
            double rz)
        {
            var corner = Vector4.CreatePosition(0, 0, 0);
            var v1 = Vector4.CreateDirection(2, 0, 0);
            var v2 = Vector4.CreateDirection(0, 0, 1);
            var light = new AreaLight(
                corner,
                v1,
                4,
                v2,
                2,
                Color.White);

            var pt = light.GetPoint(u, v);
            var expected = Vector4.CreatePosition(rx, ry, rz);
            Assert.Equal(expected, pt);
        }

        [Theory]
        [InlineData(0, 0, 2, 0.0)]
        [InlineData(1, -1, 2, 0.25)]
        [InlineData(1.5, 0, 2, 0.5)]
        [InlineData(1.25, 1.25, 3, 0.75)]
        [InlineData(0, 0, -2, 1.0)]
        public void AreaLightIntensityFunction(
            double px,
            double py,
            double pz,
            double result)
        {
            var w = new DefaultWorld();
            var corner = Vector4.CreatePosition(-0.5, -0.5, -5);
            var v1 = Vector4.CreateDirection(1, 0, 0);
            var v2 = Vector4.CreateDirection(0, 1, 0);
            var light = new AreaLight(
                corner,
                v1,
                2,
                v2,
                2,
                Color.White);

            var pt = Vector4.CreatePosition(px, py, pz);
            var intensity = light.GetIntensity(pt, w);
            Assert.Equal(result, intensity);
        }

        [Theory]
        [InlineData(0, 0, 0.15, 0, 0.35)]
        [InlineData(1, 0, 0.65, 0, 0.35)]
        [InlineData(0, 1, 0.15, 0, 0.85)]
        [InlineData(2, 0, 1.15, 0, 0.35)]
        [InlineData(3, 1, 1.65, 0, 0.85)]
        public void FindSinglePointOnJitteredAreaLight(
            double u,
            double v,
            double rx,
            double ry,
            double rz)
        {
            var corner = Vector4.CreatePosition(0, 0, 0);
            var v1 = Vector4.CreateDirection(2, 0, 0);
            var v2 = Vector4.CreateDirection(0, 0, 1);
            var light = new AreaLight(
                corner,
                v1,
                4,
                v2,
                2,
                Color.White)
            {
                Jitter = new Sequence(0.3, 0.7),
            };

            var pt = light.GetPoint(u, v);
            var expected = Vector4.CreatePosition(rx, ry, rz);
            Assert.Equal(expected, pt);
        }

        [Theory]
        [InlineData(0, 0, 2, 0.0)]
        [InlineData(1, -1, 2, 0.5)]
        [InlineData(1.5, 0, 2, 0.75)]
        [InlineData(1.25, 1.25, 3, 0.75)]
        [InlineData(0, 0, -2, 1.0)]
        public void AreaLightWithJitteredSamples(
            double px,
            double py,
            double pz,
            double result)
        {
            var w = new DefaultWorld();
            var corner = Vector4.CreatePosition(-0.5, -0.5, -5);
            var v1 = Vector4.CreateDirection(1, 0, 0);
            var v2 = Vector4.CreateDirection(0, 1, 0);
            var light = new AreaLight(
                corner,
                v1,
                2,
                v2,
                2,
                Color.White)
            {
                Jitter = new Sequence(0.7, 0.3, 0.9, 0.1, 0.5),
            };

            var pt = Vector4.CreatePosition(px, py, pz);
            var intensity = light.GetIntensity(pt, w);
            Assert.Equal(result, intensity);
        }
    }
}