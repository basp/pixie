namespace Pixie;

internal abstract class AbstractEqualityComparer<T>
    where T : INumber<T>
{
    protected readonly T Atol;

    protected AbstractEqualityComparer(T atol)
    {
        this.Atol = atol;
    }
}