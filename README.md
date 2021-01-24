# Pixie
This is the next version of Pixie. The primary focus is on improving sampler
support. Secondary are general architecture and code clarity without sacrificing
too much performance.

## Features
* Pixie is now a single assembly
* Improved sampler support
* Imporved camera support
* General improved architecture
* Uses `Linie` ray tracing kernel

## Tutorial
There are no formal cameras yet for this version. However, that should not stop anybody from rendering images. In fact, clients will now have much more control over how their images are rendered.

The easiest way to get to grasp with Pixie is to render basic circle from a sphere.

A sphere is simple enough but all shapes in Pixie are defined at the origin and defined in unit terms. They will then need to be transformed into their world scale and position. In this case we will keep the center of the sphere at the origin but make it a lot bigger.
```
var sphere = new Sphere
{
    Transform = Transform.Scale(85),
};
```

This will scale the sphere by a factor of `85` in all dimensions while keeping the center at the origin.

So now that we have a nice sphere setup we need some kind of camera. Here are some basic constants to define an orthographic camera.
```
const int hres = 200;
const int vres = 200;
const double s = 1.0;       // pixel size
const double zw = -100;     // camera z coordinate
```

We need a few more things before we can start our algorithm.
```
// the world object contains the sphere object
var world = Build();

// grabs the known sphere from the world to trace it
var tracer = new SingleSphereTracer(world);

// default sampler will just fire a single ray per pixel
var sampler = new DefaultSampler();

// holds the image
var canvas = new Canvas(hres, vres);

// all our camera rays will be orthogonal
var d = Vector4.CreateDirection(0, 0, 1);
```

Amd with all this in place we can perform a basic ray tracing algorithm:
```
for (var r = 0; r < vres; r++)
{
    for (var c = 0; c < hres; c++)
    {
        // for every pixel we start with black
        var color = new Color(0);

        // integrate over the number of samples
        // using unit square sampling
        for (var j = 0; j < numberOfSamples; j++)
        {
            // get sampling point offsets
            var sp = sampler.SampleUnitSquare();

            // calculate world x and y coordinates
            var x = s * (c - 0.5 * hres + sp.X);
            var y = s * (r - 0.5 * vres + sp.Y);

            // shoot ray
            var o = Vector4.CreatePosition(x, y, zw);
            var ray = new Ray4(o, d);
            color += tracer.Trace(ray);
        }

        // we take a number of samples by calculating 
        // subpixel x and y positions and averaging
        // color values
        color = color / numberOfSamples;
        canvas[c, r] = color;
    }
}
```