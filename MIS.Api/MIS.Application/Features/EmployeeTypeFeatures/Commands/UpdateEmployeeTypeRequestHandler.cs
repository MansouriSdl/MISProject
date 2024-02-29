using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeTypeFeatures.Commands
{
    public class UpdateEmployeeTypeRequestHandler : IRequestHandler<UpdateEmployeeTypeRequest, MessageResponse>
    {

        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public UpdateEmployeeTypeRequestHandler(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        public Task<MessageResponse> Handle(UpdateEmployeeTypeRequest request, CancellationToken cancellationToken)
        {
            return _employeeTypeRepository.UpdateEmployeeType(request);
        }
    }
}
