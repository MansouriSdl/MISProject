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

namespace MIS.Application.Features.ServiceFeatures.Queries
{
    public class GetAllServicesRequestHandler : IRequestHandler<GetAllServicesRequest, IEnumerable<ServiceResponse>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllServicesRequestHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task<IEnumerable<ServiceResponse>> Handle(GetAllServicesRequest request, CancellationToken cancellationToken)
        {
            return _serviceRepository.GetAllServices();
        }
    }
}
