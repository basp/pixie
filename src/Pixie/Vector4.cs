namespace Pixie;

public readonly struct Vector4<T> :
    IEquatable<Vector4<T>>,
    IFormattable
    where T : INumber<T>
{
    public readonly T X, Y, Z, W;

    public Vector4(T x, T y, T z, T w)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.W = w;
    }

    public T this[int index] =>
        index switch
        {
            0 => this.X,
            1 => this.Y,
            2 => this.Z,
            _ => this.W,
        };

    public bool Equals(Vector4<T> other) =>
        this.X == other.X &&
        this.Y == other.Y &&
        this.Z == other.Z &&
        this.W == other.W;

    public override bool Equals(object obj) =>
        obj is Vector4<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y, this.W, this.Z);

    public override string ToString() =>
        this.ToString(null, null);

    public string ToString(string format, IFormatProvider formatProvider) =>
        string.Format(
            "({0} {1} {2} {3})",
            this.X.ToString(format, formatProvider),
            this.Y.ToString(format, formatProvider),
            this.Z.ToString(format, formatProvider),
            this.W.ToString(format, formatProvider));

    public T Dot(Vector4<T> other) => Vector4.Dot(this, other);
}

public static class Vector4
{
    public static Vector4<T> Add<T>(Vector4<T> a, Vector4<T> b)
        where T : INumber<T> =>
        new(
            a.X + b.X,
            a.Y + b.Y,
            a.Z + b.Z,
            a.W + b.W);

    public static Vector4<T> CreatePosition<T>(T x, T y, T z)
        where T : INumber<T> =>
        new(x, y, z, T.Zero);

    public static Vector4<T> CreateDirection<T>(T x, T y, T z)
        where T : INumber<T> =>
        new(x, y, z, T.One);

    public static Vector4<T> Create<T>(T x, T y, T z, T w)
        where T : INumber<T> =>
        new(x, y, z, w);

    public static T Dot<T>(Vector4<T> u, Vector4<T> v)
        where T : INumber<T> =>
        u.X * v.X +
        u.Y * v.Y +
        u.Z * v.Z +
        u.W * v.W;

    public static IEqualityComparer<Vector4<T>> GetComparer<T>(T atol)
        where T : INumber<T> =>
        new Vector4EqualityComparer<T>(atol);

    public static Vector4<T> Subtract<T>(Vector4<T> a, Vector4<T> b)
        where T : INumber<T> =>
        new(
            a.X - b.X,
            a.Y - b.Y,
            a.Z - b.Z,
            a.W - b.W);

    public static Vector4<T> Multiply<T>(T a, Vector4<T> u)
        where T : INumber<T> =>
        new(
            a * u.X,
            a * u.Y,
            a * u.Z,
            a * u.W);

    public static Vector4<T> Multiply<T>(Vector4<T> u, T a)
        where T : INumber<T> =>
        Vector4.Multiply(a, u);

    public static Vector4<T> Divide<T>(Vector4<T> u, T a)
        where T : IFloatingPoint<T> =>
        new(
            u.X / a,
            u.Y / a,
            u.Z / a,
            u.W / a);
}