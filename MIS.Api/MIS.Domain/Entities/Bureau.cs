using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Bureau : AuditEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public Service Service { get; set; }
        public Guid ServiceId { get; set; }
        public List<BureauStockInventory> BureauStockInventories { get; set; }
        public List<BureauStockAssignment> BureauStockAssignments { get; set; }
    }
}
