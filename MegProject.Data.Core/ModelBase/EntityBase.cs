using System;

namespace MegProject.Data.Core.ModelBase
{
    public abstract class EntityBase:IEntityBase
    {
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? CreatorId { get; set; }
    }
}