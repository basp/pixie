namespace Pixie.Cmd
{
    using System;
    using Pixie.Core;

    class Program
    {
        const string Banner = @"Pixie 0.0.1";

        static void Main(string[] args)
        {
            Console.WriteLine(Banner);

            var sphere = new Sphere();

            sphere.Material = 
                new Material
                {
                    Color = new Color(1, 0.2f, 1),
                };

            var lightPosition = Float4.Point(-10, 10, -10);
            var lightColor = new Color(1, 1, 1);
            var light = new PointLight(lightPosition, lightColor);
       }
    }
}
