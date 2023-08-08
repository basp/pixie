namespace Pixie;

public class Matrix4x4Comparer : IEqualityComparer<Matrix4x4>
{
    private readonly float atol;

    public Matrix4x4Comparer(float atol)
    {
        this.atol = atol;
    }

    public bool Equals(Matrix4x4 a, Matrix4x4 b) =>
        MathF.Abs(a.M11 - b.M11) < this.atol &&
        MathF.Abs(a.M12 - b.M12) < this.atol &&
        MathF.Abs(a.M13 - b.M13) < this.atol &&
        MathF.Abs(a.M14 - b.M14) < this.atol &&
        MathF.Abs(a.M21 - b.M21) < this.atol &&
        MathF.Abs(a.M22 - b.M22) < this.atol &&
        MathF.Abs(a.M23 - b.M23) < this.atol &&
        MathF.Abs(a.M24 - b.M24) < this.atol &&
        MathF.Abs(a.M31 - b.M31) < this.atol &&
        MathF.Abs(a.M32 - b.M32) < this.atol &&
        MathF.Abs(a.M33 - b.M33) < this.atol &&
        MathF.Abs(a.M34 - b.M34) < this.atol &&
        MathF.Abs(a.M41 - b.M41) < this.atol &&
        MathF.Abs(a.M42 - b.M42) < this.atol &&
        MathF.Abs(a.M43 - b.M43) < this.atol &&
        MathF.Abs(a.M44 - b.M44) < this.atol;

    public int GetHashCode(Matrix4x4 obj) =>
        obj.GetHashCode();
}