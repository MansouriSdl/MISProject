using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeFeatures.Commands
{
    public class UpdateUserRequest : IRequest<MessageResponse>
    {
        public UpdateUserRequest(Guid employeeId, string firstName, string lastName, string identityCard, string userName, string email, Guid employeeTypeId, string address)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            IdentityCard = identityCard;
            UserName = userName;
            Email = email;
            EmployeeTypeId = employeeTypeId;
            Address = address;
        }

        public Guid EmployeeId { get; set; }
        public Guid? ServiceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid EmployeeTypeId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
