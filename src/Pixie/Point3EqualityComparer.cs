namespace Pixie;

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
        a.X.IsApprox(b.X, this.atol) &&
        a.Y.IsApprox(b.Y, this.atol) &&
        a.Z.IsApprox(b.Z, this.atol);

    public int GetHashCode(Point3<T> obj) =>
        HashCode.Combine(obj.X, obj.Y, obj.Z);
}