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

        [MegCache(key:"Users",duration:60)]
        public List<IdentityUser> GetAllUser()
        {
            List<IdentityUser> userList = null;
            MegCacheManager cacheManager = new MegCacheManager();
            var test= cacheManager.GetCache();
            
            if (test != null)
            {
                userList = test as List<IdentityUser>;
            }
            else
            {
                userList = _unitOfWork.GetRepository<IdentityUser>().GetAll().ToList();               
                cacheManager.SetCache(userList);
            }
            
            return userList;

        }
    }
}