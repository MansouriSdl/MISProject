using MIS.Application.Features.DivisionFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IDivisionRepository : IRepository<Division>
    {
        Task<IEnumerable<DivisionResponse>> GetAllDivisions();
        Task<Guid> GetDivisionIdByName(string name);
        Task<MessageResponse> AddDivision(DivisionRequest request);
        Task<MessageResponse> UpdateDivision(UpdateDivisionRequest request);
        Task<MessageResponse> DeleteDivision(Guid id);
    }
}
