using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionFeatures.Commands
{
    public class AddDivisionRequestHandler : IRequestHandler<DivisionRequest, MessageResponse>
    {
        private readonly IDivisionRepository _divisionRepository;

        public AddDivisionRequestHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public Task<MessageResponse> Handle(DivisionRequest request, CancellationToken cancellationToken)
        {
            return _divisionRepository.AddDivision(request);
        }
    }
}
