namespace Pixie
{
    /// <summary>
    /// Returns a color value for the given pixel coordinate.
    /// </summary>
    public interface ISampler
    {
        Color Sample(int x, int y);
    }
}