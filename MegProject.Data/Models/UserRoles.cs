using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class UserRoles:EntityBase
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual Users Users { get; set; }
        public virtual Permission Permission { get; set; }
    }
}