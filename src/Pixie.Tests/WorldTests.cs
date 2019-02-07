namespace Pixie.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Pixie.Core;

    public class WorldTests
    {
        [Fact]
        public void TestDefaultWorld()
        {
            var light = new PointLight(
                Double4.Point(-10, 10, -10),
                Color.White);
            
            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2,
                },
            };

            var s2 = new Sphere
            {
                Transform = Transform.Scale(0.5, 0.5, 0.5),
            };

            var w = new DefaultWorld();
            Assert.Contains(light, w.Lights);
            Assert.Contains(s1, w.Objects);
            Assert.Contains(s2, w.Objects);
        }

        [Fact]
        public void TestIntersectWorldWithRay()
        {
            var w = new DefaultWorld();
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var xs = w.Intersect(r);
            Assert.Equal(4, xs.Count);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(4.5, xs[1].T);
            Assert.Equal(5.5, xs[2].T);
            Assert.Equal(6, xs[3].T);
        }

        [Fact]
        public void TestShadingAnIntersection()
        {
            var w = new DefaultWorld();
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var shape = w.Objects[0];
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            var c = w.Shade(comps);
            var expected = new Color(0.38066, 0.47583, 0.2855);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void TestShadingAnIntersectionFromTheInside()
        {
            var w = new DefaultWorld();
            w.Lights.Clear();
            w.Lights.Add(new PointLight(Double4.Point(0, 0.25, 0), Color.White));
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 0, 1));
            var shape = w.Objects[1];
            var i = new Intersection(0.5, shape);
            var comps = i.PrepareComputations(r);
            var c = w.Shade(comps);
            var expected = new Color(0.90498, 0.90498, 0.90498);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void TestColorWhenRayMisses()
        {
            var w = new DefaultWorld();
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 1, 0));
            var c = w.ColorAt(r);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void TestColorWhenRayHits()
        {
            var w = new DefaultWorld();
            var r = new Ray(Double4.Point(0, 0, -5), Double4.Vector(0, 0, 1));
            var c = w.ColorAt(r);
            var expected = new Color(0.38066, 0.47583, 0.2855);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void TestColorWithIntersectionBehindRay()
        {
            var w = new DefaultWorld();
            var outer = w.Objects[0];
            outer.Material.Ambient = 1;
            var inner = w.Objects[1];
            inner.Material.Ambient = 1;
            var r = new Ray(Double4.Point(0, 0, 0.75), Double4.Vector(0, 0, -1));
            var c = w.ColorAt(r);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(inner.Material.Color, c, comparer);
        }

        [Fact]
        public void ShadeIntersectionInShadow()
        {
            var w = new World();
            w.Lights.Add(
                new PointLight(
                    Double4.Point(0, 0, -10), 
                    Color.White));
            
            var s1 = new Sphere();
            var s2 = new Sphere()
            {
                Transform = Transform.Translate(0, 0, 10),
            };

            w.Objects.Add(s1);
            w.Objects.Add(s2);

            var r = new Ray(Double4.Point(0, 0, 5), Double4.Vector(0, 0, 1));
            var i = new Intersection(4, s2);
            var comps = i.PrepareComputations(r);
            var c = w.Shade(comps);
            var expected = new Color(0.1, 0.1, 0.1);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void ReflectedColorForNonReflectiveMaterial()
        {
            var w = new DefaultWorld();
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 0, 1));
            var shape = w.Objects[1];
            shape.Material.Ambient = 1;
            var i = new Intersection(1, shape);
            var comps = i.PrepareComputations(r);
            var c = w.ReflectedColor(comps);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void ReflectedColorForReflectiveMaterial()
        {
            var w = new DefaultWorld();
            var plane = new Plane();
            plane.Material.Reflective = 0.5;
            plane.Transform = Transform.Translate(0, -1, 0);
            w.Objects.Add(plane);
            var r = new Ray(
                Double4.Point(0, 0, -3), 
                Double4.Vector(0, -Math.Sqrt(2)/2, Math.Sqrt(2)/2));
            var i = new Intersection(Math.Sqrt(2), plane);
            var comps = i.PrepareComputations(r);
            var c = w.ReflectedColor(comps);
            var expected = new Color(0.19032, 0.2379, 0.14274);
            const double eps = 0.0001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void ShadeWithReflectiveMaterial()
        {
            var w = new DefaultWorld();
            var plane = new Plane();
            plane.Material.Reflective = 0.5;
            plane.Transform = Transform.Translate(0, -1, 0);
            w.Objects.Add(plane);
            var r = new Ray(
                Double4.Point(0, 0, -3),
                Double4.Vector(0, -Math.Sqrt(2)/2, Math.Sqrt(2)/2));
            var i = new Intersection(Math.Sqrt(2), plane);
            var comps = i.PrepareComputations(r);
            var c = w.Shade(comps);
            var expected = new Color(0.87677, 0.92436, 0.82918);
            const double eps = 0.0001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void ColorAtWithMutuallyReflectiveSurfaces()
        {
            var w = new World();
            var light = new PointLight(Double4.Point(0, 0, 0), Color.White);
            w.Lights.Add(light);
            var lower = new Plane();
            lower.Material.Reflective = 1;
            lower.Transform = Transform.Translate(0, -1, 0);
            w.Objects.Add(lower);
            var upper = new Plane();
            upper.Material.Reflective = 1;
            upper.Transform = Transform.Translate(0, 1, 0);
            w.Objects.Add(upper);
            var r = new Ray(Double4.Point(0, 0, 0), Double4.Vector(0, 1, 0));
            var c = w.ColorAt(r);
            Assert.True(true);
        }

        [Fact]
        public void ReflectedColorAtMaxRecursiveDepth()
        {
            var w = new DefaultWorld();
            var plane = new Plane();
            plane.Material.Reflective = 0.5;
            plane.Transform = Transform.Translate(0, -1, 0);
            w.Objects.Add(plane);
            var r = new Ray(
                Double4.Point(0, 0, -3),
                Double4.Vector(0, -Math.Sqrt(2)/2, Math.Sqrt(2)/2));
            var i = new Intersection(Math.Sqrt(2), plane);
            var comps = i.PrepareComputations(r);
            var c = w.ReflectedColor(comps, 0);
            Assert.Equal(Color.Black, c);
        }
    }
}