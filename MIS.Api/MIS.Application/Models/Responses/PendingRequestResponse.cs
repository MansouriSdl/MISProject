using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class PendingRequestResponse
    {
        public Guid RequestId { get; set; }
        public string EquipmentName { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public int? ReturnedQuantity { get; set; }
        public int ExistingQuantity { get; set; }
        public string UserName { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
        public string Observation { get; set; }
        public string SupplierType { get; set; }
        public string Supplier { get; set; }
        public string File { get; set; }

    }
}
