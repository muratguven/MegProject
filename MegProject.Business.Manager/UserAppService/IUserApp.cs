using System.Collections.Generic;
using System.Threading.Tasks;
using MegProject.Business.Core;
using MegProject.Data.Models;
using MegProject.Dto;

namespace MegProject.Business.Manager.UserAppService
{
    public interface IUserApp:IApplicationCore
    {
       // /// <summary>
       // /// Tüm kullanıcıları getirir.
       // /// </summary>
       // /// <returns></returns>
       // List<Users> GetAllUsers();

       // /// <summary>
       // /// Email ve pass e göre kullanıcı bilgisini getirir.
       // /// </summary>
       // /// <param name="email"></param>
       // /// <param name="password"></param>
       // /// <returns></returns>
       // Users GetUser(string email, string password);

       // /// <summary>
       // /// Kullanıcı ve rolleri ile yeni kullanıcı ve rol atama,güncelleme işlemlerini gerçekleştirir.
       // /// </summary>
       // /// <param name="user"></param>
       // /// <param name="userRoles"></param>
       // /// <returns></returns>
       // bool CreateOrUpdateUser(Users user, List<Roles> userRoles);

       // /// <summary>
       // /// Kullanıcı Id ye göre Kullanıcı rollerini getirir.
       // /// </summary>
       // /// <param name="userId"></param>
       // /// <returns></returns>
       //List<UserRoles> GetUserRole(int userId);
       // /// <summary>
       // /// Kullanıcı Id ye göre kullanıcı bilgisini getirir.
       // /// </summary>
       // /// <param name="Id"></param>
       // /// <returns></returns>
       // Users GetUser(int? Id);

       // /// <summary>
       // /// Id'e göre kullanıcı bilgisini siler. ( Status =-1)
       // /// </summary>
       // /// <param name="Id"></param>
       // /// <returns></returns>
       // bool DeleteUser(int? Id);

       // /// <summary>
       // /// Asenkron olarak Tüm Kullanıcıları getirir.
       // /// </summary>
       // /// <returns></returns>
       // List<Users> GetAllUsersAsync();
    }
}