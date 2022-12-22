using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class SandwichMaster : IEmployee
    {
        public async Task<IEnumerable<Sandwich>> Work(ProductionOrder order)
        {
            var result = new List<Sandwich>(order.Count);
            for (int i = 0; i < order.Count; i++)
            {
                var sandwich = Produce(order.Kind);
                result.Add(sandwich);
            }

            return await Task.FromResult(result);
        }

        private Sandwich Produce(SandwichKind kind)
        {
            return kind switch
            {
                SandwichKind.Beef => ProduceSandwich(kind, DateTimeOffset.Now.AddMinutes(3)),
                SandwichKind.Cheese => ProduceSandwich(kind, DateTimeOffset.Now.AddSeconds(90)),
                SandwichKind.Chicken => ProduceSandwich(kind, DateTimeOffset.Now.AddMinutes(4)),
                SandwichKind.Pork => ProduceSandwich(kind, DateTimeOffset.Now.AddSeconds(150)),
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };
        }

        private Sandwich ProduceSandwich(SandwichKind kind, DateTimeOffset addMinutes)
        {
            return new Sandwich(kind, addMinutes);
        }
    }
}