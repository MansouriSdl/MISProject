using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class PostStocksListRequest
    {
        public string Designation { get; set; }
        public int Quantity { get; set; }
        public Guid EquipmentId { get; set; }
        public bool IsMultiple { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}
