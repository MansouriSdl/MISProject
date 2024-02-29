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

namespace MIS.Application.Features.BureauFeatures.Queries
{
    public class GetAllBureausRequestHandler : IRequestHandler<GetAllBureausRequest, IEnumerable<BureauResponse>>
    {
        private readonly IBureauRepository _bureauRepository;

        public GetAllBureausRequestHandler(IBureauRepository bureauRepository)
        {
            _bureauRepository = bureauRepository;
        }

        public Task<IEnumerable<BureauResponse>> Handle(GetAllBureausRequest request, CancellationToken cancellationToken)
        {
            return _bureauRepository.GetAllBureaus();
        }
    }
}
