namespace Pixie.Core
{
    using System.Collections.Generic;
    
    public interface ILightSource
    {
        IEnumerable<ILight> GetLights();
    }
}