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
    public class DeleteEquipmentTypeRequestHandler : IRequestHandler<DeleteEquipmentTypeRequest, MessageResponse>
    {
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public DeleteEquipmentTypeRequestHandler(IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public Task<MessageResponse> Handle(DeleteEquipmentTypeRequest request, CancellationToken cancellationToken)
        {
            return _equipmentTypeRepository.DeleteEquipmentType(request.Id);
        }
    }
}
