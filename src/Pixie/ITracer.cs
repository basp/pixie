namespace Pixie
{
    using Linie;

    public interface ITracer
    {
        Color Trace(Ray4 ray);
    }
}