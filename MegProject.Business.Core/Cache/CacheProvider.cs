namespace MegProject.Business.Core.Cache
{
    public abstract class CacheProvider
    {
        public static int CacheDuration = 60;
        public static  CacheProvider Instance { get; set; }

        public abstract object Get(CacheKey key);
        public abstract void Set(CacheKey key, object value);
        public abstract bool IsExist(CacheKey key);
        public abstract void Remove(CacheKey key);

    }
}