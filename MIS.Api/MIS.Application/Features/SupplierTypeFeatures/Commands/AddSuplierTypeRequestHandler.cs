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
    public class AddSuplierTypeRequestHandler : IRequestHandler<SupplierTypeRequest, MessageResponse>
    {
        private readonly ISupplierTypeRepository _supplierTypeRepository;

        public AddSuplierTypeRequestHandler(ISupplierTypeRepository supplierTypeRepository)
        {
            _supplierTypeRepository = supplierTypeRepository;
        }

        public Task<MessageResponse> Handle(SupplierTypeRequest request, CancellationToken cancellationToken)
        {
            return _supplierTypeRepository.AddSupplierType(request);
        }
    }
}
