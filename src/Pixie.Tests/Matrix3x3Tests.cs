namespace Pixie.Tests;

public class Matrix3x3Tests
{
    private readonly Matrix3x3<int> m = new(
        1, 2, 3,
        4, 5, 6,
        7, 8, 9);

    [Fact]
    public void TestEquality()
    {
        var n = new Matrix3x3<int>(
            1, 2, 3,
            4, 5, 6,
            7, 8, 9);

        var o = new object();

        Assert.Equal(this.m, n);
        Assert.NotEqual(this.m, o);
    }

    [Fact]
    public void TestSubmatrix()
    {
        var actual = this.m.Submatrix(1, 1);
        var expected = Matrix2x2.Create(1, 3, 7, 9);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestGetRow()
    {
        var tests = new[]
        {
            (0, new Vector3<int>(1, 2, 3)),
            (1, new Vector3<int>(4, 5, 6)),
            (2, new Vector3<int>(7, 8, 9)),
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
            (0, new Vector3<int>(1, 4, 7)),
            (1, new Vector3<int>(2, 5, 8)),
            (2, new Vector3<int>(3, 6, 9)),
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
            .Range(0, 3)
            .Select(i => this.m.GetRow(i));

        Assert.Equal(want, this.m.EnumerateRows());
    }

    [Fact]
    public void TestColumnEnumeration()
    {
        var want = Enumerable
            .Range(0, 3)
            .Select(j => this.m.GetColumn(j));

        Assert.Equal(want, this.m.EnumerateColumns());
    }

    [Fact]
    public void TestTransposing()
    {
        var want = new Matrix3x3<int>(
            1, 4, 7,
            2, 5, 8,
            3, 6, 9);

        var mt = this.m.Transpose();
        Assert.Equal(want, mt);
    }

    [Fact]
    public void TestDeterminant()
    {
        var n = new Matrix3x3<int>(
            +1, +2, +6,
            -5, +8, -4,
            +2, +6, +4);

        Assert.Equal(56, n.Cofactor(0, 0));
        Assert.Equal(12, n.Cofactor(0, 1));
        Assert.Equal(-46, n.Cofactor(0, 2));
        Assert.Equal(-196, n.Determinant());
    }
}