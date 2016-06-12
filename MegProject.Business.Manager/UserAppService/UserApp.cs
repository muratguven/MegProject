using System.Security.Policy;
using AutoMapper;
using MegProject.Business.Core;
using MegProject.Data.Repositories.Users;
using MegProject.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MegProject.Common;
using MegProject.Data;
using MegProject.Data.Repositories.UserRoles;
using log4net;
using System;
using System.Threading.Tasks;
using MegProject.Data.Models;

namespace MegProject.Business.Manager.UserAppService
{
    public class UserApp:ApplicationCore,IUserApp
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        
        public UserApp(IUserRepository userRepository, IUserRolesRepository userRolesRepository)
        {
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
            
        }

        /// <summary>
        /// Tüm kullanıcıları getirir.
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.List<Data.Models.Users> GetAllUsers()
        {
           
            var result = _userRepository.GetAll();            
            return result.ToList();
            
        }

        /// <summary>
        /// Email ve pass e göre kullanıcı bilgisini getirir.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Users GetUser(string email, string password)
        {
            var result = _userRepository.Find(x => x.Email.Contains(email) && x.Password.Contains(password));
            return result;
        }



        /// <summary>
        /// Kullanıcı ve rolleri ile yeni kullanıcı ve rol atama işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public bool CreateOrUpdateUser(Users user, List<Roles> userRoles)
        {
            if (user.Id > 0)
            {
                
                //UPDATE USER
                var dbUser = _userRepository.Find(x => x.Id == user.Id);
                if (dbUser != null)
                {
                    try
                    {
                        //Update Users data
                        dbUser.UserName = user.UserName;
                        dbUser.Email = user.Email;
                        //dbUser.UserGroupId = user.UserGroupId;
                        dbUser.Password = user.Password;

                        _userRepository.Update(dbUser);
                        _userRepository.Save();

                        //Update( Add or Remove) User Roles
                        var dbUserRoles = _userRolesRepository.FindList(x => x.UserId == user.Id);
                        var addUserRoles =
                            userRoles.Select(n => n.Id).Except(dbUserRoles.Select(n => n.RoleId)).ToList();

                        // Add New Roles for User
                        foreach (var addItem in addUserRoles)
                        {
                            Data.Models.UserRoles temp = new UserRoles()
                            {
                                RoleId = addItem,
                                UserId = user.Id

                            };
                            _userRolesRepository.Add(temp);
                            _userRolesRepository.Save();
                        }

                        var deleteUserRoles =
                            dbUserRoles.Select(n => n.RoleId).Except(userRoles.Select(n => n.Id)).ToList();
                        foreach (var deleteItem in deleteUserRoles)
                        {
                            int delId = deleteItem.CInt32();
                            var delUserRole = _userRolesRepository.Find(x => x.RoleId == delId && x.UserId == user.Id);
                            _userRolesRepository.Delete(delUserRole.Id);
                            _userRolesRepository.Save();
                        }
                        log.Info(user.Id.CString() + " ID'li kullanıcı güncellendi.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        log.Fatal(user.Id.CString()+" ID'li kullanıcı güncellemede hata oluştu",ex);
                        return false;
                    }

                }
                else
                {
                    log.Error(user.Id.CString()+" 'ID'li kullanıcı güncelleme işlemi için veri tabanında bulunamadı!");
                    return false;
                }

            }
            else
            {
                //Create
                try
                {
                    //User Create
                   
                    _userRepository.Add(user);
                    _userRepository.Save();

                    //UserRoleCreate
                    List<UserRoles> userRolesEntity = new List<UserRoles>();
                    foreach (var item in userRoles)
                    {
                        UserRoles temp = new UserRoles()
                        {
                            UserId = user.Id,
                            RoleId = item.Id
                        };
                        userRolesEntity.Add(temp);
                    }

                    _userRolesRepository.SaveAll(userRolesEntity);
                    log.Info("Yeni kullanıcı oluşturuldu.");
                    return true;
                }
                catch (Exception ex)
                {
                    log.Fatal("Kullanıcı oluşturmada hata oluştu!",ex);
                    return false;
                }

            }



        }

        #region UserRoles
        public List<UserRoles> GetUserRole(int userId)
        {
            var result = _userRolesRepository.FindList(x => x.UserId == userId&&x.Roles.Status!=-1);

            return result.ToList();

        }

        #endregion


        public Users GetUser(int? Id)
        {
           if(Id!=default(int))
           {

               var result = _userRepository.Find(x => x.Id == Id);
               return result;

           }
           else
           {
               return null;
           }
        }


        public bool DeleteUser(int? Id)
        {
            var deleteUser = _userRepository.Find(x => x.Id == Id);
            deleteUser.Status = -1;            
            _userRepository.Update(deleteUser);
            _userRepository.Save();
            return true;
        }


        public List<Users> GetAllUsersAsync()
        {
            var usersAsync= _userRepository.GetAllAsync();
            var users = usersAsync.Result;
            return users.ToList();
        }
    }
}