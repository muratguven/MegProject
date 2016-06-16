using System;
using System.ComponentModel.DataAnnotations.Schema;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class PermissionDetails:EntityBase
    {
        public int Id { get; set; }
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }
        [ForeignKey("SystemControllers")]
        public int ControllerId { get; set; }
        [ForeignKey("SystemActions")]
        public int ActionId { get; set; }        
        public virtual Permission Permission { get; set; }
        public virtual SystemActions SystemActions { get; set; }
        public virtual SystemControllers SystemControllers { get; set; }
    }
}