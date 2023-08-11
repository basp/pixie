namespace Pixie.Tests;

public class LightTests
{
    [Fact]
    public void PointLightHasPositionAndIntensity()
    {
        var intensity = new Vector3(1, 1, 1);
        var position = new Vector3(0, 0, 0).AsPosition();
        var light = new PointLight(position, intensity);
        Assert.Equal(position, light.Position);
        Assert.Equal(intensity, light.Intensity);
    }
}