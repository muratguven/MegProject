using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MegProject.Data.Core.ModelBase;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MegProject.Data.Models
{
    public class Roles:IdentityRole
    {

      //  public virtual ICollection<UserRoles> UserRoles { get; set; } 
       
        public virtual  ICollection<RolePermissions> RolePermissions { get; set; }
    }
}