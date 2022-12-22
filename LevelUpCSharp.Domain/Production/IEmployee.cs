using System.Collections.Generic;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public interface IEmployee
    {
        IEnumerable<Sandwich> Work(ProductionOrder order);
    }
}