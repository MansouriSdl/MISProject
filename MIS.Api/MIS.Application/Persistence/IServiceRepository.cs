using MIS.Application.Features.ServiceFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<ServiceResponse>> GetAllServices();

        Task<IEnumerable<Service>> GetServicesByDivisionId(Guid divisionId);
        Task<Guid> GetServiceIdByUserId(Guid userId);
        Task<MessageResponse> AddService(ServiceRequest request);
        Task<MessageResponse> UpdateService(UpdateServiceRequest request);
        Task<MessageResponse> DeleteService(Guid id);

    }
}
