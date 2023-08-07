namespace Pixie.Tests;

public class Vector4Tests
{
    [Fact]
    public void TestConstructors()
    {
        var u = new Vector4<int>();

        Assert.IsType<Vector4<int>>(u);
        Assert.Equal(0, u.X);
        Assert.Equal(0, u.Y);
        Assert.Equal(0, u.Z);
        Assert.Equal(0, u.W);
    }
    
    [Fact]
    public void TestCreation()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);

        Assert.Equal(1, u.X);
        Assert.Equal(2, u.Y);
        Assert.Equal(3, u.Z);
        Assert.Equal(4, u.W);
    }

    [Fact]
    public void TestIndexing()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        
        Assert.Equal(1, u[0]);
        Assert.Equal(2, u[1]);
        Assert.Equal(3, u[2]);
        Assert.Equal(4, u[3]);
    }

    [Fact]
    public void TestEquality()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        var v = Vector4.Create(1.0, 2, 3, 4);
        var w = Vector4.Create(2.0, 3, 4, 5);

        Assert.Equal(u, v);
        Assert.Equal(v, u);

        Assert.NotEqual(u, w);
        Assert.NotEqual(v, w);

        object o1 = new();
        Assert.NotEqual(w, o1);

        object o2 = Vector4.Create(2, 3, 4, 5);
        Assert.NotEqual(w, o2);
        
        object o3 = Vector4.Create(2.0, 3, 4, 5);
        Assert.Equal(w, o3);
    }

    [Fact]
    public void TestGetHashCode()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        var v = Vector4.Create(1.0, 2, 3, 4);
        var w = Vector4.Create(2.0, 3, 4, 5);

        Assert.Equal(u.GetHashCode(), v.GetHashCode());
        
        Assert.NotEqual(u.GetHashCode(), w.GetHashCode());
        Assert.NotEqual(v.GetHashCode(), w.GetHashCode());
    }

    [Fact]
    public void TestToString()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        var s = u.ToString();
        Assert.Equal("(1 2 3 4)", s);
    }

    [Fact]
    public void TestAddition()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        var v = Vector4.Create(0.0, 1, 2, 3);
        
        var w = Vector4.Add(u, v);
        
        Assert.Equal(1, w.X);
        Assert.Equal(3, w.Y);
        Assert.Equal(5, w.Z);
        Assert.Equal(7, w.W);
    }

    [Fact]
    public void TestSubtraction()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        var v = Vector4.Create(0.0, 1, 2, 3);

        var w = Vector4.Subtract(u, v);

        Assert.Equal(1, w.X);
        Assert.Equal(1, w.Y);
        Assert.Equal(1, w.Z);
        Assert.Equal(1, w.W);
    }

    [Fact]
    public void TestMultiplication()
    {
        var u = Vector4.Create(1.0, 2, 3, 4);
        
        var v = Vector4.Multiply(u, 2);
        var w = Vector4.Multiply(2, u);

        Assert.Equal(v, w);
        
        Assert.Equal(2, w.X);
        Assert.Equal(4, w.Y);
    }

    [Fact]
    public void TestDivide()
    {
        var u = Vector4.Create(1.0, 2.0, 3.0, 4.0);
        var v = Vector4.Divide(u, 2.0);
        Assert.Equal(0.5, v.X);
        Assert.Equal(1.0, v.Y);
        Assert.Equal(1.5, v.Z);
        Assert.Equal(2.0, v.W);
    }
}