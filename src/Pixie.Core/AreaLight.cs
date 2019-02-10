namespace Pixie.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AreaLight : ILightSource
    {
        private readonly Color intensity;
        private readonly Double4 position;
        private readonly Double4 axis1;
        private readonly Double4 axis2;
        private readonly int rows;
        private readonly int columns;
        private readonly IList<ILight> lights;

        public AreaLight(
            Double4 position,
            Color intensity,
            Double4 axis1,
            Double4 axis2,
            int rows,
            int columns)
        {
            this.position = position;
            this.intensity = intensity;
            this.axis1 = axis1;
            this.axis2 = axis2;
            this.rows = rows; // size1
            this.columns = columns; // size2
            this.lights = this.BuildLights().ToList();
        }


        public Double4 Position => this.position;

        public Color Intensity => this.intensity;

        public IEnumerable<ILight> GetLights() => this.lights;

        private IEnumerable<ILight> BuildLights()
        {
            var center = this.position;

            var ax1 = Double4.Vector(0, 0, 2);
            var ax2 = Double4.Vector(0, 2, 0);

            var d = ax1 + ax2;
            var t = Transform.Translate(-d.X / 2, -d.Y / 2, -d.Z / 2);

            const int rows = 2;
            const int cols = 2;

            var si = 1.0 / (rows - 1);
            var sj = 1.0 / (cols - 1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    var ti = i * si;
                    var tj = j * sj;

                    var vi = ti * ax1;
                    var vj = tj * ax2;

                    var p = center + (vi + vj);
                    p = t * p;
                    yield return new PointLight(p, this.intensity);
                }
            }
        }
    }
}