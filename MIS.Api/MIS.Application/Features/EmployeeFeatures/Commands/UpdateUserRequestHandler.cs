using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeFeatures.Commands
{
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, MessageResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateUserRequestHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<MessageResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            return _employeeRepository.UpdateUser(request);
        }
    }
}
