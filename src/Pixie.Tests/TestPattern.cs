namespace Pixie.Tests
{    
    using Linie;
    
    public class TestPattern : Pattern
    {
        public override Color GetColor(Vector4 point) =>
            new Color(point.X, point.Y, point.Z);
    }
}