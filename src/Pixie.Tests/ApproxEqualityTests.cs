namespace Pixie.Tests;

public class ApproxEqualityTests
{
    [Fact]
    public void TestApproxFloatEquality()
    {
        var tests = new[]
        {
            (0.1, 0.11, true),
            (0.1, 0.09, false),
        };

        foreach (var (v, tol, want) in tests)
        {
            Assert.Equal(want, v.IsApprox(0, tol));
        }
    }

    [Fact]
    public void TestApproxDoubleEquality()
    {
        var tests = new[]
        {
            (0.125, 0.5, true),
            (0.125, 0.05, false),
            (0.005, 0.05, true),
        };

        foreach (var (v, tol, want) in tests)
        {
            Assert.Equal(want, v.IsApprox(0, tol));
        }
    }

    [Fact]
    public void TestVector2EqualityComparer()
    {
        var tests = new[]
        {
            (new Vector2<float>(0.50f, 0.60f), new Vector2<float>(0.59f, 0.51f), 0.1f, true),
            (new Vector2<float>(0.50f, 0.60f), new Vector2<float>(0.50f, 0.605f), 0.05f, true),
        };

        foreach (var (u, v, tol, want) in tests)
        {
            var cmp = Vector2.GetEqualityComparer(tol);
            Assert.Equal(want, cmp.Equals(u, v));
        }
    }
}
