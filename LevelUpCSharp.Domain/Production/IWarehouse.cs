using System.Collections.Generic;

namespace LevelUpCSharp.Production
{
    public interface IWarehouse<T> : IEnumerable<T>
    {
        T Peak();

        IEnumerable<T> PeakRange(int upTo);

        void Add(T item);

        void Add(IEnumerable<T> sandwiches);
    }
}