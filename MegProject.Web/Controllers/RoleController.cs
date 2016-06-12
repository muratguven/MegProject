using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Business.Manager.RoleAppService;
using MegProject.Dto;
using MegProject.Dto.CustomDto.ViewModels;
using MegProject.Web.Auth;
using MegProject.Web.Base;
using Ninject;

namespace MegProject.Web.Controllers
{
    public class RoleController : BaseController
    {
        //[Inject]
        //public IRoleApp _roleApp { private get; set; }

        //[Inject]
        //public IControllerActionApp _controllerActionApp { private get; set; }

        //GET: Role
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult RegisterRole(int roleId = 0)
        //{
        //    RoleRegisterViewModel model = new RoleRegisterViewModel();
        //    model.Controllers = new List<DtoSystemControllers>();
        //    var controllers = _controllerActionApp.GetAllControllers();
        //    if (controllers != null)
        //    {
        //        model.Controllers = controllers;

        //    }

        //    return View(model);
        //}


        //#region JsonResult Methods

        //[HttpPost]
        //[ValidateForgeryTokenCore]
        //public JsonResult CreateOrUpdateRole(DtoRoles role, List<DtoRoleAction> actions)
        //{
        //    if (role != null && !String.IsNullOrEmpty(role.RoleName))
        //    {
        //        if (_roleApp.CreateOrUpdateRole(role, actions))
        //        {
        //            return Json(new { result = "success", message = "Kayıt oluşturuldu." });
        //        }
        //        else
        //        {
        //            return Json(new { result = "danger", message = "Kayıt işlemi sırasında bir hata oluştu!" });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { result = "danger", message = "Rol bilgisi boş olamaz!" });
        //    }

        //}


        //#endregion
        

    }
}