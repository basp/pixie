namespace Pixie;

public record Interaction
{
    public Material Material { get; set; }
    
    public Vector4 Point { get; set; }
    
    public Vector4 Eye { get; set;  }
    
    public Vector4 Normal { get; set;  }
}