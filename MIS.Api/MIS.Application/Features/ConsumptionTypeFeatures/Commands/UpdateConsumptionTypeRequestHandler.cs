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
    public class UpdateConsumptionTypeRequestHandler : IRequestHandler<UpdateConsumptionTypeRequest, MessageResponse>
    {
        private readonly IConsumptionTypeRepository _consumptionTypeRepository;

        public UpdateConsumptionTypeRequestHandler(IConsumptionTypeRepository consumptionTypeRepository)
        {
            _consumptionTypeRepository = consumptionTypeRepository;
        }

        public Task<MessageResponse> Handle(UpdateConsumptionTypeRequest request, CancellationToken cancellationToken)
        {
            return _consumptionTypeRepository.UpdateConsumptionType(request);
        }
    }
}
