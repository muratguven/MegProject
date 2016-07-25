using System;
using System.ComponentModel.DataAnnotations;
using MegProject.Data.Core.ModelBase;

namespace MegProject.Data.Models
{
    public class SystemControllers:EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Channel { get; set; }

    }
}