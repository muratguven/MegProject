using System;
using System.Collections.Generic;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class Roles:EntityBase
    {
        public int Id { get; set; }
        public string RoleName { get; set; }           
        public virtual  ICollection<RolePermissions> RolePermissions { get; set; }
    }
}