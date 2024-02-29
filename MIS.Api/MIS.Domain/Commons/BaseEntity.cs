using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Commons
{
    public abstract class BaseEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual bool IsNew()
        {
            return Guid.Empty.Equals(Id);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity)) return false;
            var entity = (BaseEntity)obj;
            return Id.Equals(entity.Id);
        }
        protected bool Equals(BaseEntity other)
        {
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
