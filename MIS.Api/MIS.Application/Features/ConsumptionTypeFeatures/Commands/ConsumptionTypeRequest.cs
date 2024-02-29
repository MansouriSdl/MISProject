using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ConsumptionTypeFeatures.Commands
{
    public class ConsumptionTypeRequest : IRequest<MessageResponse>
    {
        public ConsumptionTypeRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

    }
}
