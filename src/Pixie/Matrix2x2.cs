namespace Pixie;

public class Matrix2x2<T> :
    IEquatable<Matrix2x2<T>>,
    IFormattable
    where T : INumber<T>
{
    public static Matrix2x2<T> Identity => new(
        T.One, T.Zero,
        T.Zero, T.One);

    private readonly T[] data;

    public Matrix2x2()
        : this(T.Zero)
    {
    }

    public Matrix2x2(T v)
    {
        this.data = new[]
        {
            v, v,
            v, v
        };
    }

    public Matrix2x2(T m00, T m01, T m10, T m11)
    {
        this.data = new[]
        {
            m00, m01,
            m10, m11,
        };
    }

    public T this[int row, int column]
    {
        get => this.data[(row * 2) + column];
        set => this.data[(row * 2) + column] = value;
    }

    public Vector2<T> GetRow(int i) =>
        new(
            this[i, 0],
            this[i, 1]);

    public Vector2<T> GetColumn(int j) =>
        new(
            this[0, j],
            this[1, j]);

    public IEnumerable<Vector2<T>> EnumerateRows() =>
        Enumerable.Range(0, 2)
            .Select(this.GetRow);

    public IEnumerable<Vector2<T>> EnumerateColumns() =>
        Enumerable.Range(0, 2)
            .Select(this.GetColumn);

    public Matrix2x2<U> Map<U>(Func<T, U> f)
        where U : IFloatingPointIeee754<U> =>
        new(
            f(this[0, 0]),
            f(this[0, 1]),
            f(this[1, 0]),
            f(this[1, 1]));

    public T Determinant() =>
        (this[0, 0] * this[1, 1]) -
        (this[0, 1] * this[1, 0]);

    public Matrix2x2<T> Transpose()
    {
        var m = new Matrix2x2<T>();
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                m[j, i] = this[i, j];
            }
        }

        return m;
    }

    public bool Equals(Matrix2x2<T> other)
    {
        return other != null && this.data.SequenceEqual(other.data);
    }


    public override bool Equals(object obj)
    {
        if (object.ReferenceEquals(this, obj))
        {
            return true;
        }

        var other = obj as Matrix2x2<T>;
        return other != null && this.Equals(other);
    }

    public override int GetHashCode() =>
        HashCode.Combine(
            this.GetColumn(0),
            this.GetColumn(1));

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

public static class Matrix2x2
{
    public static Matrix2x2<T> Create<T>(T m00, T m01, T m10, T m11)
        where T : INumber<T> => new(m00, m01, m10, m11);
}