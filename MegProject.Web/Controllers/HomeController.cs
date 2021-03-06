﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Manager.TestCacheApp;
using MegProject.Data.Models;
using MegProject.Web.Auth;
using MegProject.Web.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;

namespace MegProject.Web.Controllers
{
    [MegAuthorization]
    public class HomeController : BaseController
    {

        [Inject]
        public ITestCache _testCache { private get; set; }

        public ActionResult Index()
        {
            var user = _testCache.GetAllUser();
            var userEmail = _testCache.GetUserByEmail("muratguven_ktu@hotmail.com");
            var users = _testCache.GetAll<IdentityUser>();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}