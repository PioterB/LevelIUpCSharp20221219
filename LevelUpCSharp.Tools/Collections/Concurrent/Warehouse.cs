using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LevelUpCSharp.Collections.Concurrent
{
    internal class Warehouse<T> : IWarehouse<T>, IEnumerable<T>
    {
        private readonly List<T> _memory = new List<T>();

        public void Add(IEnumerable<T> sandwiches)
        {
            lock (_memory)
            {
                _memory.AddRange(sandwiches);
            }
        }

        public T Peak()
        {
            lock (_memory)
            {
                T item = _memory[0];
                _memory.RemoveAt(0);
                return item;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (_memory)
            {
                return _memory.GetEnumerator();
            }        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            lock (_memory)
            {
                _memory.Add(item);
            }
        }

        public IEnumerable<T> PeakRange(int upTo)
        {
            lock (_memory)
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

                return _memory.Take(upTo);
            }
        }
    }
}