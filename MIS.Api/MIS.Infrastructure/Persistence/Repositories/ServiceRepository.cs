using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.ServiceFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ServiceRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<ServiceResponse>> GetAllServices()
        {
            var services =  await context.Services.Include(s => s.Division).Where(s => s.DeletedAt == null).ToListAsync();
            return services.Select(b => new ServiceResponse
            {
                Id = b.Id,
                Name = b.Name,
                DivisionId = b.DivisionId,
                Division = b.Division?.Name,
                Update = null,
                Delete = null
            }).ToList();
        }
        public async Task<MessageResponse> AddService(ServiceRequest request)
        {
            context.Set<Service>().Add(_mapper.Map<ServiceRequest, Service>(request));
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

        public async Task<IEnumerable<Service>> GetServicesByDivisionId(Guid divisionId)
        {
            return await context.Services
                 .Where(s => s.DivisionId == divisionId && s.DeletedAt == null)
                 .AsNoTracking().ToListAsync();
        }

        public async Task<MessageResponse> UpdateService(UpdateServiceRequest request)
        {
            Service service = _mapper.Map<ServiceRequest, Service>(request.ServiceRequest);
            service.Id = request.Id;
            Service existingService = await context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (existingService == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            service.CreatedAt = existingService.CreatedAt;
            service.CreatedBy = existingService.CreatedBy;

            var result = UpdateAsync(service);
            MessageResponse messageResponse = new();
            if (result.Result > 0)
            {
                messageResponse = new MessageResponse()
                {
                    StatusCode = 200,
                    Message = "Success"
                };
            }
            else
            {
                messageResponse = new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }

            return messageResponse;
        }

        public async Task<MessageResponse> DeleteService(Guid id)
        {
            var existingService = await context.Services.FirstOrDefaultAsync(b => b.Id == id);
            if (existingService == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingService.DeletedBy = userName;
            existingService.DeletedAt = DateTime.Now;
            context.Entry(existingService).State = EntityState.Modified;
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

        public async Task<Guid> GetServiceIdByUserId(Guid userId)
        {
            if(userId != Guid.Empty)
            {
                var employee = await context.Employees.FirstOrDefaultAsync(s => s.Id == userId && s.DeletedAt == null);
                if (employee != null && employee.ServiceId.HasValue)
                    return employee.ServiceId.Value;
                else
                {
                    return Guid.Empty;
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

    }
}
