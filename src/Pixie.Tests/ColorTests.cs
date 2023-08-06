namespace Pixie.Tests;

public class ColorTests
{
    [Fact]
    public void TestMapping()
    {
        var c1 = new Color<double>(0.0, 0.5, 1.0);
        var c2 = c1.Map(u => (int)Math.Floor(u * 255.999));
        var want = new Color<int>(0, 127, 255);
        Assert.Equal(want, c2);
    }
}