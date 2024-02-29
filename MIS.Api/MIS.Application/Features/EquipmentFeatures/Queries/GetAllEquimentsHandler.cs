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

namespace MIS.Application.Features.EquipmentFeatures.Queries
{
    public class GetAllEquimentsHandler : IRequestHandler<GetAllEquipments, IEnumerable<EquipmentResponse>>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public GetAllEquimentsHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public Task<IEnumerable<EquipmentResponse>> Handle(GetAllEquipments request, CancellationToken cancellationToken)
        {
            return _equipmentRepository.GetAllEquipments();
        }
    }
}
