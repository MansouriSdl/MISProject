using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.OrderFeatures.Commands
{
    public class AddOrdersListRequestHandler : IRequestHandler<AddOrdersListRequest, MessageResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrdersListRequestHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<MessageResponse> Handle(AddOrdersListRequest request, CancellationToken cancellationToken)
        {
            PostOrdersListRequest postOrdersListRequest = new() {
                AcceptedRequests = request.AcceptedRequests,
                InStockRequests = request.InStockRequests,
                RejectedRequests = request.RejectedRequests
            };
            return await _orderRepository.AddOrdersList(postOrdersListRequest);
        }
    }
}
