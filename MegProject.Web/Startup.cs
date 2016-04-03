using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MegProject.Web.Startup))]
namespace MegProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
