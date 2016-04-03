using System.Security.Principal;

namespace MegProject.Web.Auth
{
    public class MegIdentity:IIdentity
    {
        private IIdentity megIdentity { get; set; }
        
        public SerializeLoginModel userModel { get; set; }

        public MegIdentity(SerializeLoginModel user)
        {
            megIdentity = new GenericIdentity(user.UserName);
            userModel = user;
        }

        public string AuthenticationType
        {
            get { return megIdentity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return megIdentity.IsAuthenticated; }
        }

        public string Name
        {
            get { return megIdentity.Name; }
        }
    }
}