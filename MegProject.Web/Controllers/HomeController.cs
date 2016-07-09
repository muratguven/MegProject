using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MegProject.Business.Manager.TestCacheApp;
using MegProject.Business.Manager.UserAppService;
using MegProject.Web.Base;
using Ninject;

namespace MegProject.Web.Controllers
{
    public class HomeController : BaseController
    {

        [Inject]
        public ITestCache _testCache { private get; set; }

        public ActionResult Index()
        {
           var user=  _testCache.GetAllUser();
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