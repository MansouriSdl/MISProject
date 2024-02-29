using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.OrderFeatures.Queries
{
    public class GetAllOrders : IRequest<List<OrderResponse>>
    {
        public GetAllOrders(FilterRequest filterRequest)
        {
            FilterRequest = filterRequest;
        }

        public FilterRequest FilterRequest { get; set; }
    }
}
