namespace Pixie.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using PowerArgs;
    using Pixie.Core;
    using Pixie.Cmd.Examples;

    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class Program
    {
        [HelpHook]
        [ArgShortcut("-?")]
        [ArgDescription("Shows this help")]
        public static bool Help { get; set; }

        [ArgActionMethod]
        public static void Render(RenderArgs args)
        {
            var pixels = args.Width * args.Height;
            // var t = Sandbox.Create(args.Width, args.Height);
            // var t = Test01.Create(args.Width, args.Height);
            var t = Cover.Create(args.Width, args.Height);
            var sampler = new RandomSuperSampler(t.Item1, t.Item2, n: 8);
            var sw = new Stopwatch();
            sw.Start();
            var img = t.Item2.Render(t.Item1, sampler);
            img.SavePpm(args.Out);
            sw.Stop();

            var stats = Camera.Stats;

            Console.WriteLine($"{sw.Elapsed}");
            Console.WriteLine($"{(double)pixels / sw.ElapsedMilliseconds}px/ms");
            Console.WriteLine($"Intersection tests: {stats.Tests}");
            Console.WriteLine($"Primary rays:       {stats.PrimaryRays}");
            Console.WriteLine($"Secondary rays:     {stats.SecondaryRays}");
            Console.WriteLine($"Shadow rays:        {stats.ShadowRays}"); 
            Console.WriteLine($"Output:             {Path.GetFullPath(args.Out)}");
        }

        static void Main(string[] args)
        {
            Args.InvokeAction<Program>(args);
        }
    }
}
