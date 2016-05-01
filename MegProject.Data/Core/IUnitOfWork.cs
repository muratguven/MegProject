using System;
using MegProject.Data.Repositories.RoleAction;
using MegProject.Data.Repositories.Roles;
using MegProject.Data.Repositories.SystemActions;
using MegProject.Data.Repositories.SystemControllers;
using MegProject.Data.Repositories.UserGroup;
using MegProject.Data.Repositories.UserProfile;
using MegProject.Data.Repositories.UserRoles;
using MegProject.Data.Repositories.Users;

namespace MegProject.Data.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserRolesRepository UserRolesRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
        ISystemControllerRepository SystemControllerRepository { get; }
        ISystemActionRepository SystemActionRepository { get; }
        IRolesRepository RolesRepository { get; }
        IRoleActionRepository RoleActionRepository { get; }
        int Commit();
    }
}