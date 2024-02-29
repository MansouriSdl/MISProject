using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Queries
{
    public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliers, IEnumerable<SupplierResponse>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetAllSuppliersHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Task<IEnumerable<SupplierResponse>> Handle(GetAllSuppliers request, CancellationToken cancellationToken)
        {
            return _supplierRepository.GetAllSuppliers();
        }
    }
}
