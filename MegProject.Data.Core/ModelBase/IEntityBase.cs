using System;
using System.ComponentModel.DataAnnotations;

namespace MegProject.Data.Core.ModelBase
{
    public interface IEntityBase
    {
         [Required]
         int Status { get; set; }
         Nullable<DateTime> CreateDate { get; set; }
         Nullable<DateTime> ModifyDate { get; set; }
         int? CreatorId { get; set; }
    }
}