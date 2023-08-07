namespace Pixie;

internal class Vector2EqualityComparer<T> :
    IEqualityComparer<Vector2<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Vector2EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Vector2<T> u, Vector2<T> v) =>
        T.Abs(u.X - v.X) < this.atol &&
        T.Abs(u.Y - v.Y) < this.atol;

    public int GetHashCode(Vector2<T> obj) =>
        obj.GetHashCode();
}