using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ConsumptionTypeFeatures.Queries
{
    public class GetAllConsumptionTypesRequestHandler : IRequestHandler<GetAllConsumptionTypesRequest, IEnumerable<ConsumptionTypeResponse>>
    {
        private readonly IConsumptionTypeRepository _consumptionTypeRepository;

        public GetAllConsumptionTypesRequestHandler(IConsumptionTypeRepository consumptionTypeRepository)
        {
            _consumptionTypeRepository = consumptionTypeRepository;
        }

        public Task<IEnumerable<ConsumptionTypeResponse>> Handle(GetAllConsumptionTypesRequest request, CancellationToken cancellationToken)
        {
            return _consumptionTypeRepository.GetAllConsumptionTypes();
        }
    }
}
