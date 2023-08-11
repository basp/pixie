namespace Pixie.Examples;

public class Integrator03 : Integrator
{
    private readonly PointLight light;

    public Integrator03(Primitive aggregate, PointLight light)
        : base(aggregate)
    {
        this.light = light;
    }

    public override Vector3 Li(Ray ray)
    {
        var maybeHit = ray
            .Transform(this.aggregate.Transform.Inverse)
            .Intersect(this.aggregate)
            .GetHit();

        if (!maybeHit.HasValue)
        {
            return new Vector3(0, 0, 0);
        }

        var hit = maybeHit.ValueOrFailure();
        if (!hit.Material.HasValue)
        {
            return new Vector3(0, 0, 0);
        }

        var mat = hit.Material.ValueOrFailure();
        var point = ray.GetPointAt(hit.T);
        var normal = this.aggregate.GetNormalAt(point);
        var eye = -ray.Direction;
        return mat.GetLighting(this.light, point, eye, normal);
    }
}