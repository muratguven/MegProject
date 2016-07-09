namespace MegProject.Business.Core.Cache
{
    public class CacheKey
    {
        private readonly string _generatedKey;
        

        public CacheKey(string objectId)
        {
            _generatedKey = objectId;
        }

        public static CacheKey New(string Id)
        {
            return new CacheKey(Id);
        }


        public override string ToString()
        {
            return _generatedKey;
        }
    }
}