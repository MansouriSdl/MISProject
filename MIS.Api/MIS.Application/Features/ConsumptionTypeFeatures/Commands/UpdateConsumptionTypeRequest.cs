using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ConsumptionTypeFeatures.Commands
{
    public class UpdateConsumptionTypeRequest : IRequest<MessageResponse>
    {
        public UpdateConsumptionTypeRequest(Guid id, ConsumptionTypeRequest consumptionTypeRequest)
        {
            Id = id;
            ConsumptionTypeRequest = consumptionTypeRequest;
        }

        public Guid Id { get; set; }
        public ConsumptionTypeRequest ConsumptionTypeRequest { get; set; }
    }
}
