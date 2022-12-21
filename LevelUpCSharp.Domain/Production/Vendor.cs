﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Helpers;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public class Vendor
    {
        private static readonly Random Rand;

        private readonly IWarehouse<Sandwich> _warehouse;
        private readonly ConcurrentQueue<ProductionOrder> _workload;

        private readonly Thread _worker;

        static Vendor()
        {
            Rand = new Random((int)DateTime.Now.Ticks);
        }

        public Vendor(string name, IWarehouse<Sandwich> warehouse)
        {
            Name = name;
            _warehouse = warehouse;
            _workload = new ConcurrentQueue<ProductionOrder>();
            _worker = new Thread(DoProduction) { IsBackground = true };
            _worker.Start();
        }

        public event Action<Sandwich[]> Produced;

        public string Name { get; }

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

        private void DoProduction(object obj)
        {
            SandwichKind[] sandwichKinds = EnumHelper.GetValues<SandwichKind>();

            while (true)
            {
                var wasOrder = _workload.TryDequeue(out ProductionOrder order);
                if (!wasOrder)
                {
                    var kind = Rand.Next(1, sandwichKinds.Length);
                    order = new ProductionOrder((SandwichKind)kind, 1);
                }
                
                for (int i = 0; i < order.Count; i++)
                {
                    var sandwich = Produce(order.Kind);

                    _warehouse.Add(sandwich);

                    Produced?.Invoke(new[] { sandwich });
                }

                Thread.Sleep(5 * 1000);
            }
        }

        private Sandwich ProduceSandwich(SandwichKind kind, DateTimeOffset addMinutes)
        {
            return new Sandwich(kind, addMinutes);
        }
    }
}
