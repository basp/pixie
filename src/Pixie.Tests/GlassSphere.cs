namespace Pixie.Tests
{
    using Pixie.Core;

    public class GlassSphere : Sphere
    {
        public GlassSphere()
        {
            this.Material.Transparency = 1.0;
            this.Material.RefractiveIndex = 1.5;
        }    
    }
}