using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.BureauFeatures.Commands;
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
    public class BureauRepository : Repository<Bureau>, IBureauRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public BureauRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<BureauResponse> GetBureauById(Guid id)
        {
            var bureau = await context.Bureaus
                 .Include(b => b.Service)
                 .FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null);
            return  new BureauResponse
            {
                Id = bureau.Id,
                Name = bureau.Name,
                Abbreviation = bureau.Abbreviation,
                ServiceId = bureau.ServiceId,
                DivisioneId = bureau.Service != null ? bureau.Service.DivisionId : Guid.Empty,
                ServiceName = bureau.Service?.Name,
                Update = null,
                Delete = null
            };
        }

        public async Task<IEnumerable<BureauResponse>> GetAllBureaus()
        {
            var bureaus = await context.Bureaus
                .Include(b => b.Service)
                .Where(b => b.DeletedAt == null).ToListAsync();
            return bureaus.Select(b => new BureauResponse
            {
                Id = b.Id,
                Name = b.Name,
                Abbreviation = b.Abbreviation,
                ServiceId = b.ServiceId,
                DivisioneId = b.Service != null ? b.Service.DivisionId : Guid.Empty,
                ServiceName = b.Service?.Name,
                Update = null,
                Delete = null
            }).ToList();
        }
        public async Task<IEnumerable<Bureau>> GetBureausByServiceId(Guid serviceId)
        {
            return await context.Bureaus
                 .Where(s => s.ServiceId == serviceId && s.DeletedAt == null)
                 .AsNoTracking().ToListAsync();
        }
        public async Task<MessageResponse> AddBureau(BureauRequest request)
        {
            context.Set<Bureau>().Add(_mapper.Map<BureauRequest, Bureau>(request));
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
       

        public async Task<MessageResponse> DeleteBureau(Guid id)
        {
            var existingBureau = await context.Bureaus.FirstOrDefaultAsync(b => b.Id == id);
            if(existingBureau == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingBureau.DeletedBy = userName;
            existingBureau.DeletedAt = DateTime.Now;
            context.Entry(existingBureau).State = EntityState.Modified;
            if(await context.SaveChangesAsync() > 0)
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

        public async Task<MessageResponse> UpdateBureau(UpdateBureauRequest request)
        {

            Bureau bureau = _mapper.Map<BureauRequest, Bureau>(request.BureauRequest);
            bureau.Id = request.Id;
            Bureau existingBureau = await context.Bureaus
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (existingBureau == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            bureau.CreatedAt = existingBureau.CreatedAt;
            bureau.CreatedBy = existingBureau.CreatedBy;
            
            var result = UpdateAsync(bureau);
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
