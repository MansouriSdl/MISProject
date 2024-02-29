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
    public class SupplierRequestHandler : IRequestHandler<SupplierRequest, MessageResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierRequestHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Task<MessageResponse> Handle(SupplierRequest request, CancellationToken cancellationToken)
        {
            return _supplierRepository.AddSupplier(request);
        }
    }
}
