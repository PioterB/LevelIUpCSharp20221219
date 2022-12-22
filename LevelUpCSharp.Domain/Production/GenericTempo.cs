using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class GenericTempo : IEmployee
    {
        private readonly IEmployee _inner;
        private readonly int _speed;

        public GenericTempo(IEmployee inner, int speed)
        {
            _inner = inner;
            _speed = speed;
        }

        public Task<IEnumerable<Sandwich>> Work(ProductionOrder order)
        {
            var result =  _inner.Work(order);
            Thread.Sleep(_speed * 1000);
            return result;
        }
    }
}