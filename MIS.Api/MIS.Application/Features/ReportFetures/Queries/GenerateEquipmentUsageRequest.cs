using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ReportFetures.Queries
{
    public   class GenerateEquipmentUsageRequest:IRequest<IEnumerable<EquipmentUsageReportModel>>
    {
        public PredicateDate predicateDate { get; set; }
        public GenerateEquipmentUsageRequest(PredicateDate predicateDate)
        {
            this.predicateDate = predicateDate;
        }

    }
}
