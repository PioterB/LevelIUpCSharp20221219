using System;
using System.Threading;
using System.Threading.Tasks;
using LevelUpCSharp.Storage.Safe;

namespace LevelUpCSharp
{
    class Program
    {
        private static Random _r = new Random();
        private static bool _close = false;

        static void Main(string[] args)
        {
            var worker = new Task(Pipeline, TaskCreationOptions.LongRunning);
            worker.Start();

            Console.ReadKey(true);
            Console.WriteLine("Closing...");
            _close = true;
            worker.Wait();
            Console.WriteLine("Closed");
        }

        private static void Insert(object? obj)
        {
            var vault = (Vault<int>)obj;

            while (!_close)
            {
                var found = _r.Next(100);
                Console.WriteLine("[Insert] i have: " + found);

                vault.Put(found);
                Console.WriteLine("[Insert] stored: " + found);

                Console.WriteLine("[Insert] break");
                Thread.Sleep(3 * 1000);
            }
        }

        private static void Pickup(object? obj)
        {
            var vault = (Vault<int>)obj;

            while (!_close)
            {
                Console.WriteLine("[Pickup] ready to pickup");

                var found = vault.Get();

                Console.WriteLine("[Pickup] get:" + found);
                Thread.Sleep(7 * 1000);
            }
        }

        private static void Pipeline()
        {
            while (!_close)
            {
                var producent = Task.Run(() => GetNumber());

                /*
                 * logic next to production
                 */

                var number = producent.Result;

                Task.Run(() => ConsumNumber(number)).Wait(); 
            }
        }

        private static void PipelineV2()
        {
            while (!_close)
            {
                Task.Run(() => GetNumber())
                    .ContinueWith(prev => ConsumNumber(prev.Result))
                    .Wait();
            }
        }

        private static void ConsumNumber(int number)
        {
            Thread.Sleep(7 * 1000);

            Console.WriteLine("[Pickup] get:" + number);
        }

        private static int GetNumber()
        {
            Thread.Sleep(3 * 1000);

            var found = _r.Next(100);
            Console.WriteLine("[Insert] i have: " + found);
            return found;
        }
    }
}
