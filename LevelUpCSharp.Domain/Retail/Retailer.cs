﻿using System;
using System.Collections.Generic;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Retail
{
    public class Retailer
    {
        private static Retailer _instance;
        private readonly IVendingMachine _storage;

        protected Retailer(string name)
        {
            Name = name;
            _storage = new VendingMachine();
        }

        public static Retailer Instance => _instance ?? (_instance = new Retailer("Build-in"));

        public event Action<PackingSummary> Packed;
        public event Action<DateTimeOffset, Sandwich> Purchase;

        public string Name { get; }

        public Result<Sandwich> Sell(SandwichKind kind)
        {
            var notExist = !_storage.Has(kind);
            
            if (notExist)
            {
                return Result<Sandwich>.Failed();
            }

            var sandwich = _storage.Take(kind);
            OnPurchase(DateTimeOffset.Now, sandwich);
            return Result<Sandwich>.Success(sandwich);
        }

        public void Pack(IEnumerable<Sandwich> package, string deliver)
        {
            /* use linq to create summary, see constructor for expectations */

            Dictionary<SandwichKind, int> sums = new Dictionary<SandwichKind, int>();
            foreach (var sandwich in package)
            {
                _storage.Put(sandwich);

                if (sums.ContainsKey(sandwich.Kind) == false)
                {
                    sums.Add(sandwich.Kind, 0);
                }

                sums[sandwich.Kind]++;
            }

            var summaryPositions = new List<LineSummary>();

            foreach (var pair in sums)
            {
                summaryPositions.Add(new LineSummary(pair.Key, pair.Value));
            }

            var summary = new PackingSummary(summaryPositions, deliver);
            OnPacked(summary);
        }

        protected virtual void OnPacked(PackingSummary summary)
        {
            Packed?.Invoke(summary);
        }

        protected virtual void OnPurchase(DateTimeOffset time, Sandwich product)
        {
            Purchase?.Invoke(time, product);
        }
    }
}
