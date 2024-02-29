using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierTypeFeatures.Commands
{
    public class DeleteSupplierTypeRequestHandler : IRequestHandler<DeleteSupplierTypeRequest, MessageResponse>
    {
        private readonly ISupplierTypeRepository _supplierTypeRepository;

        public DeleteSupplierTypeRequestHandler(ISupplierTypeRepository supplierTypeRepository)
        {
            _supplierTypeRepository = supplierTypeRepository;
        }

        public Task<MessageResponse> Handle(DeleteSupplierTypeRequest request, CancellationToken cancellationToken)
        {
            return _supplierTypeRepository.DeleteSupplierType(request.Id);
        }
    }
}
