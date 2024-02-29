using MediatR;
using MIS.Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockInventoryFeatures.Commands
{
    public class UpdateInventoryAvailability : IRequest<int>
    {
        public UpdateInventoryAvailability(List<UpdateInventoryAvailabilityRequest> updateAssignmentAvailabilityRequestList)
        {
            UpdateAssignmentAvailabilityRequestList = updateAssignmentAvailabilityRequestList;
        }

        public List<UpdateInventoryAvailabilityRequest> UpdateAssignmentAvailabilityRequestList { get; set; }
    }
}
