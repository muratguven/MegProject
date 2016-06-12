using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegProject.Data.Models
{
    public class UserRoles
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