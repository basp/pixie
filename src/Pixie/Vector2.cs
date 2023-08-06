namespace Pixie;

/// <summary>
/// Represents a vector of two elements of a specified numeric type.
/// </summary>
/// <typeparam name="T">
/// The type of the elements in the vector. <c>T</c> can be any primitive
/// numeric type.
/// </typeparam>
public readonly struct Vector2<T> :
    IEquatable<Vector2<T>>
    where T : IFloatingPointIeee754<T>
{
    public readonly T X, Y;

    /// <summary>
    /// Creates a vector whose elements have the specified values.
    /// </summary>
    /// <param name="x">The value to assign to the <see cref="X"/> field.</param>
    /// <param name="y">The value to assign to the <see cref="Y"/> field.</param>
    public Vector2(T x, T y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">The index of the element to get.</param>
    public T this[int index] =>
        index switch
        {
            0 => this.X,
            _ => this.Y,
        };

    public static bool operator ==(Vector2<T> left, Vector2<T> right) =>
        left.Equals(right);

    public static bool operator !=(Vector2<T> left, Vector2<T> right) =>
        !(left == right);

    public static Vector2<T> operator -(Vector2<T> u) =>
        new(
            -u.X,
            -u.Y);

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="u">The first vector to add.</param>
    /// <param name="v">The second vector to add.</param>
    /// <returns>The summed vector.</returns>
    public static Vector2<T> operator +(Vector2<T> u, Vector2<T> v) =>
        new(u.X + v.X, u.Y + v.Y);

    public static Vector2<T> operator -(Vector2<T> u, Vector2<T> v) =>
        new(u.X - v.X, u.Y - v.Y);

    public static Vector2<T> operator *(Vector2<T> u, T s) =>
        new(u.X * s, u.Y * s);

    public T Dot(Vector2<T> other) => Vector2.Dot(this, other);

    public T LengthSquared() => Vector2.Dot(this, this);

    public Vector2<U> Map<U>(Func<T, U> f)
        where U : IFloatingPointIeee754<U> =>
        new(
            f(this.X),
            f(this.Y));

    /// <summary>
    /// Deconstructs the components of this instance into separate variables.
    /// </summary>
    /// <param name="x">
    /// When this method returns, contains the value of the <c>x</c> component.
    /// </param>
    /// <param name="y">
    /// When this method returns, contains the value of the <c>y</c> component.
    /// </param>
    public void Deconstruct(out T x, out T y)
    {
        x = this.X;
        y = this.Y;
    }

    /// <summary>
    /// Returns a value that indicates whether this instance and another vector
    /// are equal.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>
    /// <c>true</c> if the two vectors are equal, otherwise <c>false</c>.
    /// </returns>
    public bool Equals(Vector2<T> other) =>
        this.X == other.X &&
        this.Y == other.Y;

    /// <summary>
    /// Returns a value that indicates whether this instance and a specified
    /// object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <c>true</c> if the current instance and <c>obj</c> are equal; otherwise, <c>false</c>. If <c>obj</c> is
    /// <c>null</c>, the method returns <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The current instance and <c>obj</c> are equal if <c>obj</c> is a <see cref="Vector2"/> object and their
    /// <see cref="X"/> and <see cref="Y"/> elements are equal.
    /// </remarks>
    public override bool Equals([NotNullWhen(true)] object obj) =>
        obj is Vector2<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y);

    public override string ToString() =>
        $"({this.X} {this.Y})";
}

public static class Vector2
{
    public static Vector2<T> Create<T>(T x, T y)
        where T : IFloatingPointIeee754<T> =>
        new(x, y);

    public static Vector2<T> Add<T>(Vector2<T> u, Vector2<T> v)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X + v.X,
            u.Y + v.Y);

    public static Vector2<T> Subtract<T>(Vector2<T> u, Vector2<T> v)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X - v.X,
            u.Y - v.Y);

    public static Vector2<T> Multiply<T>(T a, Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        new(
            a * u.X,
            a * u.Y);

    public static Vector2<T> Multiply<T>(Vector2<T> u, T a)
        where T : IFloatingPointIeee754<T> => Vector2.Multiply(a, u);

    public static Vector2<T> Divide<T>(Vector2<T> u, T a)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X / a, 
            u.Y / a);

    public static T Magnitude<T>(Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        T.Sqrt(MagnitudeSquared(u));

    public static T MagnitudeSquared<T>(Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        Vector2.Dot(u, u);
    
    public static T Dot<T>(Vector2<T> u, Vector2<T> v)
        where T : IFloatingPointIeee754<T> =>
        u.X * v.X +
        u.Y * v.Y;

    public static Vector2<T> Negate<T>(Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        new(-u.X, -u.Y);

    public static Vector2<T> Normalize<T>(Vector2<T> u)
        where T : IFloatingPointIeee754<T> =>
        Vector2.Divide(u, Vector2.Magnitude(u));

    public static IEqualityComparer<Vector2<T>> GetEqualityComparer<T>(T atol)
        where T : IFloatingPointIeee754<T> =>
        new Vector2EqualityComparer<T>(atol);
}
