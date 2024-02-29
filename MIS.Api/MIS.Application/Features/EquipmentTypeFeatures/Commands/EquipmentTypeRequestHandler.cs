using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentTypeFeatures.Commands
{
    public class EquipmentTypeRequestHandler : IRequestHandler<EquipmentTypeRequest, MessageResponse>
    {
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public EquipmentTypeRequestHandler(IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public Task<MessageResponse> Handle(EquipmentTypeRequest request, CancellationToken cancellationToken)
        {
            return _equipmentTypeRepository.AddEquipmentType(request);
        }
    }
}
