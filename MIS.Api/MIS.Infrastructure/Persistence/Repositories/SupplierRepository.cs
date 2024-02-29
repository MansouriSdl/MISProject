using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.SupplierFeatures.Commands;
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
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SupplierRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<SupplierResponse>> GetAllSuppliers()
        {
            var suppliers =  await context.Suppliers
                .Include(s => s.SupplierType)
                .Where(s => s.DeletedAt == null).ToListAsync();
            return suppliers.Select(b => new SupplierResponse
            {
                Id = b.Id,
                CompanyName = b.CompanyName,
                CommanCompanyIndentifier = b.CommanCompanyIndentifier,
                PhoneNumber ="+212 " + b.PhoneNumber,
                BankAccountNumber = b.BankAccountNumber,
                SupplierTypeId = b.SupplierTypeId,
                SupplierTypeName = b.SupplierType?.Name,
                Update = null,
                Delete = null
            }).ToList();
        }
        public async Task<MessageResponse> AddSupplier(SupplierRequest request)
        {
            context.Set<Supplier>().Add(_mapper.Map<SupplierRequest, Supplier>(request));
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

        public async Task<MessageResponse> UpdateSupplier(UpdateSupplierRequest request)
        {
            Supplier supplier = _mapper.Map<SupplierRequest, Supplier>(request.SupplierRequest);
            supplier.Id = request.Id;
            Supplier existingSupplier = await context.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if(existingSupplier == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            supplier.CreatedAt = existingSupplier.CreatedAt;
            supplier.CreatedBy = existingSupplier.CreatedBy;
            var result = UpdateAsync(supplier);
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

        public async Task<MessageResponse> DeleteSupplier(Guid id)
        {
            var existingSupplier = await context.Suppliers.FirstOrDefaultAsync(b => b.Id == id);
            if (existingSupplier == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingSupplier.DeletedBy = userName;
            existingSupplier.DeletedAt = DateTime.Now;
            context.Entry(existingSupplier).State = EntityState.Modified;
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

        public async Task<IEnumerable<NameResponse>> GetSuppliersByTypeId(Guid typeId)
        {
            var suppliers = await context.Suppliers.Where(s => s.DeletedAt == null && s.SupplierTypeId == typeId).ToListAsync();
            return suppliers.Select(r => new NameResponse
            {
                Id = r.Id,
                Name = r.CompanyName
            }).ToList();
        }
    }
}
