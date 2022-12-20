namespace LevelUpCSharp.Storage
{
    public class Vault<TSecret> : IVault<TSecret>
    {
        private TSecret _secret;

        public void Put(TSecret secret)
        {
            _secret = secret;
        }

        public TSecret Get()
        {
            var result = _secret;
            _secret = default(TSecret);
            return result;
        }
    }
}