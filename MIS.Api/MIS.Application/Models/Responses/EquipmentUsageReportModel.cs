using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class EquipmentUsageReportModel
    {
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; } // Assuming you have a name or similar attribute
        public int TotalQuantity { get; set; }

    }
}
