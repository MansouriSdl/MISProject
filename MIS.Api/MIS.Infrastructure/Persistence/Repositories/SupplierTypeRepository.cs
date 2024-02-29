using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.SupplierTypeFeatures.Commands;
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
    public class SupplierTypeRepository : Repository<SupplierType>, ISupplierTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SupplierTypeRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<MessageResponse> AddSupplierType(SupplierTypeRequest request)
        {
            await context.Set<SupplierType>().AddAsync(_mapper.Map<SupplierTypeRequest, SupplierType>(request));
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

        public async Task<MessageResponse> DeleteSupplierType(Guid id)
        {
            var existingSupplierType = await context.SupplierTypes.FirstOrDefaultAsync(b => b.Id == id);
            if (existingSupplierType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingSupplierType.DeletedBy = userName;
            existingSupplierType.DeletedAt = DateTime.Now;
            context.Entry(existingSupplierType).State = EntityState.Modified;
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

        public async Task<IEnumerable<SupplierTypeResponse>> GetAllSupplierTypes()
        {
            var supplierTypes = await context.SupplierTypes.Where(e => e.DeletedAt == null).ToListAsync();
            return supplierTypes.Select(e => new SupplierTypeResponse
            {
                Id = e.Id,
                Name = e.Name,
                Update = null,
                Delete = null
            });
        }

        public async Task<MessageResponse> UpdateSupplierType(UpdateSupplierTypeRequest request)
        {
           SupplierType supplierType = _mapper.Map<SupplierTypeRequest, SupplierType>(request.SupplierTypeRequest);
           supplierType.Id = request.Id;
           SupplierType existingSupplierType = await context.SupplierTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedAt == null);
            if (existingSupplierType == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            supplierType.CreatedAt = existingSupplierType.CreatedAt;
            supplierType.CreatedBy = existingSupplierType.CreatedBy;
            var result = UpdateAsync(supplierType);
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
    }
}
