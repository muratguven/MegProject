using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MegProject.Data.Models
{
    public class UserLogin:IdentityUserLogin
    {
        //[Key]
        //public string UserId { get; set; }
        //[Key]
        //public string ProviderKey { get; set; }
    }
}