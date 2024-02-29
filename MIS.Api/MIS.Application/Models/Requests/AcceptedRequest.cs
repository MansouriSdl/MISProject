using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class AcceptedRequest
    {
        public PostOrderRequest PostOrderRequest { get; set; }
        public Guid RequestId { get; set; }
    }
}
