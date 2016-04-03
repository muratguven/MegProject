using System.Collections.Generic;
using MegProject.Business.Core;
using MegProject.Dto;

namespace MegProject.Business.Manager.RoleAppService
{
    public interface IRoleApp:IApplicationCore
    {
        /// <summary>
        /// Kullanıcı Id nin rol adında role sahip mi değil mi sonucunu döndürür.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsInRole(string role,int userId);

        /// <summary>
        /// Bütün Rolleri dödürür.
        /// </summary>
        /// <returns></returns>
        List<DtoRoles> GetAllRoles();

        /// <summary>
        /// Role ID ye göre rol bilgisini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DtoRoles GetRole(int? Id);

        /// <summary>
        /// Role bilgisi ile role oluşuturur.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool CreateOrUpdateRole(DtoRoles role);

        /// <summary>
        /// Role ve Actionlar ile role ve RoleAction kaydı oluşturur.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="actions"></param>
        /// <returns></returns>
        bool CreateOrUpdateRole(DtoRoles role,List<DtoRoleAction> actions);

        /// <summary>
        /// Kullanıcının Contoller ve bağlı bulunan action izni var mı sorgusunu gerçekleştirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        bool IsInControlActions(int userId, string controllerName, string actionName);
        /// <summary>
        /// Role ID ye göre role Controller action  bilgilerini getirir.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<DtoRoleAction> GetRoleAction(int roleId);
        /// <summary>
        /// Verilen Id ye göre rol bilgisini siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteRole(int? Id);
    }
}