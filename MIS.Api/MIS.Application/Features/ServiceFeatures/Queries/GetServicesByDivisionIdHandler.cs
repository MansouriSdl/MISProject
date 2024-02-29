using MediatR;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Queries
{
    public class GetServicesByDivisionIdHandler : IRequestHandler<GetServicesByDivisionId, IEnumerable<Service>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServicesByDivisionIdHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task<IEnumerable<Service>> Handle(GetServicesByDivisionId request, CancellationToken cancellationToken)
        {
            return _serviceRepository.GetServicesByDivisionId(request.DivisionId);
        }
    }
}
