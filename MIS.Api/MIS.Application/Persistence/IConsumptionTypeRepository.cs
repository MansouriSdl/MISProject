using MIS.Application.Features.ConsumptionTypeFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IConsumptionTypeRepository : IRepository<ConsumptionType>
    {
        Task<IEnumerable<ConsumptionTypeResponse>> GetAllConsumptionTypes();
        Task<MessageResponse> AddConsumptionType(ConsumptionTypeRequest request);
        Task<MessageResponse> UpdateConsumptionType(UpdateConsumptionTypeRequest request);
        Task<MessageResponse> DeleteConsumptionType(Guid id);


    }
}
