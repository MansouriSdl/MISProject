using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Commands
{
    public class DeleteSupplierRequestHandler : IRequestHandler<DeleteSupplierRequest, MessageResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierRequestHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Task<MessageResponse> Handle(DeleteSupplierRequest request, CancellationToken cancellationToken)
        {
            return _supplierRepository.DeleteSupplier(request.Id);
        }
    }
}
