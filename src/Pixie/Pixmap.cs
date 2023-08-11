namespace Pixie;

public class Pixmap : Canvas
{
    public Pixmap(int width, int height)
        : base(width, height)
    {
    }

    public override void Write(Stream s)
    {
        using var w = new StreamWriter(s);

        w.WriteLine($"P3");
        w.WriteLine($"{this.Width} {this.Height}");
        w.WriteLine($"255");

        // var min = new Color<int>(0, 0, 0);
        // var max = new Color<int>(255, 255, 255);
        var min = new Vector3(0, 0, 0);
        var max = new Vector3(255, 255, 255);

        for (var j = this.Height - 1; j >= 0; j--)
        {
            for (var i = 0; i < this.Width; i++)
            {
                var c = Vector3.Clamp(this[i, j] * 255, min, max);
                w.WriteLine($"{(int)c.X} {(int)c.Y} {(int)c.Z}");
            }
        }
    }
}