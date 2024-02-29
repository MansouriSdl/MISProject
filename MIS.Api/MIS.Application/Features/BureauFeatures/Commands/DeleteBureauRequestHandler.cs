using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Commands
{
    public class DeleteBureauRequestHandler : IRequestHandler<DeleteBureauRequest, MessageResponse>
    {
        private readonly IBureauRepository _bureauRepository;

        public DeleteBureauRequestHandler(IBureauRepository bureauRepository)
        {
            _bureauRepository = bureauRepository;
        }

        public Task<MessageResponse> Handle(DeleteBureauRequest request, CancellationToken cancellationToken)
        {
            return _bureauRepository.DeleteBureau(request.Id);
        }
    }
}
