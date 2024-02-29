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
    public class UpdateDivisionRequestHandler : IRequestHandler<UpdateDivisionRequest, MessageResponse>
    {
        private readonly IDivisionRepository _divisionRepository;

        public UpdateDivisionRequestHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public Task<MessageResponse> Handle(UpdateDivisionRequest request, CancellationToken cancellationToken)
        {
            return _divisionRepository.UpdateDivision(request);
        }
    }
}
