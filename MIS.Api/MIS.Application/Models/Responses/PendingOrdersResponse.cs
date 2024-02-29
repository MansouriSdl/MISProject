using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class PendingOrdersResponse
    {
        public Guid OrderId { get; set; }
        public Guid EquipmentId { get; set; }
        public string Equipment { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public string User { get; set; }
        public string OrderDate { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public string ExpirationDate { get; set; }
        public string MultipleOrOneStock { get; set; }
        public string CheckBox { get; set; }

    }
}
