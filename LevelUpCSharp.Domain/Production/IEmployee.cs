using System.Collections.Generic;
using System.Threading.Tasks;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public interface IEmployee
    {
        Task<IEnumerable<Sandwich>> Work(ProductionOrder order);
    }
}