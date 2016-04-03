using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Manager.UserAppService;
using MegProject.Web.Base;
using Ninject;

namespace MegProject.Web.Controllers
{
    public class HomeController : BaseController
    {
     

        public ActionResult Index()
        {
            
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