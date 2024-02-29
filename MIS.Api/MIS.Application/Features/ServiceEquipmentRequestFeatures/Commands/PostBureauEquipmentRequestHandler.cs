using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceEquipmentRequestFeatures.Commands
{
    public class PostBureauEquipmentRequestHandler : IRequestHandler<PostBureauEquipmentRequest, MessageResponse>
    {
        private readonly IServiceEquipmentRequestRepository _ServiceEquipmentRequestRepository;

        public PostBureauEquipmentRequestHandler(IServiceEquipmentRequestRepository ServiceEquipmentRequestRepository)
        {
            _ServiceEquipmentRequestRepository = ServiceEquipmentRequestRepository;
        }

        public Task<MessageResponse> Handle(PostBureauEquipmentRequest request, CancellationToken cancellationToken)
        {
            PostServiceEquipmentRequestRequest postServiceEquipmentRequest = new()
            {
                EquipmentId = request.EquipmentId,
                Quantity = request.Quantity,
                ReturnedQuantity = request.ReturnedQuantity,
                UserId = request.UserId
            };
            return _ServiceEquipmentRequestRepository.AddServiceEquipmentRequest(postServiceEquipmentRequest);
        }
    }
}
