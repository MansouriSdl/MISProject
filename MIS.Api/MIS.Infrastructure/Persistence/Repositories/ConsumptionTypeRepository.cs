using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.ConsumptionTypeFeatures.Commands;
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
    public class ConsumptionTypeRepository : Repository<ConsumptionType>, IConsumptionTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ConsumptionTypeRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<MessageResponse> AddConsumptionType(ConsumptionTypeRequest request)
        {
            await context.Set<ConsumptionType>().AddAsync(_mapper.Map<ConsumptionTypeRequest, ConsumptionType>(request));
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

      

        public async Task<IEnumerable<ConsumptionTypeResponse>> GetAllConsumptionTypes()
        {
            var consumptionTypes =  await context.ConsumptionTypes.Where(c => c.DeletedAt == null).ToListAsync();
            return consumptionTypes.Select(b => new ConsumptionTypeResponse
            {
                Id = b.Id,
                Name = b.Name,
                Update = null,
                Delete = null
            }).ToList();
        }

        public async Task<MessageResponse> UpdateConsumptionType(UpdateConsumptionTypeRequest request)
        {
            ConsumptionType consumptionType = _mapper.Map<ConsumptionTypeRequest, ConsumptionType>(request.ConsumptionTypeRequest);
            consumptionType.Id = request.Id;
            ConsumptionType existingConsumptionType = await context.ConsumptionTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (existingConsumptionType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            consumptionType.CreatedAt = existingConsumptionType.CreatedAt;
            consumptionType.CreatedBy = existingConsumptionType.CreatedBy;

            var result = UpdateAsync(consumptionType);
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


        public async Task<MessageResponse> DeleteConsumptionType(Guid id)
        {
            var existingConsumptionType = await context.ConsumptionTypes.FirstOrDefaultAsync(b => b.Id == id);
            if (existingConsumptionType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingConsumptionType.DeletedBy = userName;
            existingConsumptionType.DeletedAt = DateTime.Now;
            context.Entry(existingConsumptionType).State = EntityState.Modified;
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
