# pixie
Basic .NET ray-tracer implementation as specified by 
the awesome [The Ray Tracer Challenge](https://pragprog.com/book/jbtracer/the-ray-tracer-challenge) book.

Pixie is designed to be used from the API level. That means there is no scene
description language support for now. Although Pixie isn't designed for speed for speed first, it is not a slouch. It will hapilly put all your cores to full use.

## reference image
The image below is the reference image from the cover of the book as rendered by Pixie. Note that it's currently incorrect due to an error in the translation of the scene code, not due to an error in Pixie itself.
![reference image](https://i.imgur.com/jszWCMD.png)

## geometry
Pixie does not really distinguish between points and vectors on a low level. This allows for some freedom but also for some mishaps. For instance, it is very much possible to perform an addition operation on two points even though this makes little sense from a math perspective.

Both points and vectors are represented as instances of `Double4` structures. In the case of points, the *w* component equals one and otherwise zero. It is recommended to use the `Double4.Vector` and `Double4.Point` factory methods to create values of these kinds.

Points and vectors in Pixie support all the operations that you would expect to perform on them (and a little bit more as mentioned above). 

Such as for example:
```
var p0 = Double4.Point(0, 0, 0);
var p1 = Double4.Point(1, 0, 1.5);
var v = p1 - p0;
var v2 = v * 2;
var vn = v2.Normalize();
var vv = v.Dot(v);
```

## shapes
Pixie supports all the usual suspects:

* sphere
* plane
* cube
* cylinder
* cone
* triangle

As well as composites such as *constructive solid geometry* (CSG) and group 
shapes.

## shapes
To create a shape, we just invoke its constructor:
```
var sphere = new Sphere();
var box = new Cube();
var plane = new Plane();
```

Note that in general, these shapes do not take any arguments. In order to scale 
or translate them into place we need to apply a *transformation matrix* to them.
```
var plane = new Plane();
var sphere = new Sphere()
{
    Transform = Transform.Translate(0, 1, 0),
};
```

Since all these shapes are of *unit dimension* and centered around the origin we 
can easily transform them to the place where we want them to sit in the world. The matrix above will lift the sphere (with radius 1) above the plane.

## light
Pixie has `PointLight` and `AreaLight` sources but can be easily extended using the `ILightSource` and `ILight` interfaces.
```
var light = new PointLight(
    Double4.Point(10, 10, -10),
    new Color(1, 1, 1));
```

Area lights are still pretty much experimental, undocumented and their API is subject to change.

## world
A `World` is nothing more than a container for lights and objects. Now that we 
have a plane and a sphere it's easy to create one. In fact, we could have created
an empty one but there is usually not much point for that.
```
var world = new World()
{
    Lights = new [] { light },
    Objects = new Shape[] { plane, sphere },
};
```

## materials
Materials are implemented based mostly on *Phong shading* whith some extensions for reflection, transparency and refraction.

Every new material will have have a white color and defaults that make sure it shows up. 
If you are going to use a particular material multiple times you probably should cache it 
but otherwise it is probably better to set it inline.
```
var s = new Cube()
{
    Material = new Material()
    {
        Specular = 0,
        Ambient = 0.3,
        Diffuse = 0.5,
        Color = new Color(0.8, 0.2, 0.8),
    },
};
```

## patterns
Patterns are basically procedural textures. They support transformations as well.
All of the classics such as stripe, gradient and checkers are included. Pixie
supports nested and blended patterns as well.

## ideas
* Bezier curves
* L-systems
* More shapes & patterns
* Texture mapping
* Improve area lights
* Polynomial support
* Normal pertubation (smoothed triangles still todo)