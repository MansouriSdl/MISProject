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

namespace MIS.Application.Features.DivisionFeatures.Queries
{
    public class GetAllDivisionsHandler : IRequestHandler<GetAllDivisions, IEnumerable<DivisionResponse>>
    {
        private readonly IDivisionRepository _divisionRepository;

        public GetAllDivisionsHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public Task<IEnumerable<DivisionResponse>> Handle(GetAllDivisions request, CancellationToken cancellationToken)
        {
            return _divisionRepository.GetAllDivisions();
        }
    }
}
