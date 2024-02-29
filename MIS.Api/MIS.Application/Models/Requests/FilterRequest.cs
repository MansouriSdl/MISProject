using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class FilterRequest
    {
        public Guid? DivisionId { get; set; }
        public Guid? EquipmentId { get; set; }
        public Guid? SupplierId { get; set; }
        public DateTime? Date { get; set; }
        public Guid? BureauId { get; set; }
        public Guid? StockId { get; set; }
        public Guid? ServiceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
