using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.BulkInsert.Extensions;
using MegProject.Data.Core.Cache;

namespace MegProject.Data.Core.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {



        public DbContext Context { get; set; }
        public DbSet<T> ObjectDbset { get; set; }



        protected GenericRepository()
        {

            ObjectDbset = Context.Set<T>();
        }


        public GenericRepository(DbContext context)
        {
            Context = context;
            ObjectDbset = Context.Set<T>();
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


        public T Add(T entity)
        {
            return ObjectDbset.Add(entity);
        }


        public void Delete(object id)
        {
            T entityDelete = ObjectDbset.Find(id);
            ObjectDbset.Remove(entityDelete);
        }

        public T Update(T entity)
        {
            var result = ObjectDbset.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return result;
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
            using (var scope = Context.Database.BeginTransaction())
            {
                try
                {
                    ObjectDbset.Add(entity);
                    await Context.SaveChangesAsync();
                    scope.Commit();
                    return entity;
                }
                catch (Exception)
                {
                    scope.Rollback();
                    return null;
                }

            }

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


        public async Task<IQueryable<T>> GetAllAsync()
        {
            return ObjectDbset.ToList().AsQueryable();
        }

        public IQueryable<T> AsCached(string key, int? cacheDuration)
        {
            if (!String.IsNullOrEmpty(key))
            {
                var cacheValues = MegCacheManager.GetCache(key);
                if (cacheValues != null)
                {
                    return cacheValues as IQueryable<T>;
                }

                MegCacheManager.SetCache(key, ObjectDbset, cacheDuration);

            }
            return ObjectDbset;
        }

        #region Data Table 

        public DataTable LinqQueryToDataTable(IEnumerable<dynamic> v)
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
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    int result = Context.SaveChanges();
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }


            }

        }


        public void SaveAll(List<T> entityList)
        {
            using (var scope = Context.Database.BeginTransaction())
            {
                foreach (var item in entityList)
                {
                    ObjectDbset.Add(item);
                    Save();
                }
                scope.Commit();
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





    }
}