namespace Pixie;

public readonly struct Vector3<T> :
    IEquatable<Vector3<T>>
    where T : INumber<T>
{
    public readonly T X, Y, Z;

    public Vector3(T x, T y, T z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public T this[int i] =>
        i switch
        {
            0 => this.X,
            1 => this.Y,
            _ => this.Z,
        };

    public Vector3<U> Map<U>(Func<T, U> f)
        where U : INumber<U> =>
        new(
            f(this.X),
            f(this.Y),
            f(this.Z));

    public void Deconstruct(out T x, out T y, out T z)
    {
        x = this.X;
        y = this.Y;
        z = this.Z;
    }

    public bool Equals(Vector3<T> other) =>
        this.X == other.X &&
        this.Y == other.Y &&
        this.Z == other.Z;

    public override bool Equals(object obj) =>
        obj is Vector3<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y, this.Z);

    public override string ToString() =>
        $"({this.X} {this.Y} {this.Z})";
}

public static class Vector3
{
    public static Vector3<T> Create<T>(T x, T y, T z)
        where T : IFloatingPointIeee754<T> =>
        new(x, y, z);

    public static Vector3<T> Add<T>(Vector3<T> u, Vector3<T> v)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X + v.X,
            u.Y + v.Y,
            u.Z + v.Z);

    public static Vector3<T> Subtract<T>(Vector3<T> u, Vector3<T> v)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X - v.X,
            u.Y - v.Y,
            u.Z - v.Z);

    public static Vector3<T> Multiply<T>(T a, Vector3<T> u)
        where T : IFloatingPointIeee754<T> =>
        new(
            a * u.X,
            a * u.Y,
            a * u.Z);

    public static Vector3<T> Multiply<T>(Vector3<T> u, T a)
        where T : IFloatingPointIeee754<T> =>
        Vector3.Multiply(a, u);

    public static Vector3<T> Divide<T>(Vector3<T> u, T a)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X / a,
            u.Y / a,
            u.Z / a);

    public static T Magnitude<T>(Vector3<T> u)
        where T : IFloatingPointIeee754<T> =>
        T.Sqrt(Vector3.MagnitudeSquared(u));

    public static T MagnitudeSquared<T>(Vector3<T> u)
        where T : IFloatingPointIeee754<T> =>
        Vector3.Dot(u, u);

    public static T Dot<T>(Vector3<T> u, Vector3<T> v)
        where T : IFloatingPointIeee754<T> =>
        u.X * v.X +
        u.Y * v.Y +
        u.Z * v.Z;

    public static Vector3<T> Cross<T>(Vector3<T> u, Vector3<T> v)
        where T : IFloatingPointIeee754<T> =>
        new(
            Utils.DifferenceOfProducts(u.Y, v.Z, u.Z, v.Y),
            Utils.DifferenceOfProducts(u.Z, v.X, u.X, v.Z),
            Utils.DifferenceOfProducts(u.X, v.Y, u.Y, v.X));

    public static Vector3<T> Negate<T>(Vector3<T> u)
        where T : IFloatingPointIeee754<T> =>
        new(-u.X, -u.Y, -u.Z);

    public static Vector3<T> Normalize<T>(Vector3<T> u)
        where T : IFloatingPointIeee754<T> =>
        Vector3.Divide(u, Vector3.Magnitude(u));

    public static IEqualityComparer<Vector3<T>> GetEqualityComparer<T>(T atol)
        where T : IFloatingPointIeee754<T> =>
        new Vector3EqualityComparer<T>(atol);
}