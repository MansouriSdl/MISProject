using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockInventoryFeatures.Commands
{
    public class PostInventoryForNewEquipmentsRequest : IRequest<BureauStockInventoryResponse>
    {
        public string QrCode { get; set; }
        public Guid UserId { get; set; }
        public Guid StockId { get; set; }
        public Guid BureauId { get; set; }
        public int State { get; set; }
        public string Designation { get; set; }
        public Guid SupplierId { get; set; }
        public string MarketReference { get; set; }
        public string MarketObject { get; set; }
    }
}
