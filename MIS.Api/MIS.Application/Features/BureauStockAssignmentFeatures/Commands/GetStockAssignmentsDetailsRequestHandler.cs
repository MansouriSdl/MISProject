using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Commands
{
    public class GetStockAssignmentsDetailsRequestHandler : IRequestHandler<GetStockAssignmentsDetailsRequest, StockAssignmentsDetailsResponse>
    {
        private readonly IBureauStockAssignmentRepository _bureauStockAssignmentRepository;

        public GetStockAssignmentsDetailsRequestHandler(IBureauStockAssignmentRepository bureauStockAssignmentRepository)
        {
            _bureauStockAssignmentRepository = bureauStockAssignmentRepository;
        }

        public Task<StockAssignmentsDetailsResponse> Handle(GetStockAssignmentsDetailsRequest request, CancellationToken cancellationToken)
        {
            return _bureauStockAssignmentRepository.GetStockAssignmentsDetails(request.FilterRequest);
        }
    }
}
