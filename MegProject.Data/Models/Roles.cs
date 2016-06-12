using System;
using System.Collections.Generic;

namespace MegProject.Data.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }        
        public virtual  ICollection<RolePermissions> RolePermissions { get; set; }
    }
}