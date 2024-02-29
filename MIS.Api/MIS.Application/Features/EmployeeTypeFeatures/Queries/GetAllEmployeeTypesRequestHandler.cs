using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeTypeFeatures.Queries
{
    public class GetAllEmployeeTypesRequestHandler : IRequestHandler<GetAllEmployeeTypesRequest, IEnumerable<EmployeeTypeResponse>>
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public GetAllEmployeeTypesRequestHandler(IEmployeeTypeRepository employeeTypeReposirory)
        {
            _employeeTypeRepository = employeeTypeReposirory;
        }

        public Task<IEnumerable<EmployeeTypeResponse>> Handle(GetAllEmployeeTypesRequest request, CancellationToken cancellationToken)
        {
            return _employeeTypeRepository.GetAllEmployeeTypes();
        }
    }
}
