using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierTypeFeatures.Commands
{
    public class UpdateSupplierTypeRequest : IRequest<MessageResponse>
    {
        public UpdateSupplierTypeRequest(Guid id, SupplierTypeRequest supplierTypeRequest)
        {
            Id = id;
            SupplierTypeRequest = supplierTypeRequest;
        }

        public Guid Id { get; set; }
        public SupplierTypeRequest SupplierTypeRequest { get; set; }
    }
}
