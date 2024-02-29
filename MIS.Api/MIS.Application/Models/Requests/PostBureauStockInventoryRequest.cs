using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class PostBureauStockInventoryRequest
    {
        public string QrCode { get; set; }
        public Guid UserId { get; set; }
        public Guid StockId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public int State { get; set; }
        public bool IsCanceled { get; set; }
    }
}
