namespace Pixie.Tests;

public class Matrix2x2Tests
{
    private readonly Matrix2x2<int> m = new(
        1, 2,
        3, 4);

    [Fact]
    public void TestConstructor()
    {
        var n = new Matrix2x2<int>(00, 01, 10, 11);
        Assert.Equal(0, n[0, 0]);
        Assert.Equal(1, n[0, 1]);
        Assert.Equal(10, n[1, 0]);
        Assert.Equal(11, n[1, 1]);
    }
    
    [Fact]
    public void TestGetRow()
    {
        var tests = new[]
        {
            (0, new Vector2<int>(1, 2)),
            (1, new Vector2<int>(3, 4)),
        };

        foreach (var (i, want) in tests)
        {
            Assert.Equal(want, this.m.GetRow(i));
        }
    }

    [Fact]
    public void TestGetColumn()
    {
        var tests = new[]
        {
            (0, new Vector2<int>(1, 3)),
            (1, new Vector2<int>(2, 4)),
        };

        foreach (var (j, want) in tests)
        {
            Assert.Equal(want, this.m.GetColumn(j));
        }
    }

    [Fact]
    public void TestRowEnumeration()
    {
        var want = Enumerable
            .Range(0, 2)
            .Select(i => this.m.GetRow(i));

        Assert.Equal(want, this.m.EnumerateRows());
    }

    [Fact]
    public void TestColumnEnumeration()
    {
        var want = Enumerable
            .Range(0, 2)
            .Select(j => this.m.GetColumn(j));

        Assert.Equal(want, this.m.EnumerateColumns());
    }

    [Fact]
    public void TestTransposing()
    {
        var want = new Matrix2x2<int>(
            1, 3,
            2, 4);

        var mt = this.m.Transpose();
        Assert.Equal(want, mt);
    }

    [Fact]
    public void TestDeterminant()
    {
        var a = new Matrix2x2<int>(
            1, 5,
            -3, 2);

        Assert.Equal(17, a.Determinant());
    }

    [Fact]
    public void TestEquality()
    {
        var m = new Matrix2x2<int>(1, 2, 3, 4)
            .Map(v => (double)v);
        var n = new Matrix2x2<double>(1, 2, 3, 4);
        Assert.True(m.Equals(n));
    }
}
