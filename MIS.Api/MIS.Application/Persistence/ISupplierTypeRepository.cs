using MIS.Application.Features.SupplierTypeFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface ISupplierTypeRepository : IRepository<SupplierType>
    {
        Task<IEnumerable<SupplierTypeResponse>> GetAllSupplierTypes();
        Task<MessageResponse> AddSupplierType(SupplierTypeRequest request);
        Task<MessageResponse> UpdateSupplierType(UpdateSupplierTypeRequest request);
        Task<MessageResponse> DeleteSupplierType(Guid id);

    }
}
