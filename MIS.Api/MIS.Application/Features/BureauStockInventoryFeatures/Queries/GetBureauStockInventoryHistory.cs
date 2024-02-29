using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauEquipmentAssignmentFeatures.Queries
{
    public class GetBureauStockInventoryHistory : IRequest<InventoryHistoryResponse>
    {
        public GetBureauStockInventoryHistory(BureauStockInventoryFilterRequest request)
        {
            Request = request;
        }

        public BureauStockInventoryFilterRequest Request { get; set; }
    }
}
