﻿namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using Pixie.Core;

    class Program
    {
        static void Main(string[] args)
        {
            const int width = 1280;
            const int height = 1024;

            var t = Scenes.Example15(width, height);

            var sw = new Stopwatch();
            sw.Start();
            var img = t.Item2.Render(t.Item1);
            const string path = @"D:\temp\test.ppm";
            img.SavePpm(path);
            sw.Stop();

            const int nPixels = width * height;
            Console.WriteLine($"Total: {sw.Elapsed})");
            Console.WriteLine($"{nPixels / sw.ElapsedMilliseconds}px/ms");
            Console.WriteLine($"{Stats.Tests} intersection tests");
            Console.WriteLine(path);
        }
    }
}
