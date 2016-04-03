
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.BulkInsert.Extensions;
using MegProject.Data;

namespace MegProject.Data.Core
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private bool disposed = false;

        //private DbSet<T> ObjectDbset
        //{
        //    set { ObjectDbset = value; }
        //    get { return ObjectDbset; }
        //}

     

        private DbContext Context;
        private DbSet<T> ObjectDbset;

        public MegProjectDbEntities context
        {
            get { return context; }
            set { context = new MegProjectDbEntities(); }
        }

        protected GenericRepository()
        {
            Context = new MegProjectDbEntities();
            ObjectDbset = Context.Set<T>();
        }

        
        protected GenericRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return ObjectDbset.ToList().AsQueryable();
        }

        public IQueryable<T> FindList(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return ObjectDbset.Where(where).ToList().AsQueryable();
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return ObjectDbset.FirstOrDefault(where);
        }

        public IQueryable<T> AsQueryable()
        {
            return ObjectDbset;
        }


        public void Add(T entity)
        {            
            ObjectDbset.Add(entity);
        }

       
        public void Delete(object id)
        {
            T entityDelete = ObjectDbset.Find(id);
            ObjectDbset.Remove(entityDelete);
        }

        public void Update(T entity)
        {
            ObjectDbset.Attach(entity);            
            Context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> GetByPaging<TResult>(int pageNumber, int pageSize, System.Linq.Expressions.Expression<Func<T, TResult>> orderByQuery, bool isAscendingOrder, out int rowsCount)
        {
            if (pageSize <= 0) pageSize = 20;

            IQueryable<T> query = ObjectDbset;
            //Total result count
            rowsCount = query.Count();

            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNumber <= 0) pageNumber = 1;

            //Calculate nunber of rows to skip on pagesize
            int excludedRows = (pageNumber - 1) * pageSize;

            query = isAscendingOrder ? query.OrderBy(orderByQuery) : query.OrderByDescending(orderByQuery);

            //Skip the required rows for the current page and take the next records of pagesize count
            return query.Skip(excludedRows).Take(pageSize);
        }

        public async Task<T> AddAsync(T entity)
        {
            ObjectDbset.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            ObjectDbset.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            ObjectDbset.Remove(entity);
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return true;
        }




        #region Data Table 

        public  DataTable LinqQueryToDataTable(IEnumerable<dynamic> v)
        {
            
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            PropertyInfo[] infos = firstRecord.GetType().GetProperties();

            
            DataTable table = new DataTable();

            
            foreach (var info in infos)
            {

                Type propType = info.PropertyType;

                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) //Nullable types should be handled too
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            
            DataRow row;

            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ? infos[i].GetValue(record) : DBNull.Value;
                }

                table.Rows.Add(row);
            }

            
            table.AcceptChanges();

            return table;
        }

        #endregion



        // Save All
        public int Save()
        {
            return Context.SaveChanges();
        }


        public void SaveAll(List<T> entityList)
        {
            foreach (var item in entityList)
            {
                ObjectDbset.Add(item);
                Save();
            }
            
        }


        public bool BulkInsert(List<T> entityList)
        {
            using (var transactionScope = new TransactionScope())
            {
                Context.BulkInsert(entityList);

                Context.SaveChanges();

                transactionScope.Complete();
                return true;
            }

        }


        public bool IsInDb(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return ObjectDbset.Any(where);
        }

        #region Disposing

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
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