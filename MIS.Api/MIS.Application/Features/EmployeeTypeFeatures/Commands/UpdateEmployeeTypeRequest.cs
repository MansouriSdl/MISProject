using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeTypeFeatures.Commands
{
    public class UpdateEmployeeTypeRequest : IRequest<MessageResponse>
    {
        public UpdateEmployeeTypeRequest(Guid id, EmployeeTypeRequest employeeTypeRequest)
        {
            Id = id;
            EmployeeTypeRequest = employeeTypeRequest;
        }

        public Guid Id { get; set; }
        public EmployeeTypeRequest EmployeeTypeRequest { get; set; }
    }
}
