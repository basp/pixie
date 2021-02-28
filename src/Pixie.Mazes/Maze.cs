namespace Pixie.Mazes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    public class Maze : IEnumerable<Cell>
    {
        private readonly CartesianGrid grid;

        private readonly IDictionary<Cell,int> distances;

        public Maze(CartesianGrid grid, IDictionary<Cell,int> distances)
        {
            this.grid = grid;
            this.distances = distances;
        }

        public IEnumerator<Cell> GetEnumerator() =>
            this.grid.GetEnumerator();

        public Bitmap Render()
        {
            const int offset = 5;
            const int cellSize = 30;
            var imageWidth = (2 * offset) + (cellSize * this.grid.Columns);
            var imageHeight = (2 * offset) + (cellSize * this.grid.Rows);
            var background = Color.White;
            var wall = Color.Black;
            var bmp = new Bitmap(imageWidth, imageHeight);
            var modes = new[] { "backgrounds", "walls" };

            Func<Cell, Color> getBackgroundColor = cell =>
            {
                Func<double, int> round = x => (int)Math.Round(x);
                var max = this.distances.Values.Max();
                var d = this.distances[cell];
                var intensity = (max - d) / (double)max;
                var dark = round(255 * intensity);
                var bright = 128 + round(127 * intensity);
                return Color.FromArgb(dark, bright, dark);
            };

            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.FillRectangle(
                    new SolidBrush(background),
                    new Rectangle(0, 0, imageWidth, imageHeight));

                foreach (var mode in modes)
                {
                    foreach (var cell in this)
                    {
                        var x1 = offset + cell.Column * cellSize;
                        var y1 = offset + cell.Row * cellSize;
                        var x2 = offset + (cell.Column + 1) * cellSize;
                        var y2 = offset + (cell.Row + 1) * cellSize;

                        Action drawNorthWall = () =>
                            gfx.DrawLine(new Pen(new SolidBrush(wall)), x1, y1, x2, y1);

                        Action drawSouthWall = () =>
                            gfx.DrawLine(new Pen(new SolidBrush(wall)), x1, y2, x2, y2);

                        Action drawWestWall = () =>
                            gfx.DrawLine(new Pen(new SolidBrush(wall)), x1, y1, x1, y2);

                        Action drawEastWall = () =>
                            gfx.DrawLine(new Pen(new SolidBrush(wall)), x2, y1, x2, y2);

                        if (mode == "backgrounds")
                        {
                            var color = getBackgroundColor(cell);
                            gfx.FillRectangle(
                                new SolidBrush(color),
                                new Rectangle(x1, y1, cellSize, cellSize));
                        }
                        else
                        {
                            this.grid.North(cell).MatchNone(drawNorthWall);
                            this.grid.West(cell).MatchNone(drawWestWall);

                            this.grid.East(cell).Match(
                                x =>
                                {
                                    if (cell.IsLinked(x))
                                    {
                                        return;
                                    }

                                    drawEastWall();
                                },
                                drawEastWall);

                            this.grid.South(cell).Match(
                                x =>
                                {
                                    if (cell.IsLinked(x))
                                    {
                                        return;
                                    }

                                    drawSouthWall();
                                },
                                drawSouthWall);
                        }
                    }
                }
            }

            return bmp;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();
    }
}
