namespace Pixie;

internal class Vector3EqualityComparer<T> :
    IEqualityComparer<Vector3<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Vector3EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Vector3<T> x, Vector3<T> y) =>
        T.Abs(x.X - y.X) < this.atol &&
        T.Abs(x.Y - y.Y) < this.atol &&
        T.Abs(x.Z - y.Z) < this.atol;

    public int GetHashCode(Vector3<T> obj) =>
        obj.GetHashCode();
}