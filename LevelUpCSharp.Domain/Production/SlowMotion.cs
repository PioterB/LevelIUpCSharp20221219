using System.Collections.Generic;
using System.Threading;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class SlowMotion : IEmployee
    {
        private readonly IEmployee _inner;

        public SlowMotion(IEmployee inner)
        {
            _inner = inner;
        }

        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            Thread.Sleep(8 * 1000);
            return _inner.Work(order);
        }
    }
}