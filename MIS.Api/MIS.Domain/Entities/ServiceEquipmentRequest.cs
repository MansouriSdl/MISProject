using MIS.Domain.Commons;
using MIS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class ServiceEquipmentRequest : AuditEntity
    {
        public Guid UserId { get; set; }
        public Employee User { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public DateTime RequestDate { get; set; }
        public RequestStatus Status { get; set; }
        public int Quantity { get; set; }
        public int? ReturnedQuantity { get; set; }
        public Guid? OrderId { get; set; }
        public Order Order { get; set; }
        public string Observation { get; set; }
    }
}
