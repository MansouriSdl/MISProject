using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class ServiceEquipmentRequestResponse
    {
        public string EquipmentName { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public int? ReturnedQuantity { get; set; }
    }
}
