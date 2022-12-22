using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Helpers;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class SmallFactory
    {
        private readonly IEmployee _employee;
        private static readonly Random Rand;

        private readonly IWarehouse<Sandwich> _warehouse;
        private readonly ConcurrentQueue<ProductionOrder> _workload;

        private readonly Thread _worker;
        private readonly CancellationTokenSource _cancel;

        static SmallFactory()
        {
            Rand = new Random((int)DateTime.Now.Ticks);
        }

        public SmallFactory(string name)
            : this(name, new LevelUpCSharp.Collections.Concurrent.Warehouse<Sandwich>(), new SandwichMaster())
        {
        }

        public SmallFactory(string name, IWarehouse<Sandwich> warehouse)
            : this(name, warehouse, new SandwichMaster().AsRabbit())
        {
        }

        public SmallFactory(string name, IWarehouse<Sandwich> warehouse, IEmployee employee)
        {
            Name = name;
            _employee = employee;
            _warehouse = warehouse;
            _cancel = new CancellationTokenSource();
            _workload = new ConcurrentQueue<ProductionOrder>();
            _worker = new Thread(DoProduction) { IsBackground = true };
            _worker.Start();
        }

        public event Action<Sandwich[]> Produced;

        public string Name { get; }

        public void Shutdown()
        {
            _cancel.Cancel();
            _worker.Join();
        }

        public IEnumerable<Sandwich> Buy(int howMuch = 0)
        {
            var toSell = _warehouse.PeakRange(howMuch);
            return toSell;
        }

        public void Order(SandwichKind kind, int count)
        {
            var order = new ProductionOrder(kind, count);
            _workload.Enqueue(order);
        }

        public IEnumerable<StockItem> GetStock()
        {
            Dictionary<SandwichKind, int> counts = new Dictionary<SandwichKind, int>()
            {
                {SandwichKind.Cheese, 0},
                {SandwichKind.Chicken, 0},
                {SandwichKind.Beef, 0},
                {SandwichKind.Pork, 0},
            };

            foreach (Sandwich sandwich in _warehouse)
            {
                counts[sandwich.Kind] += 1;
            }

            var result = new StockItem[counts.Count];

            int i = 0;
            foreach (var count in counts)
            {
                result[i] = new StockItem(count.Key, count.Value);
                i++;
            }

            return result;
        }

        private void DoProduction(object obj)
        {
            SandwichKind[] sandwichKinds = EnumHelper.GetValues<SandwichKind>();
            var token = _cancel.Token;
            while (!token.IsCancellationRequested)
            {
                var wasOrder = _workload.TryDequeue(out ProductionOrder order);
                if (!wasOrder)
                {
                    var kind = Rand.Next(1, sandwichKinds.Length);
                    order = new ProductionOrder((SandwichKind)kind, 10);
                }

                for (int i = 0; i < order.Count; i++)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        var sandwiches = _employee.Work(new ProductionOrder(order.Kind, 1));

                        _warehouse.Add(sandwiches.Result);

                        Produced?.Invoke(sandwiches.Result.ToArray());
                    });
                }
            }
        }
    }
}
