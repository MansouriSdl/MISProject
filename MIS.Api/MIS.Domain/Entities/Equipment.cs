using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Equipment : AuditEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public Guid ConsumptionTypeId { get; set; }
        public ConsumptionType ConsumptionType { get; set; }
        public Guid EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<ServiceEquipmentRequest> ServiceEquipmentRequests { get; set; }
        public List<Order> Orders { get; set; }
        public int StockLimit { get; set; }
        public int? ExpirationDateLimit { get; set; }

    }
}
