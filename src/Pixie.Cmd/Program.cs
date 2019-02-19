namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using Humanizer;
    using Pixie.Core;

    class Program
    {
        static void Main(string[] args)
        {
            const int width = 1280;
            const int height = 1024;

            // var t = Cover.Create(width, height);
            var t = Sandbox.Create(width, height);

            var sw = new Stopwatch();
            sw.Start();
            var img = t.Item2.Render(t.Item1);
            const string path = @"D:\temp\test.ppm";
            img.SavePpm(path);
            sw.Stop();

            const int nPixels = width * height;
            Console.WriteLine($"Total: {sw.Elapsed})");
            Console.WriteLine($"{width} * {height} / {sw.ElapsedMilliseconds} = {(double)nPixels / sw.ElapsedMilliseconds}px/ms");
            Console.WriteLine($"{Stats.Tests} intersection tests");
            Console.WriteLine(path);
        }
    }
}
