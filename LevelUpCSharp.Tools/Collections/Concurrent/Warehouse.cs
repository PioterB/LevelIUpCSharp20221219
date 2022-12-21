using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LevelUpCSharp.Collections.Concurrent
{
    public class Warehouse<T> : IWarehouse<T>, IEnumerable<T>
    {
        private readonly LevelUpCSharp.Collections.Warehouse<T> _memory;

        public Warehouse()
        {
            _memory = new Collections.Warehouse<T>();
        }

        public Warehouse(IEnumerable<T> items)
        {
            _memory = new LevelUpCSharp.Collections.Warehouse<T>(items);
        }

        public void Add(IEnumerable<T> sandwiches)
        {
            lock (_memory)
            {
                _memory.Add(sandwiches);
            }
        }

        public T Peak()
        {
            lock (_memory)
            {
                return _memory.Peak();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_memory)
            {
                return _memory.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
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
                return _memory.PeakRange(upTo);
            }
        }
    }
}