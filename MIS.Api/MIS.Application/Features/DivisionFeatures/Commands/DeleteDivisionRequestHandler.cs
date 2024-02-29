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
    public class DeleteDivisionRequestHandler : IRequestHandler<DeleteDivisionRequest, MessageResponse>
    {
        private readonly IDivisionRepository _divisionRepository;

        public DeleteDivisionRequestHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public Task<MessageResponse> Handle(DeleteDivisionRequest request, CancellationToken cancellationToken)
        {
            return _divisionRepository.DeleteDivision(request.Id);
        }
    }
}
