using System;
using System.Collections.Generic;

namespace MegProject.Data.Models
{
    public class Users
    {
        public int Id { get; set; }        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> EmailConfirmed { get; set; }
        public int? AccessFailedCount { get; set; }
        public bool? LockoutEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public string SecurityStamp { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; } 
    }
}