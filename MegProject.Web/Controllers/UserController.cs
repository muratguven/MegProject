using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Manager.RoleAppService;
using MegProject.Business.Manager.UserAppService;
using MegProject.Dto;
using MegProject.Dto.CustomDto.ComponentModels;
using MegProject.Dto.CustomDto.ViewModels;
using MegProject.Web.Auth;
using MegProject.Web.Base;

namespace MegProject.Web.Controllers
{
    public class UserController : BaseController
    {

        
        private readonly IUserApp _userApp;
        private readonly IRoleApp _roleApp;

        public UserController(IUserApp userApp, IRoleApp roleApp)
        {
            
            _userApp = userApp;
            _roleApp = roleApp;
        }

        //#region ActionResult Methods

        //#region User
        //// GET: User
        //public ActionResult Index()
        //{
        //    List<DtoUsers> model = new List<DtoUsers>();
        //    model = _userApp.GetAllUsersAsync();
        //    return View(model);
        //}


        //public ActionResult Register(int? userId = 0)
        //{
        //    ViewBag.UserGroups = _userGroupApp.GetAllUserGroups();
        //    RegisterViewModel model = new RegisterViewModel();
        //    List<DtoRoles> roles = new List<DtoRoles>();
        //    roles = _roleApp.GetAllRoles();
        //    #region Mapping ChechkBox Model 

        //    if (roles != null)
        //    {
        //        foreach (var role in roles)
        //        {
        //            CheckBoxModel checkBox = new CheckBoxModel()
        //            {
        //                Id = role.Id,
        //                Name = role.RoleName,
        //                IsSelected = false
        //            };
        //            model.Roles.Add(checkBox);
        //        }
        //    }
        //    #endregion
        //    if (userId > 0)
        //    {
        //        var user = _userApp.GetUser(userId);
        //        if (user != null)
        //        {
        //            model = Mapper.Map<RegisterViewModel>(user);
        //        }
        //    }

        //    return View(model);
        //}

        //#endregion

        

        //#endregion

        //#region JsonResult Methods

        

        //#region User Json Methods

        //public JsonResult CreateOrUpdateUser(RegisterViewModel user)
        //{
        //    if (user != null)
        //    {
        //        if (user.Password == user.ConfirmPassword)
        //        {
        //            DtoUsers dtoUser = new DtoUsers()
        //            {
        //                Email = user.Email,
        //                UserGroupId = user.UserGroupId,
        //                Password = MegProject.Common.Cryptor.EncryptString(user.Password),
        //                UserName =user.UserName,
        //                EmailConfirmed = false,
        //                CreateDate = DateTime.Now,
        //                Status = (int)Common.Constants.Status.Active
                       
        //            };

                    
                    
                    
        //        }
                
        //    }
        //    return null;
        //}

        //#endregion

        //#endregion

    }
}