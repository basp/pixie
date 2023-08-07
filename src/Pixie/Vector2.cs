namespace Pixie;

/// <summary>
/// Represents a direction in two-dimensional space.
/// </summary>
/// <typeparam name="T">
/// The type of elements in the vector. <c>T</c> can be any primitive numeric
/// type.
/// </typeparam>
public readonly struct Vector2<T> :
    IEquatable<Vector2<T>>,
    IFormattable
    where T : INumber<T>
{
    public readonly T X, Y;

    public Vector2()
        : this(T.Zero)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Vector2{T}"/> object whose two elements have
    /// the same value. 
    /// </summary>
    /// <param name="v">
    /// The value to assign to both elements.
    /// </param>
    public Vector2(T v)
        : this(v, v)
    {
    }

    /// <summary>
    /// Creates a vector whose elements have the specified values.
    /// </summary>
    /// <param name="x">
    /// The value to assign to the <see cref="X"/> field.
    /// </param>
    /// <param name="y">
    /// The value to assign to the <see cref="Y"/> field.
    /// </param>
    public Vector2(T x, T y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="index">
    /// The index of the element to get.
    /// </param>
    public T this[int index] =>
        index switch
        {
            0 => this.X,
#if DEBUG
            1 => this.Y,
            _ => throw new IndexOutOfRangeException(nameof(index)),
#else
            _ => this.Y,
#endif
        };

    public bool Equals(Vector2<T> other) =>
        this.X == other.X &&
        this.Y == other.Y;

    public override bool Equals(object obj) =>
        obj is Vector2<T> other && this.Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(this.X, this.Y);

    public override string ToString() =>
        this.ToString(null, null);

    public string ToString(string format, IFormatProvider formatProvider) =>
        string.Format(
            "({0} {1})",
            this.X.ToString(format, formatProvider),
            this.Y.ToString(format, formatProvider));

    public static bool operator ==(Vector2<T> u, Vector2<T> v) =>
        u.Equals(v);

    public static bool operator !=(Vector2<T> u, Vector2<T> v) =>
        !(u == v);
}

public static class Vector2
{
    public static Vector2<T> Create<T>(T x, T y)
        where T : INumber<T> =>
        new(x, y);

    public static IEqualityComparer<Vector2<T>> GetComparer<T>(T atol)
        where T : INumber<T> =>
        new Vector2EqualityComparer<T>(atol);

    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of the
    /// specified vector's elements.
    /// </summary>
    /// <param name="u">
    /// A vector.
    /// </param>
    /// <typeparam name="T">
    /// The type of elements in the vector. <c>T</c> can be any primitive
    /// numeric type.
    /// </typeparam>
    /// <returns>
    /// The absolute value vector.
    /// </returns>
    public static Vector2<T> Abs<T>(Vector2<T> u)
        where T : INumber<T> =>
        new(
            T.Abs(u.X),
            T.Abs(u.Y));

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="u">
    /// The first vector to add.
    /// </param>
    /// <param name="v">
    /// The second vector to add.
    /// </param>
    /// <typeparam name="T">
    /// The type of elements in the vector. <c>T</c> can be any primitive
    /// numeric type.
    /// </typeparam>
    /// <returns>
    /// The summed vector.
    /// </returns>
    public static Vector2<T> Add<T>(Vector2<T> u, Vector2<T> v)
        where T : INumber<T> =>
        new(
            u.X + v.X,
            u.Y + v.Y);

    /// <summary>
    /// Subtracts the second vector from the first.
    /// </summary>
    /// <param name="u">
    /// The first vector.
    /// </param>
    /// <param name="v">
    /// The second vector.
    /// </param>
    /// <typeparam name="T">
    /// The type of elements int he vector. <c>T</c> can be any primitive
    /// numeric type.
    /// </typeparam>
    /// <returns>
    /// The difference vector.
    /// </returns>
    public static Vector2<T> Subtract<T>(Vector2<T> u, Vector2<T> v)
        where T : INumber<T> =>
        new(
            u.X - v.X,
            u.Y - v.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="a">A scalar.</param>
    /// <param name="u">A vector.</param>
    /// <typeparam name="T">
    /// The type of the scalar and elements in the vector.
    /// </typeparam>
    /// <returns>
    /// The scaled vector.
    /// </returns>
    public static Vector2<T> Multiply<T>(T a, Vector2<T> u)
        where T : INumber<T> =>
        new(
            a * u.X,
            a * u.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="u">A vector.</param>
    /// <param name="a">A scalar.</param>
    /// <typeparam name="T">
    /// The type of the scalar and elements in the vector.
    /// </typeparam>
    /// <returns>
    ///The scaled vector.
    /// </returns>
    public static Vector2<T> Multiply<T>(Vector2<T> u, T a)
        where T : INumber<T> => Vector2.Multiply(a, u);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="u">A vector.</param>
    /// <param name="a">A scalar.</param>
    /// <typeparam name="T">
    ///The type of the scalar and elements in the vector.
    /// </typeparam>
    /// <returns>
    /// The scaled vector.
    /// </returns>
    public static Vector2<T> Divide<T>(Vector2<T> u, T a)
        where T : IFloatingPointIeee754<T> =>
        new(
            u.X / a,
            u.Y / a);
}