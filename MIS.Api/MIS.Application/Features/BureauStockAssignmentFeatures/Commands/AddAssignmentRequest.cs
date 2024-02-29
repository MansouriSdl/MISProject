using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Commands
{
    public class AddAssignmentRequest : IRequest<MessageResponse>
    {
        public AddAssignmentRequest(Guid bureauId, Guid stockId, int state, Guid userId, int quantity)
        {
            BureauId = bureauId;
            StockId = stockId;
            State = state;
            UserId = userId;
            Quantity = quantity;
        }

        public Guid BureauId { get; set; }
        public Guid StockId { get; set; }
        public int State { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}
