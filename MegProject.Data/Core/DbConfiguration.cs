using EFCache;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegProject.Data.Core
{
    //public class DbConfiguration : System.Data.Entity.DbConfiguration
    //{
    //    public DbConfiguration()
    //    {
    //        var transactionHandler = new CacheTransactionHandler(new InMemoryCache());
    //        AddInterceptor(transactionHandler);
    //        var cachingPolicy = new CachingPolicy();
    //        Loaded +=
    //            (sender, args) => args.ReplaceService<DbProviderServices>(
    //            (s, _) => new CachingProviderServices(s, transactionHandler,
    //                cachingPolicy));
    //    }
    //}
}
