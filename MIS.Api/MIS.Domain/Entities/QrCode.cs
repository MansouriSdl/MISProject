using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class QrCode : AuditEntity
    {
        public string Designation { get; set; }
        public List<BureauStockInventory> BureauStockInventories { get; set; }
    }
}
