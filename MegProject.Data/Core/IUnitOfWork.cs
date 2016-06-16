using System;

using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.UserRoles;
using MegProject.Data.Repositories.Users;

namespace MegProject.Data.Core
{
    public interface IUnitOfWork: IDisposable
    {
        Data.Core.Base.IGenericRepository<T> GetRepository<T>() where T : class;


        //IUserRepository UserRepository { get; }
        //IUserRolesRepository UserRolesRepository { get; }

        //ISystemControllerRepository SystemControllerRepository { get; }
        //ISystemActionRepository SystemActionRepository { get; }
        //IRolesRepository RolesRepository { get; }

        int Commit();
    }
}