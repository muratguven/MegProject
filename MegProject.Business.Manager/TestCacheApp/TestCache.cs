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
        //TODO:Cache kullanım yapısı düzeltilecek.
        [Inject]
        public IUnitOfWork _unitOfWork { private get; set; }

        
        public List<IdentityUser> GetAllUser()
        {
            List<IdentityUser> userList = null;
            
            var test= MegCacheManager<IdentityUser>.GetCache();
            
            if (test != null)
            {
                userList = test as List<IdentityUser>;
            }
            else
            {
                userList = _unitOfWork.GetRepository<IdentityUser>().GetAll().ToList();
                MegCacheManager<IdentityUser>.SetCache(userList,120);
            }
            
            return userList;

        }

        public List<IdentityUser> GetUserByEmail(string email)
        {
            List<IdentityUser> userList = null;
            var test = MegCacheManager<IdentityUser>.GetCache();
            if (test != null)
            {
                userList = ((List<IdentityUser>) test).Where(x => x.Email == email).ToList();
                
            }
            if(userList==null)
            {
                userList = _unitOfWork.GetRepository<IdentityUser>().GetAll().ToList();
                MegCacheManager<IdentityUser>.SetCache(userList);
            }

            return userList;
        } 

    }
}