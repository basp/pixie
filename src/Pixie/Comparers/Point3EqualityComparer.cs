namespace Pixie.Comparers;

internal class Point3EqualityComparer<T> : 
    AbstractEqualityComparer<T>,
    IEqualityComparer<Point3<T>>
    where T : INumber<T>
{
    public Point3EqualityComparer(T tol)
        : base(tol)
    {
    }

    public bool Equals(Point3<T> u, Point3<T> v) =>
        u.X.IsApprox(v.X, this.Atol) &&
        u.Y.IsApprox(v.Y, this.Atol);

    public int GetHashCode(Point3<T> obj) =>
        obj.GetHashCode();
}