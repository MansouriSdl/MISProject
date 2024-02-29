using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentTypeFeatures.Queries
{
    public class GetAllEquipmentTypesHandler : IRequestHandler<GetAllEquipmentTypes, IEnumerable<EquipmentTypeResponse>>
    {
        private readonly IEquipmentTypeRepository _equipmentTypeRepository;

        public GetAllEquipmentTypesHandler(IEquipmentTypeRepository equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public Task<IEnumerable<EquipmentTypeResponse>> Handle(GetAllEquipmentTypes request, CancellationToken cancellationToken)
        {
            return _equipmentTypeRepository.GetAllEquipmentTypes();
        }
    }
}
