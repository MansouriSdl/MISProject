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
    public class AddOrderRequestHandler : IRequestHandler<AddOrderRequest, MessageResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderRequestHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<MessageResponse> Handle(AddOrderRequest request, CancellationToken cancellationToken)
        {
            PostOrderRequest postOrderRequest = new() { 
                File = request.File,
                SupplierId = request.SupplierId,
                EquipmentId = request.EquipmentId,
                Quantity = request.Quantity,
                UserId = request.UserId,
                Description = request.Description,
                Type = request.Type
            };
            return _orderRepository.AddOrder(postOrderRequest);
        }
    }
}
