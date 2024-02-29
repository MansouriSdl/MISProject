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
    public  class GenerateMonthlyOrderReportRequest:IRequest<IEnumerable<MonthlyOrderReportModel>>
    {
        public  int PredicateDate { get; set; }
        public GenerateMonthlyOrderReportRequest(int PredicateDate)
        {
            this.PredicateDate = PredicateDate;
        }
    }
}
