using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class WorkBroker : IEmployee
    {
        private readonly IEmployee _worker;

        public WorkBroker(IEmployee worker)
        {
            _worker = worker;
        }

        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            return Enumerable.Range(0, order.Count).AsParallel().SelectMany(index => _worker.Work(new ProductionOrder(order.Kind, 1))).ToArray();
        }
    }
}