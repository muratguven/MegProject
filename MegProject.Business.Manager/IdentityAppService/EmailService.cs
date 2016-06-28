using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MegProject.Business.Manager.IdentityAppService
{
    public class EmailService: IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}