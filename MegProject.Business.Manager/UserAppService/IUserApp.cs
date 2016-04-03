using System.Collections.Generic;
using System.Threading.Tasks;
using MegProject.Business.Core;
using MegProject.Dto;

namespace MegProject.Business.Manager.UserAppService
{
    public interface IUserApp:IApplicationCore
    {
        /// <summary>
        /// Tüm kullanıcıları getirir.
        /// </summary>
        /// <returns></returns>
        List<DtoUsers> GetAllUsers();

        /// <summary>
        /// Email ve pass e göre kullanıcı bilgisini getirir.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        DtoUsers GetUser(string email, string password);

        /// <summary>
        /// Kullanıcı ve rolleri ile yeni kullanıcı ve rol atama,güncelleme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        bool CreateOrUpdateUser(DtoUsers user, List<DtoRoles> userRoles);

        /// <summary>
        /// Kullanıcı Id ye göre Kullanıcı rollerini getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
       List<DtoUserRoles> GetUserRole(int userId);
        /// <summary>
        /// Kullanıcı Id ye göre kullanıcı bilgisini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        DtoUsers GetUser(int? Id);

        /// <summary>
        /// Id'e göre kullanıcı bilgisini siler. ( Status =-1)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteUser(int? Id);

        /// <summary>
        /// Asenkron olarak Tüm Kullanıcıları getirir.
        /// </summary>
        /// <returns></returns>
        List<DtoUsers> GetAllUsersAsync();
    }
}