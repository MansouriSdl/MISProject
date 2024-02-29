using MIS.Domain.Commons;
using MIS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Order : AuditEntity
    {
        public ServiceEquipmentRequest ServiceEquipmentRequest { get; set; }
      
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        public Employee User { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public Guid? EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public Guid? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public int? Quantity { get; set; }

    }
}
