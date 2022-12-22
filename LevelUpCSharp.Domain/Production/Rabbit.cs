using System.Collections.Generic;
using System.Threading;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class Rabbit : IEmployee
    {
        private readonly IEmployee _inner;

        public Rabbit(IEmployee inner)
        {
            _inner = inner;
        }

        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            Thread.Sleep(1 * 1000);
            return _inner.Work(order);
        }
    }
}