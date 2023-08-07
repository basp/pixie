﻿namespace Pixie;

public abstract class Canvas<T>
    where T : INumber<T>
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
        get => this.data[y * this.Width + x];
        set => this.data[y * this.Width + x] = value;
    }

    public int Width { get; }

    public int Height { get; }

    public abstract void Write(Stream s);
}