using MIS.Domain.Commons;
using MIS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class BureauStockInventory : AuditEntity
    {
        public QrCode QrCode { get; set; }
        public Guid QrCodeId { get; set; }

        public Bureau Bureau { get; set; }
        public Guid BureauId { get; set; }
        public  Employee User { get; set; }
        public Guid UserId { get; set; }
        public Stock Stock { get; set; }
        public Guid StockId { get; set; }
        public DateTime InventoryDate { get; set; }
        public bool IsAvailable { get; set; }
        public EquipmentState EquipmentState { get; set; }

        public string Designation { get; set; }
        public Guid? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public string MarketReference { get; set; }
        public string MarketObject { get; set; }

    }
}
