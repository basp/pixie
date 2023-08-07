namespace Pixie;

/// <summary>
/// Represents an x- and y-coordinate in two-dimensional space.
/// </summary>
/// <typeparam name="T">
/// The type of elements in the point. <c>T</c> can be any primitive numeric
/// type.
/// </typeparam>
public readonly struct Point2<T> :
    IEquatable<Point2<T>>
    where T : INumber<T>
{
    public readonly T X, Y;

    /// <summary>
    /// Creates a 2D point whose elements have the specified values.
    /// </summary>
    /// <param name="x">
    /// The value to assign to the <see cref="X"/> field.
    /// </param>
    /// <param name="y">
    /// The value to assign to the <see cref="Y"/> field.
    /// </param>
    public Point2(T x, T y)
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
            _ => this.Y,
        };

    /// <summary>
    /// Returns a value that indicates whether this instance and another
    /// <see cref="Point2{T}"/> are equal.
    /// </summary>
    /// <param name="other">The other point.</param>
    /// <returns>
    /// <c>true</c> if the two points are equal, otherwise <c>false</c>.
    /// </returns>
    public bool Equals(Point2<T> other) =>
        this.X == other.X &&
        this.Y == other.Y;
}

public static class Point2
{
    public static Point2<T> Create<T>(T x, T y)
        where T : INumber<T> =>
        new(x, y);

    /// <summary>
    /// Adds a vector to a point.
    /// </summary>
    /// <param name="a">
    /// A point.
    /// </param>
    /// <param name="u">
    /// A vector.
    /// </param>
    /// <typeparam name="T">
    /// The type of elements in the point and vector.
    /// </typeparam>
    /// <returns>
    /// A new point created by adding the vector <c>u</c> to point <c>a</c>.
    /// </returns>
    public static Point2<T> Add<T>(Point2<T> a, Vector2<T> u)
        where T : INumber<T> =>
        new(
            a.X + u.X,
            a.Y + u.Y);

    /// <summary>
    /// Subtracts a vector from a point.
    /// </summary>
    /// <param name="a">A point.</param>
    /// <param name="u">A vector.</param>
    /// <typeparam name="T">
    /// The type of elements in the point and vector.
    /// </typeparam>
    /// <returns>
    /// A new point created by subtracting the vector <c>u</c> from point <c>a</c>.
    /// </returns>
    public static Point2<T> Subtract<T>(Point2<T> a, Vector2<T> u)
        where T : INumber<T> =>
        new(
            a.X - u.X,
            a.Y - u.Y);
}