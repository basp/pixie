using System.Drawing;

namespace Pixie.Tests;

public class Point3Tests
{
    [Fact]
    public void TestConstructor()
    {
        var a = new Point3<int>();
        var b = new Point3<double>(1.5);

        Assert.IsType<Point3<int>>(a);
        Assert.Equal(0, a.X);
        Assert.Equal(0, a.Y);
        Assert.Equal(0, a.Z);

        Assert.IsType<Point3<double>>(b);
        Assert.Equal(1.5, b.X);
        Assert.Equal(1.5, b.Y);
        Assert.Equal(1.5, b.Z);
    }

    [Fact]
    public void TestCreation()
    {
        var p = Point3.Create(1.0, 2, 3);

        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
        Assert.Equal(3, p.Z);
    }

    [Fact]
    public void TestFromCylindrical()
    {
        var tests = new[]
        {
            new
            {
                r = 1,
                theta = (1.0 / 4) * Math.PI,
                y = 0,
                want = Point3.Create(Utils.Sqrt2 / 2, 0, Utils.Sqrt2 / 2),
            },
            new
            {
                r = 2,
                theta = (2.0 / 4) * Math.PI,
                y = 1,
                want = Point3.Create(2.0, 1, 0),
            },
        };

        foreach (var @case in tests)
        {
            var ans = Point3.FromCylindrical(
                @case.r,
                @case.theta,
                @case.y);
            var cmp = Point3.GetComparer(1e-6);
            Assert.Equal(@case.want, ans, cmp);
        }
    }

    [Fact]
    public void TestFromSpherical()
    {
        var tests = new[]
        {
            new
            {
                r = 1.0,
                theta = 0.0,
                phi = 0.0,
                want = Point3.Create(0.0, 1, 0),
            },
            new
            {
                r = 1.0,
                theta = Utils.Radians(45.0),
                phi = 0.0,
                want = Point3.Create(0.0, Utils.Sqrt2/2, Utils.Sqrt2/2),
            },
            new
            {
                r = 1.0,
                theta = Utils.Radians(90.0),
                phi = Utils.Radians(45.0),
                want = Point3.Create(Utils.Sqrt2/2, 0.0, Utils.Sqrt2/2),
            },
            new
            {
                r = 2.0,
                theta = Utils.Radians(90.0),
                phi = Utils.Radians(135.0),
                want = Point3.Create(Utils.Sqrt2, 0.0, -Utils.Sqrt2)
            },
        };

        foreach (var @case in tests)
        {
            var ans = Point3.FromSpherical(
                @case.r, 
                @case.theta, 
                @case.phi);
            var cmp = Point3.GetComparer(1e-6);
            Assert.Equal(@case.want, ans, cmp);
        }
    }

    [Fact]
    public void TestIndexing()
    {
        var p = Point3.Create(1, 2, 3);

        Assert.Equal(1, p[0]);
        Assert.Equal(2, p[1]);
        Assert.Equal(3, p[2]);
    }

    [Fact]
    public void TestEquality()
    {
        var a = Point3.Create(1.5, 2.5, 3.0);
        var b = Point3.Create(1.5, 2.5, 3.0);
        var c = Point3.Create(0.0, 2.5, 3.0);

        Assert.Equal(a, b);
        Assert.Equal(b, a);

        Assert.NotEqual(a, c);
        Assert.NotEqual(b, c);
    }

    [Fact]
    public void TestToString()
    {
        var p = Point3.Create(1, 2, 3);
        var s = p.ToString();
        Assert.Equal("(1 2 3)", s);
    }
}