using Microsoft.EntityFrameworkCore;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Constants;
using MIS.Domain.Entities;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MISDbContext context) : base(context)
        {
        }

        public async Task<MessageResponse> AddOrder(PostOrderRequest request)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                Order order = new()
                {
                    SupplierId = request.SupplierId,
                    EquipmentId = request.EquipmentId,
                    UserId = request.UserId,
                    Status = Domain.Enums.OrderStatus.ORDERED,
                    Quantity = request.Quantity ?? null
                };
                if (request.File != null && request.Description != null && request.Type != null)
                {
                    Attachment attachment = new()
                    {
                        File = request.File,
                        ContentType = request.Type,
                        Type = request.Type,
                        Extention = ".pdf",
                        Description = request.Description
                    };
                    await context.Attachments.AddAsync(attachment);
                    order.AttachmentId = attachment.Id;
                }
                await context.Orders.AddAsync(order);
                if (await context.SaveChangesAsync() > 0)
                {
                    transaction.Commit();
                    return new MessageResponse()
                    {
                        StatusCode = 200,
                        Message = "Success"
                    };
                }
                else
                {
                    transaction.Rollback();
                    return new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            
        }

        public async Task<MessageResponse> AddOrdersList(PostOrdersListRequest request)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {
                foreach (var acceptedRequest in request.AcceptedRequests)
                {
                    Order order = new()
                    {
                        SupplierId = acceptedRequest.PostOrderRequest.SupplierId,
                        UserId = acceptedRequest.PostOrderRequest.UserId,
                        Status = Domain.Enums.OrderStatus.ORDERED,
                    };
                    if (acceptedRequest.PostOrderRequest.File != null && acceptedRequest.PostOrderRequest.Description != null && acceptedRequest.PostOrderRequest.Type != null)
                    {
                        Attachment attachment = new()
                        {
                            File = acceptedRequest.PostOrderRequest.File,
                            ContentType = acceptedRequest.PostOrderRequest.Type,
                            Type = acceptedRequest.PostOrderRequest.Type,
                            Extention = ".pdf",
                            Description = acceptedRequest.PostOrderRequest.Description
                        };
                        await context.Attachments.AddAsync(attachment);
                        order.AttachmentId = attachment.Id;
                    }
                    await context.Orders.AddAsync(order);
                    var orderRequest = await context.ServiceEquipmentRequests.FirstOrDefaultAsync(r => r.Id == acceptedRequest.RequestId);
                    if (orderRequest == null)
                        return null;
                    orderRequest.Status = Domain.Enums.RequestStatus.ORDERED;
                    orderRequest.OrderId = order.Id;
                    context.Entry(orderRequest).State = EntityState.Modified;
                }
                foreach (var inStockRequest in request.InStockRequests)
                {
                    var orderRequest = await context.ServiceEquipmentRequests.FirstOrDefaultAsync(r => r.Id == inStockRequest.RequestId);
                    if (orderRequest == null)
                        return null;
                    orderRequest.Status = Domain.Enums.RequestStatus.IN_STOCK;
                    context.Entry(orderRequest).State = EntityState.Modified;

                }
                foreach (var rejectedRequest in request.RejectedRequests)
                {
                    var orderRequest = await context.ServiceEquipmentRequests.FirstOrDefaultAsync(r => r.Id == rejectedRequest.RequestId);
                    if (orderRequest == null)
                        return null;
                    orderRequest.Status = Domain.Enums.RequestStatus.REJECTED;
                    orderRequest.Observation = rejectedRequest.Observation;
                    context.Entry(orderRequest).State = EntityState.Modified;

                }
                if (await context.SaveChangesAsync() > 0)
                {
                    transaction.Commit();
                    return new MessageResponse()
                    {
                        StatusCode = 200,
                        Message = "Success"
                    };
                }
                else
                {
                    transaction.Rollback();
                    return new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }
        }
            
            catch (Exception)
            {
                transaction.Rollback();
                return new MessageResponse()
        {
            StatusCode = 400,
                    Message = "Bad Request"
                };
    }
}

        public async Task<List<OrderResponse>> GetOrders(FilterRequest request)
        {
            var orders = await context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.Equipment)
                .Include(o => o.User)
                .Include(o => o.ServiceEquipmentRequest.Equipment)
                .Include(o => o.ServiceEquipmentRequest.User)
                .Where(o => o.DeletedAt == null)
                .ToListAsync();
            if(request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                orders = orders.Where(o => o.EquipmentId == request.EquipmentId).ToList();
            }
            if (request.SupplierId.HasValue && request.SupplierId != Guid.Empty)
            {
                orders = orders.Where(o => o.SupplierId == request.SupplierId).ToList();
            }
            if (request.Date.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt.Year == request.Date.Value.Year && o.CreatedAt.Month == request.Date.Value.Month && o.CreatedAt.Day == request.Date.Value.Day).ToList();
            }
            return orders.Select(o => new OrderResponse()
            {
                Equipment = o.Equipment != null ? o.Equipment.Name : (o.ServiceEquipmentRequest != null) ? o.ServiceEquipmentRequest.Equipment.Name : null,
                OrderDate = o.CreatedAt.ToShortDateString(),
                Quantity = o.Quantity.HasValue ? o.Quantity.Value : (o.ServiceEquipmentRequest != null) ? o.ServiceEquipmentRequest.Quantity : 0,
                Supplier = o.Supplier.CompanyName,
                User = o.ServiceEquipmentRequest == null ? o.User.LastName + " " + o.User.FirstName : (o.ServiceEquipmentRequest != null) ? (o.ServiceEquipmentRequest.User.LastName + " " + o.ServiceEquipmentRequest.User.FirstName) : null,
                Status = (o.Status == 0) ? OrderStatus.ORDERED : OrderStatus.IN_STOCK
            }).ToList();
        }

        public async Task<List<PendingOrdersResponse>> GetPendingOrders(FilterRequest request)
        {
            var pendingOrders = await context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.Equipment)
                .Include(o => o.User)
                .Include(o => o.ServiceEquipmentRequest.Equipment)
                .Include(o => o.ServiceEquipmentRequest.User)
                .Where(o => o.DeletedAt == null && o.Status == 0)
                .ToListAsync();
            if (request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                pendingOrders = pendingOrders.Where(o => o.EquipmentId == request.EquipmentId).ToList();
            }
            if (request.SupplierId.HasValue && request.SupplierId != Guid.Empty)
            {
                pendingOrders = pendingOrders.Where(o => o.SupplierId == request.SupplierId).ToList();
            }
            if (request.Date.HasValue)
            {
                pendingOrders = pendingOrders.Where(o => o.CreatedAt.Year == request.Date.Value.Year && o.CreatedAt.Month == request.Date.Value.Month && o.CreatedAt.Day == request.Date.Value.Day).ToList();
            }
            return pendingOrders.Select(o => new PendingOrdersResponse()
            {
                Equipment = o.Equipment != null ? o.Equipment.Name : (o.ServiceEquipmentRequest != null) ? o.ServiceEquipmentRequest.Equipment.Name : null,
                OrderDate = o.CreatedAt.ToShortDateString(),
                Quantity = o.Quantity.HasValue ? o.Quantity.Value : (o.ServiceEquipmentRequest != null) ? o.ServiceEquipmentRequest.Quantity : 0,
                Supplier = o.Supplier.CompanyName,
                User = o.ServiceEquipmentRequest == null ? o.User.LastName + " " + o.User.FirstName : (o.ServiceEquipmentRequest != null) ? (o.ServiceEquipmentRequest.User.LastName + " " + o.ServiceEquipmentRequest.User.FirstName) : null,
                EquipmentId = o.ServiceEquipmentRequest != null ? o.ServiceEquipmentRequest.EquipmentId : o.EquipmentId.HasValue ? o.EquipmentId.Value : Guid.Empty,
                OrderId = o.Id,
                Designation = null,
                Description = null,
                ExpirationDate = null,
                MultipleOrOneStock = null,
                CheckBox = null
            }).ToList();
        }
    }
}
