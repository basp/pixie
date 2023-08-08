namespace Pixie.Tests;

public class ColorTests
{
    [Fact]
    public void TestDeconstruction()
    {
        var c = new Color<int>(1, 2, 3);
        var (r, g, b) = c;
        Assert.Equal(1, r);
        Assert.Equal(2, g);
        Assert.Equal(3, b);
    }

    [Fact]
    public void TestIndexing()
    {
        var c = new Color<int>(1, 2, 3);
        Assert.Equal(1, c[0]);
        Assert.Equal(2, c[1]);
        Assert.Equal(3, c[2]);
    }
    
    [Fact]
    public void TestMapping()
    {
        var c1 = new Color<double>(0.0, 0.5, 1.0);
        var c2 = c1.Map(u => (int)Math.Floor(u * 255.999));
        var want = new Color<int>(0, 127, 255);
        Assert.Equal(want, c2);
    }
}