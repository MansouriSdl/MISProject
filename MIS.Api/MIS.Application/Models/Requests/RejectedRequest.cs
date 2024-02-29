using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class RejectedRequest
    {
        public Guid RequestId { get; set; }
        public string Observation { get; set; }
    }
}
