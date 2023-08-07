namespace Pixie;

public class Matrix4x4EqualityComparer<T> :
    IEqualityComparer<Matrix4x4<T>>
    where T : INumber<T>
{
    private readonly T atol;

    public Matrix4x4EqualityComparer(T atol)
    {
        this.atol = atol;
    }

    public bool Equals(Matrix4x4<T> x, Matrix4x4<T> y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                if (!x[i, j].IsApprox(y[i, j], this.atol))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int GetHashCode(Matrix4x4<T> obj) =>
        obj.GetHashCode();
}