namespace Pixie.Tests;

public class Matrix4x4Tests
{
    private readonly Matrix4x4<int> m = new(
        00, 01, 02, 03,
        10, 11, 12, 13,
        20, 21, 22, 23,
        30, 31, 32, 33);

    [Fact]
    public void TestSubmatrix()
    {
        var actual = this.m.Submatrix(1, 1);
        var expected = Matrix3x3.Create(
            00, 02, 03,
            20, 22, 23,
            30, 32, 33);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestIndexing()
    {
        Assert.Equal(11, m[1, 1]);
        Assert.Equal(12, m[1, 2]);
        Assert.Equal(31, m[3, 1]);
        Assert.Equal(33, m[3, 3]);
    }

    [Fact]
    public void TestLinearIndex()
    {
        var expected = new[]
        {
            00, 01, 02, 03,
            10, 11, 12, 13,
            20, 21, 22, 23,
            30, 31, 32, 33,
        };

        for (var i = 0; i < this.m.Count; i++)
        {
            Assert.Equal(expected[i], this.m[i]);
        }
    }

    [Fact]
    public void TestCount()
    {
        Assert.Equal(16, this.m.Count);
    }

    [Fact]
    public void TestCreate()
    {
        var n = Matrix4x4.Create(0);
        Assert.All(n, x => Assert.Equal(0, x));
    }

    [Fact]
    public void TestGetRow()
    {
        var tests = new[]
        {
            (0, new Vector4<int>(00, 01, 02, 03)),
            (1, new Vector4<int>(10, 11, 12, 13)),
            (2, new Vector4<int>(20, 21, 22, 23)),
            (3, new Vector4<int>(30, 31, 32, 33)),
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
            (0, new Vector4<int>(00, 10, 20, 30)),
            (1, new Vector4<int>(01, 11, 21, 31)),
            (2, new Vector4<int>(02, 12, 22, 32)),
            (3, new Vector4<int>(03, 13, 23, 33)),
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
            .Range(0, 4)
            .Select(i => this.m.GetRow(i));

        Assert.Equal(want, this.m.EnumerateRows());
    }

    [Fact]
    public void TestColumnEnumeration()
    {
        var want = Enumerable
            .Range(0, 4)
            .Select(j => this.m.GetColumn(j));

        Assert.Equal(want, this.m.EnumerateColumns());
    }

    [Fact]
    public void TestTransposing()
    {
        var n = new Matrix4x4<int>(
            0, 9, 3, 0,
            9, 8, 0, 8,
            1, 8, 5, 3,
            0, 0, 5, 8);

        var want = new Matrix4x4<int>(
            0, 9, 1, 0,
            9, 8, 8, 0,
            3, 0, 5, 5,
            0, 8, 3, 8);

        Assert.Equal(want, n.Transpose());
    }

    [Fact]
    public void TestDimensions()
    {
        var want = new[] { 4, 4 };
        var n = new Matrix4x4<int>();
        Assert.Equal(want, n.Dimensions);
    }

    [Fact]
    public void TestDeterminant()
    {
        var a = new Matrix4x4<double>(
            -2, -8, 3, 5,
            -3, 1, 7, 3,
            1, 2, -9, 6,
            -6, 7, 7, -9);

        Assert.Equal(690, a.Cofactor(0, 0));
        Assert.Equal(447, a.Cofactor(0, 1));
        Assert.Equal(210, a.Cofactor(0, 2));
        Assert.Equal(51, a.Cofactor(0, 3));
        Assert.Equal(-4071, a.Determinant());
    }

    [Fact]
    public void TestCalculateInverse()
    {
        var a = new Matrix4x4<double>(
            -5, 2, 6, -8,
            1, -5, 1, 8,
            7, 7, -6, -7,
            1, -3, 7, 4);

        var b = a.Invert();
        var cmp = Matrix4x4.GetComparer(1e-5);
        var want = new Matrix4x4<double>(
            +0.21805, +0.45113, +0.24060, -0.04511,
            -0.80827, -1.45677, -0.44361, +0.52068,
            -0.07895, -0.22368, -0.05263, +0.19737,
            -0.52256, -0.81391, -0.30075, +0.30639);

        Assert.Equal(want, b, cmp);
    }

    [Fact]
    public void TestDegenerate()
    {
        var a = new Matrix4x4<double>(
            1, 2, 3, 4,
            5, 6, 7, 8,
            1, 2, 3, 4,
            5, 6, 7, 8);

        Assert.Throws<InvalidOperationException>(() => a.Invert());
    }

    [Fact]
    public void TestFillWithNaN()
    {
        var n = new Matrix4x4<double>(double.NaN);
        Assert.All(n, x => Assert.True(double.IsNaN(x)));
    }

    [Fact]
    public void TestTranslate()
    {
        var n = Matrix4x4.Translate(1, 2, 3);
        var a = Vector4.CreatePosition(0, 0, 0);
        var b = Matrix4x4.Multiply(n, a);
        Assert.Fail("todo");
    }

    [Fact]
    public void TestScale()
    {
        var n = Matrix4x4.Scale<double>(1, 2, 3);
        var v = Vector4.CreateDirection<double>(1, 1, 1);
        var ans = Matrix4x4.Multiply(n, v);
        var want = Vector4.CreateDirection(1.0, 2, 3);
        Assert.Equal(want, ans);
    }

    [Fact]
    public void TestRotatePointAroundXAxis()
    {
        var p = Vector4.CreatePosition(0.0, 1, 0);
        var tests = new[]
        {
            new
            {
                T = Matrix4x4.RotateX(Math.PI / 4),
                Want = Vector4.CreatePosition(
                    0,
                    Math.Sqrt(2) / 2,
                    Math.Sqrt(2) / 2)
            },
            new
            {
                T = Matrix4x4.RotateX(Math.PI / 2),
                Want = Vector4.CreatePosition(0.0, 0, 1),
            },
            new
            {
                T = Matrix4x4.RotateX(-Math.PI / 2),
                Want = Vector4.CreatePosition(0.0, 0, -1),
            },
            new
            {
                T =
                    Matrix4x4.Multiply(
                        Matrix4x4.RotateX(Math.PI / 2),
                        Matrix4x4.RotateX(-Math.PI / 2)),
                Want = p,
            }
        };

        foreach (var t in tests)
        {
            var ans = Matrix4x4.Multiply(t.T, p);
            var cmp = Vector4.GetComparer(1e-6);
            Assert.Equal(t.Want, ans, cmp);
        }
    }

    [Fact]
    public void TestRotatePointAroundYAxis()
    {
        var p = Vector4.CreatePosition(0.0, 0, 1);
        var tests = new[]
        {
            new
            {
                T = Matrix4x4.RotateY(Math.PI / 4),
                Want = Vector4.CreatePosition(
                    Math.Sqrt(2) / 2,
                    0,
                    Math.Sqrt(2) / 2),
            },
            new
            {
                T = Matrix4x4.RotateY(Math.PI / 2),
                Want = Vector4.CreatePosition(1.0, 0, 0),
            },
            new
            {
                T = Matrix4x4.RotateY(-Math.PI / 2),
                Want = Vector4.CreatePosition(-1.0, 0, 0),
            },
            new
            {
                T =
                    Matrix4x4.Multiply(
                        Matrix4x4.RotateY(Math.PI / 2),
                        Matrix4x4.RotateY(-Math.PI / 2)),
                Want = p,
            }
        };

        foreach (var t in tests)
        {
            var ans = Matrix4x4.Multiply(t.T, p);
            var cmp = Vector4.GetComparer(1e-6);
            Assert.Equal(t.Want, ans, cmp);
        }
    }

    [Fact]
    public void TestRotatePointAroundZAxis()
    {
        var p = Vector4.CreatePosition(0.0, 1, 0);
        var tests = new[]
        {
            new
            {
                T = Matrix4x4.RotateZ(Math.PI / 4),
                Want = Vector4.CreatePosition(
                    -Math.Sqrt(2) / 2,
                    Math.Sqrt(2) / 2,
                    0),
            },
            new
            {
                T = Matrix4x4.RotateZ(Math.PI / 2),
                Want = Vector4.CreatePosition(-1.0, 0, 0),
            },
            new
            {
                T = Matrix4x4.RotateZ(-Math.PI / 2),
                Want = Vector4.CreatePosition(1.0, 0, 0),
            },
            new
            {
                T =
                    Matrix4x4.Multiply(
                        Matrix4x4.RotateZ(-Math.PI / 2),
                        Matrix4x4.RotateZ(Math.PI / 2)),
                Want = p,
            },
        };

        foreach (var t in tests)
        {
            var ans = Matrix4x4.Multiply(t.T, p);
            var cmp = Vector4.GetComparer(1e-6);
            Assert.Equal(t.Want, ans, cmp);
        }
    }

    [Fact]
    public void TestInverseScale()
    {
        var (sx, sy, sz) = (1.5, 2.0, 3.0);
        var o = Matrix4x4.Scale(sx, sy, sz);
        var p = Matrix4x4.InverseScale(sx, sy, sz);
        var q = Matrix4x4.Multiply(o, p);
        var cmp = Matrix4x4.GetComparer(1e-6);
        Assert.Equal(Matrix4x4<double>.Identity, q, cmp);
    }

    [Fact]
    public void TestInverseRotations()
    {
        const double r = Math.PI / 4;
        var tests = new[]
        {
            new
            {
                M = Matrix4x4.RotateX(r),
                Inv = Matrix4x4.InverseRotateX(r),
            },
            new
            {
                M = Matrix4x4.RotateY(r),
                Inv = Matrix4x4.InverseRotateY(r),
            },
            new
            {
                M = Matrix4x4.RotateZ(r),
                Inv = Matrix4x4.InverseRotateZ(r),
            }
        };

        var id = Matrix4x4<double>.Identity;
        var cmp = Matrix4x4.GetComparer(1e-6);
        foreach (var @case in tests)
        {
            var q = Matrix4x4.Multiply(@case.M, @case.Inv);
            Assert.Equal(id, q, cmp);
        }
    }

    [Fact]
    public void TestMultiplication()
    {
        var a = Matrix4x4.Create(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 8, 7, 6,
            5, 4, 3, 2);

        var b = Matrix4x4.Create(
            -2, 1, 2, 3,
            3, 2, 1, -1,
            4, 3, 6, 5,
            1, 2, 7, 8);

        var expected = Matrix4x4.Create(
            20, 22, 50, 48,
            44, 54, 114, 108,
            40, 58, 110, 102,
            16, 26, 46, 42);

        var actual = Matrix4x4.Multiply(a, b);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TestMapping()
    {
        var p = Matrix4x4<int>.Identity;
        var o = p.Map(x => (float)x);
        Assert.Fail("todo");
    }

    [Fact]
    public void TestHashing()
    {
        var o = Matrix4x4.Create(2);
        var n = Matrix4x4.Create(3);

        var d = new Dictionary<Matrix4x4<int>, string>
        {
            [o] = "m",
            [n] = "n",
        };

        Assert.Equal("m", d[o]);
        Assert.Equal("n", d[n]);
    }

    [Fact]
    public void TestChain()
    {
        var a = Matrix4x4.RotateX(Math.PI / 2);
        var b = Matrix4x4.Scale(5.0, 5, 5);
        var c = Matrix4x4.Translate(10.0, 5, 7);
        var t1 = Matrix4x4
            .Multiply(Matrix4x4
                .Multiply(c, b), 
                a);
        // var t1 = c * b * a;
        var t2 = Matrix4x4<double>
            .Identity
            .RotateX(Math.PI / 2)
            .Scale(5, 5, 5)
            .Translate(10, 5, 7);

        var p0 = Vector4.CreatePosition(1.0, 0, 1);
        // var p1 = t1 * p0;
        var p1 = Matrix4x4.Multiply(t1, p0);
        // var p2 = t2 * p0;
        var p2 = Matrix4x4.Multiply(t2, p0);

        Assert.Equal(Vector4.CreatePosition(15.0, 0, 7), p1);
        Assert.Equal(p1, p2);
    }
}