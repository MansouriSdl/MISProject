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

    public class UpdateEquipmentTypeRequestHandler : IRequestHandler<UpdateEquipmentTypeRequest, MessageResponse>
    {

        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public UpdateEquipmentTypeRequestHandler(IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public Task<MessageResponse> Handle(UpdateEquipmentTypeRequest request, CancellationToken cancellationToken)
        {
            return _equipmentTypeRepository.UpdateEquipmentType(request);
        }
    }
}
