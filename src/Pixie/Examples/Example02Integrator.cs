namespace Pixie.Examples;

public class Integrator02 : Integrator
{
    // private readonly Color<double> color = new(1.0, 0, 0);
    private readonly Vector3 red = new(1, 0, 0);
    private readonly Vector3 black = new(0, 0, 0);

    public Integrator02(Primitive aggregate)
        : base(aggregate)
    {
    }

    public override Vector3 Li(Ray ray) => ray
        .Transform(this.aggregate.Transform.Inverse)
        .Intersect(this.aggregate)
        .GetHit()
        .Map(_ => this.red)
        .ValueOr(this.black);
}