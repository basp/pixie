namespace Pixie;

public record Material
{
    public static Material Default { get; } = new()
    {
        Color = new Vector3(1, 1, 1),
        Ambient = 0.1f,
        Diffuse = 0.9f,
        Specular = 0.9f,
        Shininess = 200.0f,
    };
    
    public Vector3 Color { get; set; }

    public float Ambient { get; set; }

    public float Diffuse { get; set; }

    public float Specular { get; set; }

    public float Shininess { get; set; }

    public Vector3 GetLighting(
        PointLight light,
        Vector4 point,
        Vector4 eyev,
        Vector4 normalv)
    {
        var effectiveColor = this.Color * light.Intensity;
        var lightv = Vector4.Normalize(light.Position - point);
        var ambient = effectiveColor * this.Ambient;
        var lightDotNormal = Vector4.Dot(lightv, normalv);
        if (lightDotNormal < 0)
        {
            return ambient;
        }
        var diffuse = effectiveColor * this.Diffuse * lightDotNormal;
        var reflectv = -lightv.Reflect(normalv);
        var reflectDotEye = Vector4.Dot(reflectv, eyev);
        if (reflectDotEye <= 0)
        {
            return ambient + diffuse;
        }
        var factor = MathF.Pow(reflectDotEye, this.Shininess);
        var specular = light.Intensity * this.Specular * factor;
        return ambient + diffuse + specular;
    }
}