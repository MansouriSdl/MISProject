using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.EquipmentTypeFeatures.Commands;
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
    public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EquipmentTypeRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<MessageResponse> AddEquipmentType(EquipmentTypeRequest request)
        {
            await context.Set<EquipmentType>().AddAsync(_mapper.Map<EquipmentTypeRequest, EquipmentType>(request));
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


        public async Task<IEnumerable<EquipmentTypeResponse>> GetAllEquipmentTypes()
        {
            var equipmentTypes =  await context.EquipmentTypes
                .Where(et => et.DeletedAt == null).ToListAsync();
            return equipmentTypes.Select(b => new EquipmentTypeResponse
            {
                Id = b.Id,
                Name = b.Name,
                Update = null,
                Delete = null
            }).ToList();
        }

        public async Task<MessageResponse> UpdateEquipmentType(UpdateEquipmentTypeRequest request)
        {
            EquipmentType equipmentType = _mapper.Map<EquipmentTypeRequest, EquipmentType>(request.EquipmentTypeRequest);
            equipmentType.Id = request.Id;
            EquipmentType existingEquipmentType = await context.EquipmentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (existingEquipmentType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            equipmentType.CreatedAt = existingEquipmentType.CreatedAt;
            equipmentType.CreatedBy = existingEquipmentType.CreatedBy;

            var result = UpdateAsync(equipmentType);
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

        public async Task<MessageResponse> DeleteEquipmentType(Guid id)
        {
            var existingEquipmentType = await context.EquipmentTypes.FirstOrDefaultAsync(b => b.Id == id);
            if (existingEquipmentType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingEquipmentType.DeletedBy = userName;
            existingEquipmentType.DeletedAt = DateTime.Now;
            context.Entry(existingEquipmentType).State = EntityState.Modified;
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
