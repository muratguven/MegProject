using System.Collections.Generic;
using System.Linq;
using MegProject.Business.Core;
using MegProject.Business.Core.Cache;
using MegProject.Data.Core;
using MegProject.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using Ninject;

namespace MegProject.Business.Manager.TestCacheApp
{
    public class TestCache:ApplicationCore,ITestCache
    {
        [Inject]
        public IUnitOfWork _unitOfWork { private get; set; }

        [MegCache(duration:60)]
        public List<IdentityUser> GetAllUser()
        {
            List<IdentityUser> userList = null;
            CacheKey key = CacheKey.New("Users");
            CacheProvider.Instance = new DefaultCacheProvider();
            if (CacheProvider.Instance.IsExist(key))
            {
                userList = CacheProvider.Instance.Get(key) as List<IdentityUser>;
            }
            else
            {
                userList = _unitOfWork.GetRepository<IdentityUser>().GetAll().ToList();
                CacheProvider.Instance.Set(key,userList);
            }
            
            return userList;

        }
    }
}