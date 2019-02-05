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
                Float4.Point(-10, 10, -10),
                Color.White);
            
            var s1 = new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f,
                },
            };

            var s2 = new Sphere
            {
                Transform = Transform.Scale(0.5f, 0.5f, 0.5f),
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
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var xs = w.Intersect(r);
            Assert.Equal(4, xs.Count);
            Assert.Equal(4, xs[0].T);
            Assert.Equal(4.5f, xs[1].T);
            Assert.Equal(5.5f, xs[2].T);
            Assert.Equal(6, xs[3].T);
        }

        [Fact]
        public void TestShadingAnIntersection()
        {
            var w = new DefaultWorld();
            var r = new Ray(Float4.Point(0, 0, -5), Float4.Vector(0, 0, 1));
            var shape = w.Objects[0];
            var i = new Intersection(4, shape);
            var comps = i.PrepareComputations(r);
            var c = w.Shade(comps);
            var expected = new Color(0.38066f, 0.47583f, 0.2855f);
            const float epsilon = 0.00001f;
            var comparer = new ApproxColorEqualityComparer(epsilon);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void TestShadingAnIntersectionFromTheInside()
        {
            var w = new DefaultWorld();
            w.Lights.Clear();
            w.Lights.Add(new PointLight(Float4.Point(0, 0.25f, 0), Color.White));
            var r = new Ray(Float4.Point(0, 0, 0), Float4.Vector(0, 0, 1));
            var shape = w.Objects[1];
            var i = new Intersection(0.5f, shape);
            var comps = i.PrepareComputations(r);
            var c = w.Shade(comps);
            var expected = new Color(0.90498f, 0.90498f, 0.90498f);
            const float epsilon = 0.00001f;
            var comparer = new ApproxColorEqualityComparer(epsilon);
            Assert.Equal(expected, c, comparer);
        }
    }
}