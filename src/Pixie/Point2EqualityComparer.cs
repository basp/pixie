namespace Pixie;

internal class Point2EqualityComparer<T>
    : IEqualityComparer<Point2<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Point2EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Point2<T> u, Point2<T> v) =>
        u.X.IsApprox(v.X, this.atol) &&
        u.Y.IsApprox(v.Y, this.atol);

    public int GetHashCode(Point2<T> obj) =>
        obj.GetHashCode();
}