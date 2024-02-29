using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class InventoryHistoryResponse
    {
        public int Count { get; set; }
        public List<BureauStockInventoryHistoryResponse> BureauStockInventoryHistoryResponses { get; set; }
    }
}
