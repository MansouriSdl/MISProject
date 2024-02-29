using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.OrderFeatures.Queries
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<List<OrderResponse>> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            return _orderRepository.GetOrders(request.FilterRequest);
        }
    }
}
