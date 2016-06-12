using System;
using System.ComponentModel.DataAnnotations;

namespace MegProject.Data.Models
{
    public class SystemControllers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}