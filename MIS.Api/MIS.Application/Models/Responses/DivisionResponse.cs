using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class DivisionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
}
