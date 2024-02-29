using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Features.EmployeeFeatures.Commands;
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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public EmployeeRepository(MISDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

       

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await context.Employees
                .Include(e => e.ApplicationUser)
                .Include(e => e.EmployeeType)
                .Include(e => e.Service)
                .Where(u => u.DeletedAt == null)
                .ToListAsync();
            return users.Select(u => new UserResponse { 
                EmployeeId = u.Id,
                EmployeeTypeId = u.EmployeeTypeId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IdentityCard = u.IdentityCard,
                UserName = u.ApplicationUser?.UserName,
                Email = u.ApplicationUser?.Email,
                PhoneNumber = u.ApplicationUser?.PhoneNumber,
                EmployeeType = u.EmployeeType?.Name,
                ServiceId = u.ServiceId,
                Service = u.Service?.Name,
                Update = null,
                Delete = null
            }).ToList();
        }

        public async Task<MessageResponse> UpdateUser(UpdateUserRequest request)
        {
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                Employee existingEmployee = await context.Employees
                .Include(e => e.ApplicationUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.EmployeeId && s.DeletedAt == null);

                if (existingEmployee == null)
                {
                    return new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }
                ApplicationUser existingUser = existingEmployee.ApplicationUser;

                // Update the Employee properties
                existingEmployee.EmployeeTypeId = request.EmployeeTypeId;
                existingEmployee.FirstName = request.FirstName;
                existingEmployee.LastName = request.LastName;
                existingEmployee.IdentityCard = request.IdentityCard;
                existingEmployee.ServiceId = request.ServiceId;
                context.Entry(existingEmployee).State = EntityState.Modified;

                // Update the ApplicationUser properties
                existingUser.Email = request.Email;
                existingUser.NormalizedEmail = request.Email.ToUpper();
                existingUser.NormalizedUserName = request.UserName.ToUpper();
                existingUser.UserName = request.UserName;
                existingUser.PhoneNumber = request.PhoneNumber;
                context.Entry(existingUser).State = EntityState.Modified;
                MessageResponse messageResponse = new();
                if (await context.SaveChangesAsync() > 0)
                {
                    await transaction.CommitAsync();
                    messageResponse = new MessageResponse()
                    {
                        StatusCode = 200,
                        Message = "Success"
                    };
                }
                else
                {
                    await transaction.RollbackAsync();
                    messageResponse = new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }

                return messageResponse;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new MessageResponse()
                {
                    StatusCode = 400,
                    Message = "Bad Request"
                };
            }
            
        }
        public async Task<MessageResponse> DeleteUser(Guid id)
        {
                Employee existingEmployee = await context.Employees
                 .AsNoTracking()
                 .FirstOrDefaultAsync(s => s.Id == id && s.DeletedAt == null);
                if (existingEmployee == null)
                {
                    return new MessageResponse()
                    {
                        StatusCode = 400,
                        Message = "Bad Request"
                    };
                }
                var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";
                existingEmployee.DeletedBy = userName;
                existingEmployee.DeletedAt = DateTime.Now;
                context.Entry(existingEmployee).State = EntityState.Modified;
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
