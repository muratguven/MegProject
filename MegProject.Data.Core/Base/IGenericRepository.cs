using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MegProject.Data.Core.Base
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Context bilgisi
        /// </summary>
        DbContext Context { get; set; }
        /// <summary>
        /// DbSet bilgisi 
        /// </summary>
        DbSet<T> ObjectDbset { get; set; }
        /// <summary>
        /// Tüm entity listesini getirir.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Verilen şarta göre entity listesini getirir.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> where);
        /// <summary>
        /// Şarta göre entity bilgisini getirir.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> where);
        /// <summary>
        /// Veriyi queryable tipine çevirir.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AsQueryable();
        /// <summary>
        ///  Entity ekleme işlemi
        /// </summary>
        /// <param name="entity"></param>
        T Add(T entity);
        /// <summary>
        /// 1-50 entity listesi için toplu ekleme işlemini yapar.
        /// </summary>
        /// <param name="entityList"></param>
        void SaveAll(List<T> entityList);
        /// <summary>
        /// Verilen Id 'ye göre silme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// Entity güncelleme işlemini yapar.(Dikkat bu işlemler veri tabanından satırı siler)
        /// </summary>
        /// <param name="entity"></param>
        T Update(T entity);
        /// <summary>
        /// Verilen entity listesini toplu olarak ekler.
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        bool BulkInsert(List<T> entityList);
        /// <summary>
        /// Verileri sayfalı veya parçalı şekilde DB den çekme işlemini yapar.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByQuery"></param>
        /// <param name="isAscendingOrder"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        IQueryable<T> GetByPaging<TResult>(int pageNumber, int pageSize, Expression<Func<T, TResult>> orderByQuery,
            bool isAscendingOrder, out int rowsCount);
        /// <summary>
        /// Yapılan işlemleri(CRUD) veri tabanına bildirir.
        /// </summary>
        /// <returns></returns>
        int Save();
        /// <summary>
        /// Verilen veri listesini DataTable dönüştürür.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        DataTable LinqQueryToDataTable(IEnumerable<dynamic> v);
        /// <summary>
        /// Asenkron ekleme işlemini yapar.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Asenkron güncelleme işlemini yapar.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// Asenkron silme işlemi yapar.(Dikkat bu işlemler veri tabanından satırı siler)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);
        /// <summary>
        /// Verilen şarta göre entity veri tabanında var mı sorgusunu yapar.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsInDb(System.Linq.Expressions.Expression<Func<T, bool>> where);
        /// <summary>
        /// Asenkron Tüm dataları getirmeye yarar.
        /// </summary>7017581268
        /// <returns></returns>
        Task<IQueryable<T>> GetAllAsync();
        /// <summary>
        /// Caching işlemini yapar yada key göre datayı Cache'den okur.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IQueryable<T> AsCached(string key, int? cacheDuration);




    }
}