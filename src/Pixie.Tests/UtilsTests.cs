namespace Pixie.Tests;

public class UtilsTests
{
    [Fact]
    public void TestSafeSqrt()
    {
        const double atol = 0.1;
        var valid = new[]
        {
            (-0.1, 0),
            (-0.09, 0),
            (-0.01, 0),
            (-0, 0),
            (0, 0),
            (+0, 0),
            (0.1, Math.Sqrt(0.1)),
            (1, Math.Sqrt(1)),
            (2, Math.Sqrt(2)),
            (3.5, Math.Sqrt(3.5)),
        };

        var invalid = new[]
        {
            -0.12, -0.11, -0.1001,
        };

        foreach (var (x, want) in valid)
        {
            Assert.Equal(want, Utils.SafeSqrt(x, atol));
        }

        foreach (var x in invalid)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => Utils.SafeSqrt(x, atol));
        }
    }
}
