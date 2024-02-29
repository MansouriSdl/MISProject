using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Commands
{
    public class UpdateServiceRequestHandler : IRequestHandler<UpdateServiceRequest, MessageResponse>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceRequestHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task<MessageResponse> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
        {
            return _serviceRepository.UpdateService(request);
        }
    }
}
