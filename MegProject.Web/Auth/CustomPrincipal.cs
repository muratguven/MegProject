using System;
using System.Linq;
using System.Security.Principal;
using MegProject.Business.Manager.RoleAppService;


namespace MegProject.Web.Auth
{
    public  class CustomPrincipal:ICustomPrincipal
    {

        private readonly IRoleApp _roleApp;
        private readonly MegIdentity _megIdentity;

        public CustomPrincipal(MegIdentity megIdentity)
        {
           _roleApp= new RoleApp();
            _megIdentity = megIdentity;
        }

        
        //public int Id { get; set; }

        //public string Email { get; set; }
     

        //public string UserName { get; set; }


        public IIdentity Identity
        {
            get { return _megIdentity; }
        }

        public bool IsInRole(string role)
        {
          return  _roleApp.IsInRole(role, _megIdentity.userModel.Id);
        }
    }
}