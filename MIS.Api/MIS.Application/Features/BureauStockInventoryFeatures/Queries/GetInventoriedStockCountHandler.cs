using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauEquipmentAssignmentFeatures.Queries
{
    public class GetInventoriedStockCountHandler : IRequestHandler<GetInventoriedStockCount, List<InventoriedStockCountResponse>>
    {
        private readonly IBureauStockInventoryRepository _bureauEquipmentAssignmentRepository;

        public GetInventoriedStockCountHandler(IBureauStockInventoryRepository bureauEquipmentAssignmentRepository)
        {
            _bureauEquipmentAssignmentRepository = bureauEquipmentAssignmentRepository;
        }

        public Task<List<InventoriedStockCountResponse>> Handle(GetInventoriedStockCount request, CancellationToken cancellationToken)
        {
            return _bureauEquipmentAssignmentRepository.GeAssignedEquipmentsCount(request.Request);
        }
    }
}
