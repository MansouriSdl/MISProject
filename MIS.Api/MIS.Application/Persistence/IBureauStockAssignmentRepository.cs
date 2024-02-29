using MIS.Application.Features.BureauStockAssignmentFeatures.Commands;
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
    public interface IBureauStockAssignmentRepository : IRepository<BureauStockAssignment>
    {
        Task<List<BureauStockAssignmentsResponse>> GeBureauStockAssignments(FilterRequest request);
        Task<MessageResponse> AddAssignment(AddAssignmentRequest request);
        Task<int> UpdateBureauStockAssignmentsAvailability(UpdateBureauStockAssignmentAvailabilityRequest request);
        Task<StockAssignmentsDetailsResponse> GetStockAssignmentsDetails(FilterRequest request);


    }
}
