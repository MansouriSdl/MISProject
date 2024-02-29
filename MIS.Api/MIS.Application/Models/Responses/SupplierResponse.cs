using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class SupplierResponse
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string CommanCompanyIndentifier { get; set; }
        public Guid SupplierTypeId { get; set; }
        public string SupplierTypeName { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
}
