using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class AlertResponse
    {
        public List<NameValueResponse> QuantityLimitAlerts { get; set; }
        public List<NameValueResponse> ExpirationDateLimitAlerts { get; set; }
    }
}
