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
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, MessageResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteUserRequestHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<MessageResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            return _employeeRepository.DeleteUser(request.Id);
        }
    }
}
