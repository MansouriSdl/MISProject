using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockInventoryFeatures.Commands
{
    public class PostBureauStockInventory : IRequest<BureauStockInventoryResponse>
    {
        public string QrCode { get; set; }
        public Guid UserId { get; set; }
        public Guid StockId { get; set; }
        public DateTime InventoryDate { get; set; }
        public int State { get; set; }
        public bool IsAvailable { get; set; }
    }
}
