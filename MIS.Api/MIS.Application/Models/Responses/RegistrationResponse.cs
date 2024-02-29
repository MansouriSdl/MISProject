using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Models.Responses
{
    public class RegisterResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public string WorkId { get; set; }
    }
}
