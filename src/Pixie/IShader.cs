namespace Pixie
{
    using Linie;

    public interface IShader
    {
        Color Render(Interaction @int, int depth);
    }
}