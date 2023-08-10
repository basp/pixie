﻿namespace Pixie;

public readonly struct Intersection : IComparable<Intersection>
{
    public Intersection(float t, Primitive obj)
    {
        this.T = t;
        this.Obj = obj;
    }

    public float T { get; }

    public Primitive Obj { get; }

    public int CompareTo(Intersection other) =>
        this.T.CompareTo(other.T);
}

public static class IntersectionExtensions
{
    public static Option<Intersection> GetHit(this IEnumerable<Intersection> xs)
    {
        var list = xs
            .Where(y => y.T >= 0)
            .ToList();

        if (list.Count == 0)
        {
            return Option.None<Intersection>();
        }

        list.Sort();
        return Option.Some(list[0]);
    }
}