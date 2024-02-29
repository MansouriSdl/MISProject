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
    public class GenerateCurrentInventoryLevelsReportRequestHandler : IRequestHandler<GenerateCurrentInventoryLevelsReportRequest, IEnumerable<InventoryLevelReportModel>>
    {
        IReport Report;
        public GenerateCurrentInventoryLevelsReportRequestHandler(IReport report)
        {
            Report = report;
        }

        public async Task<IEnumerable<InventoryLevelReportModel>> Handle(GenerateCurrentInventoryLevelsReportRequest request, CancellationToken cancellationToken)
        {
            return await Report.GenerateCurrentInventoryLevelsReport();
        }
    }
}
