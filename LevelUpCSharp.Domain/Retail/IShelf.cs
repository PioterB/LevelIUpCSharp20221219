using System.Collections.Generic;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Retail
{
    public interface IShelf : IEnumerable<Sandwich>
    {
        void Put(Sandwich item);

        void Put(IEnumerable<Sandwich> package);

        Result<Sandwich> Take(SandwichKind kind);

        bool Has(SandwichKind kind);

    }
}