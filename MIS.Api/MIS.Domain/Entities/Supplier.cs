using MIS.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class Supplier : AuditEntity
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string CommanCompanyIndentifier { get; set; }
        public Guid SupplierTypeId { get; set; }
        public SupplierType SupplierType { get; set; }
        public List<Order> Orders { get; set; }
        public List<BureauStockInventory> BureauStockInventories { get; set; }
    }
}
