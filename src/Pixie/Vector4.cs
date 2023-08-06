namespace Pixie;

public struct Vector4<T> :
    IEquatable<Vector4<T>>
    where T : INumber<T>
{
    public readonly T X, Y, Z, W;

    public Vector4()
        : this(T.Zero)
    {
    }

    public Vector4(T v)
        : this(v, v, v, v)
    {
    }

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

    public Vector4<U> Map<U>(Func<T, U> f)
        where U : INumber<U> =>
        new(
            f(this.X),
            f(this.Y),
            f(this.Z),
            f(this.W));

    public void Deconstruct(out T x, out T y, out T z, out T w)
    {
        x = this.X;
        y = this.Y;
        z = this.Z;
        w = this.W;
    }
    
    public bool Equals(Vector4<T> other) =>
        this.X == other.X &&
        this.Y == other.Y &&
        this.Z == other.Z &&
        this.W == other.W;

    public override bool Equals(object obj) =>
        obj is Vector4<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y, this.Z, this.W);

    public override string ToString() =>
        $"({this.X} {this.Y} {this.Z} {this.W})";
}

public static class Vector4
{
    public static Vector4<T> Create<T>(T x, T y, T z, T w)
        where T : IFloatingPointIeee754<T> =>
        new(x, y, z, w);
}