namespace MegProject.Data.Core.Cache
{
    public class CacheKey
    {
        private readonly string _generatedKey;


        public CacheKey(string key)
        {
            _generatedKey = key;
        }

        public static CacheKey New(string key)
        {
            return new CacheKey(key);
        }


        public override string ToString()
        {
            return _generatedKey;
        }
    }
}