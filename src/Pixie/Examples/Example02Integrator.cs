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
        .Map(_ => this.red)
        .ValueOr(this.black);
}

public static class Integrator02Extensions
{
    public static Option<Intersection> Intersect(this Ray self, Primitive primitive) =>
            primitive.Intersect(self);
}