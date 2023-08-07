namespace Pixie;

public class Matrix2x2EqualityComparer<T> :
    IEqualityComparer<Matrix2x2<T>> 
    where T : INumber<T>
{
    private readonly T atol;
    
    public Matrix2x2EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Matrix2x2<T> a, Matrix2x2<T> b) =>
        a[0, 0].IsApprox(b[0, 0], this.atol) &&
        a[0, 1].IsApprox(b[0, 1], this.atol) &&
        a[1, 0].IsApprox(b[1, 0], this.atol) &&
        a[1, 1].IsApprox(b[1, 1], this.atol);

    public int GetHashCode(Matrix2x2<T> obj) =>
        obj.GetHashCode();
}