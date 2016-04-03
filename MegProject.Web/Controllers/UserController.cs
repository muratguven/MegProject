using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Manager.UserGroupAppService;
using MegProject.Dto;
using MegProject.Dto.CustomDto.ViewModels;
using MegProject.Web.Auth;
using MegProject.Web.Base;

namespace MegProject.Web.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserGroupApp _userGroupApp;

        public UserController(IUserGroupApp userGroupApp)
        {
            _userGroupApp = userGroupApp;
        }

        #region ActionResult Methods

        #region User
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            ViewBag.UserGroups = _userGroupApp.GetAllUserGroups();
            RegisterViewModel model = new RegisterViewModel();
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

        public PartialViewResult RegisterUserGroup(int userGroupId=0)
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
            return PartialView("RegisterUserGroup",model);
        }

        #endregion 

        #endregion

        #region JsonResult Methods

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
                    return Json(new {result ="success",header="Başarılı",message="Kullanıcı grubu eklendi."});
                }
                else
                {
                    return Json(new {result = "error",header="Hata", message = "Kullanıcı grubu eklenme sırasında bir hata oluştu!"});
                }

            }
            else
            {
                return Json(new {result="warning",header="Uyarı", message="Kullanıcı grubu parametreleri boş olamaz!"});
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

    }
}