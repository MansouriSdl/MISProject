using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class UserResponse
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityCard { get; set; }
        public string EmployeeType { get; set; }
        public Guid EmployeeTypeId { get; set; }

        public Guid? ServiceId { get; set; }
        public string Service { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
    }
}
