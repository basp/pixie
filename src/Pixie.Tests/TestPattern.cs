namespace Pixie.Tests
{
    using Pixie.Core;
    
    public class TestPattern : Pattern
    {
        public override Color PatternAt(Vector4 point) =>
            new Color(point.X, point.Y, point.Z);
    }
}