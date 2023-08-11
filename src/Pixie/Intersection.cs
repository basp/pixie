namespace Pixie;

public struct Intersection : 
    IEquatable<Intersection>,
    IComparable<Intersection>
{
    public Intersection(float t)
    {
        this.T = t;
        this.Material = Option.None<Material>();
    }

    public Intersection(float t, Option<Material> material)
    {
        this.T = t;
        this.Material = material;
    }

    public float T { get; }
    
    public Option<Material> Material { get; set; }

    public int CompareTo(Intersection other) =>
        this.T.CompareTo(other.T);

    public bool Equals(Intersection other) =>
        this.T.Equals(other.T) &&
        this.Material.Equals(other.Material);
}