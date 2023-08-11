# Pixie
Pixie is an an implementation of the ray-tracer described in the excellent 
[The Ray-Tracer Challenge](http://#) book by **Jamis Buck**. This is a complete 
rewrite that is based on `System.Numerics` .

## Overview
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
    <>--> Bounds2i

Ray
    <>--> Origin
    <>--> Direction
```