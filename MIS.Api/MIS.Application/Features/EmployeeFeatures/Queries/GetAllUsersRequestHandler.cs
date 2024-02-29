using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeFeatures.Queries
{
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<UserResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllUsersRequestHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<IEnumerable<UserResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetAllUsers();
        }
    }
}
