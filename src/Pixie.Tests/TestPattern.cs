namespace Pixie.Tests
{    
    using Linsi;
    
    public class TestPattern : Pattern
    {
        public override Color GetColor(Vector4 point) =>
            new Color(point.X, point.Y, point.Z);
    }
}