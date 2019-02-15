namespace Pixie.Core
{
    using System;

    public class Material : IEquatable<Material>
    {
        public double Ambient;
        
        public double Diffuse;
        
        public double Specular;
        
        public double Shininess;
        
        public double Reflective;
        
        public double Transparency;
        
        public double RefractiveIndex;
        
        public Color Color;
        
        public Pattern Pattern;

        public Material()
        {
            this.Ambient = 0.1;
            this.Diffuse = 0.9;
            this.Specular = 0.9;
            this.Shininess = 200.0;
            this.Reflective = 0.0;
            this.Transparency = 0.0;
            this.RefractiveIndex = 1.0;
            this.Color = Color.White;
        }

        public bool Equals(Material other) =>
            this.Ambient == other.Ambient &&
            this.Diffuse == other.Diffuse &&
            this.Specular == other.Specular &&
            this.Shininess == other.Shininess &&
            this.Color.Equals(other.Color);

        public Color Li(
            Shape obj,
            ILight light,
            Double4 point,
            Double4 eyev,
            Double4 normalv,
            double shadow = 0.0)
        {
            Color color, ambient, diffuse, specular;

            if (this.Pattern != null)
            {
                color = this.Pattern.PatternAt(obj, point);
            }
            else
            {
                color = this.Color;
            }

            var effectiveColor = color * light.Intensity;
            var lightv = (light.Position - point).Normalize();
            var lightDotNormal = Double4.Dot(lightv, normalv);

            ambient = effectiveColor * this.Ambient;

            // var shadowf = 0.0;
            // if (shadow)
            // {
            //     shadowf = 1.0;
            // }

            if (lightDotNormal < 0)
            {
                diffuse = Color.Black;
                specular = Color.Black;
            }
            else
            {
                diffuse = effectiveColor * this.Diffuse * lightDotNormal;

                var reflectv = Double4.Reflect(-lightv, normalv);
                var reflectDotEye = Double4.Dot(reflectv, eyev);

                if (reflectDotEye <= 0)
                {
                    specular = Color.Black;
                }
                else
                {
                    var factor = (double)Math.Pow(reflectDotEye, this.Shininess);
                    specular = light.Intensity * this.Specular * factor;
                }
            }

            return ambient + (1.0 - shadow) * (diffuse + specular);
        }
    }
}