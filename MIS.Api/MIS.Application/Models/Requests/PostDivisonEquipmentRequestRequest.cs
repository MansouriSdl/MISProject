using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class PostDivisonEquipmentRequestRequest
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public Guid EquipmentId { get; set; }
        public int Quantity { get; set; }
        public int? ReturnedQuantity { get; set; }
    }
}
