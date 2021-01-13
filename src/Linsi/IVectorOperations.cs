namespace Linsi
{
    public interface IVectorOperations<T>
    {
        T Add(T u, T v);

        T Subtract(T u, T v);

        T Scale(T u, double a);

        double Dot(T u, T v);
    }
}