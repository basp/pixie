namespace Pixie;

public class Matrix3x3<T> :
    IEquatable<Matrix3x3<T>>,
    IFormattable
    where T : INumber<T>
{
    public static Matrix3x3<T> Identity => new(
        T.One, T.Zero, T.Zero,
        T.Zero, T.One, T.Zero,
        T.Zero, T.Zero, T.One);

    private readonly T[] data;

    public Matrix3x3()
        : this(T.Zero)
    {
    }

    public Matrix3x3(T v)
    {
        this.data = new[]
        {
            v, v, v,
            v, v, v,
            v, v, v,
        };
    }

    public Matrix3x3(
        T m00, T m01, T m02,
        T m10, T m11, T m12,
        T m20, T m21, T m22)
    {
        this.data = new[]
        {
            m00, m01, m02,
            m10, m11, m12,
            m20, m21, m22,
        };
    }

    public T this[int row, int column]
    {
        get => this.data[(row * 3) + column];
        set => this.data[(row * 3) + column] = value;
    }

    public Vector3<T> GetRow(int i) =>
        new(
            this[i, 0],
            this[i, 1],
            this[i, 2]);

    public Vector3<T> GetColumn(int j) =>
        new(
            this[0, j],
            this[1, j],
            this[2, j]);

    public IEnumerable<Vector3<T>> EnumerateRows() =>
        Enumerable.Range(0, 3)
            .Select(this.GetRow);

    public IEnumerable<Vector3<T>> EnumerateColumns() =>
        Enumerable.Range(0, 3)
            .Select(this.GetColumn);

    public Matrix3x3<U> Map<U>(Func<T, U> f)
        where U : INumber<U> =>
        new(
            f(this[0, 0]),
            f(this[0, 1]),
            f(this[0, 2]),
            f(this[1, 0]),
            f(this[1, 1]),
            f(this[1, 2]),
            f(this[2, 0]),
            f(this[2, 1]),
            f(this[2, 2]));

    public Matrix2x2<T> Submatrix(int row, int column)
    {
        var rows = Enumerable
            .Range(0, 3)
            .Where(i => i != row)
            .ToArray();

        var cols = Enumerable
            .Range(0, 3)
            .Where(j => j != column)
            .ToArray();

        var m = new Matrix2x2<T>();
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                m[i, j] = this[rows[i], cols[j]];
            }
        }

        return m;
    }

    public T Minor(int row, int column) =>
        this.Submatrix(row, column).Determinant();

    public T Cofactor(int row, int column) =>
        (row + column) % 2 == 0
            ? this.Minor(row, column)
            : -this.Minor(row, column);

    public T Determinant() =>
        (this[0, 0] * this.Cofactor(0, 0)) +
        (this[0, 1] * this.Cofactor(0, 1)) +
        (this[0, 2] * this.Cofactor(0, 2));

    public Matrix3x3<T> Transpose()
    {
        var m = new Matrix3x3<T>();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                m[j, i] = this[i, j];
            }
        }

        return m;
    }

    public bool Equals(Matrix3x3<T> other) =>
        other != null && this.data.SequenceEqual(other.data);

    public override bool Equals(object obj)
    {
        if (object.ReferenceEquals(this, obj))
        {
            return false;
        }

        var other = obj as Matrix3x3<T>;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode() =>
        HashCode.Combine(
            this.GetColumn(0),
            this.GetColumn(1),
            this.GetColumn(2));

    public string ToString(string format, IFormatProvider formatProvider)
    {
        var rows = this
            .EnumerateRows()
            .Select(r => r.ToString(format, formatProvider))
            .ToArray();

        return $"[{string.Join(' ', rows)}]";
    }
    
    public override string ToString() =>
        this.ToString(null, null);
}

public static class Matrix3x3
{
    public static void Multiply<T>(
        in Matrix3x3<T> a,
        in Matrix3x3<T> b,
        Matrix3x3<T> c)
        where T : INumber<T>
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                c[i, j] =
                    (a[i, 0] * b[0, j]) +
                    (a[i, 1] * b[1, j]) +
                    (a[i, 2] * b[2, j]);
            }
        }
    }
}