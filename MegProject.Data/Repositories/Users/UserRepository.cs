using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using MegProject.Data.Core;



namespace MegProject.Data.Repositories.Users
{
    public class UserRepository:GenericRepository<Data.Users>,IUserRepository
    {
        
        private MegProjectDbEntities db = new MegProjectDbEntities();

        public DataTable GetAllObjects()
        {

            using (db = new MegProjectDbEntities())
            {
                DataTable tb = new DataTable();
                DataSet ds = new DataSet();
                
                

                //var a = (from u in db.Users
                //    join t in db.UserProfile on u.Id equals t.UserId
                //    select u);

                var a = db.Users.ToList();

                return this.LinqQueryToDataTable(a);
            }

            
        }
    }
}