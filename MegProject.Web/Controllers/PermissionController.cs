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
    public class PermissionController : BaseController
    {

        //[Inject]
        //public IControllerActionApp _controllerActionApp { private get; set; }

        //[Inject]
        //public IRoleApp _roleApp { private get; set; }

        //// GET: Permission
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult CreatePermission(int? permissionId)
        //{
        //    PermissionRegisterViewModel model = new PermissionRegisterViewModel();
        //    model.ControllersActions = _controllerActionApp.GetAllControllers();
            
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateForgeryTokenCore]
        //public JsonResult CreateOrUpdatePermission(Data.Models.Permission permission,List<Data.Models.PermissionDetails> permissionDetails)
        //{
        //    if (permission != null && !String.IsNullOrEmpty(permission.PermissionName) && permissionDetails != null)
        //    {
        //        if (_roleApp.CreateOrUpdatePermission(permission, permissionDetails))
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
        //        return Json(new { result = "danger", message = "İzin bilgisi boş olamaz!" });
        //    }
            
        //}

    }
}