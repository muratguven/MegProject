using System;
using System.Runtime.Caching.Configuration;

namespace MegProject.Data.Core.Cache
{
    public static  class MegCacheManager 
    {
        public static object GetCache(string key)
        {
            
            CacheKey cacheKey = CacheKey.New(key);
            
            CacheProvider.Instance = new DefaultCacheProvider();
           
            #region Get Cache Process

            if (CacheProvider.Instance.IsExist(cacheKey))
            {
                return CacheProvider.Instance.Get(cacheKey);
            }
            #endregion


            return null;
        }


        public static void SetCache(string key, object value, int? cacheDuration = 0)
        {
            
            CacheKey cacheKey = CacheKey.New(key);
            if (cacheDuration != default(int) && cacheDuration != null)
            {
                CacheProvider.CacheDuration = (int)cacheDuration;
            }
            CacheProvider.Instance = new DefaultCacheProvider();
            if (CacheProvider.Instance.IsExist(cacheKey))
            {
                CacheProvider.Instance.Remove(cacheKey);
            }
            CacheProvider.Instance.Set(cacheKey, value);

        }

       


    }
}