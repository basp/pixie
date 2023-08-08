namespace Pixie;

public class Vector4Comparer : IEqualityComparer<Vector4>
{
    private readonly float atol;

    public Vector4Comparer(float atol)
    {
        this.atol = atol;
    }

    public bool Equals(Vector4 u, Vector4 v) =>
        MathF.Abs(u.X - v.X) < this.atol &&
        MathF.Abs(u.Y - v.Y) < this.atol &&
        MathF.Abs(u.Z - v.Z) < this.atol &&
        MathF.Abs(u.W - v.W) < this.atol;

    public int GetHashCode(Vector4 obj) => throw new NotImplementedException();
}