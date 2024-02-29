using MIS.Application.Features.EmployeeTypeFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IEmployeeTypeRepository : IRepository<EmployeeType>
    {
        Task<IEnumerable<EmployeeTypeResponse>> GetAllEmployeeTypes();
        Task<MessageResponse> AddEmployeeType(EmployeeTypeRequest request);
        Task<MessageResponse> UpdateEmployeeType(UpdateEmployeeTypeRequest request);
        Task<MessageResponse> DeleteEmployeeType(Guid id);
    }
}
