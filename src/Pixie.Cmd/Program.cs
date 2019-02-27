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
            // var t = Cover.Create(args.Width, args.Height);
            var t = Test01.Create(args.Width, args.Height);

            var sw = new Stopwatch();
            sw.Start();
            var img = t.Item2.Render(
                t.Item1,
                () => new FocalBlurSampler(t.Item1, t.Item2, 2.305, 0.09, 100));

            img.SavePpm(args.Out);
            sw.Stop();

            Console.WriteLine($"{sw.Elapsed}");
            Console.WriteLine($"{(double)pixels / sw.ElapsedMilliseconds}px/ms");
            Console.WriteLine($"Intersection tests: {Stats.Tests}");
            Console.WriteLine($"Primary rays:       {Stats.PrimaryRays}");
            Console.WriteLine($"Secondary rays:     {Stats.SecondaryRays}");
            Console.WriteLine($"Shadow rays:        {Stats.ShadowRays}");
            Console.WriteLine($"Output:             {Path.GetFullPath(args.Out)}");
        }

        static void Main(string[] args)
        {
            Args.InvokeAction<Program>(args);
        }
    }
}
