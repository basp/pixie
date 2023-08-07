namespace Pixie;

public class Matrix3x3EqualityComparer<T> :
    IEqualityComparer<Matrix3x3<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Matrix3x3EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Matrix3x3<T> x, Matrix3x3<T> y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (!x[i, j].IsApprox(y[i, j], this.atol))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int GetHashCode(Matrix3x3<T> obj) =>
        obj.GetHashCode();
}