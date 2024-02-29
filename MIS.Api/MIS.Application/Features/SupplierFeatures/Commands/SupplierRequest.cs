using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Commands
{
    public class SupplierRequest : IRequest<MessageResponse>
    {
        public SupplierRequest(string companyName, string phoneNumber, string bankAccountNumber, string commanCompanyIndentifier)
        {
            CompanyName = companyName;
            PhoneNumber = phoneNumber;
            BankAccountNumber = bankAccountNumber;
            CommanCompanyIndentifier = commanCompanyIndentifier;
        }

        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string CommanCompanyIndentifier { get; set; }
        public Guid SupplierTypeId { get; set; }
    }
}
