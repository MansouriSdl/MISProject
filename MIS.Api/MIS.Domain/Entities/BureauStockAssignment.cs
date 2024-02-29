using MIS.Domain.Commons;
using MIS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class BureauStockAssignment : AuditEntity
    {

        public Bureau Bureau { get; set; }
        public Guid BureauId { get; set; }
        public Employee User { get; set; }
        public Guid UserId { get; set; }
        public Stock Stock { get; set; }
        public Guid StockId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public bool IsAvailable { get; set; }
        public int? Quantity { get; set; }

    }
}
