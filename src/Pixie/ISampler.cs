namespace Pixie
{
    /// <summary>
    /// The main responsibility of a sampler is to return a color
    /// for a given pixel coordinate. The flexibility of this interface
    /// means that some samples can only shoot one ray per pixel while
    /// other samples might shoot up to hundreds of rays per pixel.
    /// </summary>
    public interface ISampler
    {
        Color Sample(int x, int y);
    }
}