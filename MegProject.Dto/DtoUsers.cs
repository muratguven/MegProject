//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MegProject.Dto
{
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    [DataContract]
    [Serializable]
    public class DtoUsers
    {
    
    	[DataMember]
        public int Id { get; set; }
    	[DataMember]
        public Nullable<int> UserGroupId { get; set; }
    	[DataMember]
        public string UserName { get; set; }
    	[DataMember]
        public string Email { get; set; }
    	[DataMember]
        public string Password { get; set; }
    	[DataMember]
        public int Status { get; set; }
    	[DataMember]
        public Nullable<System.DateTime> CreateDate { get; set; }
    	[DataMember]
        public Nullable<System.DateTime> ModifyDate { get; set; }
    	[DataMember]
        public Nullable<bool> EmailConfirmed { get; set; }
    	[DataMember]
        public int AccessFailedCount { get; set; }
    	[DataMember]
        public bool LockoutEnabled { get; set; }
    	[DataMember]
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
    	[DataMember]
        public string SecurityStamp { get; set; }
    
    	[DataMember]
        public  DtoUserGroups UserGroups { get; set; }
    	[DataMember]
        public  ICollection<DtoUserProfile> UserProfile { get; set; }
    	[DataMember]
        public  ICollection<DtoUserRoles> UserRoles { get; set; }
    }
}
