using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentFeatures.Commands
{
    public class UpdateEquipmentRequestHandler : IRequestHandler<UpdateEquipmentRequest, MessageResponse>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public UpdateEquipmentRequestHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public Task<MessageResponse> Handle(UpdateEquipmentRequest request, CancellationToken cancellationToken)
        {
            return _equipmentRepository.UpdateEquipment(request);
        }
    }
}
