using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class StockResponse
    {
        public Guid StockId { get; set; }
        public Guid EquipmentId { get; set; }
        public string Designation { get; set; }
        public int Quantity { get; set; }
        public int NextDeliveryQuantity { get; set; }
        public string Detail { get; set; }
    }
}
