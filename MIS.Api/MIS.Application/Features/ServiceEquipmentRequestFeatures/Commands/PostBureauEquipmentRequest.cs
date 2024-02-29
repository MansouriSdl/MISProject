using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceEquipmentRequestFeatures.Commands
{
    public class PostBureauEquipmentRequest : IRequest<MessageResponse>
    {
        public Guid UserId { get; set; }
        public Guid EquipmentId { get; set; }
        public int Quantity { get; set; }
        public int? ReturnedQuantity { get; set; }
    }
}
