using System.Collections.Generic;
using MegProject.Business.Core;
using MegProject.Data.Models;
using MegProject.Dto;

namespace MegProject.Business.Manager.RoleAppService
{
    public interface IRoleApp:IApplicationCore
    {

        #region Roles

        /// <summary>
        /// Kullanıcı Id nin rol adında role sahip mi değil mi sonucunu döndürür.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsInRole(string role, int userId);

        /// <summary>
        /// Bütün Rolleri dödürür.
        /// </summary>
        /// <returns></returns>
        List<Roles> GetAllRoles();

        /// <summary>
        /// Role ID ye göre rol bilgisini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Roles GetRole(int? Id);

        /// <summary>
        /// Role bilgisi ile role oluşuturur.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool CreateOrUpdateRole(Roles role);

        /// <summary>
        /// Role ve Actionlar ile role ve RoleAction kaydı oluşturur.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool CreateOrUpdateRole(Roles role, List<RolePermissions> permissions);


        /// <summary>
        /// Role ID ye göre role Controller action  bilgilerini getirir.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<RolePermissions> GetRolePermissions(int roleId);
        /// <summary>
        /// Verilen Id ye göre rol bilgisini siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteRole(int? Id);

        #endregion


        #region Permissions

        /// <summary>
        /// Kullanıcının Contoller ve bağlı bulunan action izni var mı sorgusunu gerçekleştirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        bool IsInPermissionDetail(int userId, string controllerName, string actionName);

        /// <summary>
        /// Yeni İzin Oluşturur veya Günceller.
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool CreateOrUpdatePermission(Permission permission);

        /// <summary>
        /// İzin ve İzin Detaylarını Ekler veya Günceller
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        bool CreateOrUpdatePermission(Permission permission, List<PermissionDetails> details);

        #endregion

    }
}