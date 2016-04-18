using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Business.Manager.RoleAppService;
using MegProject.Dto;
using MegProject.Dto.CustomDto.ViewModels;
using MegProject.Web.Base;
using Ninject;

namespace MegProject.Web.Controllers
{
    public class RoleController : BaseController
    {
        [Inject]
        public IRoleApp _roleApp { private get; set; }

        [Inject]
        public IControllerActionApp _controllerActionApp { private get; set; }
        
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterRole(int roleId=0)
        {
            RoleRegisterViewModel model = new RoleRegisterViewModel();
            model.Controllers = new List<DtoSystemControllers>();
            var controllers = _controllerActionApp.GetAllControllers();
            if (controllers != null)
            {
                model.Controllers = controllers;
               
            }


            return View(model);
        }
        
    }
}