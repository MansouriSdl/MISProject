using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IServiceEquipmentRequestRepository : IRepository<ServiceEquipmentRequest>
    {
        Task<IEnumerable<ServiceEquipmentRequestResponse>> GetServiceEquipmentRequestByService(Guid userId, FilterRequest request);
        Task<MessageResponse> AddServiceEquipmentRequest(PostServiceEquipmentRequestRequest request);
        Task<IEnumerable<PendingRequestResponse>> GetPendingServiceEquipmentRequest(FilterRequest request);



    }
}
