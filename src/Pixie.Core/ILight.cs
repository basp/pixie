namespace Pixie.Core
{
    using System;

    public interface ILight : IEquatable<ILight>
    {
        Color Intensity { get; }

        Double4 Position { get; }
    }
}