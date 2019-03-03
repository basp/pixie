namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public interface ILight : IEquatable<ILight>
    {
        Color Intensity { get; }

        int Samples { get; }   

        double IntensityAt(Double4 point, World w);

        Double4 PointOnLight(double u, double v);   

        IEnumerable<Double4> Sample();      
    }
}