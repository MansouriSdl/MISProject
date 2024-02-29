using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class BureauStockInventoryHistoryResponse
    {
        public Guid Id { get; set; }
        public string QrCode { get; set; }
        public string DivisionName { get; set; }
        public string ServiceName { get; set; }
        public string BureauName { get; set; }
        public string EquipmentName { get; set; }
        public string StockDesignation { get; set; }
        public string SupplierType { get; set; }
        public string Supplier { get; set; }
        public string Designation { get; set; }
        public string MarketReference { get; set; }
        public string MarketObject { get; set; }
        public string State { get; set; }
        public bool IsAvailable { get; set; }
        
    }
}
