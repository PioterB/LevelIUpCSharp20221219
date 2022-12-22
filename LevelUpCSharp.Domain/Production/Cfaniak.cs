using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class Cfaniak : IEmployee
    {
        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            Task<Sandwich>[] workers = new Task<Sandwich>[order.Count];
            for (int i = 0; i < order.Count; i++)
            {
                Task<Sandwich> worker = new Task<Sandwich>(() => ProduceSandwich(order.Kind));
                worker.Start();
                workers[i] = worker;
            }
            var sandwiches = workers.Select(worker => worker.Result).ToArray();

            return sandwiches;
        }
        private Sandwich ProduceSandwich(SandwichKind kind)
        {
            return new Sandwich(kind, DateTimeOffset.Now.AddDays(2));
        }
    }
}