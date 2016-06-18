using System;


namespace MegProject.Data.Core
{
    public interface IUnitOfWork: IDisposable
    {
        
        Data.Core.Base.IGenericRepository<T> GetRepository<T>() where T : class;

        int Commit();
    }
}