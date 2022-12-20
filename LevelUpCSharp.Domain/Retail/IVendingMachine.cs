using System.Collections.Generic;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Retail
{
    public interface IVendingMachine : IEnumerable<Sandwich>
    {
        void Put(Sandwich item);

        Result<Sandwich> Take(SandwichKind kind);

        bool Has(SandwichKind kind);
    }
}