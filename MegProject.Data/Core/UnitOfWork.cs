using System;
using System.Data.Entity;
using System.Security.AccessControl;
using System.Transactions;
using MegProject.Data.Core.Base;
using MegProject.Data.Models.Context;

namespace MegProject.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool disposed = false;

        public DbContext Context
        {
            get
            {
                _dbContext = new MegDbContext();
                return _dbContext;
            }
        }

        public UnitOfWork()
        {
            Database.SetInitializer<Models.Context.MegDbContext>(null);
            
            _dbContext = new MegDbContext();
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
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}