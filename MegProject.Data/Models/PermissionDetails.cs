using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegProject.Data.Models
{
    public class PermissionDetails
    {
        public int Id { get; set; }
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }
        [ForeignKey("SystemControllers")]
        public int ControllerId { get; set; }
        [ForeignKey("SystemActions")]
        public int ActionId { get; set; }
        public int Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreatorId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual SystemActions SystemActions { get; set; }
        public virtual SystemControllers SystemControllers { get; set; }
    }
}