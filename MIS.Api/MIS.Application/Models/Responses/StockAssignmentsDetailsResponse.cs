using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class StockAssignmentsDetailsResponse
    {
        public NameResponse Equipment { get; set; }
        public List<DetailResponse> CardsResponses { get; set; }
        public List<DetailResponse> ListResponses { get; set; }
    }
}
