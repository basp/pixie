namespace Pixie.Core
{
    using System;

    /// <summary>
    /// The most basic of light sources. This is quick to render, easy to
    /// use and also a component of more advanced light sources such as the
    /// area light.
    /// </summary>
    public interface ILight : IEquatable<ILight>
    {
        Color Intensity { get; }

        Double4 Position { get; }

        double IntensityAt(Double4 point, World w);
    }
}