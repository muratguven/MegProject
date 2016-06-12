using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using MegProject.Data.Core;
using MegProject.Data.Models.Context;


namespace MegProject.Data.Repositories.Users
{
    public class UserRepository:GenericRepository<Models.Users>,IUserRepository
    {


        //public DataTable GetAllObjects()
        //{
        //    var db = Context;
        //    using (db)
        //    {
        //        DataTable tb = new DataTable();
        //        DataSet ds = new DataSet();
                
                

        //        //var a = (from u in db.Users
        //        //    join t in db.UserProfile on u.Id equals t.UserId
        //        //    select u);

        //        var a = db..Users.ToList();

        //        return this.LinqQueryToDataTable(a);
        //    }

            
        //}
    }
}