﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using MegProject.Web.Auth;
using MegProject.Business.Manager;
using System.Text;
using AutoMapper;
using MegProject.Business.Core;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Web.Base;

namespace MegProject.Web.Base
{
    public class BaseController : Controller
    {
        protected new virtual IMapper Mapper
        {
            get
            {
              ApplicationCore core = new ControllerActionApp();
                return core.Mapper;
            }
        } 
        protected new virtual CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
        
        protected override JsonResult Json(object data, string contentType,
       Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}