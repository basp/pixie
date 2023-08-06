namespace Pixie.Comparers;

internal class Vector3EqualityComparer<T> : 
    AbstractEqualityComparer<T>,
    IEqualityComparer<Vector3<T>>
    where T : IFloatingPointIeee754<T>
{
    public Vector3EqualityComparer(T atol)
        : base(atol)
    {
    }

    public bool Equals(Vector3<T> u, Vector3<T> v) =>
        u.X.IsApprox(v.X, this.Atol) &&
        u.Y.IsApprox(v.Y, this.Atol) &&
        u.Z.IsApprox(v.Z, this.Atol);

    public int GetHashCode(Vector3<T> obj) =>
        obj.GetHashCode();
}