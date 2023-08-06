namespace Pixie;

public readonly struct Point3<T> :
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

    public override bool Equals(object obj) =>
        obj is Point3<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y, this.Z);

    public override string ToString() =>
        $"({this.X} {this.Y} {this.Z})";
}