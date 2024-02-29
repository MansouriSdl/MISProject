using MediatR;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockInventoryFeatures.Commands
{
    public class UpdateInventoryAvailabilityHandler : IRequestHandler<UpdateInventoryAvailability, int>
    {
        private readonly IBureauStockInventoryRepository _bureauEquipmentAssignmentRepository;

        public UpdateInventoryAvailabilityHandler(IBureauStockInventoryRepository bureauEquipmentAssignmentRepository)
        {
            _bureauEquipmentAssignmentRepository = bureauEquipmentAssignmentRepository;
        }

        public Task<int> Handle(UpdateInventoryAvailability request, CancellationToken cancellationToken)
        {
            return _bureauEquipmentAssignmentRepository.UpdateBureauEquipmentAssignmentsAvailability(request.UpdateAssignmentAvailabilityRequestList);
        }
    }
}
