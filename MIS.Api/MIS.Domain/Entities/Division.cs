using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Division : AuditEntity
    {
        public string Name { get; set; }
        public List<Service> Services { get; set; }
    }
}
