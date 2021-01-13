namespace Linsi
{
    public class Vector3Operations : IVectorOperations<Vector3>
    {
        public Vector3 Add(Vector3 u, Vector3 v) => u + v;

        public Vector3 Cross(Vector3 u, Vector3 v) => Vector3.Cross(u, v);

        public double Dot(Vector3 u, Vector3 v) => Vector3.Dot(u, v);

        public Vector3 Scale(Vector3 u, double a) => a * u;

        public Vector3 Subtract(Vector3 u, Vector3 v) => u - v;
    }
}