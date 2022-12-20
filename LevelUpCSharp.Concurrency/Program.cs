using System;
using System.Threading;

namespace LevelUpCSharp.Concurrency
{
    class Program
    {
        private static Random r = new Random();

        private static SemaphoreSlim _in = new SemaphoreSlim(1);
        private static SemaphoreSlim _out = new SemaphoreSlim(0);

        static void Main(string[] args)
        {
            var courier = new Thread(Pickup);
            var sender = new Thread(Insert);
            var vault = new Vault<int>();

            courier.Start(vault);
            sender.Start(vault);

            Console.ReadKey(true);
            Console.WriteLine("Closing...");
        }

        private static void Insert(object? obj)
        {
            var vault = (Vault<int>)obj;

            while (true)
            {
                var found = r.Next(100);
                Console.WriteLine("[B] i have: " + found);

                _in.Wait();
                vault.Put(found);
                _out.Release();
                Console.WriteLine("[B] stored: " + found);

                Console.WriteLine("[B] break");
                Thread.Sleep(3 * 1000);
                
            }
        }

        private static void Pickup(object? obj)
        {
            var vault = (Vault<int>)obj;

            while (true)
            {
                Console.WriteLine("[A] ready to pickup");

                _out.Wait();
                var found = vault.Get();
                _in.Release();

                Console.WriteLine("[A] get:" + found);
                Thread.Sleep(7 * 1000);
            }
        }
    }
}
