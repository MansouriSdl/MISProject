using MIS.Application.Features.SupplierFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<IEnumerable<SupplierResponse>> GetAllSuppliers();
        Task<MessageResponse> AddSupplier(SupplierRequest request);
        Task<MessageResponse> UpdateSupplier(UpdateSupplierRequest request);
        Task<MessageResponse> DeleteSupplier(Guid id);
        Task<IEnumerable<NameResponse>> GetSuppliersByTypeId(Guid typeId);

    }
}
