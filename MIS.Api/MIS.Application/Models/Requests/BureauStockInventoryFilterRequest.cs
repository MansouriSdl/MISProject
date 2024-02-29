using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Requests
{
    public class BureauStockInventoryFilterRequest
    {
        public Guid? QrCodeId { get; set; }
        public Guid? DivisionId { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? BureauId { get; set; }
        public Guid? StockId { get; set; }
        public Guid? EquipmentId { get; set; }
        public Guid? SupplierTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}
