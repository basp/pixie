namespace Pixie;

public class Example02Integrator : Integrator
{
    private readonly Color<double> color = new(1.0, 0, 0);

    public Example02Integrator(Primitive aggregate)
        : base(aggregate)
    {
    }

    public override Color<double> Li(Ray ray) => ray
        .Transform(this.aggregate.Transform.Inverse)
        .Intersect(this.aggregate)
        .GetHit()
        .Map(_ => this.color)
        .ValueOr(Color<double>.Black);
}