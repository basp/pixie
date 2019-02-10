using System.Collections.Generic;

namespace Pixie.Core
{
    public class AreaLight : ILightSource
    {
        private readonly Color intensity;
        private readonly Double4 position;
        private readonly Double4 axis1;
        private readonly Double4 axis2;
        private readonly int rows;
        private readonly int columns;

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
        }

        public Double4 Position => this.position;

        public Color Intensity => this.intensity;

        public IEnumerable<ILight> GetLights()
        {
            var center = Double4.Point(0, 0, 0);

            var d = this.axis1 + this.axis2;
            var t = Transform.Translate(-d.X / 2, -d.Y / 2, -d.Z / 2);

            var si = 1.0 / (this.rows - 1);
            var sj = 1.0 / (this.columns - 1);

            for (var i = 0; i < this.rows; i++)
            {
                for (var j = 0; j < this.columns; j++)
                {
                    var ti = i * si;
                    var tj = j * sj;

                    var vi = ti * this.axis1;
                    var vj = tj * this.axis2;

                    var p = center + (vi + vj);
                    p = t * p;
                    yield return new PointLight(p, this.intensity);
                }
            }
        }
    }
}