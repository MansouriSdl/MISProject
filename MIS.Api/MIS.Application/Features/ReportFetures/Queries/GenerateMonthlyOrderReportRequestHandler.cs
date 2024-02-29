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
    public class GenerateMonthlyOrderReportRequestHandler : IRequestHandler<GenerateMonthlyOrderReportRequest, IEnumerable<MonthlyOrderReportModel>>
    {

        IReport Report;
        public GenerateMonthlyOrderReportRequestHandler(IReport report)
        {
            Report = report;
        }

        public async Task<IEnumerable<MonthlyOrderReportModel>> Handle(GenerateMonthlyOrderReportRequest request, CancellationToken cancellationToken)
        {

            return await Report.GenerateMonthlyOrderReportAsync(request.PredicateDate);

        }
    }
}
