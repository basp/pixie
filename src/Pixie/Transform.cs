namespace Pixie;

public class Transform
{
    public Transform()
        : this(Matrix4x4.Identity, Matrix4x4.Identity)
    {
    }

    public Transform(Matrix4x4 m)
        : this(m, Utils.InvertOrNan(m))
    {
    }
    
    public Transform(Matrix4x4 m, Matrix4x4 inv)
    {
        this.Matrix = m;
        this.Inverse = inv;
    }

    public Matrix4x4 Matrix { get; }

    public Matrix4x4 Inverse { get; }

    public static Transform CreateShear(
        float xy, float xz,
        float yx, float yz,
        float zx, float zy)
    {
        Matrix4x4 m = new(
            1, xy, xz, 0,
            yx, 1, yz, 0,
            zx, zy, 1, 0,
            0f, 0f, 0, 1);
        m = Matrix4x4.Transpose(m);
        var inv = Utils.InvertOrNan(m);
        return new Transform(m, inv);
    }
}