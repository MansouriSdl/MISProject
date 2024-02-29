using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Queries
{
    public class GetSuppliersByTypeRequestHandler : IRequestHandler<GetSuppliersByTypeRequest, IEnumerable<NameResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSuppliersByTypeRequestHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Task<IEnumerable<NameResponse>> Handle(GetSuppliersByTypeRequest request, CancellationToken cancellationToken)
        {
            return _supplierRepository.GetSuppliersByTypeId(request.TypeId);
        }
    }
}
