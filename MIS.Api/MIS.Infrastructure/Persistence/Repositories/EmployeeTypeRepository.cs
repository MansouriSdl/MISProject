using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.EmployeeTypeFeatures.Commands;
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
    public class EmployeeTypeRepository : Repository<EmployeeType>, IEmployeeTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public EmployeeTypeRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<EmployeeTypeResponse>> GetAllEmployeeTypes()
        {
            var employeeTypes = await context.EmployeeTypes.Where(e => e.DeletedAt == null).ToListAsync();
            return employeeTypes.Select(e => new EmployeeTypeResponse
            {
                Id = e.Id,
                Name = e.Name,
                Update = null,
                Delete = null
            });
        }

        public async Task<MessageResponse> AddEmployeeType(EmployeeTypeRequest request)
        {
            await context.Set<EmployeeType>().AddAsync(_mapper.Map<EmployeeTypeRequest, EmployeeType>(request));
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

        public async Task<MessageResponse> UpdateEmployeeType(UpdateEmployeeTypeRequest request)
        {
            EmployeeType employeeType = _mapper.Map<EmployeeTypeRequest, EmployeeType>(request.EmployeeTypeRequest);
            employeeType.Id = request.Id;
            EmployeeType existingEmployeeType = await context.EmployeeTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedAt == null);
            if (existingEmployeeType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            employeeType.CreatedAt = existingEmployeeType.CreatedAt;
            employeeType.CreatedBy = existingEmployeeType.CreatedBy;
            var result = UpdateAsync(employeeType);
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

        public async Task<MessageResponse> DeleteEmployeeType(Guid id)
        {
            var existingEmployeeType = await context.EmployeeTypes.FirstOrDefaultAsync(b => b.Id == id);
            if (existingEmployeeType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingEmployeeType.DeletedBy = userName;
            existingEmployeeType.DeletedAt = DateTime.Now;
            context.Entry(existingEmployeeType).State = EntityState.Modified;
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

   
        
    }
}
