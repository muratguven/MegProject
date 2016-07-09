
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;

namespace MegProject.Business.Core.Cache
{
    public class DefaultCacheProvider:CacheProvider
    {

        private ObjectCache _cache = null;
        private CacheItemPolicy _policy = null;
        public DefaultCacheProvider()
        {
            _cache = MemoryCache.Default;
            _policy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(CacheDuration),
                RemovedCallback = new CacheEntryRemovedCallback(CacheRemovedCallBack)
            };
        }

        public static void CacheRemovedCallBack(CacheEntryRemovedArguments arguments)
        {
            Trace.WriteLine("----------Cache Expire Oldu----------");
            Trace.WriteLine("Key : " + arguments.CacheItem.Key);
            Trace.WriteLine("Value : " + arguments.CacheItem.Value.ToString());
            Trace.WriteLine("RemovedReason : " + arguments.RemovedReason);
            Trace.WriteLine("-------------------------------------");
        } 

        public override object Get(CacheKey key)
        {
            object retVal = null;
            try
            {
                retVal = _cache.Get(key.ToString());
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hata : CacheProvider.Get()\n" + e.Message);
                throw new Exception("Cache Get sırasında bir hata oluştu!", e);
                
            }
            return retVal;            
        }

        public override void Set(CacheKey key, object value)
        {
            try
            {
                Trace.WriteLine("Cache Setleniyor. Key : " + key.ToString());
                _cache.Set(key.ToString(), value, _policy);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hata : CacheProvider.Set()\n" + e.Message);
                throw new Exception("Cache Set sırasında bir hata oluştu!", e);
            }

            
        }

        public override bool IsExist(CacheKey key)
        {
            return _cache.Any(x => x.Key == key.ToString());
        }

        public override void Remove(CacheKey key)
        {
            _cache.Remove(key.ToString());
        }
    }
}