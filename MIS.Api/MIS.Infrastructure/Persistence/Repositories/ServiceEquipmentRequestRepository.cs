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
    public class ServiceEquipmentRequestRepository : Repository<ServiceEquipmentRequest>, IServiceEquipmentRequestRepository
    {
        public ServiceEquipmentRequestRepository(MISDbContext context) : base(context)
        {
        }

        public async Task<MessageResponse> AddServiceEquipmentRequest(PostServiceEquipmentRequestRequest request)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == request.UserId);
            if (employee != null && employee.ServiceId.HasValue && employee.ServiceId != null)
            {
                ServiceEquipmentRequest ServiceEquipmentRequest = new()
                {
                    ServiceId = employee.ServiceId.Value,
                    EquipmentId = request.EquipmentId,
                    UserId = request.UserId,
                    Quantity = request.Quantity,
                    ReturnedQuantity = request.ReturnedQuantity,
                    RequestDate = DateTime.Now,
                    Status = Domain.Enums.RequestStatus.PENDING
                };
                await context.ServiceEquipmentRequests.AddAsync(ServiceEquipmentRequest);
                if (await context.SaveChangesAsync() > 0)
                {
                    return new MessageResponse()
                    {
                        StatusCode = 200,
                        Message = "Success"
                    };
                }
                else
                {
                    return new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }
            }
            else
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }

        }

        public async Task<IEnumerable<ServiceEquipmentRequestResponse>> GetServiceEquipmentRequestByService(Guid userId, FilterRequest request)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == userId && e.DeletedAt == null);
            if (employee != null && employee.ServiceId.HasValue && employee.ServiceId != null)
            {
                var requests = await context.ServiceEquipmentRequests.Include(r => r.Equipment).Where(r => r.DeletedAt == null && r.ServiceId == employee.ServiceId).ToListAsync();
                if (request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
                {
                    requests = requests.Where(r => r.EquipmentId == request.EquipmentId).ToList();
                }
                var data = requests.Select(d => new ServiceEquipmentRequestResponse()
                {
                    EquipmentName = d.Equipment.Name,
                    Quantity = d.Quantity,
                    ReturnedQuantity = d.ReturnedQuantity,
                    Status = (d.Status == 0) ? RequestStatus.PENDING : ((int)d.Status == 1) ? RequestStatus.ORDERED : ((int)d.Status == 2) ? RequestStatus.IN_STOCK : RequestStatus.REJECTED
                }).ToList();

                return data;

            }
            else
                return null;
        }

        public async Task<IEnumerable<PendingRequestResponse>> GetPendingServiceEquipmentRequest(FilterRequest request)
        {
            var pendingRequests = await context.ServiceEquipmentRequests
                .Include(r => r.Equipment)
                .Include(r => r.Service)
                .Include(r => r.User)
                .Where(r => r.DeletedAt == null && r.Status == 0)
                .ToListAsync();
            if (request.ServiceId.HasValue && request.ServiceId != Guid.Empty)
            {
                pendingRequests = pendingRequests.Where(r => r.ServiceId == request.ServiceId).ToList();
            }
            if (request.EquipmentId.HasValue && request.EquipmentId != Guid.Empty)
            {
                pendingRequests = pendingRequests.Where(r => r.EquipmentId == request.EquipmentId).ToList();
            }
            return pendingRequests.Select(r => new PendingRequestResponse()
            {
                RequestId = r.Id,
                EquipmentName = r.Equipment.Name,
                Quantity = r.Quantity,
                ReturnedQuantity = r.ReturnedQuantity,
                ExistingQuantity = context.Stocks.Where(s => s.EquipmentId == r.EquipmentId & s.IsMultiple).Sum(s => s.Quantity) - context.Stocks.Include(s => s.BureauStockAssignments).Where(s => s.EquipmentId == r.EquipmentId && s.IsMultiple && s.BureauStockAssignments.Any(a => a.IsAvailable == true && a.DeletedAt == null)).Sum(s => s.Quantity) + context.Stocks.Where(s => s.EquipmentId == r.EquipmentId && s.DeletedAt == null && !s.IsMultiple).Sum(s => s.Quantity),
                RequestDate = r.RequestDate.ToShortDateString(),
                UserName = r.User.LastName + "  " + r.User.FirstName,
                ServiceName = r.Service?.Name,
                Status = null,
                Observation = null,
                SupplierType = null,
                Supplier = null,
                File = null
            }).ToList();
        }
    }
}
