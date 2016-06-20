using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using MegProject.Business.Core.ControllerActionAppService;
using MegProject.Data.Models;
using MegProject.Dto;
using MegProject.Web.Auth;
using Ninject.Web.Mvc;

namespace MegProject.Web
{
    public class MvcApplication :System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GetAllControllers();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (!String.IsNullOrEmpty(authTicket.UserData))
                {
                    var serializer = new JavaScriptSerializer();
                    var serializeModel = serializer.Deserialize<SerializeLoginModel>(authTicket.UserData);
                    MegIdentity identity = new MegIdentity(serializeModel);
                    var newUser = new CustomPrincipal(identity);
                 

                    HttpContext.Current.User = newUser;

                }

            }
        }

        protected void GetAllControllers()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
            .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
            .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            MegProject.Business.Core.ControllerActionAppService.IControllerActionApp _controllerApp = new ControllerActionApp();




            foreach (var item in controlleractionlist.Select(x => x.Controller).Distinct())
            {
                Data.Models.SystemControllers controllers = new SystemControllers();
                controllers.Name = item;
                List<SystemActions> actionlist = new List<SystemActions>();
                foreach (var actions in controlleractionlist.Where(x => x.Controller == item).ToList())
                {
                    SystemActions temp = new SystemActions()
                    {
                        Name = actions.Action
                    };

                    actionlist.Add(temp);

                }

                //Clear
                _controllerApp.ClearControllerActions(controllers, actionlist);

                //Create
                _controllerApp.CreateControllerAction(controllers, actionlist);

            }

            //_controllerApp.ClearActions(actionlist);
        }

    }
}
