namespace Pixie;

public readonly struct Point3<T> :
    IEquatable<Point3<T>>
    where T : INumber<T>
{
    public readonly T X, Y, Z;

    public bool Equals(Point3<T> other) =>
        this.X == other.X &&
        this.Y == other.Y &&
        this.Z == other.Z;
}