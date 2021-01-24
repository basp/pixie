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
const double s = 1.0;
const double zw = -100;
```

TODO