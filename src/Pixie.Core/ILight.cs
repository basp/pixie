namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The most basic of light sources. This is quick to render, easy to
    /// use and also a component of more advanced light sources such as the
    /// area light.
    /// </summary>
    public interface ILight : IEquatable<ILight>
    {
        Color Intensity { get; }

        int Samples { get; }   

        double IntensityAt(Double4 point, World w);

        Double4 PointOnLight(double u, double v);   

        IEnumerable<Double4> Sample();      
    }
}