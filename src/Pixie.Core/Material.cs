namespace Pixie.Core
{
    using System;

    public class Material : IMaterial, IEquatable<Material>
    {
        public float Ambient;
        public float Diffuse;
        public float Specular;
        public float Shininess;
        public Color Color;

        public Material()
        {
            this.Ambient = 0.1f;
            this.Diffuse = 0.9f;
            this.Specular = 0.9f;
            this.Shininess = 200.0f;
            this.Color = Color.White;
        }

        public bool Equals(Material other) =>
            this.Ambient == other.Ambient &&
            this.Diffuse == other.Diffuse &&
            this.Specular == other.Specular &&
            this.Shininess == other.Shininess &&
            this.Color.Equals(other.Color);

        public Color Li(
            PointLight light, 
            Float4 point, 
            Float4 eyev, 
            Float4 normalv)
        {
            Color ambient, diffuse, specular;

            var effectiveColor = this.Color * light.Intensity;
            var lightv = (light.Position - point).Normalize();
            var lightDotNormal = Float4.Dot(lightv, normalv);

            ambient = effectiveColor * this.Ambient;
            
            if(lightDotNormal < 0)
            {
                diffuse = Color.Black;
                specular = Color.Black;
            }
            else
            {
                diffuse = effectiveColor * this.Diffuse * lightDotNormal;

                var reflectv = Float4.Reflect(-lightv, normalv);
                var reflectDotEye = Float4.Dot(reflectv, eyev);

                if (reflectDotEye <= 0)
                {
                    specular = Color.Black;
                }
                else
                {
                    var factor = (float)Math.Pow(reflectDotEye, this.Shininess);
                    specular = light.Intensity * this.Specular * factor;
                }
            }

            return ambient + diffuse + specular;
        }
    }
}