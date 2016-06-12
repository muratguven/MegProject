using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegProject.Data.Models
{
    public class SystemActions
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SystemControllers")]
        public Nullable<int> ControllerId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }

        public virtual SystemControllers SystemControllers { get; set; }
    }
}