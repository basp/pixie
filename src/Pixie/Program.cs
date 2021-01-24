namespace Pixie
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Linie;
    using PowerArgs;
    using Serilog;

    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class Program
    {
        [HelpHook]
        [ArgShortcut("-?")]
        [ArgDescription("Shows this help")]
        public static bool Help { get; set; }

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Console.WriteLine(banner);

            Args.InvokeAction<Program>(args);
        }

        [ArgActionMethod]
        [ArgDescription("Test 1")]
        public static void Test1()
        {
            var sphere = new Sphere()
            {
            };

            var ray = new Ray4(
                Vector4.CreatePosition(0, 0, -2),
                Vector4.CreateDirection(0, 0, 1));

            var xs = sphere.Intersect(ray);
            foreach (var ix in xs)
            {
                Console.WriteLine(ix);
            }
        }

        [ArgActionMethod]
        [ArgDescription("Test 2")]
        public static void Test2()
        {
            var world = new World
            {
                Sphere = new Sphere
                {
                    Transform = Transform.Scale(85, 85, 85),
                }
            };

            var tracer = new SingleSphereTracer(world);

            const int hres = 200;
            const int vres = 200;
            const double s = 1.0;
            const double zw = -100;
            var canvas = new Canvas(hres, vres);
            var d = Vector4.CreateDirection(0, 0, 1);
            for (var r = 0; r < vres; r++)
            {
                for (var c = 0; c < hres; c++)
                {
                    var x = s * (c - 0.5 * (hres - 1));
                    var y = s * (r - 0.5 * (vres - 1));
                    var o = Vector4.CreatePosition(x, y, zw);
                    var ray = new Ray4(o, d);
                    canvas[c, r] = tracer.Trace(ray);
                }
            }

            canvas.SavePpm(@"out.ppm");
        }

        static World Build()
        {
            var s1 = new Sphere
            {
                Transform = Transform
                    .Scale(80, 80, 80)
                    .Translate(0, -25, 0),

                Color = new Color(1, 0, 0),
            };

            var s2 = new Sphere
            {
                Transform = Transform
                    .Scale(60, 60, 60)
                    .Translate(0, 30, 0),

                Color = new Color(1, 1, 0),
            };

            var p = new Plane
            {
                Transform = Transform
                    .RotateX(-Math.PI / 4),

                Color = new Color(0, 0.3, 0),
            };

            var w = new World();
            w.Objects.Add(s1);
            w.Objects.Add(s2);
            w.Objects.Add(p);

            return w;
        }

        [ArgActionMethod]
        [ArgDescription("Test 3")]
        public static void Test3()
        {
            const int hres = 200;
            const int vres = 200;
            const double s = 1.0;
            const double zw = -100;

            var world = Build();
            var tracer = new MultipleObjectsTracer(world);
            var canvas = new Canvas(hres, vres);
            var d = Vector4.CreateDirection(0, 0, 1);
            for (var r = 0; r < vres; r++)
            {
                for (var c = 0; c < hres; c++)
                {
                    var x = s * (c - 0.5 * (hres - 1));
                    var y = s * (r - 0.5 * (vres - 1));
                    var o = Vector4.CreatePosition(x, y, zw);
                    var ray = new Ray4(o, d);
                    canvas[c, r] = tracer.Trace(ray);
                }
            }

            canvas.SavePpm(@"out.ppm");
        }

        [ArgActionMethod]
        [ArgDescription("Regular sampling")]
        public static void Test4()
        {
            const int hres = 200;
            const int vres = 200;
            const double s = 1.0;
            const double zw = -100;
            const int numberOfSamples = 25;

            var n = (int)Math.Sqrt(numberOfSamples);
            var world = Build();
            var tracer = new MultipleObjectsTracer(world);
            var canvas = new Canvas(hres, vres);
            var d = Vector4.CreateDirection(0, 0, 1);
            for (var r = 0; r < vres; r++)
            {
                for (var c = 0; c < hres; c++)
                {
                    var color = new Color(0);
                    for (var p = 0; p < n; p++)
                    {
                        for (var q = 0; q < n; q++)
                        {
                            var x = s * (c - 0.5 * hres + (q + 0.5) / n);
                            var y = s * (r - 0.5 * vres + (p + 0.5) / n);
                            var o = Vector4.CreatePosition(x, y, zw);
                            var ray = new Ray4(o, d);
                            color += tracer.Trace(ray);
                        }
                    }

                    color = color / numberOfSamples;
                    canvas[c, r] = color;
                }
            }

            canvas.SavePpm(@"out.ppm");
        }

        [ArgActionMethod]
        [ArgDescription("Random sampling")]
        public static void Test5()
        {
            const int hres = 200;
            const int vres = 200;
            const double s = 1.0;
            const double zw = -100;
            const int numberOfSamples = 5 * 5;

            var rng = new Random();
            var n = (int)Math.Sqrt(numberOfSamples);
            var world = Build();
            var tracer = new MultipleObjectsTracer(world);
            var canvas = new Canvas(hres, vres);
            var d = Vector4.CreateDirection(0, 0, 1);
            for (var r = 0; r < vres; r++)
            {
                for (var c = 0; c < hres; c++)
                {
                    var color = new Color(0);
                    for (var p = 0; p < n; p++)
                    {
                        for (var q = 0; q < n; q++)
                        {
                            var x = s * (c - 0.5 * hres + rng.NextDouble());
                            var y = s * (r - 0.5 * vres + rng.NextDouble());
                            var o = Vector4.CreatePosition(x, y, zw);
                            var ray = new Ray4(o, d);
                            color += tracer.Trace(ray);
                        }
                    }

                    color = color / numberOfSamples;
                    canvas[c, r] = color;
                }
            }

            canvas.SavePpm(@"out.ppm");
        }

        [ArgActionMethod]
        [ArgDescription("Jittered sampling")]
        public static void Test6()
        {
            const int hres = 200;
            const int vres = 200;
            const double s = 1.0;
            const double zw = -100;
            const int numberOfSamples = 5 * 5;

            var rng = new Random();
            var n = (int)Math.Sqrt(numberOfSamples);
            var world = Build();
            var tracer = new MultipleObjectsTracer(world);
            var canvas = new Canvas(hres, vres);
            var d = Vector4.CreateDirection(0, 0, 1);
            for (var r = 0; r < vres; r++)
            {
                for (var c = 0; c < hres; c++)
                {
                    var color = new Color(0);
                    for (var p = 0; p < n; p++)
                    {
                        for (var q = 0; q < n; q++)
                        {
                            var x = s * (c - 0.5 * hres + (q + rng.NextDouble()) / n);
                            var y = s * (r - 0.5 * vres + (p + rng.NextDouble()) / n);
                            var o = Vector4.CreatePosition(x, y, zw);
                            var ray = new Ray4(o, d);
                            color += tracer.Trace(ray);
                        }
                    }

                    color = color / numberOfSamples;
                    canvas[c, r] = color;
                }
            }

            canvas.SavePpm(@"out.ppm");
        }

        [ArgActionMethod]
        [ArgDescription("Sampler")]
        public static void Test7()
        {
            const int hres = 200;
            const int vres = 200;
            const double s = 1.0;
            const double zw = -100;
            const int numberOfSamples = 5 * 5;

            var world = Build();
            var tracer = new MultipleObjectsTracer(world);
            // var sampler = new JitteredSampler(numberOfSamples);
            var sampler = new DefaultSampler(16);
            var canvas = new Canvas(hres, vres);
            var d = Vector4.CreateDirection(0, 0, 1);
            for (var r = 0; r < vres; r++)
            {
                for (var c = 0; c < hres; c++)
                {
                    var color = new Color(0);
                    for (var j = 0; j < numberOfSamples; j++)
                    {
                        var sp = sampler.SampleUnitSquare();
                        var x = s * (c - 0.5 * hres + sp.X);
                        var y = s * (r - 0.5 * vres + sp.Y);
                        var o = Vector4.CreatePosition(x, y, zw);
                        var ray = new Ray4(o, d);
                        color += tracer.Trace(ray);
                    }

                    color = color / numberOfSamples;
                    canvas[c, r] = color;
                }
            }

            canvas.SavePpm(@"out.ppm");
        }

        [ArgActionMethod]
        [ArgDescription("Render an image")]
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
