using System;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class RolePermissions:EntityBase
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }       
        public virtual Permission Permission { get; set; }
        public virtual Roles Roles { get; set; }
    }
}