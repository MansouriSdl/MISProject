using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Commons
{
    public abstract class AuditEntity : BaseEntity
    {

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AuditEntity entity && base.Equals(obj) && Equals(entity);
        }
        protected bool Equals(AuditEntity other)
        {
            return base.Equals(other) && CreatedAt.Equals(other.CreatedAt) && Equals(CreatedBy, other.CreatedBy) && Nullable.Equals(LastUpdatedAt, other.LastUpdatedAt) && Equals(LastUpdatedBy, other.LastUpdatedBy) && Nullable.Equals(DeletedAt, other.DeletedAt) && Equals(DeletedBy, other.DeletedBy);
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
