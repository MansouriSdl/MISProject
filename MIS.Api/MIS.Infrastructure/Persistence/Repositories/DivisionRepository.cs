using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.DivisionFeatures.Commands;
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
    public class DivisionRepository : Repository<Division>, IDivisionRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;


        public DivisionRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> GetDivisionIdByName(string name)
        {
            var division = await context.Divisions.FirstOrDefaultAsync(d =>d.DeletedAt == null && d.Name.ToLower() == name.ToLower());
            return (Guid) division?.Id;
        }

        public async Task<IEnumerable<DivisionResponse>> GetAllDivisions()
        {
            var divisions = await context.Divisions.Where(d => d.DeletedAt == null).ToListAsync();
            return divisions.Select(b => new DivisionResponse
            {
                Id = b.Id,
                Name = b.Name,
                Update = null,
                Delete = null
            }).ToList();
        }
        public async Task<MessageResponse> AddDivision(DivisionRequest request)
        {
            await context.Set<Division>().AddAsync(_mapper.Map<DivisionRequest, Division>(request));
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

        public async Task<MessageResponse> UpdateDivision(UpdateDivisionRequest request)
        {
            Division division = _mapper.Map<DivisionRequest, Division>(request.DivisionRequest);
            division.Id = request.Id;
            Division existingDivision = await context.Divisions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (existingDivision == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            division.CreatedAt = existingDivision.CreatedAt;
            division.CreatedBy = existingDivision.CreatedBy;
            var result = UpdateAsync(division);
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

        public async Task<MessageResponse> DeleteDivision(Guid id)
        {
            var existingDivision = await context.Divisions.FirstOrDefaultAsync(b => b.Id == id);
            if (existingDivision == null)
            {
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
            existingDivision.DeletedBy = userName;
            existingDivision.DeletedAt = DateTime.Now;
            context.Entry(existingDivision).State = EntityState.Modified;
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
