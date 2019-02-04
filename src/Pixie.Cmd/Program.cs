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
            var canvas = CanvasExamples.Example1();
            canvas.SavePpm(@"d:\temp\frotz.ppm");
        }
    }
}
