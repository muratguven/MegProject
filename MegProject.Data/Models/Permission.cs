using System;
using System.Collections.Generic;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class Permission:EntityBase
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }       
        public virtual ICollection<PermissionDetails> PermissionDetails { get; set; }
    }
}