namespace Pixie.Cmd
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using PowerArgs;

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
            var numberOfPixels = args.Width * args.Height;
            var (world, camera) = Cover.Create(args.Width, args.Height);
            var scene = new Scene(world, camera)
            {
                ProgressMonitorFactory =
                    (rows, _cols) => new ProgressBarProgressMonitor(rows),

                SamplerFactory =
                    () => new RandomSuperSampler(world, camera, n: args.N),
            };

            var sw = new Stopwatch();
            sw.Start();
            var img = scene.Render();
            img.SavePpm(args.Out);
            sw.Stop();

            var rate = Math.Round(
                (double)numberOfPixels / sw.ElapsedMilliseconds,
                digits: 2);

            Console.WriteLine($"Speed:              {rate} px/ms");
            Console.WriteLine($"---");
            Console.WriteLine($"Super sampling:     {args.N}x");
            Console.WriteLine($"Output:             {Path.GetFullPath(args.Out)}");
            Console.WriteLine($"---");
            Console.WriteLine($"Primary rays:       {Stats.PrimaryRays}");
            Console.WriteLine($"Secondary rays:     {Stats.SecondaryRays}");
            Console.WriteLine($"Shadow rays:        {Stats.ShadowRays}");
            Console.WriteLine($"---");
            Console.WriteLine($"Intersection tests: {Stats.Tests}");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine(BANNER);
            Args.InvokeAction<Program>(args);
        }

        private const string BANNER = @"
        .__       .__        
______ |__|__  __|__| ____  
\____ \|  \  \/  /  |/ __ \ 
|  |_> >  |>    <|  \  ___/ 
|   __/|__/__/\_ \__|\___  >
|__|            \/       \/ 0.8
        ";
    }
}
