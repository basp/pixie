namespace Pixie;

public class Vector3Comparer : IEqualityComparer<Vector3>
{
    private readonly float atol;

    public Vector3Comparer(float atol)
    {
        this.atol = atol;
    }


    public bool Equals(Vector3 u, Vector3 v) =>
        MathF.Abs(u.X - v.X) < this.atol &&
        MathF.Abs(u.Y - v.Y) < this.atol &&
        MathF.Abs(u.Z - v.Z) < this.atol;

    public int GetHashCode(Vector3 obj) => throw new NotImplementedException();
}