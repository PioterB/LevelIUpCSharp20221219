using System.Threading;

namespace LevelUpCSharp.Storage.Safe
{
    public class Vault<TSecret> : IVault<TSecret>
    {
        private TSecret _secretSafe;
        private readonly SemaphoreSlim _in = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _out = new SemaphoreSlim(0);

        public void Put(TSecret secret)
        {
            _in.Wait();
            _secretSafe = secret;
            _out.Release();
        }

        public TSecret Get()
        {
            _out.Wait();
            var result = _secretSafe;
            _secretSafe = default(TSecret);
            _in.Release();
            return result;
        }
    }
}