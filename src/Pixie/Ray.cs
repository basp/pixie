namespace Pixie;

public readonly struct Ray
{
    public Ray(Vector3 origin, Vector3 direction)
    {
        this.Origin = origin.AsPosition();
        this.Direction = direction.AsDirection();
    }

    public Ray(Vector4 origin, Vector4 direction)
    {
        this.Origin = origin;
        this.Direction = direction;
    }
    
    public Vector4 Origin { get;  }

    public Vector4 Direction { get; }

    public static Ray Transform(Ray ray, Matrix4x4 m) =>
        new(
            Vector4.Transform(ray.Origin, m),
            Vector4.Transform(ray.Direction, m));
    
    public Vector4 GetPointAt(float t) =>
        this.Origin + (this.Direction * t);

    public Ray Transform(Matrix4x4 m) => Ray.Transform(this, m);
}