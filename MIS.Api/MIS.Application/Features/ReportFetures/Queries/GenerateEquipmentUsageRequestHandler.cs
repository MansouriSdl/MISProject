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
    public class GenerateEquipmentUsageRequestHandler : IRequestHandler<GenerateEquipmentUsageRequest, IEnumerable<EquipmentUsageReportModel>>
    { 

        IReport Report;
        public GenerateEquipmentUsageRequestHandler(IReport report)
        {
            Report = report;
        }
        public async Task<IEnumerable<EquipmentUsageReportModel>> Handle(GenerateEquipmentUsageRequest request, CancellationToken cancellationToken)
        {
            return await Report.GenerateEquipmentUsageReportAsync(request.predicateDate);
        }
    }
}
