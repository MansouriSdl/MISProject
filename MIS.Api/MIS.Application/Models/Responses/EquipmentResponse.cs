using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class EquipmentResponse
    {
        public Guid Id { get; set; }
        public Guid EquipmentTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string EquipmentType { get; set; }
        public Guid ConsumptionTypeId { get; set; }
        public string ConsumptionType { get; set; }
        public int StockLimit { get; set; }
        public string ExpirationDateLimit { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
}
