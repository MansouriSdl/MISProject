using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Queries
{
    public class GetBueauStockAssignmentsRequestHandler : IRequestHandler<GetBueauStockAssignmentsRequest, List<BureauStockAssignmentsResponse>>
    {
        private readonly IBureauStockAssignmentRepository _bureauStockAssignmentRepository;

        public GetBueauStockAssignmentsRequestHandler(IBureauStockAssignmentRepository bureauStockAssignmentRepository)
        {
            _bureauStockAssignmentRepository = bureauStockAssignmentRepository;
        }

        public Task<List<BureauStockAssignmentsResponse>> Handle(GetBueauStockAssignmentsRequest request, CancellationToken cancellationToken)
        {
            return _bureauStockAssignmentRepository.GeBureauStockAssignments(request.FilterRequest);
        }
    }
}
