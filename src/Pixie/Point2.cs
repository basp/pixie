using System.Runtime.CompilerServices;

namespace Pixie;

public readonly struct Point2<T> :
    IEquatable<Point2<T>>
    where T : INumber<T>
{
    public readonly T X, Y;

    public Point2()
        : this(T.Zero)
    {
    }

    public Point2(T v)
        : this(v, v)
    {
    }

    public Point2(T x, T y)
    {
        this.X = x;
        this.Y = y;
    }

    public T this[int index] =>
        index switch
        {
            0 => this.X,
            _ => this.Y,
        };

    public bool Equals(Point2<T> other) =>
        this.X == other.X &&
        this.Y == other.Y;

    public override bool Equals(object obj) =>
        obj is Point2<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y);

    public override string ToString() =>
        $"({this.X} {this.Y})";
}

public static class Point2
{
    public static Point2<T> Create<T>(T x, T y)
        where T : INumber<T> =>
        new(x, y);

    public static Point2<T> Add<T>(Point2<T> a, Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        new(
            a.X + u.X,
            a.Y + u.Y);

    public static Point2<T> Subtract<T>(Point2<T> a, Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        new(
            a.X - u.X,
            a.Y - u.Y);

    public static Vector2<T> Subtract<T>(Point2<T> a, Point2<T> b)
        where T : IFloatingPointIeee754<T> =>
        new(
            a.X - b.X,
            a.Y - b.Y);

    public static Point2<T> Multiply<T>(T c, Point2<T> a)
        where T : IFloatingPointIeee754<T> =>
        new(
            c * a.X,
            c * a.Y);

    public static Point2<T> Multiply<T>(Point2<T> a, T c)
        where T : IFloatingPointIeee754<T> => Point2.Multiply(c, a);
}