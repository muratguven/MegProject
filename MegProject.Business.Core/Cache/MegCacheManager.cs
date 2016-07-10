using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace MegProject.Business.Core.Cache
{
    public class MegCacheManager
    {

        public object GetCache()
        {
            StackTrace trace = new StackTrace();
            var callerMethod = trace.GetFrame(1).GetMethod();
            var attribute = callerMethod.GetCustomAttribute(typeof(MegCache));
            MethodInfo methodInfo = callerMethod as MethodInfo;
            if (attribute != null)
            {
                #region Get Cache Process


                string attrKey = (attribute as MegCache).Key;

                CacheKey key = CacheKey.New(attrKey);
                CacheProvider.Instance = new DefaultCacheProvider();
                if (CacheProvider.Instance.IsExist(key) && methodInfo != null)
                {

                    return CacheProvider.Instance.Get(key);

                }


                #endregion
            }
            
            return null;
        }


        public void SetCache(object value)
        {
            StackTrace trace = new StackTrace();
            var callerMethod = trace.GetFrame(1).GetMethod();
            var attribute = callerMethod.GetCustomAttribute(typeof(MegCache));            
            if (attribute != null)
            {
                string attrKey = (attribute as MegCache).Key;
                CacheKey key = CacheKey.New(attrKey);
                CacheProvider.Instance = new DefaultCacheProvider();
                CacheProvider.Instance.Set(key,value);
            }
            


        }

    }
}