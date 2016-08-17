using System;
using System.Threading.Tasks;


namespace MegProject.Data.Core
{
    public interface IUnitOfWork: IDisposable
    {
        
        Data.Core.Base.IGenericRepository<T> GetRepository<T>() where T : class;

        int Commit();
        Task<int> CommitAsync();
    }
}