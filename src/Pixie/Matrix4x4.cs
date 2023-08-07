namespace Pixie;

public class Matrix4x4<T> :
    IEquatable<Matrix4x4<T>>,
    IFormattable,
    IEnumerable<T>
    where T : INumber<T>
{
    public static Matrix4x4<T> Identity =>
        new(
            T.One, T.Zero, T.Zero, T.Zero,
            T.Zero, T.One, T.Zero, T.Zero,
            T.Zero, T.Zero, T.One, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);

    private readonly T[] data;

    public Matrix4x4()
        : this(T.Zero)
    {
    }

    public Matrix4x4(T v)
    {
        this.data = new[]
        {
            v, v, v, v,
            v, v, v, v,
            v, v, v, v,
            v, v, v, v,
        };
    }

    public Matrix4x4(
        T m00, T m01, T m02, T m03,
        T m10, T m11, T m12, T m13,
        T m20, T m21, T m22, T m23,
        T m30, T m31, T m32, T m33)
    {
        this.data = new[]
        {
            m00, m01, m02, m03,
            m10, m11, m12, m13,
            m20, m21, m22, m23,
            m30, m31, m32, m33,
        };
    }

    public Matrix4x4(T[] data)
    {
        this.data = data;
    }

    public T this[int row, int column]
    {
        get => this.data[(row * 4) + column];
        set => this.data[(row * 4) + column] = value;
    }

    public T this[int index] => this.data[index];

    public int Count => 16;

    public int[] Dimensions => new[] { 4, 4 };

    public Vector4<T> GetRow(int i) =>
        new(
            this[i, 0],
            this[i, 1],
            this[i, 2],
            this[i, 3]);

    public Vector4<T> GetColumn(int j) =>
        new(
            this[0, j],
            this[1, j],
            this[2, j],
            this[3, j]);

    public IEnumerable<Vector4<T>> EnumerateRows() =>
        Enumerable.Range(0, 4)
            .Select(this.GetRow);

    public IEnumerable<Vector4<T>> EnumerateColumns() =>
        Enumerable.Range(0, 4)
            .Select(this.GetColumn);

    public Matrix4x4<T> Multiply(Matrix4x4<T> other) =>
        Matrix4x4.Multiply(this, other);

    public Matrix3x3<T> Submatrix(int row, int column)
    {
        var rows = Enumerable.Range(0, 4).Where(i => i != row).ToArray();
        var cols = Enumerable.Range(0, 4).Where(j => j != column).ToArray();
        var m = new Matrix3x3<T>();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                m[i, j] = this[rows[i], cols[j]];
            }
        }

        return m;
    }

    public T Cofactor(int row, int column) =>
        (row + column) % 2 == 0
            ? this.Minor(row, column)
            : -this.Minor(row, column);

    public T Determinant() =>
        (this[0, 0] * this.Cofactor(0, 0)) +
        (this[0, 1] * this.Cofactor(0, 1)) +
        (this[0, 2] * this.Cofactor(0, 2)) +
        (this[0, 3] * this.Cofactor(0, 3));

    public Matrix4x4<T> Invert()
    {
        if (!this.TryInvert(out var result))
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public Matrix4x4<T> Transpose()
    {
        var m = new Matrix4x4<T>();
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                m[j, i] = this[i, j];
            }
        }

        return m;
    }

    public bool Equals(Matrix4x4<T> other) =>
        other != null && this.data.SequenceEqual(other.data);

    public string ToString(string format, IFormatProvider formatProvider)
    {
        var rows = this
            .EnumerateRows()
            .Select(r => r.ToString(format, formatProvider))
            .ToArray();

        return string.Format(
            "[{0}]",
            string.Join(' ', rows));
    }

    public IEnumerator<T> GetEnumerator() =>
        this.data.Cast<T>().GetEnumerator();

    public override bool Equals(object obj)
    {
        if (object.ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is Matrix4x4<T> other && this.Equals(other);
    }

    public override int GetHashCode() =>
        HashCode.Combine(
            this.GetColumn(0),
            this.GetColumn(1),
            this.GetColumn(2),
            this.GetColumn(3));

    public override string ToString() => this.ToString(null, null);

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private T Minor(int row, int column) =>
        this.Submatrix(row, column).Determinant();
}

public static class Matrix4x4
{
    public static Matrix4x4<T> Create<T>(T v) where T : INumber<T> => new(v);

    public static Matrix4x4<T> Create<T>(
        T m00, T m01, T m02, T m03,
        T m10, T m11, T m12, T m13,
        T m20, T m21, T m22, T m23,
        T m30, T m31, T m32, T m33)
        where T : INumber<T> =>
        new(
            m00, m01, m02, m03,
            m10, m11, m12, m13,
            m20, m21, m22, m23,
            m30, m31, m32, m33);

    public static IEqualityComparer<Matrix4x4<T>> GetComparer<T>(T atol)
        where T : INumber<T> =>
        new Matrix4x4EqualityComparer<T>(atol);

