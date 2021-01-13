namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using Linsi;

    public interface ILight : IEquatable<ILight>
    {
        Color Intensity { get; }

        int Samples { get; }   

        double GetIntensity(Vector4 point, World w);

        Vector4 GetPoint(double u, double v);   

        IEnumerable<Vector4> Sample();      
    }
}