namespace Pixie.Cmd
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using PowerArgs;
    using Examples;

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
            // var t = Test01.Create(args.Width, args.Height);
            // var t = Test02.Create(args.Width, args.Height);
            // var t = Test03.Create(args.Width, args.Height);
            // var t = Test04.Create(args.Width, args.Height);
            var t = Cover.Create(args.Width, args.Height);

            var world = t.Item1;
            var camera = t.Item2;

            // Func<ISampler> samplerFactory = () =>
            //     new RandomSuperSampler(world, camera, args.N);

            // Func<ISampler> samplerFactory = () =>
            //     new FocalBlurSampler(t.Item1, t.Item2, 10.96, 0.11, args.N);

            Func<ISampler> samplerFactory = () =>
                new DefaultSampler(world, camera);

            t.Item2.ProgressMonitorFactory = (rows, _) => 
                new ProgressBarProgressMonitor(rows);

            var sw = new Stopwatch();
            sw.Start();
            var img = t.Item2.Render(t.Item1, samplerFactory);

            img.SavePpm(args.Out);
            sw.Stop();

            Console.WriteLine($"{sw.Elapsed}");
            Console.WriteLine($"{(double)pixels / sw.ElapsedMilliseconds}px/ms");
            Console.WriteLine($"Intersection tests: {Stats.Tests}");
            Console.WriteLine($"Primary rays:       {Stats.PrimaryRays}");
            Console.WriteLine($"Secondary rays:     {Stats.SecondaryRays}");
            Console.WriteLine($"Shadow rays:        {Stats.ShadowRays}");
            Console.WriteLine($"Super sampling:     {args.N}x");
            Console.WriteLine($"Output:             {Path.GetFullPath(args.Out)}");
        }

        static void Main(string[] args)
        {
            Args.InvokeAction<Program>(args);
        }
    }
}
