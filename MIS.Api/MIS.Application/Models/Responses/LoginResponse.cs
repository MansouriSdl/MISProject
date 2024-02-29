using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public DateTime Expiration { get; set; }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get; set; }
        public string WorkId { get; set; }
        public string EmployeeType { get; set; }
        public string UserName { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
