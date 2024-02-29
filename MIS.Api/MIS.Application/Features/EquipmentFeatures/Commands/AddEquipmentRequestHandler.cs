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
    public class AddEquipmentRequestHandler : IRequestHandler<EquipmentRequest, MessageResponse>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public AddEquipmentRequestHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public Task<MessageResponse> Handle(EquipmentRequest request, CancellationToken cancellationToken)
        {
            return _equipmentRepository.AddEquipment(request);
        }
    }
}
