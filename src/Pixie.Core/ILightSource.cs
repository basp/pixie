namespace Pixie.Core
{
    using System.Collections.Generic;
    
    public interface ILightSource : ILight
    {
        IEnumerable<ILight> GetLights();
    }
}