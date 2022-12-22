using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelUpCSharp.Production;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Employees
{
    public class VillagePerson : IEmployee
    {
        public Task<IEnumerable<Sandwich>> Work(ProductionOrder order)
        {
            return Task.FromResult(Enumerable.Empty<Sandwich>());
        }
    }
}
