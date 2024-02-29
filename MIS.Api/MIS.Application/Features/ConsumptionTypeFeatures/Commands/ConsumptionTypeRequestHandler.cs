using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ConsumptionTypeFeatures.Commands
{
    public class ConsumptionTypeRequestHandler : IRequestHandler<ConsumptionTypeRequest, MessageResponse>
    {
        private readonly IConsumptionTypeRepository _consumptionTypeRepository;

        public ConsumptionTypeRequestHandler(IConsumptionTypeRepository consumptionTypeRepository)
        {
            _consumptionTypeRepository = consumptionTypeRepository;
        }

        public Task<MessageResponse> Handle(ConsumptionTypeRequest request, CancellationToken cancellationToken)
        {
            return _consumptionTypeRepository.AddConsumptionType(request);
        }
    }
}
