//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MegProject.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleAction
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ControllerId { get; set; }
        public int ActionId { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreatorId { get; set; }
    
        public virtual Roles Roles { get; set; }
        public virtual SystemActions SystemActions { get; set; }
        public virtual SystemControllers SystemControllers { get; set; }
    }
}