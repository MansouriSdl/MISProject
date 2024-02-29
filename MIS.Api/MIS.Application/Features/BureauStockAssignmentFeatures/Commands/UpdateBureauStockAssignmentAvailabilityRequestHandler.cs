using MediatR;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Commands
{
    public class UpdateBureauStockAssignmentAvailabilityRequestHandler : IRequestHandler<UpdateBureauStockAssignmentAvailabilityRequest, int>
    {
        private readonly IBureauStockAssignmentRepository _bureauStockAssignmentRepository;

        public UpdateBureauStockAssignmentAvailabilityRequestHandler(IBureauStockAssignmentRepository bureauStockAssignmentRepository)
        {
            _bureauStockAssignmentRepository = bureauStockAssignmentRepository;
        }

        public Task<int> Handle(UpdateBureauStockAssignmentAvailabilityRequest request, CancellationToken cancellationToken)
        {
            return _bureauStockAssignmentRepository.UpdateBureauStockAssignmentsAvailability(request);
        }
    }
}
