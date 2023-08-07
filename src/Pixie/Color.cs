namespace Pixie;

public readonly struct Color<T> :
    IEquatable<Color<T>>
    where T : INumber<T>
{
    public readonly T R, G, B;

    public Color(T v)
        : this(v, v, v)
    {
    }

    public Color(T r, T g, T b)
    {
        this.R = r;
        this.G = g;
        this.B = b;
    }

    public T this[int i] =>
        i switch
        {
            0 => this.R,
            1 => this.G,
            _ => this.B,
        };

    public static bool operator ==(Color<T> left, Color<T> right) =>
        left.Equals(right);

    public static bool operator !=(Color<T> left, Color<T> right) =>
        !(left == right);

    public void Deconstruct(out T r, out T g, out T b)
    {
        r = this.R;
        g = this.G;
        b = this.B;
    }

    public Color<U> Map<U>(Func<T, U> f)
        where U : INumber<U> =>
        new(
            f(this.R),
            f(this.G),
            f(this.B));

    public Color<T> Clamp(Color<T> min, Color<T> max) =>
        Color.Clamp(this, min, max);

    public bool Equals(Color<T> other) =>
        this.R == other.R &&
        this.G == other.G &&
        this.B == other.B;

    public override bool Equals(object obj) =>
        obj is Color<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.R, this.G, this.B);
}

public static class Color
{
    public static Color<T> Clamp<T>(Color<T> a, Color<T> min, Color<T> max)
        where T : INumber<T> =>
        new(
            T.Clamp(a.R, min.R, max.R),
            T.Clamp(a.G, min.G, max.G),
            T.Clamp(a.B, min.B, max.B));
}