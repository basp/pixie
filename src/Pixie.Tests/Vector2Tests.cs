namespace Pixie.Tests;

public class Vector2Tests
{
    [Fact]
    public void TestEquality()
    {
        var u = Vector2.Create(1.0, 2);
        var v = Vector2.Create(1.0, 2);
        var w = Vector2.Create(2.0, 3);
        var o = new object();
        
        Assert.Equal(u, v);
        Assert.NotEqual(u, w);
        Assert.NotEqual(u, o);
    }
    
    [Fact]
    public void TestCreate()
    {
        var u = Vector2.Create(1.0, 2.5);
        Assert.Equal(1.0, u.X);
        Assert.Equal(2.5, u.Y);
    }
    
    [Fact]
    public void TestIndexing()
    {
        var u = new Vector2<double>(2, 3);
        Assert.Equal(2, u[0]);
        Assert.Equal(3, u[1]);
        Assert.Equal(3, u[1000]);
    }

    [Fact]
    public void TestNegation()
    {
        var u = new Vector2<double>(1, 2);
        var w = Vector2.Negate(u);
        Assert.Equal(-1, w.X);
        Assert.Equal(-2, w.Y);
    }

    [Fact]
    public void TestAddition()
    {
        var u = new Vector2<float>(2, 3);
        var v = new Vector2<float>(1, 2);
        var w = Vector2.Add(u, v);
        Assert.Equal(3, w.X);
        Assert.Equal(5, w.Y);
    }

    [Fact]
    public void TestSubtraction()
    {
        var u = new Vector2<float>(2, 1);
        var v = new Vector2<float>(1, 1);
        var w = Vector2.Subtract(u, v);
        Assert.Equal(1, w.X);
        Assert.Equal(0, w.Y);
    }

    [Fact]
    public void TestMultiplication()
    {
        var u = new Vector2<double>(2, 3);
        var v = Vector2.Multiply(u, 0.5);
        Assert.Equal(1, v.X);
        Assert.Equal(1.5, v.Y);
    }

    [Fact]
    public void TestDeconstruction()
    {
        var (x, y) = new Vector2<double>(1, 2);
        Assert.Equal(1, x);
        Assert.Equal(2, y);
    }

    [Fact]
    public void TestMapping()
    {
        var u = new Vector2<double>(1.5, 2.5);
        var v = u.Map(x => (float)Math.Floor(x));
        Assert.IsType<Vector2<float>>(v);
    }
}
