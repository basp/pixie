namespace Pixie.Mazes
{
    using System;
    using System.Collections.Generic;

    public class Cell : IEquatable<Cell>
    {
        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; }

        public int Column { get; }

        public ISet<Cell> Links { get; } = new HashSet<Cell>();

        public void Link(Cell other, bool reverse = true)
        {
            this.Links.Add(other);
            if (reverse)
            {
                other.Link(this, reverse: false);
            }
        }

        public void Unlink(Cell other, bool reverse = true)
        {
            this.Links.Remove(other);
            if (reverse)
            {
                other.Unlink(this, reverse: false);
            }
        }

        public bool IsLinked(Cell other) => this.Links.Contains(other);

        public bool Equals(Cell other) =>
            this.Row == other.Row && this.Column == other.Column;

        public override int GetHashCode() =>
            HashCode.Combine(this.Row, this.Column);

        public override string ToString() =>
            $"({this.Row}, {this.Column})";
    }
}
