using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.OrderFeatures.Queries
{
    public class GetPendingOrdersRequestHandler : IRequestHandler<GetPendingOrdersRequest, List<PendingOrdersResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetPendingOrdersRequestHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<List<PendingOrdersResponse>> Handle(GetPendingOrdersRequest request, CancellationToken cancellationToken)
        {
            return _orderRepository.GetPendingOrders(request.FilterRequest);
        }
    }
}
