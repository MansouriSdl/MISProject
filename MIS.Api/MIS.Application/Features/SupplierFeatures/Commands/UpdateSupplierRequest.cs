using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Commands
{
    public class UpdateSupplierRequest : IRequest<MessageResponse>
    {
        public UpdateSupplierRequest(Guid id, SupplierRequest supplierRequest)
        {
            Id = id;
            SupplierRequest = supplierRequest;
        }

        public Guid Id { get; set; }
        public SupplierRequest SupplierRequest { get; set; }
    }
}
