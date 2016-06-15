using System;
using System.Data.Entity;
using System.Transactions;
using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.UserRoles;
using MegProject.Data.Repositories.Users;

namespace MegProject.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork()
        {
           
        }

        public UnitOfWork(DbContext context)
        {
            Context = context;

        }


        public IUserRepository UserRepository { get; private set; }
        public IUserRolesRepository UserRolesRepository { get; private set; }
        public ISystemControllerRepository SystemControllerRepository { get; private set; }
        public ISystemActionRepository SystemActionRepository { get; private set; }
        public IRolesRepository RolesRepository { get; private set; }
        

      

        public int Commit()
        {
            using (var tcx = Context.Database.BeginTransaction())
            {
               return  Context.SaveChanges();
            }
        }

        #region Disposing

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose(); 
            }
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}