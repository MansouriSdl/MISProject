using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class PostOrdersListRequest
    {
        public List<AcceptedRequest> AcceptedRequests { get; set; }
        public List<InStockRequest> InStockRequests { get; set; }
        public List<RejectedRequest> RejectedRequests { get; set; }
    }
}
