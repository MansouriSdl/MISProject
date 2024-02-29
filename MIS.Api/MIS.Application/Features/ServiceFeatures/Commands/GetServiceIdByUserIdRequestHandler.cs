using MediatR;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Commands
{
    public class GetServiceIdByUserIdRequestHandler : IRequestHandler<GetServiceIdByUserIdRequest, Guid>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServiceIdByUserIdRequestHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task<Guid> Handle(GetServiceIdByUserIdRequest request, CancellationToken cancellationToken)
        {
            return _serviceRepository.GetServiceIdByUserId(request.UserId);
        }
    }
}
