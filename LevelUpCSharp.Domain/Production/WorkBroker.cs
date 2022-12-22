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

        public async Task<IEnumerable<Sandwich>> Work(ProductionOrder order)
        {
            throw new NotImplementedException();
        }
    }
}