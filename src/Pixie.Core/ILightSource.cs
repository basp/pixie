namespace Pixie.Core
{
    using System.Collections.Generic;
    
    /// <summary>
    /// A light source can have multiple actual lights. This is just a
    /// convenient interface since it *is* a light but also contains
    /// other light sources.
    /// </summary>
    public interface ILightSource : ILight
    {
        IEnumerable<ILight> GetLights();
    }
}