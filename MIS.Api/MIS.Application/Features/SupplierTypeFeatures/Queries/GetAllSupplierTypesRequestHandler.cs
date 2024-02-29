using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierTypeFeatures.Queries
{
    public class GetAllSupplierTypesRequestHandler : IRequestHandler<GetAllSupplierTypesRequest, IEnumerable<SupplierTypeResponse>>
    {
        private readonly ISupplierTypeRepository _supplierTypeRepository;

        public GetAllSupplierTypesRequestHandler(ISupplierTypeRepository supplierTypeRepository)
        {
            _supplierTypeRepository = supplierTypeRepository;
        }

        public Task<IEnumerable<SupplierTypeResponse>> Handle(GetAllSupplierTypesRequest request, CancellationToken cancellationToken)
        {
            return _supplierTypeRepository.GetAllSupplierTypes();
        }
    }
}
