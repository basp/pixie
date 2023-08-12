namespace Pixie;

public class Sphere : Shape
{
    public override Vector4 GetNormalAt(Vector4 p) =>
        Vector4.Normalize(p - Vector3.Zero.AsPosition());

    public override Option<Intersection> Intersect(Ray ray)
    {
        var sphereToRay =
            ray.Origin - new Vector3(0, 0, 0).AsPosition();
        
        var a = Vector4.Dot(ray.Direction, ray.Direction);
        var b = 2 * Vector4.Dot(ray.Direction, sphereToRay);
        var c = Vector4.Dot(sphereToRay, sphereToRay) - 1;
        
        var d = b * b - 4 * a * c;
        if (d < 0)
        {
            return Option.None<Intersection>();
        }

        var t1 = (-b - MathF.Sqrt(d)) / (2 * a);
        var t2 = (-b + MathF.Sqrt(d)) / (2 * a);

        if (t1 > t2)
        {
            (t1, t2) = (t2, t1);
        }

        if (t1 >= 0)
        {
            return Option.Some(new Intersection(t1));
        }

        if (t2 >= 0)
        {
            return Option.Some(new Intersection(t2));
        }

        return Option.None<Intersection>();
    }
}