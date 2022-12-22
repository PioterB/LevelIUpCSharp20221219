using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Sandwich>> Work(ProductionOrder order)
        {
            await Task.Delay(1 * 1000);
            return await _inner.Work(order);
        }
    }
}