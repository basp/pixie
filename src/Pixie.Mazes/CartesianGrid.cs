namespace Pixie.Mazes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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

        public void Sidewinder()
        {
            foreach (var row in this.GetRows())
            {
                var run = new HashSet<Cell>();
                foreach (var cell in row)
                {
                    run.Add(cell);

                    var eastBound = !this.East(cell).HasValue;
                    var northBound = !this.North(cell).HasValue;

                    var shouldCloseRun =
                        eastBound || (!northBound && rng.Next(2) == 0);

                    if (shouldCloseRun)
                    {
                        var member = run.Sample(rng);
                        this.North(member).MatchSome(x =>
                        {
                            member.Link(x);
                        });

                        run.Clear();
                    }
                    else
                    {
                        this.East(cell).MatchSome(x =>
                        {
                            cell.Link(x);
                        });
                    }
                }
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
