using MediatR;
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
    public class GetBureausByServiceIdHandler : IRequestHandler<GetBureausByServiceId, IEnumerable<Bureau>>
    {
        private readonly IBureauRepository _bureauRepository;

        public GetBureausByServiceIdHandler(IBureauRepository bureauRepository)
        {
            _bureauRepository = bureauRepository;
        }

        public Task<IEnumerable<Bureau>> Handle(GetBureausByServiceId request, CancellationToken cancellationToken)
        {
            return _bureauRepository.GetBureausByServiceId(request.ServiceId);
        }
    }
}
