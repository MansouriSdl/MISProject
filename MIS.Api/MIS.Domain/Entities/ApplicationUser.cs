using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Employee Employee { get; set; }
        public Guid? EmployeeId { get; set; }

    }
}
