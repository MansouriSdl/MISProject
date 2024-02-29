using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionEquipmentRequestFeatures.Queries
{
    public class GetServiceEquipmentRequestByServiceHandler : IRequestHandler<GetServiceEquipmentRequestByService, IEnumerable<ServiceEquipmentRequestResponse>>
    {
        private readonly IServiceEquipmentRequestRepository _serviceEquipmentRequestRepository;

        public GetServiceEquipmentRequestByServiceHandler(IServiceEquipmentRequestRepository serviceEquipmentRequestRepository)
        {
            _serviceEquipmentRequestRepository = serviceEquipmentRequestRepository;
        }

        public Task<IEnumerable<ServiceEquipmentRequestResponse>> Handle(GetServiceEquipmentRequestByService request, CancellationToken cancellationToken)
        {
            return _serviceEquipmentRequestRepository.GetServiceEquipmentRequestByService(request.UserId, request.FilterRequest);
        }
    }
}
