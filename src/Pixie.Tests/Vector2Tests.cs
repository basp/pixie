namespace Linie.Tests;

public class Vector2Tests
{
    [Fact]
    public void TestConstructor()
    {
        var u = new Vector2<int>(2);
        Assert.IsType<Vector2<int>>(u);
        Assert.Equal(2, u.X);
        Assert.Equal(2, u.Y);
    }

    [Fact]
    public void TestCreation()
    {
        var ud = Vector2.Create(1.0, 2);
        var uf = Vector2.Create(1.0f, 2);
        var ui = Vector2.Create(1, 2);

        Assert.IsType<Vector2<double>>(ud);
        Assert.Equal(1.0, ud.X);
        Assert.Equal(2.0, ud.Y);

        Assert.IsType<Vector2<float>>(uf);
        Assert.Equal(1.0f, uf.X);
        Assert.Equal(2.0f, uf.Y);

        Assert.IsType<Vector2<int>>(ui);
        Assert.Equal(1, ui.X);
        Assert.Equal(2, ui.Y);
    }

    [Fact]
    public void TestIndexing()
    {
        var u = Vector2.Create(1, 2);

        Assert.Equal(1, u[0]);
        Assert.Equal(2, u[1]);

        Assert.Equal(u[0], u.X);
        Assert.Equal(u[1], u.Y);
    }

    [Fact]
    public void TestEquality()
    {
        var u = Vector2.Create(1.0, 2);
        var v = Vector2.Create(1.0, 2);
        var w = Vector2.Create(2.0, 2);

        Assert.Equal(u, v);
        Assert.Equal(v, u);

        Assert.NotEqual(u, w);
        Assert.NotEqual(v, w);
    }

    [Fact]
    public void TestAddition()
    {
        var u = Vector2.Create(1, 2);
        var v = Vector2.Create(2, 3);
        var w = Vector2.Add(u, v);
        Assert.Equal(Vector2.Create(3, 5), w);
    }

    [Fact]
    public void TestSubtraction()
    {
        var u = Vector2.Create(1, 2);
        var v = Vector2.Create(2, 3);
        var w = Vector2.Subtract(u, v);
        Assert.Equal(Vector2.Create(-1, -1), w);
    }

    [Fact]
    public void TestMultiplication()
    {
        var u = new Vector2<double>(1, 2);
        Assert.Equal(
            Vector2.Create(2.0, 4.0),
            Vector2.Multiply(2, u));
        Assert.Equal(
            Vector2.Create(2.0, 4.0),
            Vector2.Multiply(u, 2));
        Assert.Equal(
            Vector2.Create(0.5, 1.0),
            Vector2.Multiply(0.5, u));
        Assert.Equal(
            Vector2.Create(0.5, 1.0),
            Vector2.Multiply(u, 0.5));
    }

    [Fact]
    public void TestDivision()
    {
        var u = new Vector2<double>(1, 3.0);
        var cmp = Vector2.GetComparer(1e-6);
        Assert.Equal(
            Vector2.Create(0.5, 1.5),
            Vector2.Divide(u, 2));
    }

    [Fact]
    public void TestAbs()
    {
        var u = Vector2.Create(-1, 2);
        var v = Vector2.Abs(u);
        Assert.Equal(1, v.X);
        Assert.Equal(2, v.Y);
    }
}