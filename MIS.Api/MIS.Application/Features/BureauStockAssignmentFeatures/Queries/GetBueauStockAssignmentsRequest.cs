using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Queries
{
    public class GetBueauStockAssignmentsRequest : IRequest<List<BureauStockAssignmentsResponse>>
    {
        public GetBueauStockAssignmentsRequest(FilterRequest filterRequest)
        {
            FilterRequest = filterRequest;
        }

        public FilterRequest FilterRequest { get; set; }
    }
}
