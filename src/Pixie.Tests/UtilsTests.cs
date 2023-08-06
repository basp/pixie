namespace Pixie.Tests;

public class UtilsTests
{
    [Fact]
    public void TestRadians()
    {
        const double deg = 45.0;
        var rad = Utils.Radians(deg);
        Assert.Equal(Math.PI / 4, rad);
    }

    [Fact]
    public void TestDegrees()
    {
        const double rad = Math.PI / 4;
        Assert.Equal(45.0, Utils.Degrees(rad));
    }

    [Fact]
    public void TestSafeSqrt()
    {
        var tests = new[]
        {
            (-0.0001, 0),
            (-0.00001, 0),
            (0, 0),
            (0.1, Math.Sqrt(0.1)),
            (9, Math.Sqrt(9.0)),
            (16, Math.Sqrt(16)),
#if RELEASE
            (-5, Math.Sqrt((0)))
#endif
        };

        foreach (var (x, expected) in tests)
        {
            var actual = Utils.SafeSqrt(x);
            Assert.Equal(expected, actual);
        }
    }
}
