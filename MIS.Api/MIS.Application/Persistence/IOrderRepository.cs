using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<MessageResponse> AddOrder(PostOrderRequest request);
        Task<MessageResponse> AddOrdersList(PostOrdersListRequest request);
        Task<List<OrderResponse>> GetOrders(FilterRequest request);
        Task<List<PendingOrdersResponse>> GetPendingOrders(FilterRequest request);

    }
}
