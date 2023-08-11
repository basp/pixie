namespace Pixie;

public class PointLight
{
    public PointLight()
    {
        this.Position = new Vector4(0, 0, 0, 1);
        this.Intensity = new Vector3(0, 0, 0);
    }
    
    public PointLight(Vector4 position, Vector3 intensity)
    {
        this.Position = position;
        this.Intensity = intensity;
    }
    
    public Vector4 Position { get; set;  }
    
    public Vector3 Intensity { get; set;  }
}