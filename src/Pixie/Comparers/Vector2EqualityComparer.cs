namespace Pixie.Comparers;

internal class Vector2EqualityComparer<T> : 
    AbstractEqualityComparer<T>,
    IEqualityComparer<Vector2<T>>
    where T : IFloatingPointIeee754<T>
{
    public Vector2EqualityComparer(T tol)
        : base(tol)
    {
    }

    public bool Equals(Vector2<T> u, Vector2<T> v) =>
        u.X.IsApprox(v.X, this.Atol) &&
        u.Y.IsApprox(v.Y, this.Atol);

    public int GetHashCode(Vector2<T> obj) =>
        obj.GetHashCode();
}