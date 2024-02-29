using MediatR;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionFeatures.Queries
{
    public class GetDivisionIdByNameRequestHandler : IRequestHandler<GetDivisionIdByNameRequest, Guid>
    {
        private readonly IDivisionRepository _divisionRepository;

        public GetDivisionIdByNameRequestHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public Task<Guid> Handle(GetDivisionIdByNameRequest request, CancellationToken cancellationToken)
        {
            return _divisionRepository.GetDivisionIdByName(request.Name);
        }
    }
}
