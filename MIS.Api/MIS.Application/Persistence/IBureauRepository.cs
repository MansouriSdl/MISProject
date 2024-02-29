using MIS.Application.Features.BureauFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IBureauRepository : IRepository<Bureau>
    {
        Task<IEnumerable<BureauResponse>> GetAllBureaus();
        Task<BureauResponse> GetBureauById(Guid id);
        Task<IEnumerable<Bureau>> GetBureausByServiceId(Guid serviceId);
        Task<MessageResponse> AddBureau(BureauRequest request);
        Task<MessageResponse> UpdateBureau(UpdateBureauRequest request);

        Task<MessageResponse> DeleteBureau(Guid id);

    }
}
