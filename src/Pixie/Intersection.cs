namespace Pixie;

public struct Intersection :
    IEquatable<Intersection>,
    IComparable<Intersection>
{
    public Intersection(float t)
    {
        this.T = t;
        this.Interaction = Option.None<Interaction>();
    }

    public Intersection(float t, Material material)
    {
        this.T = t;
        this.Interaction = Option.Some(
            new Interaction
            {
                Material = material,
            });
    }

    public float T { get; }

    public Option<Interaction> Interaction { get; set; }

    public int CompareTo(Intersection other) =>
        this.T.CompareTo(other.T);

    public bool Equals(Intersection other) =>
        // By definition, intersections are associated with a single
        // ray so we can just compare the T value to see if they
        // are equal.
        this.T.Equals(other.T);

    public override bool Equals(object obj) =>
        obj is Intersection intersection && this.Equals(intersection);

    public override int GetHashCode() =>
        HashCode.Combine(
            this.T,
            this.Interaction);

    public static bool operator ==(Intersection left, Intersection right) =>
        left.Equals(right);

    public static bool operator !=(Intersection left, Intersection right) =>
        !left.Equals(right);
}