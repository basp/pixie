namespace Pixie
{
    using System.Collections.Generic;
    using Linsi;

    public class SphereLight : ILight
    {
        public Color Intensity => throw new System.NotImplementedException();

        public int Samples => throw new System.NotImplementedException();

        public bool Equals(ILight other)
        {
            throw new System.NotImplementedException();
        }

        public double GetIntensity(Vector4 point, World w)
        {
            throw new System.NotImplementedException();
        }

        public Vector4 GetPoint(double u, double v)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Vector4> Sample()
        {
            throw new System.NotImplementedException();
        }
    }
}