// Licensed under the MIT license. See LICENSE file in the samples root for full license information.

namespace Pixie
{
    using Linie;

    public class SolidPattern : Pattern
    {
        public SolidPattern(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }

        public override Color GetColor(Vector4 point) =>
            this.Color;
    }
}
