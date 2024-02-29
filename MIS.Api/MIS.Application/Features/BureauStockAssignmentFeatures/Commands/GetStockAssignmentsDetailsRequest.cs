﻿using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Commands
{
    public class GetStockAssignmentsDetailsRequest : IRequest<StockAssignmentsDetailsResponse>
    {
        public GetStockAssignmentsDetailsRequest(FilterRequest filterRequest)
        {
            FilterRequest = filterRequest;
        }

        public FilterRequest FilterRequest { get; set; }
    }
}
