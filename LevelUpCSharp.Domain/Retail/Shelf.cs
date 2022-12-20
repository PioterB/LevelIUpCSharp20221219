using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevelUpCSharp.Helpers;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Retail
{
    public class Shelf : IShelf
    {
        private readonly IDictionary<SandwichKind, Queue<Sandwich>> _lines;

        public Shelf()
        {
            _lines = InitializeLines();
        }

        public IEnumerator<Sandwich> GetEnumerator()
        {
            return _lines.Values
                .SelectMany(sandwiches => sandwiches)
                .ToList()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Put(Sandwich item)
        {
            _lines[item.Kind].Enqueue(item);
        }

        public void Put(IEnumerable<Sandwich> package)
        {
            foreach (var item in package)
            {
                _lines[item.Kind].Enqueue(item);
            }
        }

        public Result<Sandwich> Take(SandwichKind kind)
        {
            var dontHave = !HasImpl(kind);

            if (dontHave)
            {
                return Result<Sandwich>.Failed();
            }

            var sandwich = _lines[kind].Dequeue();

            return Result<Sandwich>.Success(sandwich);
        }

        public bool Has(SandwichKind kind)
        {
            return HasImpl(kind);
        }

        private bool HasImpl(SandwichKind kind)
        {
            return _lines.ContainsKey(kind) && _lines[kind].Count > 0;
        }

        private IDictionary<SandwichKind, Queue<Sandwich>> InitializeLines()
        {
            var result = new Dictionary<SandwichKind, Queue<Sandwich>>();

            foreach (var sandwichKind in EnumHelper.GetValues<SandwichKind>())
            {
                result.Add(sandwichKind, new Queue<Sandwich>());
            }

            return result;
        }
    }
}
