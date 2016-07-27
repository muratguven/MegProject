using System.Collections.Generic;
using System.Linq;
using MegProject.Business.Core;
using MegProject.Data.Core;
using Microsoft.AspNet.Identity.EntityFramework;
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
            userList = _unitOfWork.GetRepository<IdentityUser>().AsCached("userlist", 60).ToList();                   
            return userList;
        }

        public List<IdentityUser> GetUserByEmail(string email)
        {
            List<IdentityUser> userList = null;        
            userList = _unitOfWork.GetRepository<IdentityUser>().AsCached("userlist",60).Where(x=>x.Email==email).ToList();            
            return userList;
        } 

    }
}