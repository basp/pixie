namespace Pixie.Cmd.Examples
{
    using System;
    using Pixie.Core;

    public class Hexagon : Group
    {
        public static Shape Create()
        {
            var hex = new Pixie.Core.Group();
            for (var n = 0; n < 6; n++)
            {
                var side = HexagonSide();
                side.Transform = Pixie.Core.Transform.RotateY(n * Math.PI / 3);

                hex.Add(side);
            }

            return hex;
        }

        static Shape HexagonCorner() =>
            new Sphere()
            {
                Transform =
                    Pixie.Core.Transform.Translate(0, 0, -1) *
                    Pixie.Core.Transform.Scale(0.25, 0.25, 0.25),
            };

        static Shape HexagonEdge() =>
            new Cylinder()
            {
                Minimum = 0,
                Maximum = 1,
                Transform =
                    Pixie.Core.Transform.Translate(0, 0, -1) *
                        Pixie.Core.Transform.RotateY(-Math.PI / 6) *
                        Pixie.Core.Transform.RotateZ(-Math.PI / 2) *
                        Pixie.Core.Transform.Scale(0.25, 1, 0.25),
            };

        static Shape HexagonSide() =>
            new Pixie.Core.Group()
            {
                HexagonCorner(),
                HexagonEdge(),
            };    
    }
}