using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class SystemActions:EntityBase
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SystemControllers")]
        public Nullable<int> ControllerId { get; set; }
        public string Name { get; set; }
        public virtual SystemControllers SystemControllers { get; set; }
    }
}