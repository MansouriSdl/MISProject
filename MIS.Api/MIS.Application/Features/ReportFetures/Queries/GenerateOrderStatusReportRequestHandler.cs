using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.ReportFetures.Queries
{
    public class GenerateOrderStatusReportRequestHandler : IRequestHandler<GenerateOrderStatusReportRequest, IEnumerable<OrderStatusReportModel>>
    {

        IReport Report;
        public GenerateOrderStatusReportRequestHandler(IReport report)
        {
            Report = report;
        }

        public async Task<IEnumerable<OrderStatusReportModel>> Handle(GenerateOrderStatusReportRequest request, CancellationToken cancellationToken)
        {
            return await Report.GenerateOrderStatusReportAsync(request.PredicateDate);
        }
    }
}
