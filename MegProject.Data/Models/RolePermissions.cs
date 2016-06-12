using System;

namespace MegProject.Data.Models
{
    public class RolePermissions
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Roles Roles { get; set; }
    }
}