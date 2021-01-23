namespace Pixie.Tests
{
    using System;
    using Xunit;
    using Linie;

    public class WorldTests
    {
        [Fact]
        public void TestDefaultWorld()
        {
            var light = new PointLight(
                Vector4.CreatePosition(-10, 10, -10),
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
        public void TestIntersectWorldWithRay4()
        {
            var w = new DefaultWorld();
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
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
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var shape = w.Objects[0];
            var i = new Intersection(4, shape);
            var comps = i.Precompute(r);
            var c = w.Render(comps, 5);
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
            w.Lights.Add(new PointLight(Vector4.CreatePosition(0, 0.25, 0), Color.White));
            var r = new Ray4(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 0, 1));
            var shape = w.Objects[1];
            var i = new Intersection(0.5, shape);
            var comps = i.Precompute(r);
            var c = w.Render(comps, 5);
            var expected = new Color(0.90498, 0.90498, 0.90498);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void TestColorWhenRay4Misses()
        {
            var w = new DefaultWorld();
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 1, 0));
            var c = w.Trace(r, 5);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void TestColorWhenRay4Hits()
        {
            var w = new DefaultWorld();
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var c = w.Trace(r, 5);
            var expected = new Color(0.38066, 0.47583, 0.2855);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void TestColorWithIntersectionBehindRay4()
        {
            var w = new DefaultWorld();
            var outer = w.Objects[0];
            outer.Material.Ambient = 1;
            var inner = w.Objects[1];
            inner.Material.Ambient = 1;
            var r = new Ray4(Vector4.CreatePosition(0, 0, 0.75), Vector4.CreateDirection(0, 0, -1));
            var c = w.Trace(r, 5);
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
                    Vector4.CreatePosition(0, 0, -10),
                    Color.White));

            var s1 = new Sphere();
            var s2 = new Sphere()
            {
                Transform = Transform.Translate(0, 0, 10),
            };

            w.Objects.Add(s1);
            w.Objects.Add(s2);

            var r = new Ray4(Vector4.CreatePosition(0, 0, 5), Vector4.CreateDirection(0, 0, 1));
            var i = new Intersection(4, s2);
            var comps = i.Precompute(r);
            var c = w.Render(comps, 5);
            var expected = new Color(0.1, 0.1, 0.1);
            Assert.Equal(expected, c);
        }

        [Fact]
        public void ReflectedColorForNonReflectiveMaterial()
        {
            var w = new DefaultWorld();
            var r = new Ray4(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 0, 1));
            var shape = w.Objects[1];
            shape.Material.Ambient = 1;
            var i = new Intersection(1, shape);
            var comps = i.Precompute(r);
            var c = w.GetReflectedColor(comps, 5);
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
            var r = new Ray4(
                Vector4.CreatePosition(0, 0, -3),
                Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), plane);
            var comps = i.Precompute(r);
            var c = w.GetReflectedColor(comps, 5);
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
            var r = new Ray4(
                Vector4.CreatePosition(0, 0, -3),
                Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), plane);
            var comps = i.Precompute(r);
            var c = w.Render(comps, 5);
            var expected = new Color(0.87677, 0.92436, 0.82918);
            const double eps = 0.0001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void ColorAtWithMutuallyReflectiveSurfaces()
        {
            var w = new World();
            var light = new PointLight(Vector4.CreatePosition(0, 0, 0), Color.White);
            w.Lights.Add(light);
            var lower = new Plane();
            lower.Material.Reflective = 1;
            lower.Transform = Transform.Translate(0, -1, 0);
            w.Objects.Add(lower);
            var upper = new Plane();
            upper.Material.Reflective = 1;
            upper.Transform = Transform.Translate(0, 1, 0);
            w.Objects.Add(upper);
            var r = new Ray4(Vector4.CreatePosition(0, 0, 0), Vector4.CreateDirection(0, 1, 0));
            var c = w.Trace(r, 5);
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
            var r = new Ray4(
                Vector4.CreatePosition(0, 0, -3),
                Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));
            var i = new Intersection(Math.Sqrt(2), plane);
            var comps = i.Precompute(r);
            var c = w.GetReflectedColor(comps, 0);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void RefractedColorWithOpaqueSurface()
        {
            var w = new DefaultWorld();
            var shape = w.Objects[0];
            var r = new Ray4(Vector4.CreatePosition(0, 0, -5), Vector4.CreateDirection(0, 0, 1));
            var xs = IntersectionList.Create(
                new Intersection(4, shape),
                new Intersection(6, shape));
            var comps = xs[0].Precompute(r, xs);
            var c = w.GetRefractedColor(comps, 5);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void RefractedColorAtMaxRecursiveDepth()
        {
            var w = new DefaultWorld();

            var shape = w.Objects[0];
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;

            var r = new Ray4(
                Vector4.CreatePosition(0, 0, -5),
                Vector4.CreateDirection(0, 0, 1));

            var xs = IntersectionList.Create(
                new Intersection(4, shape),
                new Intersection(6, shape));

            var comps = xs[0].Precompute(r, xs);
            var c = w.GetRefractedColor(comps, 0);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void RefractedColorUnderTotalInternalReflection()
        {
            var w = new DefaultWorld();

            var shape = w.Objects[0];
            shape.Material.Transparency = 1.0;
            shape.Material.RefractiveIndex = 1.5;

            var r = new Ray4(
                Vector4.CreatePosition(0, 0, Math.Sqrt(2) / 2),
                Vector4.CreateDirection(0, 1, 0));

            var xs = IntersectionList.Create(
                new Intersection(-Math.Sqrt(2) / 2, shape),
                new Intersection(Math.Sqrt(2) / 2, shape));

            var comps = xs[1].Precompute(r, xs);
            var c = w.GetRefractedColor(comps, 5);
            Assert.Equal(Color.Black, c);
        }

        [Fact]
        public void RefractedColorWithRefractedRay4()
        {
            var w = new DefaultWorld();

            var a = w.Objects[0];
            a.Material.Ambient = 1.0;
            a.Material.Pattern = new TestPattern();

            var b = w.Objects[1];
            b.Material.Transparency = 1.0;
            b.Material.RefractiveIndex = 1.5;

            var r = new Ray4(
                Vector4.CreatePosition(0, 0, 0.1),
                Vector4.CreateDirection(0, 1, 0));

            var xs = IntersectionList.Create(
                new Intersection(-0.9899, a),
                new Intersection(-0.4899, b),
                new Intersection(0.4899, b),
                new Intersection(0.9899, a));

            var comps = xs[2].Precompute(r, xs);
            var c = w.GetRefractedColor(comps, 5);
            var expected = new Color(0, 0.99888, 0.04725);

            const double eps = 0.0001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void ShadeWithTransparentMaterial()
        {
            var w = new DefaultWorld();
            var floor = new Plane();
            floor.Transform = Transform.Translate(0, -1, 0);
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;

            var ball = new Sphere();
            ball.Transform = Transform.Translate(0, -3.5, -0.5);
            ball.Material.Color = new Color(1, 0, 0);
            ball.Material.Ambient = 0.5;

            w.Objects.Add(floor);
            w.Objects.Add(ball);

            var r = new Ray4(
                Vector4.CreatePosition(0, 0, -3),
                Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));

            var xs = IntersectionList.Create(
                new Intersection(Math.Sqrt(2), floor));

            var comps = xs[0].Precompute(r, xs);
            var c = w.Render(comps, 5);
            var expected = new Color(0.93642, 0.68642, 0.68642);

            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Fact]
        public void ShadeWithReflectiveTransparentMaterial()
        {
            var w = new DefaultWorld();
            var r = new Ray4(
                Vector4.CreatePosition(0, 0, -3),
                Vector4.CreateDirection(0, -Math.Sqrt(2) / 2, Math.Sqrt(2) / 2));

            var floor = new Plane();
            floor.Transform = Transform.Translate(0, -1, 0);
            floor.Material.Reflective = 0.5;
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;

            var ball = new Sphere();
            ball.Transform = Transform.Translate(0, -3.5, -0.5);
            ball.Material.Color = new Color(1, 0, 0);
            ball.Material.Ambient = 0.5;

            w.Objects.Add(floor);
            w.Objects.Add(ball);

            var xs = IntersectionList.Create(
                new Intersection(Math.Sqrt(2), floor));

            var comps = xs[0].Precompute(r, xs);
            var c = w.Render(comps, 5);
            var expected = new Color(0.93391, 0.69643, 0.69243);
            const double eps = 0.00001;
            var comparer = Color.GetEqualityComparer(eps);
            Assert.Equal(expected, c, comparer);
        }

        [Theory]
        [InlineData(-10, -10, 10, false)]
        [InlineData(10, 10, 10, true)]
        [InlineData(-20, -20, -20, false)]
        [InlineData(-5, -5, -5, false)]
        public void IsShadowTestsForOcclusionBetweenTwoPoints(
            double px,
            double py,
            double pz,
            bool result)
        {
            var w = new DefaultWorld();
            var lightPosition = Vector4.CreatePosition(-10, -10, -10);
            var point = Vector4.CreatePosition(px, py, pz);
            Assert.Equal(result, w.IsShadowed(lightPosition, point));
        }
    }
}