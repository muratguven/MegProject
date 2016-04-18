using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Manager.RoleAppService;
using MegProject.Business.Manager.UserAppService;
using MegProject.Business.Manager.UserGroupAppService;
using MegProject.Dto;
using MegProject.Dto.CustomDto.ComponentModels;
using MegProject.Dto.CustomDto.ViewModels;
using MegProject.Web.Auth;
using MegProject.Web.Base;

namespace MegProject.Web.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserGroupApp _userGroupApp;
        private readonly IUserApp _userApp;
        private readonly IRoleApp _roleApp;

        public UserController(IUserGroupApp userGroupApp, IUserApp userApp, IRoleApp roleApp)
        {
            _userGroupApp = userGroupApp;
            _userApp = userApp;
            _roleApp = roleApp;
        }

        #region ActionResult Methods

        #region User
        // GET: User
        public ActionResult Index()
        {
            List<DtoUsers> model = new List<DtoUsers>();
            model = _userApp.GetAllUsersAsync();
            return View(model);
        }


        public ActionResult Register(int? userId = 0)
        {
            ViewBag.UserGroups = _userGroupApp.GetAllUserGroups();
            RegisterViewModel model = new RegisterViewModel();
            List<DtoRoles> roles = new List<DtoRoles>();
            roles = _roleApp.GetAllRoles();
            #region Mapping ChechkBox Model 

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    CheckBoxModel checkBox = new CheckBoxModel()
                    {
                        Id = role.Id,
                        Name = role.RoleName,
                        IsSelected = false
                    };
                    model.Roles.Add(checkBox);
                }
            }
            #endregion
            if (userId > 0)
            {
                var user = _userApp.GetUser(userId);
                if (user != null)
                {
                    model = Mapper.Map<RegisterViewModel>(user);
                }
            }

            return View(model);
        }

        #endregion

        #region UserGroups
        public ActionResult UserGroups()
        {
            List<DtoUserGroups> model = new List<DtoUserGroups>();
            model = _userGroupApp.GetAllUserGroups();
            return View(model);
        }

        public PartialViewResult RegisterUserGroup(int userGroupId = 0)
        {
            UserGroupViewModel model = new UserGroupViewModel();
            if (userGroupId > 0)
            {
                var dtoUserGroup = _userGroupApp.GetUserGroup(userGroupId);
                if (dtoUserGroup != null)
                {
                    model.UserGroupName = dtoUserGroup.UserGroupName;
                    model.Id = dtoUserGroup.Id;
                }


            }
            return PartialView("RegisterUserGroup", model);
        }

        #endregion

        #endregion

        #region JsonResult Methods

        #region UserGroup Json Methods
        [HttpPost]
        [ValidateForgeryTokenCore]
        public JsonResult CreateOrUpdateUserGroup(UserGroupViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userGroup = Mapper.Map<DtoUserGroups>(model);
                userGroup.Status = 0;
                if (_userGroupApp.CreateOrUpdateUserGroup(userGroup))
                {
                    return Json(new { result = "success", header = "Başarılı", message = "Kullanıcı grubu eklendi." });
                }
                else
                {
                    return Json(new { result = "error", header = "Hata", message = "Kullanıcı grubu eklenme sırasında bir hata oluştu!" });
                }

            }
            else
            {
                return Json(new { result = "warning", header = "Uyarı", message = "Kullanıcı grubu parametreleri boş olamaz!" });
            }


        }

        [HttpPost]
        [ValidateForgeryTokenCore]
        public JsonResult DeleteUserGroup(int id)
        {
            if (id != default(int))
            {
                if (_userGroupApp.DeleteUserGroup(id))
                {
                    return Json(new { result = "success", header = "Başarılı", message = "Kullanıcı grubu silindi." });
                }
                else
                {
                    return Json(new { result = "error", header = "Hata", message = "Kullanıcı grubu silme sırasında sırasında bir hata oluştu!" });
                }

            }
            else
            {
                return Json(new { result = "warning", header = "Hata", message = "Kullanıcı grubu parametresi boş gönderilemez!" });
            }

        }
        #endregion

        #region User Json Methods

        public JsonResult CreateOrUpdateUser(RegisterViewModel user)
        {
            if (user != null)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    DtoUsers dtoUser = new DtoUsers()
                    {
                        Email = user.Email,
                        UserGroupId = user.UserGroupId,
                        Password = MegProject.Common.Cryptor.EncryptString(user.Password),
                        UserName =user.UserName,
                        EmailConfirmed = false,
                        CreateDate = DateTime.Now,
                        Status = (int)Common.Constants.Status.Active
                       
                    };

                    
                    
                    
                }
                
            }
            return null;
        }

        #endregion

        #endregion

    }
}