namespace Pixie;

public class Sphere : Shape
{
    public override Vector4 GetNormalAt(Vector4 p) =>
        Vector4.Normalize(p - Vector3.Zero.AsPosition());

    public override IEnumerable<Intersection> Intersect(Ray ray)
    {
        var sphereToRay =
            ray.Origin - new Vector3(0, 0, 0).AsPosition();
        
        var a = Vector4.Dot(ray.Direction, ray.Direction);
        var b = 2 * Vector4.Dot(ray.Direction, sphereToRay);
        var c = Vector4.Dot(sphereToRay, sphereToRay) - 1;
        
        var d = b * b - 4 * a * c;
        if (d < 0)
        {
            return new SortedSet<Intersection>();
        }

        var t1 = (-b - MathF.Sqrt(d)) / (2 * a);
        var t2 = (-b + MathF.Sqrt(d)) / (2 * a);
        var xs = new[]
        {
            new Intersection(t1),
            new Intersection(t2),
        };

        return new SortedSet<Intersection>(xs);
    }
}