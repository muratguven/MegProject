using System.Collections.Generic;
using MegProject.Business.Core;
using MegProject.Dto;

namespace MegProject.Business.Manager.UserGroupAppService
{
    public interface IUserGroupApp:IApplicationCore
    {
        /// <summary>
        /// Tüm kullanıcı grubu listesini verir.
        /// </summary>
        /// <returns></returns>
        List<DtoUserGroups> GetAllUserGroups();

        /// <summary>
        /// Id ye göre kullanıcı grubunu getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DtoUserGroups GetUserGroup(int Id);

        /// <summary>
        /// Kullanıcı Grubu oluşuturur veya günceller
        /// </summary>
        /// <param name="userGroup"></param>
        /// <returns></returns>
        bool CreateOrUpdateUserGroup(DtoUserGroups userGroup);

        /// <summary>
        /// Kullanıcı Grubunu Siler
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteUserGroup(int Id);
    }
}