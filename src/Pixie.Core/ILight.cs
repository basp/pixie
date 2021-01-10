namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;

    public interface ILight : IEquatable<ILight>
    {
        Color Intensity { get; }

        int Samples { get; }   

        double IntensityAt(Vector4 point, World w);

        Vector4 PointOnLight(double u, double v);   

        IEnumerable<Vector4> Sample();      
    }
}