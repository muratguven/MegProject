using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MegProject.Data.Core.ModelBase;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MegProject.Data.Models
{
    public class UserRoles:IdentityUserRole
    {
        //[Key]
        //public int Id { get; set; }
        //[ForeignKey("Users")]
        //public  string UserId { get; set; }
        //[ForeignKey("Roles")]
        //public  string RoleId { get; set; }

        [ForeignKey("Permission")]
        [Required]
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        
        //public virtual Users Users { get; set; }
        
        //public  virtual Roles Roles { get; set; }
    
    }
}