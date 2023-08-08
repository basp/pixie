namespace Pixie;

public readonly struct Ray
{
    public Ray(Vector4 origin, Vector4 direction)
    {
        this.Origin = origin;
        this.Direction = direction;
    }
    
    public Vector4 Origin { get;  }

    public Vector4 Direction { get; }
    
    public Vector4 GetPointAt(float t) =>
        this.Origin + (this.Direction * t);
}