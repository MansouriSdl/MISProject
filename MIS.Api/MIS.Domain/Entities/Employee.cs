using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Employee : AuditEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get; set; }
        public string WorkId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Guid EmployeeTypeId { get; set; }
        public List<BureauStockAssignment> BureauStockAssignments { get; set; }
        public List<BureauStockInventory> BureauStockInventories { get; set; }
        public List<ServiceEquipmentRequest> ServiceEquipmentRequests { get; set; }
        public List<Order> Orders { get; set; }
        public List<Stock> Stocks { get; set; }
        public Guid? ServiceId { get; set; }
        public Service Service { get; set; }





    }
}
