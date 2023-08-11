namespace Pixie;

public abstract class Canvas
{
    private readonly Vector3[] data;

    protected Canvas(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.data = new Vector3[height * width];
    }

    public Vector3 this[int x, int y]
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

            return this.data[y * this.Width + x];
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

            this.data[y * this.Width + x] = value;
        }
#endif
    }

    public int Width { get; }

    public int Height { get; }

    public abstract void Write(Stream s);
}