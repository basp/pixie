namespace Linie.Tests;

public class Point2Tests
{
    [Fact]
    public void TestCreateZeros()
    {
        var p = new Point2<int>();
        Assert.IsType<Point2<int>>(p);
        Assert.Equal(0, p.X);
        Assert.Equal(0, p.Y);
    }
    
    [Fact]
    public void TestCreation()
    {
        var pi = Point2.Create(1, 2);
        var pf = Point2.Create(1f, 2);
        var pd = Point2.Create(1.0, 2);

        Assert.IsType<Point2<int>>(pi);
        Assert.Equal(1, pi.X);
        Assert.Equal(2, pi.Y);

        Assert.IsType<Point2<float>>(pf);
        Assert.Equal(1f, pf.X);
        Assert.Equal(2f, pf.Y);

        Assert.IsType<Point2<double>>(pd);
        Assert.Equal(1.0, pd.X);
        Assert.Equal(2.0, pd.Y);
    }

    [Fact]
    public void TestIndexing()
    {
        var p = Point2.Create(1.0, 2);
        Assert.Equal(1.0, p[0]);
        Assert.Equal(2.0, p[1]);
    }

    [Fact]
    public void TestEquality()
    {
        var a = Point2.Create(1.0, 2);
        var b = Point2.Create(1.0, 2);
        var c = Point2.Create(2.0, 3);
        
        Assert.Equal(a, b);
        Assert.Equal(b, a);

        Assert.NotEqual(a, c);
        Assert.NotEqual(b, c);
    }

    [Fact]
    public void TestAddition()
    {
        var a = Point2.Create(1.0, 2);
        var u = Vector2.Create(-1.0, 2);
        var b = Point2.Add(a, u);
        Assert.Equal(0, b.X);
        Assert.Equal(4, b.Y);
    }
    
    [Fact]
    public void TestSubtraction()
    {
        var a = Point2.Create(1.0, 2);
        var u = Vector2.Create(2.0, 3);
        var c = Point2.Subtract(a, u);

        Assert.Equal(-1, c.X);
        Assert.Equal(-1, c.Y);
    }
}