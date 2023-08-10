namespace Pixie;

public readonly struct Ray
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        this.Origin = origin.AsPosition();
        this.Direction = direction.AsDirection();
    }

    private Ray(Vector4 origin, Vector4 direction)
    {
        this.Origin = origin;
        this.Direction = direction;
    }
    
    public Vector4 Origin { get;  }

    public Vector4 Direction { get; }
    
    public Vector4 GetPointAt(float t) =>
        this.Origin + (this.Direction * t);

    public static Ray Transform(Ray ray, Matrix4x4 m) =>
        new Ray(
            Vector4.Transform(ray.Origin, m),
            Vector4.Transform(ray.Direction, m));
}