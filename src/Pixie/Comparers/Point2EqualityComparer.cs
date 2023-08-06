namespace Pixie.Comparers;

internal class Point2EqualityComparer<T> : 
    AbstractEqualityComparer<T>,
    IEqualityComparer<Point2<T>>
    where T : INumber<T>
{
    public Point2EqualityComparer(T tol)
        : base(tol)
    {
    }

    public bool Equals(Point2<T> u, Point2<T> v) =>
        u.X.IsApprox(v.X, this.Atol) &&
        u.Y.IsApprox(v.Y, this.Atol);

    public int GetHashCode(Point2<T> obj) =>
        obj.GetHashCode();
}