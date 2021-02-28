namespace Pixie.Mazes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class ConstantDistances : IDictionary<Cell, int>
    {
        private readonly Cell root;
     
        private readonly int distance;

        public ConstantDistances(Cell root, int distance)
        {
            this.root = root;
            this.distance = distance;
        }

        public int this[Cell key]
        {
            get => this.distance;
            set => throw new NotImplementedException();
        }

        public ICollection<Cell> Keys => new [] { this.root };

        public ICollection<int> Values => new [] { this.distance };

        public int Count => 1;

        public bool IsReadOnly => true;

        public void Add(Cell key, int value) =>
            throw new NotImplementedException();

        public void Add(KeyValuePair<Cell, int> item) =>
            throw new NotImplementedException();

        public void Clear() =>
            throw new NotImplementedException();

        public bool Contains(KeyValuePair<Cell, int> item) =>
            throw new NotImplementedException();

        public bool ContainsKey(Cell key) =>
            key == this.root;

        public void CopyTo(KeyValuePair<Cell, int>[] array, int arrayIndex) =>
            throw new NotImplementedException();

        public IEnumerator<KeyValuePair<Cell, int>> GetEnumerator() =>
            throw new NotImplementedException();

        public bool Remove(Cell key) =>
            throw new NotImplementedException();

        public bool Remove(KeyValuePair<Cell, int> item) =>
            throw new NotImplementedException();

        public bool TryGetValue(Cell key, [MaybeNullWhen(false)] out int value) =>
            throw new NotImplementedException();            

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
