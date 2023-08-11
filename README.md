# Pixie
An implementation of the ray-tracer described in the excellent **The Ray-Tracer 
Challenge** book by **Jamis Buck**.

## Differences with Pixie1
### Everything is a `Vector`
Everything is built on **System.Numerics**. No more custom geometric classes 
such as points, vectors, matrices etc. Points and vectors are now just `Vector4`
values and colors are `Vector3`. In order to provide some sanity you can create
points and vectors in 3D space (using `Vector3`) and then convert them to 
homogeneous coordinates using the `AsPosition` and `AsDirection` extension
methods.
```csharp
new Vector3(0.5f, 0, 1).AsPosition();  // => <0.5, 0, 1, 1>
new Vector3(0.5f, 0, 1).AsDirection(); // => <0.5, 0, 1, 0>
```

This is very much a super-breaking chanage since it changes the whole API.

### Now `float` instead of `double`
Since we are now building on top of **System.Numerics** all our vectors are
based on `float` instead of `double`. For now, the speed increase seems to
outweigh the loss of precision.

### `Color` be gone
Finally deciding to bite the bullet, the `struct Color<T>` type has been wiped.
Colors will be represented as `Vector3` values.

### Some new concepts
There are a few new important classes that are not really part of **TRTC** but
inspired from **pbrt** instead. The `Primitive` class is used to combine a 
`Shape`, a `Material` and a `Transform`. The `Integrator` class uses a 
`Primitive`, a `Camera`, an array of `Light` instances and a `Sampler` to 
perform the final integration of sampled light values. **Pixie1** had some
equivalents to these classes but thanks to **pbrt** we can formalize them a bit 
better.

## Downsides
* The API is more low-level since we are dealing with **System.Numerics** types.
* We have less control and can provide less utility since we do not own the 
types in that package.

## Overview
Below is an overview of the system. It shows the aggregate relations between
the most important classes. This is a simplified version of the system 
described in **pbrt**.
```
Integrator 
    <>--> Camera
    <>--> Sampler
    <>--> Primitive
    <>--> Lights

Camera
    <>--> Film

Primitive
    <>--> Shape
    <>--> Material
    <>--> Transform

Intersection
    <>--> T : float
    <>--> Interaction

Interaction
    <>--> Material

Film
    <>--> Bounds : Bounds2<int>

Ray
    <>--> Origin
    <>--> Direction
```