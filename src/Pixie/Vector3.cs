namespace Linie;

public readonly struct Vector3<T> :
    IEquatable<Vector3<T>>,
    IFormattable
    where T : INumber<T>
{
    public readonly T X, Y, Z;

    public Vector3(T x, T y, T z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public T this[int index] =>
        index switch
        {
            0 => this.X,
            1 => this.Y,
            _ => this.Z,
        };

    public bool Equals(Vector3<T> other) =>
        this.X == other.X &&
        this.Y == other.Y &&
        this.Z == other.Z;

    public override bool Equals(object obj) =>
        obj is Vector3<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y, this.Z, this);

    public override string ToString() =>
        this.ToString(null, null);

    public string ToString(string format, IFormatProvider formatProvider) =>
        string.Format(
            "({0} {1} {2})",
            this.X.ToString(format, formatProvider),
            this.Y.ToString(format, formatProvider),
            this.Y.ToString(format, formatProvider));
}

internal class Vector3EqualityComparer<T> :
    IEqualityComparer<Vector3<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Vector3EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Vector3<T> x, Vector3<T> y) =>
        T.Abs(x.X - y.X) < this.atol &&
        T.Abs(x.Y - y.Y) < this.atol &&
        T.Abs(x.Z - y.Z) < this.atol;

    public int GetHashCode(Vector3<T> obj) =>
        obj.GetHashCode();
}

public static class Vector3
{
    public static IEqualityComparer<Vector3<T>> GetComparer<T>(T atol)
        where T : INumber<T> =>
        new Vector3EqualityComparer<T>(atol);

    public static Vector3<T> Create<T>(T x, T y, T z)
        where T : INumber<T> =>
        new(x, y, z);

    public static Vector3<T> Add<T>(Vector3<T> u, Vector3<T> v)
        where T : INumber<T> =>
        new(
            u.X + v.X,
            u.Y + v.Y,
            u.Z + v.Z);

    public static Vector3<T> Subtract<T>(Vector3<T> u, Vector3<T> v)
        where T : INumber<T> =>
        new(
            u.X - v.X,
            u.Y - v.Y,
            u.Z - v.Z);

    public static Vector3<T> Multiply<T>(T a, Vector3<T> u)
        where T : INumber<T> =>
        new(
            a * u.X,
            a * u.Y,
            a * u.Z);

    public static Vector3<T> Divide<T>(Vector3<T> u, T a)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X / a,
            u.Y / a,
            u.Z / a);
}