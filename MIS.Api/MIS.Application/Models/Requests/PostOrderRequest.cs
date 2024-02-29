using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class PostOrderRequest
    {
        public Guid SupplierId { get; set; }
        public Guid? EquipmentId { get; set; }
        public Guid UserId { get; set; }
        public byte[] File { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? Quantity { get; set; }
    }
}
