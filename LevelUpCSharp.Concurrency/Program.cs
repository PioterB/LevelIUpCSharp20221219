using System;
using System.Threading;
using LevelUpCSharp.Storage.Safe;

namespace LevelUpCSharp
{
    class Program
    {
        private static Random r = new Random();

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

            while (true)
            {
                Console.WriteLine("[Pickup] ready to pickup");

                var found = vault.Get();

                Console.WriteLine("[Pickup] get:" + found);
                Thread.Sleep(7 * 1000);
            }
        }
    }
}
