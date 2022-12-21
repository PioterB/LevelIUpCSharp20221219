using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LevelUpCSharp.Collections
{
    public class Warehouse<T> : IWarehouse<T>, IEnumerable<T>
    {
        private readonly List<T> _memory = new List<T>();

        public Warehouse()
        {
            
        }

        public Warehouse(IEnumerable<T> items)
        {
            _memory = new List<T>(items);
        }

        public void Add(IEnumerable<T> sandwiches)
        {
            _memory.AddRange(sandwiches);
        }

        public T Peak()
        {
            T item = _memory[0];
            _memory.RemoveAt(0);
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _memory.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _memory.Add(item);
        }

        public IEnumerable<T> PeakRange(int upTo)
        {
            if (_memory.Count == 0)
            {
                return Array.Empty<T>();
            }

            if (_memory.Count < upTo)
            {

                var result = _memory.ToArray();
                _memory.Clear();
                return result;
            }

            var subset = _memory.Take(upTo);
            _memory.RemoveRange(0, upTo);
            return subset;
        }
    }
}