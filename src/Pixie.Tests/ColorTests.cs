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

    [Fact]
    public void TestClamping()
    {
        var c = new Color<int>(260, -3, 128);
        var min = new Color<int>(0, 0, 0);
        var max = new Color<int>(255, 255, 255);
        var want = new Color<int>(255, 0, 128);
        var ans = c.Clamp(min, max);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void TestEquality()
    {
        var tests = new[]
        {
            (new Color<int>(1, 2, 3), new Color<int>(1, 2, 3), true),
            (new Color<int>(0, 0, 1), new Color<int>(0, 0, 0), false),
            (new Color<int>(0, 1, 0), new Color<int>(0, 0, 0), false),
            (new Color<int>(1, 0, 0), new Color<int>(0, 0, 0), false),
        };

        foreach (var (c1, c2, want) in tests)
        {
            Assert.Equal(c1 == c2, want);
            Assert.Equal(c1 != c2, !want);
        }
    }

    [Fact]
    public void TestObjectEquality()
    {
        object c1 = new Color<int>(1, 2, 3);
        object c2 = new Color<int>(1, 2, 3);
        object s = "foo";
        Assert.True(c1.Equals(c2));
        Assert.True(c2.Equals(c1));
        Assert.False(c1.Equals(s));
        Assert.False(c2.Equals(s));
    }
}