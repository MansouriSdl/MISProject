using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public  class InventoryLevelReportModel
    {
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int TotalQuantity { get; set; }
    }
}
