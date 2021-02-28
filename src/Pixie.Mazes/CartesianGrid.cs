namespace Pixie.Mazes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Optional;
    using Optional.Unsafe;

    public class CartesianGrid : IEnumerable<Cell>
    {
        private static readonly Random rng = new Random();

        private readonly Cell[][] cells;

        public CartesianGrid(int rows, int columns)
        {
            this.cells = new Cell[rows][];
            for (var j = 0; j < rows; j++)
            {
                this.cells[j] = new Cell[columns];
                for (var i = 0; i < columns; i++)
                {
                    this.cells[j][i] = new Cell(j, i);
                }
            }

            this.Rows = rows;
            this.Columns = columns;
        }

        public int Rows { get; }

        public int Columns { get; }

        public Option<Cell> this[int row, int column] =>
            this.GetCell(row, column);

        public Option<Cell> North(int row, int column) =>
            this.GetCell(row - 1, column);

        public Option<Cell> South(int row, int column) =>
            this.GetCell(row + 1, column);

        public Option<Cell> West(int row, int column) =>
            this.GetCell(row, column - 1);

        public Option<Cell> East(int row, int column) =>
            this.GetCell(row, column + 1);

        public Option<Cell> North(Cell cell) =>
            this.North(cell.Row, cell.Column);

        public Option<Cell> South(Cell cell) =>
            this.South(cell.Row, cell.Column);

        public Option<Cell> West(Cell cell) =>
            this.West(cell.Row, cell.Column);

        public Option<Cell> East(Cell cell) =>
            this.East(cell.Row, cell.Column);

        public IEnumerable<Cell[]> GetRows()
        {
            var nrows = this.cells.Length;
            for (var j = 0; j < nrows; j++)
            {
                yield return this.cells[j];
            }
        }

        public IEnumerable<Cell> Neighbors(int row, int column)
        {
            var n = this.North(row, column);
            var s = this.South(row, column);
            var w = this.West(row, column);
            var e = this.East(row, column);
            return new[] { n, s, w, e }
                .Where(x => x.HasValue)
                .Select(x => x.ValueOrFailure());
        }

        public Option<Cell> GetCell(int row, int column)
        {
            if (row < 0 || row >= this.cells.Length)
            {
                return Option.None<Cell>();
            }

            if (column < 0 || column >= this.cells[row].Length)
            {
                return Option.None<Cell>();
            }

            return Option.Some(this.cells[row][column]);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (var row in this.GetRows())
            {
                foreach (var cell in row)
                {
                    yield return cell;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        public void BinaryTree()
        {
            foreach (var cell in this)
            {
                var neighbors = new List<Cell>();

                this.North(cell).Match(
                    x => neighbors.Add(x),
                    () => { });

                this.East(
                    cell).Match(x => neighbors.Add(x),
                    () => { });

                if (neighbors.Count > 0)
                {
                    var index = rng.Next(neighbors.Count);
                    var neighbor = neighbors[index];
                    cell.Link(neighbor);
                }
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public Bitmap Render()
        {
            const int padding = 5;
            const int cellSize = 30;
            var imageWidth = 2 * padding + cellSize * this.Columns;
            var imageHeight = 2 * padding + cellSize * this.Rows;
            var background = Color.White;
            var wall = Color.Black;
            var bmp = new Bitmap(imageWidth, imageHeight);
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.FillRectangle(
                    new SolidBrush(background),
                    new Rectangle(0, 0, imageWidth, imageHeight));

                foreach (var cell in this)
                {
                    var x1 = padding + cell.Column * cellSize;
                    var y1 = padding + cell.Row * cellSize;
                    var x2 = padding + (cell.Column + 1) * cellSize;
                    var y2 = padding + (cell.Row + 1) * cellSize;

                    Action drawNorthWall = () =>
                        gfx.DrawLine(new Pen(new SolidBrush(wall)), x1, y1, x2, y1);

                    Action drawSouthWall = () =>
                        gfx.DrawLine(new Pen(new SolidBrush(wall)), x1, y2, x2, y2);

                    Action drawWestWall = () =>
                        gfx.DrawLine(new Pen(new SolidBrush(wall)), x1, y1, x1, y2);

                    Action drawEastWall = () =>
                        gfx.DrawLine(new Pen(new SolidBrush(wall)), x2, y1, x2, y2);

                    this.North(cell).MatchNone(drawNorthWall);
                    this.West(cell).MatchNone(drawWestWall);

                    this.East(cell).Match(
                        x =>
                        {
                            if (cell.IsLinked(x))
                            {
                                return;
                            }

                            drawEastWall();
                        },
                        drawEastWall);

                    this.South(cell).Match(
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

            return bmp;
        }
    }
}