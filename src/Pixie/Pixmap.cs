namespace Pixie;

public class Pixmap : Canvas<double>
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

        var min = new Color<int>(0, 0, 0);
        var max = new Color<int>(255, 255, 255);

        for (var j = this.Height - 1; j >= 0; j--)
        {
            for (var i = 0; i < this.Width; i++)
            {
                var (r, g, b) = this[i, j]
                    .Map(v => v * 255)
                    .Map(v => (int)v)
                    .Clamp(min, max);

                w.WriteLine($"{r} {g} {b}");
            }
        }
    }
}