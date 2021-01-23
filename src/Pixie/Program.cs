namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using PowerArgs;

    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class Program
    {
        [HelpHook]
        [ArgShortcut("-?")]
        [ArgDescription("Shows this help")]
        public static bool Help { get; set; }

        private static void Main(string[] args)
        {
            Console.WriteLine(banner);
            Args.InvokeAction<Program>(args);
        }

        [ArgActionMethod]
        public static void Render(RenderArgs args)
        {
            if (!TryLoadAssembly(args.Asm, out var asm))
            {
                Console.WriteLine($"Cannot load assembly {args.Asm}.");
                return;
            }

            if (!TryLoadScene(asm, args, out var scene))
            {
                Console.WriteLine(
                    $"Cannot find scene builder {args.Scene} in " +
                    $"assembly {args.Asm}.");

                var candidates = GetSceneBuilders(asm);
                Console.WriteLine("Possible candidates:");
                foreach (var c in candidates)
                {
                    Console.WriteLine($"- {c.FullName}");
                }

                return;
            }

            scene.ProgressMonitorFactory =
                (rows, _) => new ConsoleProgressMonitor(
                    rows,
                    $"rendering {args.Scene} from {Path.GetFileName(args.Asm)}");

            // scene.SamplerFactory =
            //     () => new RandomSuperSampler(scene.World, scene.Camera, args.N);

            scene.SamplerFactory =
                () => new FocalBlurSampler(
                            scene.World,
                            scene.Camera,
                            n: args.N,
                            focalDistance: 4.5,
                            aperture: 0.1);

            Console.WriteLine($"Super sampling:     {args.N}x");
            Console.WriteLine($"Output:             {Path.GetFullPath(args.Out)}");

            var sw = new Stopwatch();
            sw.Start();
            var img = scene.Render();
            img.SavePpm(args.Out);
            sw.Stop();

            var numberOfPixels = args.Width * args.Height;
            var pixelsPerSecond = Math.Round(
                (double)numberOfPixels / sw.Elapsed.TotalSeconds,
                digits: 2);

            Console.WriteLine($"Speed:              {pixelsPerSecond} px/s");
            Console.WriteLine($"---");
            Console.WriteLine($"Primary rays:       {Stats.PrimaryRay4s}");
            Console.WriteLine($"Secondary rays:     {Stats.SecondaryRay4s}");
            Console.WriteLine($"Shadow rays:        {Stats.ShadowRay4s}");
            Console.WriteLine($"---");
            Console.WriteLine($"Intersection tests: {Stats.Tests}");
            Console.WriteLine();
        }

        private static IEnumerable<Type> GetSceneBuilders(Assembly asm) => asm
            .GetExportedTypes()
            .Where(x => typeof(ISceneBuilder).IsAssignableFrom(x));

        private static bool TryLoadAssembly(string path, out Assembly asm)
        {
            try
            {
                asm = Assembly.LoadFile(path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                asm = null;
                return false;
            }
        }

        private static bool TryLoadScene(
            Assembly asm,
            RenderArgs args,
            out Scene scene)
        {
            var builderType = asm.GetType(args.Scene);
            if (builderType == null)
            {
                scene = null;
                return false;
            }

            var builder = (ISceneBuilder)Activator.CreateInstance(builderType);
            scene = builder.Build(args.Width, args.Height);
            return true;
        }

        private const string banner = @"
        .__       .__
______ |__|__  __|__| ____
\____ \|  \  \/  /  |/ __ \
|  |_> >  |>    <|  \  ___/
|   __/|__/__/\_ \__|\___  >
|__|            \/       \/ 0.8.1
";
    }
}
