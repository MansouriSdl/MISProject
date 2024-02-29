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
    public   class GenerateOrderStatusReportRequest:IRequest<IEnumerable<OrderStatusReportModel>>
    {
        public PredicateDate PredicateDate { get; set; }
        public GenerateOrderStatusReportRequest(PredicateDate PredicateDate)
        {
            this.PredicateDate = PredicateDate;
        }
    }
}
