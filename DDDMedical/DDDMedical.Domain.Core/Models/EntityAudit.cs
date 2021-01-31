using System;

namespace DDDMedical.Domain.Core.Models
{
    public abstract class EntityAudit : Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}