using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Queries
{
    public class GetStocksByEquipmentIdRequest : IRequest<IEnumerable<NameResponse>>
    {
        public GetStocksByEquipmentIdRequest(Guid equipmentId)
        {
            EquipmentId = equipmentId;
        }

        public Guid EquipmentId { get; set; }
    }
}
