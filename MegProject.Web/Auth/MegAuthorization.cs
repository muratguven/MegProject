
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MegProject.Business.Manager.RoleAppService;
using Ninject;

namespace MegProject.Web.Auth
{
    public class MegAuthorization : AuthorizeAttribute
    {
       // Controll User Info
        [Inject]
        public IRoleApp _roleApp { set; private get; }

        protected virtual CustomPrincipal User
        {
            get { return HttpContext.Current.User as CustomPrincipal; } 
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (User != null)
            {
                // User role validate controllers and actions !!!!!Sayfa Kontrolleri bu kısmda yazılacak request 
                //***IMPORTANT ****  
                string controller = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RequestContext.RouteData.Values["action"].ToString();


                //bool result = _roleApp.IsInControlActions(User.Id, controller + "Controller", action);

                //if (!result)
                //{
                //    UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                //    filterContext.Result = new RedirectResult(urlHelper.Action("NotPermission", "Error"));
                //}



            }
            else
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("Index","Login"));
            }


        }
    }
}