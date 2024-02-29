using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceEquipmentRequestFeatures.Queries
{
    public class GetPenddingRequestsHandler : IRequestHandler<GetPenddingRequests, IEnumerable<PendingRequestResponse>>
    {
        private readonly IServiceEquipmentRequestRepository _serviceEquipmentRequestRepository;

        public GetPenddingRequestsHandler(IServiceEquipmentRequestRepository serviceEquipmentRequestRepository)
        {
            _serviceEquipmentRequestRepository = serviceEquipmentRequestRepository;
        }

        public Task<IEnumerable<PendingRequestResponse>> Handle(GetPenddingRequests request, CancellationToken cancellationToken)
        {
            return _serviceEquipmentRequestRepository.GetPendingServiceEquipmentRequest(request.FilterRequest);
        }
    }
}
