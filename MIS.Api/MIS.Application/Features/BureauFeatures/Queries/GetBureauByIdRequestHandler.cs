using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Queries
{
    public class GetBureauByIdRequestHandler : IRequestHandler<GetBureauByIdRequest, BureauResponse>
    {
        private readonly IBureauRepository _bureauRepository;

        public GetBureauByIdRequestHandler(IBureauRepository bureauRepository)
        {
            _bureauRepository = bureauRepository;
        }

        public Task<BureauResponse> Handle(GetBureauByIdRequest request, CancellationToken cancellationToken)
        {
            return _bureauRepository.GetBureauById(request.Id);
        }
    }
}
