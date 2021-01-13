namespace Linsi
{
    public class Vector4Operations : IVectorOperations<Vector4>
    {
        public Vector4 Add(Vector4 u, Vector4 v) => u + v;

        public Vector4 Cross(Vector4 u, Vector4 v) => Vector4.Cross(u, v);

        public double Dot(Vector4 u, Vector4 v) => Vector4.Dot(u, v);

        public Vector4 Scale(Vector4 u, double a) => a * u;

        public Vector4 Subtract(Vector4 u, Vector4 v) => u - v;
    }
}