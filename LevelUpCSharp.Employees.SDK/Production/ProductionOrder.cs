using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class ProductionOrder
    {
        public ProductionOrder(SandwichKind kind, int count)
        {
            Kind = kind;
            Count = count;
        }

        public SandwichKind Kind { get; }

        public int Count { get; }
    }
}