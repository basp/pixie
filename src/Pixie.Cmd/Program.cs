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
            World world = null;
            Camera camera = null;

            var scene = new Scene(world, camera)
            {
                ProgressMonitorFactory =
                    (rows, _cols) => new ProgressBarProgressMonitor(rows),
            };

            var sw = new Stopwatch();
            sw.Start();
            var img = scene.Render();
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
            Console.WriteLine(@"Pixie v1.0");
            Args.InvokeAction<Program>(args);
        }
    }
}
