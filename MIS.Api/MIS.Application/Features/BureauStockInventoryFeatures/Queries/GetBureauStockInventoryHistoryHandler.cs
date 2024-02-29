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
    public class GetBureauStockInventoryHistoryHandler : IRequestHandler<GetBureauStockInventoryHistory, InventoryHistoryResponse>
    {
        private readonly IBureauStockInventoryRepository _bureauEquipmentAssignmentRepository;

        public GetBureauStockInventoryHistoryHandler(IBureauStockInventoryRepository bureauEquipmentAssignmentRepository)
        {
            _bureauEquipmentAssignmentRepository = bureauEquipmentAssignmentRepository;
        }

        public Task<InventoryHistoryResponse> Handle(GetBureauStockInventoryHistory request, CancellationToken cancellationToken)
        {
            return  _bureauEquipmentAssignmentRepository.GetBureauEquipmentAssignmentHistory(request.Request);
        }
    }
}
