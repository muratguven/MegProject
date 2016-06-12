using System;
using System.Collections.Generic;

namespace MegProject.Data.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual ICollection<PermissionDetails> PermissionDetails { get; set; }
    }
}