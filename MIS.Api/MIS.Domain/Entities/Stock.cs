using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Stock : AuditEntity
    {
        public string Designation { get; set; }
        public int Quantity { get; set; }
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public Guid UserId { get; set; }
        public Employee User { get; set; }
        public List<BureauStockInventory> BureauStockInventories { get; set; }
        public List<BureauStockAssignment> BureauStockAssignments { get; set; }
        public bool IsMultiple { get; set; }
        public DateTime? ExpirationDate { get; set; }


    }
}
