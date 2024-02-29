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

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Commands
{
    public class AddAssignmentRequestHandler : IRequestHandler<AddAssignmentRequest, MessageResponse>
    {
        private readonly IBureauStockAssignmentRepository _bureauStockAssignmentRepository;

        public AddAssignmentRequestHandler(IBureauStockAssignmentRepository bureauStockAssignmentRepository)
        {
            _bureauStockAssignmentRepository = bureauStockAssignmentRepository;
        }

        public Task<MessageResponse> Handle(AddAssignmentRequest request, CancellationToken cancellationToken)
        {
            return _bureauStockAssignmentRepository.AddAssignment(request);
        }
    }
}
