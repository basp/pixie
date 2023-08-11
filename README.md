# Pixie
An implementation of the ray-tracer described in the excellent **The Ray-Tracer 
Challenge** book by **Jamis Buck**.

## Differences with Pixie1
### Everything is a `Vector`
Everything is built on **System.Numerics**. No more custom geometric classes 
such as points, vectors, matrices etc. Points and vectors are now just `Vector4`
values and colors are `Vector3`. Our matrices will be `Matrix4x4`. In order to 
provide some sanity you can create points and vectors in 3D space 
(using `Vector3`) and then convert them to homogeneous coordinates using the 
`AsPosition` and `AsDirection` extension methods.
```csharp
new Vector3(0.5f, 0, 1).AsPosition();  // => <0.5, 0, 1, 1>
new Vector3(0.5f, 0, 1).AsDirection(); // => <0.5, 0, 1, 0>
```

This is very much a super-breaking change since it changes the whole API.

### Now `float` instead of `double`
Since we are now building on top of **System.Numerics** all our vectors are
based on `float` instead of `double`. For now, the speed increase seems to
outweigh the loss of precision.

> Why the speed increase? Those classes from **System.Numerics** might have 
> *intrinsics* which basically means the compiler can deal with them in a
> special way. For us this means potentially executing them on the GPU or
> using special (vectorized) CPU instructions.
 
### `Color` be gone
Finally deciding to bite the bullet, the `struct Color<T>` type has been wiped.
Colors will be represented as `Vector3` values.

### Need to use `Transform`
**Pixie1** provided operator overloads so you could multiply a vector `u` with
a matrix `m` like `m * u` or `u * m` but this is no longer possible. You need
to use the `Vector4.Transform` method now:
```csharp
var m = Matrix4x4.CreateScale(1, 0.5f, 1);
var u = new Vector3(0, 1, 0.5).AsDirection();
var v = Vector4.Transform(u, m); // <0, 0.5, 0.5>
```

> **System.Numerics** overloads a lot of common operators but if you can not
> find what you are looking for try looking into the static methods on the
> vector classes (i.e. `Vector2`, `Vector3` or `Vector4` depending on what you
> need).

### Chained transformations
In the **TRTC** book we learn that if we want to combine transformation
matrices, we have to multiple them in reverse order. So, if we first want to
scale (`S`) and then rotate (`R`) a point we would have `R * S`. This is
somewhat counter-intuitive and so the book recommends setting up an API were
we can do something like the following:
```csharp
var t = Matrix4x4.Identity
    .Translate(1, 1, 0)
    .Scale(2, 0.5, 1)
    .RotateX(MathF.PI / 5);
```

The idea behind this is that when calling `.Translate(M, N)` (for example) what
get is actually `N * M`. This will automatically wrap the matrix multiplication
so the transformations line up.

However, turns out when building on **System.Numerics** we do not need this.
```csharp
var t = Matrix4x4.Identity *
    Matrix4x4.Translate(1, 1, 0) *
    Matrix4x4.Scale(2, 0.5, 1) *
    Matrix4x4.RotateX(MathF.PI / 5);
```

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