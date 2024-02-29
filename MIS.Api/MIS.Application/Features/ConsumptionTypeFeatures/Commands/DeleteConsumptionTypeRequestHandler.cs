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
    public class DeleteConsumptionTypeRequestHandler : IRequestHandler<DeleteConsumptionTypeRequest, MessageResponse>
    {
        private readonly IConsumptionTypeRepository _consumptionTypeRepository;

        public DeleteConsumptionTypeRequestHandler(IConsumptionTypeRepository consumptionTypeRepository)
        {
            _consumptionTypeRepository = consumptionTypeRepository;
        }

        public Task<MessageResponse> Handle(DeleteConsumptionTypeRequest request, CancellationToken cancellationToken)
        {
            return _consumptionTypeRepository.DeleteConsumptionType(request.Id);
        }
    }
}
