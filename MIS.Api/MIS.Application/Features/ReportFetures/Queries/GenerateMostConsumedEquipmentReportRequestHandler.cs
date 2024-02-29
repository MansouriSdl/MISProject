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
    public class GenerateMostConsumedEquipmentReportRequestHandler : IRequestHandler<GenerateMostConsumedEquipmentReportRequest, IEnumerable<MostConsumedEquipmentModel>>
    {
        IReport Report;
        public GenerateMostConsumedEquipmentReportRequestHandler(IReport report)
        {
            Report = report;
        }
        public Task<IEnumerable<MostConsumedEquipmentModel>> Handle(GenerateMostConsumedEquipmentReportRequest request, CancellationToken cancellationToken)
        {
            return Report.GenerateMostConsumedEquipmentReport();
        }
    }
}
