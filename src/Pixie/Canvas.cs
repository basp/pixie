namespace Pixie;

public abstract class Canvas<T> where T : INumber<T>
{
    private readonly Color<T>[] data;

    protected Canvas(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.data = new Color<T>[height * width];
    }

    public Color<T> this[int x, int y]
    {
#if RELEASE
        get => this.data[y * this.Width + x];
        set => this.data[y * this.Width + x] = value;
#else
        get
        {
            if (x < 0 || x >= this.Width)
            {
                throw new IndexOutOfRangeException(nameof(x));
            }

            if (y < 0 || y >= this.Height)
            {
                throw new IndexOutOfRangeException(nameof(y));
            }

            return this.data[(y * this.Width) + x];
        }
        set
        {
            if (x < 0 || x >= this.Width)
            {
                throw new IndexOutOfRangeException(nameof(x));
            }

            if (y < 0 || y >= this.Height)
            {
                throw new IndexOutOfRangeException(nameof(y));
            }

            this.data[(y * this.Width) + x] = value;
        }
#endif
    }

    public int Width { get; }

    public int Height { get; }

    public abstract void Write(Stream s);
}