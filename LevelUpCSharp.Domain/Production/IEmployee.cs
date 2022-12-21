using System.Collections.Generic;
using System.Threading;
using LevelUpCSharp.Collections;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
    public interface IEmployee
    {
        IEnumerable<Sandwich> Work(ProductionOrder order);
    }

    public class SlowMotion : IEmployee
    {
        private readonly IEmployee _inner;

        public SlowMotion(IEmployee inner)
        {
            _inner = inner;
        }

        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            Thread.Sleep(8 * 1000);
            return _inner.Work(order);
        }
    }

    public class Rabbit : IEmployee
    {
        private readonly IEmployee _inner;

        public Rabbit(IEmployee inner)
        {
            _inner = inner;
        }

        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            Thread.Sleep(1 * 1000);
            return _inner.Work(order);
        }
    }

    public class GenericTempo : IEmployee
    {
        private readonly IEmployee _inner;
        private readonly int _speed;

        public GenericTempo(IEmployee inner, int speed)
        {
            _inner = inner;
            _speed = speed;
        }

        public IEnumerable<Sandwich> Work(ProductionOrder order)
        {
            var result =  _inner.Work(order);
            Thread.Sleep(_speed * 1000);
            return result;
        }
    }

    public static class EmployeeExtensions
    {
        public static IEmployee AsRabbit(this IEmployee source)
        {
            return new Rabbit(source);
        }
    }
}