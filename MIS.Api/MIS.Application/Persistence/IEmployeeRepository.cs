using MIS.Application.Features.EmployeeFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<MessageResponse> UpdateUser(UpdateUserRequest request);

        Task<MessageResponse> DeleteUser(Guid id);
    }
}
