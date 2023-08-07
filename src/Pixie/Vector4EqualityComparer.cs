namespace Pixie;

internal class Vector4EqualityComparer<T> :
    IEqualityComparer<Vector4<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Vector4EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Vector4<T> u, Vector4<T> v) =>
        T.Abs(u.X - v.X) < this.atol &&
        T.Abs(u.Y - v.Y) < this.atol &&
        T.Abs(u.Z - v.Z) < this.atol &&
        T.Abs(u.W - v.W) < this.atol;

    public int GetHashCode(Vector4<T> obj) =>
        obj.GetHashCode();
}