using System.Collections.Generic;
using MegProject.Business.Core;
using MegProject.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MegProject.Business.Manager.TestCacheApp
{
    public interface ITestCache:IApplicationCore
    {
        List<IdentityUser> GetAllUser();
        List<IdentityUser> GetUserByEmail(string email);
    }
}