using Linie;

namespace Pixie;

public class Transform
{
    private readonly Matrix4x4 m;

    private readonly Matrix4x4 mInv;

    public Transform()
        : this(Matrix4x4.Identity, Matrix4x4.Identity)
    {
    }

    public Transform(Matrix4x4 m)
        : this(m, m.Inverse())
    {
    }

    public Transform(Matrix4x4 m, Matrix4x4 mInv)
    {
        this.m = m;
        this.mInv = mInv;
    }

    public static Transform Inverse(Transform t) =>
        new Transform(t.mInv, t.m);

    public static Transform Transpose(Transform t) =>
        new Transform(t.m.Transpose(), t.mInv.Transpose());

    public static Transform Translate(Vector3 delta)
    {
        var m = new Matrix4x4(
            1, 0, 0, delta.X,
            0, 1, 0, delta.Y,
            0, 0, 1, delta.Z,
            0, 0, 0, 1);

        var mInv = new Matrix4x4(
            1, 0, 0, -delta.X,
            0, 1, 0, -delta.Y,
            0, 0, 1, -delta.Z,
            0, 0, 0, 1);

        return new Transform(m, mInv);
    }

    public static Transform Scale(double x, double y, double z)
    {
        var m = new Matrix4x4(
            x, 0, 0, 0,
            0, y, 0, 0,
            0, 0, z, 0,
            0, 0, 0, 1);

        var mInv = new Matrix4x4(
            1 / x, 0, 0, 0,
            0, 1 / y, 0, 0,
            0, 0, 1 / z, 0,
            0, 0, 0, 1);

        return new Transform(m, mInv);
    }

    public static Transform RotateX(Angle theta)
    {
        var sinTheta = Math.Sin(theta.Radians);
        var cosTheta = Math.Cos(theta.Radians);
        
        var m = new Matrix4x4(
            1, 0, 0, 0,
            0, cosTheta, -sinTheta, 0,
            0, sinTheta, cosTheta, 0,
            0, 0, 0, 1);

        return new Transform(m, m.Transpose());
    }

    public static Transform RotateY(Angle theta)
    {
        var sinTheta = Math.Sin(theta.Radians);
        var cosTheta = Math.Cos(theta.Radians);

        var m = new Matrix4x4(
            cosTheta, 0, sinTheta, 0,
            0, 1, 0, 0,
            -sinTheta, 0, cosTheta, 0,
            0, 0, 0, 1);

        return new Transform(m, m.Transpose());
    }

    public static Transform RotateZ(Angle theta)
    {
        var sinTheta = Math.Sin(theta.Radians);
        var cosTheta = Math.Cos(theta.Radians);

        var m = new Matrix4x4(
            cosTheta, -sinTheta, 0, 0,
            sinTheta, cosTheta, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        return new Transform(m, m.Transpose());
    }

    public static Transform Rotate(Angle theta, Vector3 axis)
    {
        var a = axis.Normalize();

        var sinTheta = Math.Sin(theta.Radians);
        var cosTheta = Math.Cos(theta.Radians);

        var m = Matrix4x4.Identity;
        
        m[0, 0] = a.X * a.X + (1 - a.X * a.X) * cosTheta;
        m[0, 1] = a.X * a.Y * (1 - cosTheta) - a.Z * sinTheta;
        m[0, 2] = a.X * a.Z * (1 - cosTheta) + a.Y * sinTheta;

        m[1, 0] = a.X * a.Y * (1 - cosTheta) + a.Z * sinTheta;
        m[1, 1] = a.Y * a.Y + (1 - a.Y * a.Y) * cosTheta;
        m[1, 2] = a.Y * a.Z * (1 - cosTheta) - a.X * sinTheta;

        m[2, 0] = a.X * a.Z * (1 - cosTheta) - a.Y * sinTheta;
        m[2, 1] = a.Y * a.Z * (1 - cosTheta) + a.X * sinTheta;
        m[2, 2] = a.Z * a.Z + (1 - a.Z * a.Z) * cosTheta;

        return new Transform(m, m.Transpose());
    }

    public Transform Inverse() => Transform.Inverse(this);

    public Transform Transpose() => Transform.Transpose(this);

    public bool HasScale()
    {
        static bool NotOne(double v) => v < 0.999 && v > 1.001;

        Vector3 t(Vector3 v)
        {
            return new Vector3(
                this.m[0, 0] * v.X + this.m[0, 1] * v.Y + m[0, 2] * v.Z,
                this.m[1, 0] * v.X + this.m[1, 1] * v.Y + m[1, 2] * v.Z,
                this.m[2, 0] * v.X + this.m[2, 1] * v.Y + m[2, 2] * v.Z);
        }

        var la2 = Vector3.MagnitudeSquared(t(new Vector3(1, 0, 0)));
        var lb2 = Vector3.MagnitudeSquared(t(new Vector3(0, 1, 0)));
        var lc2 = Vector3.MagnitudeSquared(t(new Vector3(0, 0, 1)));

        return NotOne(la2) || NotOne(lb2) || NotOne(lc2);
    }
}