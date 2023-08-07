using System.Collections;

namespace Pixie;

public struct Point3<T> :
    IEquatable<Point3<T>>
    where T : INumber<T>
{
    public readonly T X, Y, Z;

    public Point3()
        : this(T.Zero)
    {
    }

    public Point3(T v)
        : this(v, v, v)
    {
    }

    public Point3(T x, T y, T z)
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

    public bool Equals(Point3<T> other) =>
        this.X == other.X &&
        this.Y == other.Y &&
        this.Z == other.Z;

    public override string ToString() =>
        $"({this.X} {this.Y} {this.Z})";
}

internal class Point3EqualityComparer<T> :
    IEqualityComparer<Point3<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Point3EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Point3<T> a, Point3<T> b) =>
        T.Abs(a.X - b.X) < this.atol &&
        T.Abs(a.Y - b.Y) < this.atol &&
        T.Abs(a.Z - b.Z) < this.atol;

    public int GetHashCode(Point3<T> obj) =>
        HashCode.Combine(obj.X, obj.Y, obj.Z);
}

public static class Point3
{
    public static IEqualityComparer<Point3<T>> GetComparer<T>(T atol)
        where T : INumber<T> =>
        new Point3EqualityComparer<T>(atol);
    
    public static Point3<T> FromCylindrical<T>(T r, T theta, T y)
        where T : IFloatingPointIeee754<T>
    {
        var x = r * T.Sin(theta);
        var z = r * T.Cos(theta);
        return Point3.Create(x, y, z);
    }

    public static Point3<T> FromSpherical<T>(T r, T theta, T phi)
        where T : IFloatingPointIeee754<T>
    {
        var x = r * T.Sin(theta) * T.Sin(phi);
        var y = r * T.Cos(theta);
        var z = r * T.Sin(theta) * T.Cos(phi);
        return Point3.Create(x, y, z);
    }

    public static Point3<T> Create<T>(T x, T y, T z)
        where T : INumber<T> =>
        new(x, y, z);
}