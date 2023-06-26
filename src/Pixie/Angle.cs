namespace Pixie;

public struct Angle
{
    private const double Deg2Rad = (2 * Math.PI) / 360;
    
    private const double Rad2Deg = 360 / (2 * Math.PI);

    private readonly double rad;

    private Angle(double rad)
    {
        this.rad = rad;
    }

    public double Radians => this.rad;

    public double Degrees => this.rad * Rad2Deg;

    public static Angle FromDegrees(double deg) =>
        new Angle(Deg2Rad * deg);

    public static Angle FromRadians(double rad) =>
        new Angle(rad);
}