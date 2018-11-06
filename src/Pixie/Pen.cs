namespace Pixie
{
    public class Pen
    {
        readonly Color color;
        readonly float width;

        public Pen(Color color)
        {
            this.color = color;
            this.width = 1.0f;
        }

        public Pen(Color color, float width)
        {
            this.color = color;
            this.width = width;
        }

        public Color Color => this.color;

        public float Width => this.width;
    }

    public static class PenExtensions
    {
        public static Pixie.Pen ToPixiePen(this System.Drawing.Pen self) =>
            new Pixie.Pen(self.Color.ToPixieColor(), self.Width);

        public static System.Drawing.Pen ToSystemDrawingPen(this Pen self) =>
            new System.Drawing.Pen(self.Color.ToSystemDrawingColor(), self.Width);
    }
}