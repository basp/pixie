using System.Collections.Generic;

namespace Pixie.Core
{
    public class SphereLight : ILight
    {
        public Color Intensity => throw new System.NotImplementedException();

        public int Samples => throw new System.NotImplementedException();

        public bool Equals(ILight other)
        {
            throw new System.NotImplementedException();
        }

        public double IntensityAt(Vector4 point, World w)
        {
            throw new System.NotImplementedException();
        }

        public Vector4 PointOnLight(double u, double v)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Vector4> Sample()
        {
            throw new System.NotImplementedException();
        }
    }
}