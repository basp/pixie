namespace Pixie.Mazes
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class Distances : IDictionary<Cell, int>
    {
        private readonly Cell root;

        private readonly IDictionary<Cell, int> cells;

        public Distances(Cell root)
        {
            this.root = root;
            this.cells = new Dictionary<Cell, int>
            {
                [root] = 0,
            };

            var frontier = new List<Cell>(new[] { root });
            while (frontier.Count > 0)
            {
                var newFrontier = new List<Cell>();
                foreach (var cell in frontier)
                {
                    foreach (var link in cell.Links)
                    {
                        if (cells.ContainsKey(link))
                        {
                            continue;
                        }

                        cells.Add(link, cells[cell] + 1);
                        newFrontier.Add(link);
                    }
                }

                frontier = newFrontier;
            }
        }

        public int this[Cell key]
        {
            get => this.cells[key];
            set => this.cells[key] = value;
        }

        public ICollection<Cell> Keys => this.cells.Keys;

        public ICollection<int> Values => this.cells.Values;

        public int Count => this.cells.Count;

        public bool IsReadOnly => false;

        public void Add(Cell key, int value) =>
            this.cells.Add(key, value);

        public void Add(KeyValuePair<Cell, int> item) =>
            this.cells.Add(item);

        public void Clear() =>
            this.cells.Clear();

        public bool Contains(KeyValuePair<Cell, int> item) =>
            this.cells.Contains(item);

        public bool ContainsKey(Cell key) =>
            this.cells.ContainsKey(key);

        public void CopyTo(KeyValuePair<Cell, int>[] array, int arrayIndex) =>
            this.cells.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<Cell, int>> GetEnumerator() =>
            this.cells.GetEnumerator();

        public bool Remove(Cell key) =>
            this.cells.Remove(key);

        public bool Remove(KeyValuePair<Cell, int> item) =>
            this.cells.Remove(item);

        public bool TryGetValue(Cell key, [MaybeNullWhen(false)] out int value) =>
            this.cells.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
