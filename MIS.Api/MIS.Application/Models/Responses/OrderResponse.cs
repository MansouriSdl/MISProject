using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class OrderResponse
    {
        public string Equipment { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public string User { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
    }
}
