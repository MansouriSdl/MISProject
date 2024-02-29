using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.EquipmentFeatures.Commands;
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
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public EquipmentRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<MessageResponse> AddEquipment(EquipmentRequest request)
        {
            await context.Set<Equipment>().AddAsync(_mapper.Map<EquipmentRequest, Equipment>(request));
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


        public async Task<IEnumerable<EquipmentResponse>> GetAllEquipments()
        {
            var equipments =  await context.Equipments
                .Include(e => e.EquipmentType)
                .Include(e => e.ConsumptionType)
                .Where(e => e.DeletedAt == null).ToListAsync();
            return equipments.Select(b => new EquipmentResponse
            {
                Id = b.Id,
                EquipmentTypeId = b.EquipmentTypeId,
                ConsumptionTypeId = b.ConsumptionTypeId,
                Name = b.Name,
                Description = b.Description,
                SerialNumber = b.SerialNumber,
                EquipmentType = b.EquipmentType?.Name,
                ConsumptionType = b.ConsumptionType?.Name,
                ExpirationDateLimit = (b.ExpirationDateLimit.HasValue) ? (b.ExpirationDateLimit.Value).ToString() : null,
                StockLimit = b.StockLimit,
                Update = null,
                Delete = null
            }).ToList();
        }

        public async Task<MessageResponse> UpdateEquipment(UpdateEquipmentRequest request)
        {
            Equipment equipment = _mapper.Map<EquipmentRequest, Equipment>(request.EquipmentRequest);
            equipment.Id = request.Id;
            Equipment existingEquipment = await context.Equipments
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (existingEquipment == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            equipment.CreatedAt = existingEquipment.CreatedAt;
            equipment.CreatedBy = existingEquipment.CreatedBy;

            var result = UpdateAsync(equipment);
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

        public async Task<MessageResponse> DeleteEquipment(Guid id)
        {
            var existingEquipment = await context.Equipments.FirstOrDefaultAsync(b => b.Id == id);
            if (existingEquipment == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingEquipment.DeletedBy = userName;
            existingEquipment.DeletedAt = DateTime.Now;
            context.Entry(existingEquipment).State = EntityState.Modified;
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