    // ReSharper disable once ReturnTypeCanBeEnumerable.Global
    public static Matrix4x4<U> Map<T, U>(this Matrix4x4<T> m, Func<T, U> f)
        where T : INumber<T>
        where U : INumber<U>
    {
        var data = m.Select(f).ToArray();
        return new Matrix4x4<U>(data);
    }

    public static Matrix4x4<T> Multiply<T>(
        in Matrix4x4<T> a,
        in Matrix4x4<T> b)
        where T : INumber<T>
    {
        var c = new Matrix4x4<T>();
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                c[i, j] =
                    (a[i, 0] * b[0, j]) +
                    (a[i, 1] * b[1, j]) +
                    (a[i, 2] * b[2, j]) +
                    (a[i, 3] * b[3, j]);
            }
        }

        return c;
    }

    public static Vector4<T> Multiply<T>(in Matrix4x4<T> m, in Vector4<T> u)
        where T : INumber<T>
    {
        var x = m.GetRow(0).Dot(u);
        var y = m.GetRow(1).Dot(u);
        var z = m.GetRow(2).Dot(u);
        var w = m.GetRow(3).Dot(u);
        return Vector4.Create(x, y, z, w);
    }

    public static Matrix4x4<T> Translate<T>(
        this Matrix4x4<T> self,
        T x,
        T y,
        T z)
        where T : INumber<T> =>
        Matrix4x4.Translate(x, y, z).Multiply(self);

    public static Matrix4x4<T> Scale<T>(
        this Matrix4x4<T> self,
        T x,
        T y,
        T z)
        where T : IFloatingPointIeee754<T> =>
        Matrix4x4.Scale(x, y, z).Multiply(self);

    public static Matrix4x4<T> RotateX<T>(this Matrix4x4<T> self, T angle)
        where T : IFloatingPointIeee754<T> =>
        Matrix4x4.RotateX(angle).Multiply(self);

    public static Matrix4x4<T> Translate<T>(T x, T y, T z)
        where T : INumber<T> =>
        new(
            T.One, T.Zero, T.Zero, x,
            T.Zero, T.One, T.Zero, y,
            T.Zero, T.Zero, T.One, z,
            T.Zero, T.Zero, T.Zero, T.One);

    public static Matrix4x4<T> InverseTranslate<T>(T x, T y, T z)
        where T : INumber<T> => Translate(-x, -y, -z);

    public static Matrix4x4<T> Scale<T>(T x, T y, T z)
        where T : IFloatingPoint<T> =>
        new(
            x, T.Zero, T.Zero, T.Zero,
            T.Zero, y, T.Zero, T.Zero,
            T.Zero, T.Zero, z, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);

    public static Matrix4x4<T> InverseScale<T>(T x, T y, T z)
        where T : IFloatingPoint<T> =>
        new(
            T.One / x, T.Zero, T.Zero, T.Zero,
            T.Zero, T.One / y, T.Zero, T.Zero,
            T.Zero, T.Zero, T.One / z, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);

    public static Matrix4x4<T> RotateX<T>(T angle)
        where T : IFloatingPointIeee754<T>
    {
        var sinT = T.Sin(angle);
        var cosT = T.Cos(angle);
        return new Matrix4x4<T>(
            T.One, T.Zero, T.Zero, T.Zero,
            T.Zero, cosT, -sinT, T.Zero,
            T.Zero, sinT, cosT, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> RotateY<T>(T angle)
        where T : IFloatingPointIeee754<T>
    {
        var sinT = T.Sin(angle);
        var cosT = T.Cos(angle);
        return new Matrix4x4<T>(
            cosT, T.Zero, sinT, T.Zero,
            T.Zero, T.One, T.Zero, T.Zero,
            -sinT, T.Zero, cosT, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> RotateZ<T>(T angle)
        where T : IFloatingPointIeee754<T>
    {
        var sinT = T.Sin(angle);
        var cosT = T.Cos(angle);
        return new Matrix4x4<T>(
            cosT, -sinT, T.Zero, T.Zero,
            sinT, cosT, T.Zero, T.Zero,
            T.Zero, T.Zero, T.One, T.Zero,
            T.Zero, T.Zero, T.Zero, T.One);
    }

    public static Matrix4x4<T> InverseRotateX<T>(T angle)
        where T : IFloatingPointIeee754<T> =>
        Matrix4x4.RotateX(angle).Transpose();

    public static Matrix4x4<T> InverseRotateY<T>(T angle)
        where T : IFloatingPointIeee754<T> =>
        Matrix4x4.RotateY(angle).Transpose();

    public static Matrix4x4<T> InverseRotateZ<T>(T angle)
        where T : IFloatingPointIeee754<T> =>
        Matrix4x4.RotateZ(angle).Transpose();

    public static bool TryInvert<T>(
        this Matrix4x4<T> self,
        out Matrix4x4<T> result)
        where T : INumber<T>
    {
        result = null;

        var d = self.Determinant();
        if (d <= T.Zero)
        {
            return false;
        }

        result = new Matrix4x4<T>();
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                var c = self.Cofactor(i, j);
                result[j, i] = c / d;
            }
        }

        return true;
    }
}