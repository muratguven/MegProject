using System;
using System.Data.Entity;
using System.Security.AccessControl;
using System.Transactions;
using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.UserRoles;
using MegProject.Data.Repositories.Users;
using MegProject.Data.Core.Base;

namespace MegProject.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork()
        {
           //TODO: Constructor overload 
        }


        public UnitOfWork(DbContext context)
        {
            Database.SetInitializer<Models.Context.MegDbContext>(null);
            if (context == null)
            {
                throw new ArgumentNullException("context can not be null!");
            }
            _dbContext = context;

            // dbContext configurations codes here:

        }

        
        public Data.Core.Base.IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new Data.Core.Base.GenericRepository<T>(_dbContext);
        }

        public int Commit()
        {
            using (var tcx = _dbContext.Database.BeginTransaction())
            {
               return  _dbContext.SaveChanges();
            }
        }

        #region Disposing

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose(); 
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