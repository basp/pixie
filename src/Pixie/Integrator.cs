namespace Pixie;

public abstract class Integrator
{
    protected readonly Primitive aggregate;

    protected Integrator(Primitive aggregate) // TODO: lights
    {
        this.aggregate = aggregate;
    }
    
    public abstract Color<double> Li(Ray ray);
}