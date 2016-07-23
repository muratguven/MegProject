using System;

namespace MegProject.Data.Core.Cache
{
    public class MegCacheManager<TCacheEntityKey> where TCacheEntityKey:class 
    {
        public static object GetCache()
        {


            Type keyType = typeof(TCacheEntityKey);
            CacheKey key = CacheKey.New(keyType.Name);
            CacheProvider.Instance = new DefaultCacheProvider();

            #region Get Cache Process

            if (CacheProvider.Instance.IsExist(key))
            {

                return CacheProvider.Instance.Get(key);

            }
            #endregion


            return null;
        }


        public static void SetCache(object value, int? cacheDuration = 0)
        {

            Type keyType = typeof(TCacheEntityKey);
            CacheKey key = CacheKey.New(keyType.Name);
            if (cacheDuration != default(int) && cacheDuration != null)
            {
                CacheProvider.CacheDuration = (int)cacheDuration;
            }
            CacheProvider.Instance = new DefaultCacheProvider();
            CacheProvider.Instance.Set(key, value);

        }
    }
}