using MIS.Application.Features.BureauStockInventoryFeatures.Commands;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IBureauStockInventoryRepository : IRepository<BureauStockInventory>
    {
        Task<BureauStockInventoryResponse> PostBureauEquipmentAssignment(PostBureauStockInventoryRequest request);
        Task<InventoryHistoryResponse> GetBureauEquipmentAssignmentHistory(BureauStockInventoryFilterRequest request);
        Task<List<InventoriedStockCountResponse>> GeAssignedEquipmentsCount(BureauStockInventoryFilterRequest request);
        Task<int> UpdateBureauEquipmentAssignmentsAvailability(List<UpdateInventoryAvailabilityRequest> request);
        Task<BureauStockInventoryResponse> PostInventoryForNewEquipments(PostInventoryForNewEquipmentsRequest request);


    }
}
