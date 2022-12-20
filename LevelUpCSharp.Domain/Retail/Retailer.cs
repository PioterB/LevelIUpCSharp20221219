using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = _storage.Take(kind);
            if (result.Fail)
            {
                return Result<Sandwich>.Failed();
            }
            OnPurchase(DateTimeOffset.Now, result.Value);
            return Result<Sandwich>.Success(result.Value);
        }

        public void Pack(IEnumerable<Sandwich> package, string deliver)
        {
            package = package.ToArray();

            _storage.Put(package);

            var summary = ComputeReport(package, deliver);
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

        private static PackingSummary ComputeReport(IEnumerable<Sandwich> package, string deliver)
        {
            var summaryPositions = package
                .GroupBy(
                    p => p.Kind,
                    (kind, sandwiches) => new LineSummary(kind, sandwiches.Count()))
                .ToArray();

            var summary = new PackingSummary(summaryPositions, deliver);
            return summary;
        }
    }
}
