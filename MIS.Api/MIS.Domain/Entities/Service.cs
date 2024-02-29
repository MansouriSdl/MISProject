using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Service : AuditEntity
    {
        public string Name { get; set; }
        public Division Division { get; set; }
        public Guid DivisionId { get; set; }
        public List<Bureau> Bureaus { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ServiceEquipmentRequest> ServiceEquipmentRequests { get; set; }

    }
}
