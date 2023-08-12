namespace Pixie;

public class Camera
{
    private readonly float halfWidth;
    private readonly float halfHeight;

    public Camera(int width, int height, float fov)
    {
        var halfView = MathF.Tan(fov / 2);
        var aspect = (float)width / height;
        if (aspect >= 1)
        {
            this.halfWidth = halfView;
            this.halfHeight = halfView / aspect;
        }
        else
        {
            this.halfWidth = halfView * aspect;
            this.halfHeight = halfView;
        }

        this.PixelSize = (this.halfWidth * 2) / width;
    }
    
    public float PixelSize { get; }

    public Transform Transform { get; init; } = new(Matrix4x4.Identity);
   
    public Ray GenerateRay(int px, int py)
    {
        var xOffset = (px + 0.5f) * this.PixelSize;
        var yOffset = (py + 0.5f) * this.PixelSize;
        var worldX = this.halfWidth - xOffset;
        var worldY = this.halfHeight - yOffset;
        var pWorld = new Vector3(worldX, worldY, -1).AsPosition();
        var pixel = Vector4.Transform(
            pWorld, 
            this.Transform.Inverse);
        var origin = Vector4.Transform(
            Vector3.Zero.AsPosition(),
            this.Transform.Inverse);
        var direction = Vector4.Normalize(pixel - origin);
        return new Ray(origin, direction);
    }
}